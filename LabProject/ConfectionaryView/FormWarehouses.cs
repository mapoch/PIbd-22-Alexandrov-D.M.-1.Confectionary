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

namespace ConfectionaryView
{
    public partial class FormWarehouses : Form
    {
        private readonly IWarehouseLogic logic;

        public FormWarehouses(IWarehouseLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
        }

        private void FormWarehouses_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridViewWarehouses.DataSource = list;
                    dataGridViewWarehouses.Columns[0].Visible = false;
                    dataGridViewWarehouses.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewWarehouses.Columns[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormWarehouse>();
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewWarehouses.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormWarehouse>();
                form.Id = Convert.ToInt32(dataGridViewWarehouses.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewWarehouses.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewWarehouses.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        logic.Delete(new WarehouseBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
                    }
                    LoadData();
                }
            }
        }
    }
}
