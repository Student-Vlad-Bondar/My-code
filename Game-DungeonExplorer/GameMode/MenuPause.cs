using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMode
{
    public partial class MenuPause : Form
    {
        public MenuPause()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            pictureBoxExit.BackColor = Color.Transparent;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxResume_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
