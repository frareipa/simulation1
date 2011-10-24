namespace MultipleQueue
{
    partial class Server_n
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbPriority = new System.Windows.Forms.TextBox();
            this.btnServer_nInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "discrete ",
            "uniform",
            "exponention"});
            this.comboBox1.Location = new System.Drawing.Point(46, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(262, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "type of server distrubition";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Priority";
            // 
            // txtbPriority
            // 
            this.txtbPriority.Location = new System.Drawing.Point(124, 169);
            this.txtbPriority.Name = "txtbPriority";
            this.txtbPriority.Size = new System.Drawing.Size(100, 20);
            this.txtbPriority.TabIndex = 3;
            // 
            // btnServer_nInfo
            // 
            this.btnServer_nInfo.Location = new System.Drawing.Point(136, 210);
            this.btnServer_nInfo.Name = "btnServer_nInfo";
            this.btnServer_nInfo.Size = new System.Drawing.Size(75, 23);
            this.btnServer_nInfo.TabIndex = 4;
            this.btnServer_nInfo.Text = "ok";
            this.btnServer_nInfo.UseVisualStyleBackColor = true;
            this.btnServer_nInfo.Click += new System.EventHandler(this.btnServer_nInfo_Click);
            // 
            // Server_n
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 289);
            this.Controls.Add(this.btnServer_nInfo);
            this.Controls.Add(this.txtbPriority);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Server_n";
            this.Text = "Server_n";
            this.Load += new System.EventHandler(this.Server_n_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbPriority;
        private System.Windows.Forms.Button btnServer_nInfo;

    }
}