using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameMode;
using SQLite;

namespace Menu
{
    public partial class LevelSelections : Form
    {
        private int userId;
        private Database database;
        public PictureBox lastClickedPictureBox;
        public event Action<PictureBox> PictureBoxSelected;
        public LevelSelections(int userId, Database database)
        {
            InitializeComponent();
            this.userId = userId;
            this.database = database;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            pictureBox1.SendToBack();
            pictureBox2.SendToBack();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MainMenu form1 = new MainMenu();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lastClickedPictureBox != null)
            {
                Game game = new Game(lastClickedPictureBox, userId, database);
                game.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Виберіть героя перед початком гри.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Spells formSpells = new Spells(userId, database);
            formSpells.Show();
            this.Hide();
        }

        public void pictureBoxMage_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            if (sender is PictureBox clickedPictureBox)
            {
                lastClickedPictureBox = clickedPictureBox;
                lastClickedPictureBox.Image = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\mage.png");
                lastClickedPictureBox.Tag = "Mage";
                PictureBoxSelected?.Invoke(clickedPictureBox);
                RemoveBorderFromAllPictureBoxes();
                DrawBorder(clickedPictureBox);
            }
            pictureBoxMage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBoxWarior_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
            if (sender is PictureBox clickedPictureBox)
            {
                lastClickedPictureBox = clickedPictureBox;
                lastClickedPictureBox.Image = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\warrior.png");
                lastClickedPictureBox.Tag = "Warrior";
                PictureBoxSelected?.Invoke(clickedPictureBox);
                RemoveBorderFromAllPictureBoxes();
                DrawBorder(clickedPictureBox);
            }
            pictureBoxWarior.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public void DrawBorder(PictureBox clickedPictureBox)
        {
            ControlPaint.DrawBorder(clickedPictureBox.CreateGraphics(), clickedPictureBox.ClientRectangle,
                Color.Red, 2, ButtonBorderStyle.Solid,
                Color.Red, 2, ButtonBorderStyle.Solid,
                Color.Red, 2, ButtonBorderStyle.Solid,
                Color.Red, 2, ButtonBorderStyle.Solid);
        }
        private void RemoveBorderFromAllPictureBoxes()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    ControlPaint.DrawBorder(pictureBox.CreateGraphics(), pictureBox.ClientRectangle,
                        SystemColors.Control, 2, ButtonBorderStyle.Solid,
                        SystemColors.Control, 2, ButtonBorderStyle.Solid,
                        SystemColors.Control, 2, ButtonBorderStyle.Solid,
                        SystemColors.Control, 2, ButtonBorderStyle.Solid);
                }
            }
        }
    }
}
