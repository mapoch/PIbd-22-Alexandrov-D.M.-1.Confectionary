using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using Microsoft.Reporting.WinForms;

namespace ConfectionaryView
{
    public partial class FormReportDates : Form
    {
        private readonly ReportViewer reportViewer;
        private readonly IReportLogic logic;

        public FormReportDates(IReportLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
            reportViewer = new ReportViewer { Dock = DockStyle.Fill };
            reportViewer.LocalReport.LoadReportDefinition(new FileStream("ReportDates.rdlc", FileMode.Open));
            Controls.Clear();
            Controls.Add(panel);
            panelData.Controls.Add(reportViewer);
            Controls.Add(panelData);
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            try
            {
                var dataSource = logic.GetDates(new ReportBindingModel());
                var source = new ReportDataSource("DataSetDates", dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonToPdf_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    logic.SaveDatesToPdfFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
