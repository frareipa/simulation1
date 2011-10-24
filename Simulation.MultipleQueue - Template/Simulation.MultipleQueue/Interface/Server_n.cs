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
    public partial class Server_n : Form
    {
        public Server_n()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Server_n_Load(object sender, EventArgs e)
        {

        }

        private void btnServer_nInfo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2)
            {
                exponDist newex = new exponDist();
                newex.Show();
            }
            if (comboBox1.SelectedIndex == 0)
            {
                discreteDis newdis = new discreteDis();
                newdis.Show();
            }

        }
    }
}
