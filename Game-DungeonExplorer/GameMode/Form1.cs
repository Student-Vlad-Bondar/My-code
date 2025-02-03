using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryBatles;
using SQLite;

namespace GameMode
{
    public partial class Game : Form
    {
        private PictureBox heroPictureBox;
        private PictureBox[] heroAbilitiesPictureBoxes;
        private PictureBox changeBackgroundPictureBox;
        private PictureBox enemyPictureBox;
        private PictureBox chestPictureBox;
        private Point originalEnemyLocation;
        private Image[] backgroundImages = {
            Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\room1.png"),
            Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\room1.png"),
            Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\qqq.jpg"),
            Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\room2.png"),
            Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\roomEnd.jpg"),
        };
        private int currentBackgroundImageIndex = 0;
        private GameManager gameManager;
        private int userId;
        private Database database;

        public class GameSettings
        {
            public PictureBox SelectedPictureBox { get; set; }
            public int UserId { get; set; }
            public Database Database { get; set; }
        }
        public Game(GameSettings settings)
        {
            InitializeComponent();
            InitializeHero(settings.SelectedPictureBox);
            pictureBoxPause.BackColor = Color.Transparent;
            labelHeroHealth.BackColor = Color.Transparent;
            labelEnemiesHealth.BackColor = Color.Transparent;
            labelHeroDef.BackColor = Color.Transparent;
            labelEnemiesDef.BackColor = Color.Transparent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.userId = settings.UserId;
            this.database = settings.Database;
            gameManager = new GameManager(HeroTurnFinished, EnemyTurnFinished, UserSession.UserId, database);
            UpdateHealthLabels();
            SpawnEnemy();
        }
        private async void HeroTurnFinished()
        {
            if (gameManager.IsHeroTurn)
            {
                return;
            }
            gameManager.PerformEnemyTurn(async () => await DefeatHero());
            if (enemyPictureBox != null)
            {
                await AnimateEnemyAttack();
            }
            UpdateHealthLabels();
        }
        private void EnemyTurnFinished()
        {
        }
        private async Task DefeatHero()
        {
            var defeatImageForm = new Form
            {
                Size = new Size(600, 600),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.None,
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Lime,
                TransparencyKey = Color.Lime,
                BackgroundImage = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\You Died.png")
            };
            defeatImageForm.Show();
            await Task.Delay(4000);
            defeatImageForm.Hide();
            foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
            {
                form.Hide();
            }
            var defeatForm = new Defeat();
            defeatForm.Show();
        }
        private void ShowYouWinImage()
        {
            var winImageForm = new Form
            {
                Size = new Size(300, 300),
                StartPosition = FormStartPosition.Manual,
                Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - 300) / 2, 200),
                FormBorderStyle = FormBorderStyle.None,
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Gold,
                TransparencyKey = Color.Gold,
                BackgroundImage = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\You win.png")
            };
            winImageForm.Show();

