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
    public partial class Spells : Form
    {
        private int userId;
        private Database database;
        public Spells(int userId, Database database)
        {
            InitializeComponent();
            this.userId = userId;
            this.database = database;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LevelSelections form2 = new LevelSelections(userId, database);
            form2.Show();
            this.Hide();
        }
    }
}
