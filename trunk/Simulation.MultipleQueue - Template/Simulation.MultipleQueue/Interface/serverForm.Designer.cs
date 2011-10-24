namespace MultipleQueue
{
    partial class serverForm
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
            this.txtNumOfServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSelctionMethod = new System.Windows.Forms.ComboBox();
            this.btnInputServerInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNumOfServer
            // 
            this.txtNumOfServer.Location = new System.Drawing.Point(177, 45);
            this.txtNumOfServer.Name = "txtNumOfServer";
            this.txtNumOfServer.Size = new System.Drawing.Size(241, 20);
            this.txtNumOfServer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "numOfServer";
            // 
            // cmbSelctionMethod
            // 
            this.cmbSelctionMethod.FormattingEnabled = true;
            this.cmbSelctionMethod.Items.AddRange(new object[] {
            "highest priority",
            "lowest Utilization",
            "Random"});
            this.cmbSelctionMethod.Location = new System.Drawing.Point(144, 121);
            this.cmbSelctionMethod.Name = "cmbSelctionMethod";
            this.cmbSelctionMethod.Size = new System.Drawing.Size(204, 21);
            this.cmbSelctionMethod.TabIndex = 2;
            // 
            // btnInputServerInfo
            // 
            this.btnInputServerInfo.Location = new System.Drawing.Point(187, 230);
            this.btnInputServerInfo.Name = "btnInputServerInfo";
            this.btnInputServerInfo.Size = new System.Drawing.Size(150, 40);
            this.btnInputServerInfo.TabIndex = 3;
            this.btnInputServerInfo.Text = "Ok";
            this.btnInputServerInfo.UseVisualStyleBackColor = true;
            this.btnInputServerInfo.Click += new System.EventHandler(this.btnInputServerInfo_Click);
            // 
            // serverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 338);
            this.Controls.Add(this.btnInputServerInfo);
            this.Controls.Add(this.cmbSelctionMethod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumOfServer);
            this.Name = "serverForm";
            this.Text = "serverForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumOfServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSelctionMethod;
        private System.Windows.Forms.Button btnInputServerInfo;
    }
}