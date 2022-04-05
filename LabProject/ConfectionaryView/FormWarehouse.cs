using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Unity;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryView
{
    public partial class FormWarehouse : Form
    {
        public int Id { set { id = value; } }
        private readonly IWarehouseLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> storedComponents;

        public FormWarehouse(IWarehouseLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
        }

        private void FormWarehouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WarehouseViewModel view = logic.Read(new WarehouseBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
                        textBoxManager.Text = view.Manager;
                        storedComponents = view.StoredComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else storedComponents = new Dictionary<int, (string, int)>();
        }

        private void LoadData()
        {
            try
            {
                if (storedComponents != null)
                {
                    dataGridViewComponents.Rows.Clear();
                    foreach (var component in storedComponents)
                    {
                        dataGridViewComponents.Rows.Add(new object[] { component.Key, component.Value.Item1, component.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Укажите название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxManager.Text))
            {
                MessageBox.Show("Укажите заведующего", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                logic.CreateOrUpdate(new WarehouseBindingModel
                {
                    Id = id,
                    Name = textBoxName.Text,
                    Manager = textBoxManager.Text,
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
