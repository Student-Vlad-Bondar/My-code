using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryBatles;

namespace Menu
{
    public partial class MainMenu : Form
    {
        private Database database;
        public MainMenu()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            database = new Database("game.db");
            database.CreateDatabase();
            UpdateUIForLoggedInState();
        }
        private void UpdateUIForLoggedInState()
        {
            btnExitAccount.Enabled = UserSession.IsLoggedIn;
            btnLoginToAccount.Enabled = !UserSession.IsLoggedIn;
        }
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            LevelSelections form2 = new LevelSelections(UserSession.UserId, database);
            form2.Show();
            this.Hide();
        }

        private void btnExitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLoginToAccount_Click(object sender, EventArgs e)
        {
            Account form1 = new Account();
            form1.UserLoggedIn += (id) =>
            {
                UserSession.UserId = id;
                UpdateUIForLoggedInState();
            };
            form1.Show();
            this.Hide();
        }

        private void btnExitAccount_Click(object sender, EventArgs e)
        {
            UserSession.UserId = -1;
            UpdateUIForLoggedOutState();
            MessageBox.Show("Ви успішно вийшли.");
        }
        private void UpdateUIForLoggedOutState()
        {
            btnExitAccount.Enabled = false;
            btnLoginToAccount.Enabled = true;
        }

        private void btnAchievement_Click(object sender, EventArgs e)
        {
            if (UserSession.IsLoggedIn)
            {
                Achievement achievementForm = new Achievement(UserSession.UserId, database);
                achievementForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Будь ласка, спочатку увійдіть в аккаунт.");
            }
        }
    }
}
