
namespace ConfectionaryView
{
    partial class FormReplenish
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
            this.labelStorage = new System.Windows.Forms.Label();
            this.labelComponent = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.comboBoxWarehouse = new System.Windows.Forms.ComboBox();
            this.comboBoxComponent = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.buttonReplenish = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStorage
            // 
            this.labelStorage.AutoSize = true;
            this.labelStorage.Location = new System.Drawing.Point(53, 9);
            this.labelStorage.Name = "labelStorage";
            this.labelStorage.Size = new System.Drawing.Size(52, 20);
            this.labelStorage.TabIndex = 0;
            this.labelStorage.Text = "Склад:";
            // 
            // labelComponent
            // 
            this.labelComponent.AutoSize = true;
            this.labelComponent.Location = new System.Drawing.Point(14, 46);
            this.labelComponent.Name = "labelComponent";
            this.labelComponent.Size = new System.Drawing.Size(91, 20);
            this.labelComponent.TabIndex = 1;
            this.labelComponent.Text = "Компонент:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 83);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(93, 20);
            this.labelCount.TabIndex = 3;
            this.labelCount.Text = "Количество:";
            // 
            // comboBoxWarehouse
            // 
            this.comboBoxWarehouse.FormattingEnabled = true;
            this.comboBoxWarehouse.Location = new System.Drawing.Point(111, 6);
            this.comboBoxWarehouse.Name = "comboBoxWarehouse";
            this.comboBoxWarehouse.Size = new System.Drawing.Size(220, 28);
            this.comboBoxWarehouse.TabIndex = 4;
            // 
            // comboBoxComponent
            // 
            this.comboBoxComponent.FormattingEnabled = true;
            this.comboBoxComponent.Location = new System.Drawing.Point(111, 43);
            this.comboBoxComponent.Name = "comboBoxComponent";
            this.comboBoxComponent.Size = new System.Drawing.Size(220, 28);
            this.comboBoxComponent.TabIndex = 5;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(111, 80);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(220, 27);
            this.textBoxCount.TabIndex = 6;
            // 
            // buttonReplenish
            // 
            this.buttonReplenish.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonReplenish.Location = new System.Drawing.Point(127, 113);
            this.buttonReplenish.Name = "buttonReplenish";
            this.buttonReplenish.Size = new System.Drawing.Size(99, 39);
            this.buttonReplenish.TabIndex = 8;
            this.buttonReplenish.Text = "Пополнить";
            this.buttonReplenish.UseVisualStyleBackColor = false;
            this.buttonReplenish.Click += new System.EventHandler(this.buttonReplenish_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCancel.Location = new System.Drawing.Point(232, 113);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 39);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormReplenish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 161);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonReplenish);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxComponent);
            this.Controls.Add(this.comboBoxWarehouse);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelComponent);
            this.Controls.Add(this.labelStorage);
            this.Name = "FormReplenish";
            this.Text = "Пополнить склад";
            this.Load += new System.EventHandler(this.FormReplenish_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStorage;
        private System.Windows.Forms.Label labelComponent;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.ComboBox comboBoxWarehouse;
        private System.Windows.Forms.ComboBox comboBoxComponent;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Button buttonReplenish;
        private System.Windows.Forms.Button buttonCancel;
    }
}