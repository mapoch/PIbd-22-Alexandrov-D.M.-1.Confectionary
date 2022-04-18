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
    public partial class FormCreateOrder : Form
    {
        private readonly IPastryLogic logicP;
        private readonly IOrderLogic logicO;
        private readonly IClientLogic logicC;

        public FormCreateOrder(IPastryLogic _logicP, IOrderLogic _logicO, IClientLogic _logicC)
        {
            InitializeComponent();
            logicP = _logicP;
            logicO = _logicO;
            logicC = _logicC;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            List<PastryViewModel> listP = logicP.Read(null);
            if (listP != null)
            {
                comboBoxPastry.DisplayMember = "PastryName";
                comboBoxPastry.ValueMember = "Id";
                comboBoxPastry.DataSource = listP;
                comboBoxPastry.SelectedItem = null;
            }

            List<ClientViewModel> listC = logicC.Read(null);
            if (listC != null)
            {
                comboBoxPastry.DisplayMember = "Login";
                comboBoxPastry.ValueMember = "Id";
                comboBoxPastry.DataSource = listC;
                comboBoxPastry.SelectedItem = null;
            }
        }
        
        private void CalcSum()
        {
            if (comboBoxPastry.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxPastry.SelectedValue);
                    PastryViewModel product = logicP.Read(new PastryBindingModel { Id = id })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Укажите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxPastry.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logicO.CreateOrder(new CreateOrderBindingModel
                {
                    PastryId = Convert.ToInt32(comboBoxPastry.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text),
                    ClientId = Convert.ToInt32(comboBoxClients.SelectedValue)
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
