using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;

namespace ConfectionaryView
{
    public partial class FormImplementer : Form
    {
        public int Id { set { id = value; } }
        private readonly IImplementerLogic logic;
        private int? id;

        public FormImplementer(IImplementerLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
        }

        private void FormImplementer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new ImplementerBindingModel { Id = id })?[0];
                    if (view != null) 
                    { 
                        textBoxName.Text = view.FIO;
                        textBoxWorkingTime.Text = view.WorkingTime.ToString();
                        textBoxPauseTime.Text = view.PauseTime.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Введите название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxWorkingTime.Text))
            {
                textBoxWorkingTime.Text = rnd.Next(5, 10).ToString();
            }
            if (string.IsNullOrEmpty(textBoxPauseTime.Text))
            {
                textBoxPauseTime.Text = rnd.Next(5, 10).ToString();
            }

            try
            {
                logic.CreateOrUpdate(new ImplementerBindingModel { 
                    Id = id, 
                    FIO = textBoxName.Text, 
                    WorkingTime = Convert.ToInt32(textBoxWorkingTime.Text), 
                    PauseTime = Convert.ToInt32(textBoxPauseTime.Text)
                });

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
