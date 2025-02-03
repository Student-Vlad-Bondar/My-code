namespace Menu
{
    partial class MainMenu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnExitGame = new System.Windows.Forms.Button();
            this.btnLoginToAccount = new System.Windows.Forms.Button();
            this.btnExitAccount = new System.Windows.Forms.Button();
            this.btnAchievement = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartGame
            // 
            this.btnStartGame.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStartGame.Location = new System.Drawing.Point(40, 209);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(89, 36);
            this.btnStartGame.TabIndex = 0;
            this.btnStartGame.Text = "Грати";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnExitGame
            // 
            this.btnExitGame.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExitGame.Location = new System.Drawing.Point(40, 401);
            this.btnExitGame.Name = "btnExitGame";
            this.btnExitGame.Size = new System.Drawing.Size(89, 38);
            this.btnExitGame.TabIndex = 1;
            this.btnExitGame.Text = "Вийти";
            this.btnExitGame.UseVisualStyleBackColor = true;
            this.btnExitGame.Click += new System.EventHandler(this.btnExitGame_Click);
            // 
            // btnLoginToAccount
            // 
            this.btnLoginToAccount.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoginToAccount.Location = new System.Drawing.Point(21, 251);
            this.btnLoginToAccount.Name = "btnLoginToAccount";
            this.btnLoginToAccount.Size = new System.Drawing.Size(128, 50);
            this.btnLoginToAccount.TabIndex = 2;
            this.btnLoginToAccount.Text = "Ввійти в акаунт";
            this.btnLoginToAccount.UseVisualStyleBackColor = true;
            this.btnLoginToAccount.Click += new System.EventHandler(this.btnLoginToAccount_Click);
            // 
            // btnExitAccount
            // 
            this.btnExitAccount.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnExitAccount.Location = new System.Drawing.Point(6, 307);
            this.btnExitAccount.Name = "btnExitAccount";
            this.btnExitAccount.Size = new System.Drawing.Size(162, 44);
            this.btnExitAccount.TabIndex = 3;
            this.btnExitAccount.Text = "Вийти з аккаунту";
            this.btnExitAccount.UseVisualStyleBackColor = true;
            this.btnExitAccount.Click += new System.EventHandler(this.btnExitAccount_Click);
            // 
            // btnAchievement
            // 
            this.btnAchievement.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAchievement.Location = new System.Drawing.Point(12, 357);
            this.btnAchievement.Name = "btnAchievement";
            this.btnAchievement.Size = new System.Drawing.Size(138, 38);
            this.btnAchievement.TabIndex = 4;
            this.btnAchievement.Text = "Статистика";
            this.btnAchievement.UseVisualStyleBackColor = true;
            this.btnAchievement.Click += new System.EventHandler(this.btnAchievement_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(531, 610);
            this.Controls.Add(this.btnAchievement);
            this.Controls.Add(this.btnExitAccount);
            this.Controls.Add(this.btnLoginToAccount);
            this.Controls.Add(this.btnExitGame);
            this.Controls.Add(this.btnStartGame);
            this.Name = "MainMenu";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnExitGame;
        private System.Windows.Forms.Button btnLoginToAccount;
        private System.Windows.Forms.Button btnExitAccount;
        private System.Windows.Forms.Button btnAchievement;
    }
}

