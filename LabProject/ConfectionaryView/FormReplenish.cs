using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryView
{
    public partial class FormReplenish : Form
    {
        private readonly IComponentLogic logicC;
        private readonly IWarehouseLogic logicW;

        public FormReplenish(IComponentLogic _logicC, IWarehouseLogic _logicW)
        {
            InitializeComponent();
            logicC = _logicC;
            logicW = _logicW;
        }

        private void FormReplenish_Load(object sender, EventArgs e)
        {
            List<WarehouseViewModel> listW = logicW.Read(null);
            if (listW != null)
            {
                comboBoxWarehouse.DisplayMember = "Name";
                comboBoxWarehouse.ValueMember = "Id";
                comboBoxWarehouse.DataSource = listW;
                comboBoxWarehouse.SelectedItem = null;
            }
            List<ComponentViewModel> listC = logicC.Read(null);
            if (listC != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listC;
                comboBoxComponent.SelectedItem = null;
            }
        }

        private void buttonReplenish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Укажите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxWarehouse.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logicW.AddComponents(new ReplenishBindingModel
                {
                    WarehouseId = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                    ComponentId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                    ComponentName = comboBoxComponent.DisplayMember,
                    Count = Convert.ToInt32(textBoxCount.Text),
                });
                MessageBox.Show("Пополнение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
