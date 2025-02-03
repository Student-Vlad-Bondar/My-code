using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Menu
{
    public partial class Achievement : Form
    {
        private int userId;
        private Database database;
        public Achievement(int userId, Database database)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userId = userId;
            this.database = database;

            LoadStatistics();
        }
        private void LoadStatistics()
        {
            var stats = database.LoadGameStatistics(userId);
            if (stats.HasValue)
            {
                var username = database.GetUsername(userId);
                labelBoss.Text = $"{stats.Value.BossesDefeated}";
                labelEnemies.Text = $"{stats.Value.EnemiesDefeated}";
            }
            else
            {
                MessageBox.Show("No statistics found.");
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
