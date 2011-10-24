namespace MultipleQueue
{
    partial class MultipleServerSimulation
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
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnServers = new System.Windows.Forms.Button();
            this.btnSimulate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(120, 38);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(100, 23);
            this.btnCustomer.TabIndex = 0;
            this.btnCustomer.Text = "Customer Information";
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnServers
            // 
            this.btnServers.Location = new System.Drawing.Point(122, 85);
            this.btnServers.Name = "btnServers";
            this.btnServers.Size = new System.Drawing.Size(100, 25);
            this.btnServers.TabIndex = 1;
            this.btnServers.Text = "Servers";
            this.btnServers.UseVisualStyleBackColor = true;
            this.btnServers.Click += new System.EventHandler(this.btnServers_Click);
            // 
            // btnSimulate
            // 
            this.btnSimulate.Location = new System.Drawing.Point(130, 168);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(100, 23);
            this.btnSimulate.TabIndex = 2;
            this.btnSimulate.Text = "Run";
            this.btnSimulate.UseVisualStyleBackColor = true;
            // 
            // MultipleServerSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btnSimulate);
            this.Controls.Add(this.btnServers);
            this.Controls.Add(this.btnCustomer);
            this.Name = "MultipleServerSimulation";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnServers;
        private System.Windows.Forms.Button btnSimulate;


    }
}