            Timer timer = new Timer();
            timer.Interval = 4000;
            timer.Tick += (sender, args) =>
            {
                timer.Stop();
                winImageForm.Hide();
                OpenNextForm();
            };
            timer.Start();
        }
        private const int BossHealth = 55;
        private const int RegularEnemyHealth = 20;
        private void SpawnEnemy()
        {
            if (backgroundImages[currentBackgroundImageIndex] == backgroundImages[2] || backgroundImages[currentBackgroundImageIndex] == backgroundImages[4])
            {
                return;
            }
            Image enemyImage;
            if (backgroundImages[currentBackgroundImageIndex] == backgroundImages[3])
            {
                enemyImage = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\boss.png");
            }
            else
            {
                enemyImage = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\Ghibli.png");
            }
            enemyPictureBox = new PictureBox
            {
                Image = enemyImage,
                Size = new Size(240, 280),
                Location = new Point(this.ClientSize.Width - 240, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            originalEnemyLocation = enemyPictureBox.Location;
            enemyPictureBox.BackColor = Color.Transparent;
            this.Controls.Add(enemyPictureBox);

            gameManager.GameState.Enemy.Health = backgroundImages[currentBackgroundImageIndex] == backgroundImages[3] ? BossHealth : RegularEnemyHealth;
        }
        private void SpawnChest()
        {
            labelHeroHealth.Visible = false;
            labelHeroDef.Visible = false;
            labelEnemiesHealth.Visible = false;
            labelEnemiesDef.Visible = false;
            Image chestImage = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\chest.png");
            chestPictureBox = new PictureBox
            {
                Image = chestImage,
                Size = new Size(220, 180),
                Location = new Point(this.ClientSize.Width - 240, 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            chestPictureBox.Click += new EventHandler(Chest_Click);
            this.Controls.Add(chestPictureBox);
        }
        private void Chest_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(chestPictureBox);
            chestPictureBox = null;
            foreach (var abilityPictureBox in heroAbilitiesPictureBoxes)
            {
                this.Controls.Remove(abilityPictureBox);
            }

            heroPictureBox.Location = new Point(heroPictureBox.Location.X, heroPictureBox.Location.Y + 300);
            ChangeBackgroundImage();
        }
        private async void ChangeBackgroundImage()
        {
            if (backgroundImages != null && backgroundImages.Length > 0)
            {
                currentBackgroundImageIndex = (currentBackgroundImageIndex + 1) % backgroundImages.Length;
                this.BackgroundImage = backgroundImages[currentBackgroundImageIndex];
                gameManager.GameState.BackgroundImageChangeCount++;

                switch (gameManager.GameState.BackgroundImageChangeCount % 3)
                {
                    case 2:
                        this.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                }
                if (backgroundImages[currentBackgroundImageIndex] == backgroundImages[2])
                {
                    foreach (var abilityPictureBox in heroAbilitiesPictureBoxes)
                    {
                        abilityPictureBox.Visible = false;
                    }
                    changeBackgroundPictureBox = new PictureBox
                    {
                        Size = new Size(28, 60),
                        Location = new Point(heroPictureBox.Location.X + 313, heroPictureBox.Location.Y + 168),
                        BackColor = Color.Transparent,
                    };

                    changeBackgroundPictureBox.Click += ChangeBackgroundPictureBox_Click;
                    this.Controls.Add(changeBackgroundPictureBox);

                    this.Size = new Size(600, 534);
                    CenterToScreen();
                    gameManager.GameState.Hero.Health += 20;
                    if (gameManager.GameState.Hero.Health > 70) gameManager.GameState.Hero.Health = 70;

                    if (enemyPictureBox != null)
                    {
                        this.Controls.Remove(enemyPictureBox);
                        enemyPictureBox = null;
                    }
                    UpdateHealthLabels();
                }
                else
                {
                    if (changeBackgroundPictureBox != null)
                    {
                        this.Controls.Remove(changeBackgroundPictureBox);
                        changeBackgroundPictureBox = null;
                    }

                    foreach (var abilityPictureBox in heroAbilitiesPictureBoxes)
                    {
                        abilityPictureBox.Visible = true;
                    }

                    if (backgroundImages[currentBackgroundImageIndex] == backgroundImages[4])
                    {
                        this.Size = new Size(500, 734);
                        CenterToScreen();
                        if (enemyPictureBox != null)
                        {
                            this.Controls.Remove(enemyPictureBox);
                            enemyPictureBox = null;
                        }
                        await MoveHeroOffScreen();
                        ShowYouWinImage();
                    }
                    else
                    {
                        this.Size = new Size(1150, 534);
                        CenterToScreen();
                    }
                }
            }
        }
        private async Task MoveHeroOffScreen()
        {
            int step = 10;
            int duration = 4000;
            int iterations = duration / step;
            int moveStep = this.ClientSize.Width / iterations;

            for (int i = 0; i < iterations; i++)
            {
                heroPictureBox.Location = new Point(heroPictureBox.Location.X + moveStep, heroPictureBox.Location.Y);
                await Task.Delay(step);
            }
        }
        private void ChangeBackgroundPictureBox_Click(object sender, EventArgs e)
        {
            DefeatEnemy();
        }
        private void OpenNextForm()
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
        private void DefeatEnemy()
        {
            int num = 0;
            if (gameManager.GameState.BackgroundImageChangeCount == 0)
            {
                num = 1;
            }
            if (gameManager.GameState.BackgroundImageChangeCount == 1)
            {
                num = 2;
            }
            gameManager.GameState.DefeatedEnemiesCount++;
            if (backgroundImages[currentBackgroundImageIndex] == backgroundImages[3])
            {
                gameManager.GameState.BossesDefeated++;
                gameManager.GameState.DefeatedEnemiesCount--;
                database.SaveGameStatistics(userId, gameManager.GameState.BossesDefeated, gameManager.GameState.DefeatedEnemiesCount);
            }
            this.Controls.Remove(enemyPictureBox);

            if (backgroundImages[currentBackgroundImageIndex] == backgroundImages[3])
            {
                SpawnChest();
            }
            else if (gameManager.GameState.DefeatedEnemiesCount < num)
            {
                SpawnEnemy();
            }
            else
            {
                ChangeBackgroundImage();
                SpawnEnemy();
            }
            UpdateHealthLabels();
        }
        private void UpdateHealthLabels()
        {
            labelHeroHealth.Text = $"{gameManager.GameState.Hero.Health}/70";
            labelEnemiesHealth.Text = $"{gameManager.GameState.Enemy.Health}";
            labelHeroDef.Text = $"{gameManager.GameState.Hero.Defense}";
            labelEnemiesDef.Text = $"{gameManager.GameState.Enemy.Defense}";
        }
        private void InitializeHero(PictureBox selectedPictureBox)
        {
            if (selectedPictureBox.Image != null)
            {
                heroPictureBox = new PictureBox
                {
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(220, 300),
                    Location = new Point(120, 100),
                    Image = selectedPictureBox.Image
                };
                this.Controls.Add(heroPictureBox);
                if (selectedPictureBox.Tag is string heroType)
                {
                    if (heroType == "Mage")
                    {
                        InitializeHeroAbilitiesForMage();
                    }
                    else if (heroType == "Warrior")
                    {
                        InitializeHeroAbilitiesForWarrior();
                    }
                }
                else
                {
                    MessageBox.Show("Тип героя не вибрано.");
                }
            }
            else
            {
                MessageBox.Show("Зображення героя не вибрано.");
            }

        }
        private void InitializeHeroAbilitiesForWarrior()
        {
            Image[] abilityImages = {
                Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\sword.png"),
                Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\Swords.png"),
                Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\dizziness.png"),
            };

            heroAbilitiesPictureBoxes = new PictureBox[abilityImages.Length];

            for (int i = 0; i < abilityImages.Length; i++)
            {
                PictureBox abilityPictureBox = new PictureBox
                {
                    Image = abilityImages[i],
                    Size = new Size(45, 40),
                    Location = new Point(50 + i * 100, 440),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };

                abilityPictureBox.Click += new EventHandler(WarriorAbility_Click);
                abilityPictureBox.Tag = i;

                this.Controls.Add(abilityPictureBox);
                heroAbilitiesPictureBoxes[i] = abilityPictureBox;
            }
        }
        private async void WarriorAbility_Click(object sender, EventArgs e)
        {
            PictureBox clickedAbilityPictureBox = (PictureBox)sender;
            int abilityIndex = (int)clickedAbilityPictureBox.Tag;

            if (gameManager.GameState.IsHeroDebuffed)
            {
                abilityIndex = -1;
                gameManager.GameState.IsHeroDebuffed = false;
            }
            abilityIn = abilityIndex;
            if (abilityIndex == -1)
            {
                await AnimateFogAbility(clickedAbilityPictureBox);
            }
            if (abilityIndex == 1)
            {
                await AnimateShieldAbility(clickedAbilityPictureBox);
            }
            if (abilityIndex != 1)
            {
                await AnimateAbilityWarrior(clickedAbilityPictureBox);
            }

            gameManager.PerformWarriorTurn(abilityIndex, DefeatEnemy);
            UpdateHealthLabels();
        }
        private async Task AnimateAbilityWarrior(PictureBox abilityPictureBox)
        {
            if (abilityIn == -1)
            {
                return;
            }
            var originalHeroLocation = new Point(heroPictureBox.Location.X, heroPictureBox.Location.Y);
            var attackPosition = new Point(enemyPictureBox.Location.X - heroPictureBox.Width, heroPictureBox.Location.Y);
            int step = 25;

            while (heroPictureBox.Location.X < attackPosition.X)
            {
                heroPictureBox.Location = new Point(heroPictureBox.Location.X + step, heroPictureBox.Location.Y);
                await Task.Delay(5);
            }

            while (heroPictureBox.Location.X > originalHeroLocation.X)
            {
                heroPictureBox.Location = new Point(heroPictureBox.Location.X - step, heroPictureBox.Location.Y);
                await Task.Delay(5);
            }

        }
        private async Task AnimateShieldAbility(PictureBox abilityPictureBox)
        {
            var fogImage = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\Swords.png");
            var animatedAbility = new PictureBox
            {
                Image = fogImage,
                Size = new Size(250, 250),
                Location = new Point(heroPictureBox.Location.X, heroPictureBox.Location.Y + 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            this.Controls.Add(animatedAbility);
            animatedAbility.BringToFront();
            int step = 3;

            while (animatedAbility.Location.Y > 0)
            {
                animatedAbility.Location = new Point(animatedAbility.Location.X, animatedAbility.Location.Y - step);
                await Task.Delay(10);
            }

            this.Controls.Remove(animatedAbility);
        }
        private void InitializeHeroAbilitiesForMage()
        {
            Image[] abilityImages = {
                Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\bullet1.png"),
                Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\fog_icon.png"),
                Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\bullet2.png"),
            };

            heroAbilitiesPictureBoxes = new PictureBox[abilityImages.Length];

            for (int i = 0; i < abilityImages.Length; i++)
            {
                PictureBox abilityPictureBox = new PictureBox
                {
                    Image = abilityImages[i],
                    Size = new Size(50, 30),
                    Location = new Point(50 + i * 100, 440),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };

                abilityPictureBox.Click += new EventHandler(MageAbility_Click);
                abilityPictureBox.Tag = i;

                this.Controls.Add(abilityPictureBox);
                heroAbilitiesPictureBoxes[i] = abilityPictureBox;
            }
        }
        private int abilityIn = 0;
        private async void MageAbility_Click(object sender, EventArgs e)
        {
            PictureBox clickedAbilityPictureBox = (PictureBox)sender;
            int abilityIndex = (int)clickedAbilityPictureBox.Tag;

            if (gameManager.GameState.IsHeroDebuffed)
            {
                abilityIndex = -1;
                gameManager.GameState.IsHeroDebuffed = false;
            }
            abilityIn = abilityIndex;
            if (abilityIndex == 1 || abilityIndex == -1)
            {
                await AnimateFogAbility(clickedAbilityPictureBox);
            }
            if (abilityIndex != 1)
            {
                await AnimateAbilityMage(clickedAbilityPictureBox);
            }

            gameManager.PerformMageTurn(abilityIndex, DefeatEnemy);
            UpdateHealthLabels();
        }
        private async Task AnimateFogAbility(PictureBox abilityPictureBox)
        {
            var fogImage = Image.FromFile("C:\\Users\\Vlad\\source\\repos\\Універ\\Курсові роботи\\ООП\\Game-DungeonExplorer\\Image\\fog.png");
            var animatedAbility = new PictureBox
            {
                Image = fogImage,
                Size = new Size(250, 250),
                Location = new Point(heroPictureBox.Location.X, heroPictureBox.Location.Y + 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            this.Controls.Add(animatedAbility);
            animatedAbility.BringToFront();
            int step = 3;

            while (animatedAbility.Location.Y > 0)
            {
                animatedAbility.Location = new Point(animatedAbility.Location.X, animatedAbility.Location.Y - step);
                await Task.Delay(10);
            }

            this.Controls.Remove(animatedAbility);
        }
        private async Task AnimateAbilityMage(PictureBox abilityPictureBox)
        {
            if (abilityIn == -1)
            {
                return;
            }
            var animatedAbility = new PictureBox
            {
                Image = abilityPictureBox.Image,
                Size = abilityPictureBox.Size,
                Location = new Point(heroPictureBox.Location.X + 100, heroPictureBox.Location.Y + 200),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            this.Controls.Add(animatedAbility);
            int step = 15;
            while (animatedAbility.Location.X < enemyPictureBox.Location.X)
            {
                animatedAbility.Location = new Point(animatedAbility.Location.X + step, animatedAbility.Location.Y);
                await Task.Delay(2);
            }

            this.Controls.Remove(animatedAbility);
        }
        private async Task AnimateEnemyAttack()
        {
            if (enemyPictureBox == null || backgroundImages[currentBackgroundImageIndex] == backgroundImages[4] || gameManager.GameState.IsDefending)
            {
                return;
            }

            var attackPosition = new Point(heroPictureBox.Location.X + 160, enemyPictureBox.Location.Y);
            int step = 14;

            while (enemyPictureBox.Location.X > attackPosition.X)
            {
                enemyPictureBox.Location = new Point(enemyPictureBox.Location.X - step, enemyPictureBox.Location.Y);
                await Task.Delay(10);
            }

            var returnPosition = gameManager.GameState.IsDefending ? new Point(originalEnemyLocation.X + 50, originalEnemyLocation.Y) : originalEnemyLocation;

            while (enemyPictureBox.Location.X < returnPosition.X)
            {
                enemyPictureBox.Location = new Point(enemyPictureBox.Location.X + step, enemyPictureBox.Location.Y);
                await Task.Delay(10);
            }
        }
        private void pictureBoxPause_Click(object sender, EventArgs e)
        {
            using (MenuPause formPause = new MenuPause())
            {
                formPause.ShowDialog(this);
            }
        }
    }
}