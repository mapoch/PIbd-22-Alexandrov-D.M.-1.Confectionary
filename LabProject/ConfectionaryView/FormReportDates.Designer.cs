
namespace ConfectionaryView
{
    partial class FormReportDates
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelData = new System.Windows.Forms.Panel();
            this.buttonMake = new System.Windows.Forms.Button();
            this.buttonToPdf = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelData
            // 
            this.panelData.Location = new System.Drawing.Point(3, 49);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(795, 389);
            this.panelData.TabIndex = 2;
            // 
            // buttonMake
            // 
            this.buttonMake.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonMake.Location = new System.Drawing.Point(9, 13);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(371, 29);
            this.buttonMake.TabIndex = 5;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = false;
            this.buttonMake.Click += new System.EventHandler(this.buttonMake_Click);
            // 
            // buttonToPdf
            // 
            this.buttonToPdf.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonToPdf.Location = new System.Drawing.Point(414, 13);
            this.buttonToPdf.Name = "buttonToPdf";
            this.buttonToPdf.Size = new System.Drawing.Size(371, 29);
            this.buttonToPdf.TabIndex = 6;
            this.buttonToPdf.Text = "В pdf";
            this.buttonToPdf.UseVisualStyleBackColor = false;
            this.buttonToPdf.Click += new System.EventHandler(this.buttonToPdf_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.buttonToPdf);
            this.panel.Controls.Add(this.buttonMake);
            this.panel.Location = new System.Drawing.Point(3, -2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(795, 45);
            this.panel.TabIndex = 7;
            // 
            // FormReportDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.panelData);
            this.Name = "FormReportDates";
            this.Text = "Заказы по датам";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.Button buttonToPdf;
        private System.Windows.Forms.Panel panel;
    }
}