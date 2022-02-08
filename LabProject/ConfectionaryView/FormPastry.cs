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
    public partial class FormPastry : Form
    {
        public int Id { set { id = value; } }
        private readonly IPastryLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> pastryComponents;

        public FormPastry(IPastryLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
        }

        private void FormPastry_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PastryViewModel view = logic.Read(new PastryBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.PastryName;
                        textBoxPrice.Text = view.Price.ToString();
                        pastryComponents = view.PastryComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else pastryComponents = new Dictionary<int, (string, int)>();
        }

        private void LoadData()
        {
            try
            {
                if (pastryComponents != null)
                {
                    dataGridViewComponents.Rows.Clear();
                    foreach(var pastry in pastryComponents)
                    {
                        dataGridViewComponents.Rows.Add(new object[] { pastry.Key, pastry.Value.Item1, pastry.Value.Item2 });
                    }
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
            var form = Program.Container.Resolve<FormPastryComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (pastryComponents.ContainsKey(form.Id))
                {
                    pastryComponents[form.Id] = (form.ComponentName, form.Count);
                }
                else pastryComponents.Add(form.Id, (form.ComponentName, form.Count));
                LoadData();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewComponents.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormPastryComponent>();
                int id = Convert.ToInt32(dataGridViewComponents.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = pastryComponents[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    pastryComponents[form.Id] = (form.ComponentName, form.Count);
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewComponents.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    try
                    {
                        pastryComponents.Remove(Convert.ToInt32(dataGridViewComponents.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Укажите название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Укажите цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pastryComponents == null || pastryComponents.Count == 0)
            {
                MessageBox.Show("Укажите компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                logic.CreateOrUpdate(new PastryBindingModel { 
                    Id = id, PastryName = textBoxName.Text, 
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    PastryComponents = pastryComponents
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
