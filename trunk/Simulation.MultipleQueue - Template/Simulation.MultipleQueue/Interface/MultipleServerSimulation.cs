using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultipleQueue
{
    public partial class MultipleServerSimulation : Form
    {
        public MultipleServerSimulation()
        {
            this.InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            CustForm cusF = new CustForm();
            cusF.Show();
        }

        private void btnServers_Click(object sender, EventArgs e)
        {
            serverForm s = new serverForm();
            s.Show();
        }
    }
}
