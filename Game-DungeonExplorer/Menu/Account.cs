using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLite;
using LibraryBatles;

namespace Menu
{
    public partial class Account : Form
    {
        private SQLite.Database database;
        public int UserId { get; private set; }
        public event Action<int> UserLoggedIn;

        public Account()
        {
            InitializeComponent();
            labelRegistration.BackColor = Color.Transparent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            database = new SQLite.Database("game.db");
            database.CreateDatabase();
            textBoxPassword.PasswordChar = '*';
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string username = textBoxLogin.Text;
            string password = textBoxPassword.Text;

            try
            {
                database.InsertUser(username, password);
                MessageBox.Show("Registration successful! You can now log in.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed: " + ex.Message);
            }

            textBoxLogin.Text = null; 
            textBoxPassword.Text = null;
        }

        private void buttonEntryUser_Click(object sender, EventArgs e)
        {
            string username = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            UserId = database.VerifyUser(username, password);

            if (UserId != -1)
            {
                MessageBox.Show("Вхід успішний!");
                UserLoggedIn?.Invoke(UserId);

                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неправильне ім'я користувача або пароль. Будь ласка спробуйте ще раз.");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            MainMenu form1 = new MainMenu();
            form1.Show();
            this.Hide();
        }
    }
}
