namespace GameMode
{
    partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.pictureBoxPause = new System.Windows.Forms.PictureBox();
            this.labelHeroHealth = new System.Windows.Forms.Label();
            this.labelEnemiesHealth = new System.Windows.Forms.Label();
            this.labelHeroDef = new System.Windows.Forms.Label();
            this.labelEnemiesDef = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPause)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPause
            // 
            this.pictureBoxPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxPause.BackgroundImage")));
            this.pictureBoxPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxPause.Location = new System.Drawing.Point(1092, 12);
            this.pictureBoxPause.Name = "pictureBoxPause";
            this.pictureBoxPause.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxPause.TabIndex = 0;
            this.pictureBoxPause.TabStop = false;
            this.pictureBoxPause.Click += new System.EventHandler(this.pictureBoxPause_Click);
            // 
            // labelHeroHealth
            // 
            this.labelHeroHealth.AutoSize = true;
            this.labelHeroHealth.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHeroHealth.ForeColor = System.Drawing.Color.Lime;
            this.labelHeroHealth.Location = new System.Drawing.Point(189, 81);
            this.labelHeroHealth.Name = "labelHeroHealth";
            this.labelHeroHealth.Size = new System.Drawing.Size(51, 21);
            this.labelHeroHealth.TabIndex = 6;
            this.labelHeroHealth.Text = "70/70";
            // 
            // labelEnemiesHealth
            // 
            this.labelEnemiesHealth.AutoSize = true;
            this.labelEnemiesHealth.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEnemiesHealth.ForeColor = System.Drawing.Color.Lime;
            this.labelEnemiesHealth.Location = new System.Drawing.Point(958, 81);
            this.labelEnemiesHealth.Name = "labelEnemiesHealth";
            this.labelEnemiesHealth.Size = new System.Drawing.Size(28, 21);
            this.labelEnemiesHealth.TabIndex = 7;
            this.labelEnemiesHealth.Text = "15";
            // 
            // labelHeroDef
            // 
            this.labelHeroDef.AutoSize = true;
            this.labelHeroDef.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHeroDef.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelHeroDef.Location = new System.Drawing.Point(204, 51);
            this.labelHeroDef.Name = "labelHeroDef";
            this.labelHeroDef.Size = new System.Drawing.Size(0, 21);
            this.labelHeroDef.TabIndex = 8;
            // 
            // labelEnemiesDef
            // 
            this.labelEnemiesDef.AutoSize = true;
            this.labelEnemiesDef.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEnemiesDef.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelEnemiesDef.Location = new System.Drawing.Point(967, 60);
            this.labelEnemiesDef.Name = "labelEnemiesDef";
            this.labelEnemiesDef.Size = new System.Drawing.Size(0, 21);
            this.labelEnemiesDef.TabIndex = 9;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1134, 495);
            this.Controls.Add(this.labelEnemiesDef);
            this.Controls.Add(this.labelHeroDef);
            this.Controls.Add(this.labelEnemiesHealth);
            this.Controls.Add(this.labelHeroHealth);
            this.Controls.Add(this.pictureBoxPause);
            this.DoubleBuffered = true;
            this.Name = "Game";
            this.Text = "Game";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPause)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPause;
        private System.Windows.Forms.Label labelHeroHealth;
        private System.Windows.Forms.Label labelEnemiesHealth;
        private System.Windows.Forms.Label labelHeroDef;
        private System.Windows.Forms.Label labelEnemiesDef;
    }
}

