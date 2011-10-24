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
    public partial class serverForm : Form
    {
        public serverForm()
        {
            InitializeComponent();
        }

        private void btnInputServerInfo_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <int.Parse( this.txtNumOfServer.Text); i++)
            {
                Server_n ser_m = new Server_n();
                ser_m.Show();
            }
        }
    }
}
