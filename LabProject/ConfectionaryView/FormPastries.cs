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
    public partial class FormPastries : Form
    {
        private readonly IPastryLogic logic;

        public FormPastries(IPastryLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
        }

        private void FormPastries_Load(object sender, EventArgs e)
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
                    dataGridViewPastries.DataSource = list;
                    dataGridViewPastries.Columns[0].Visible = false;
                    dataGridViewPastries.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewPastries.Columns[3].Visible = false;
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
            var form = Program.Container.Resolve<FormPastry>();
            if (form.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewPastries.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormPastry>();
                form.Id = Convert.ToInt32(dataGridViewPastries.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK) LoadData();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewPastries.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewPastries.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        logic.Delete(new PastryBindingModel { Id = id });
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
