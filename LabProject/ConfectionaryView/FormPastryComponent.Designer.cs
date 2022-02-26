
namespace ConfectionaryView
{
    partial class FormPastryComponent
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
            this.comboBoxComponent = new System.Windows.Forms.ComboBox();
            this.labelComponent = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.textBoxСount = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxComponent
            // 
            this.comboBoxComponent.FormattingEnabled = true;
            this.comboBoxComponent.Location = new System.Drawing.Point(109, 6);
            this.comboBoxComponent.Name = "comboBoxComponent";
            this.comboBoxComponent.Size = new System.Drawing.Size(202, 28);
            this.comboBoxComponent.TabIndex = 0;
            // 
            // labelComponent
            // 
            this.labelComponent.AutoSize = true;
            this.labelComponent.Location = new System.Drawing.Point(12, 9);
            this.labelComponent.Name = "labelComponent";
            this.labelComponent.Size = new System.Drawing.Size(91, 20);
            this.labelComponent.TabIndex = 1;
            this.labelComponent.Text = "Компонент:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(13, 46);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(90, 20);
            this.labelCount.TabIndex = 2;
            this.labelCount.Text = "Количество";
            // 
            // textBoxСount
            // 
            this.textBoxСount.Location = new System.Drawing.Point(109, 43);
            this.textBoxСount.Name = "textBoxСount";
            this.textBoxСount.Size = new System.Drawing.Size(202, 27);
            this.textBoxСount.TabIndex = 3;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCancel.Location = new System.Drawing.Point(219, 86);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 31);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSave.Location = new System.Drawing.Point(109, 86);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(92, 31);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormProductComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 132);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxСount);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelComponent);
            this.Controls.Add(this.comboBoxComponent);
            this.Name = "FormProductComponent";
            this.Text = "Компонент изделия";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxComponent;
        private System.Windows.Forms.Label labelComponent;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.TextBox textBoxСount;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}