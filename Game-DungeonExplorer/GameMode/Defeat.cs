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
    public partial class Defeat : Form
    {
        public Defeat()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowMainMenu();
            this.Hide();
        }
        private void ShowMainMenu()
        {
            Type mainMenuType = Type.GetType("Menu.MainMenu, Menu", true);
            Form mainMenuForm = (Form)Activator.CreateInstance(mainMenuType);
            mainMenuForm.Show();
        }
    }
}
