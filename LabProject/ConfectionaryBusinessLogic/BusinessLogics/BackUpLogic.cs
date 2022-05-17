using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.StoragesContracts;
using System.Runtime.Serialization;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class BackUpLogic : IBackUpLogic
    {
        private readonly IBackUpInfo backUpInfo;
        public BackUpLogic(IBackUpInfo _backUpInfo)
        {
            backUpInfo = _backUpInfo;
        }
        public void CreateBackUp(BackUpSaveBinidngModel model)
        {
            if (backUpInfo == null)
            {
                return;
            }
            try
            {
                var dirInfo = new DirectoryInfo(model.FolderName);
                if (dirInfo.Exists)
                {
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                string fileName = $"{model.FolderName}.zip";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                Assembly assem = backUpInfo.GetAssembly();
                var dbsets = backUpInfo.GetFullList();
                MethodInfo method = GetType().BaseType.GetTypeInfo().GetDeclaredMethod("SaveToFile");
                foreach (var set in dbsets)
                {
                    var elem = assem.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    MethodInfo generic = method.MakeGenericMethod(elem.GetType());
                    generic.Invoke(this, new object[] { model.FolderName });
                }
                ZipFile.CreateFromDirectory(model.FolderName, fileName);
                dirInfo.Delete(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void SaveToFile<T>(string folderName) where T : class, new()
        {
            var records = backUpInfo.GetList<T>();
            var obj = new T();

            var xmlFormatter = new DataContractSerializer(typeof(List<T>));
            using var fs = new FileStream(string.Format("{0}/{1}.xml", folderName, obj.GetType().Name), FileMode.OpenOrCreate);
            xmlFormatter.WriteObject(fs, records);
        }
    }
}
