using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBatles
{
    public class GameState
    {
        public Hero Hero { get; private set; }
        public Enemy Enemy { get; private set; }
        public bool IsHeroDebuffed { get; set; }
        public bool IsDefending { get; set; }
        public int BossesDefeated { get; set; }
        public int DefeatedEnemiesCount { get; set; }
        public int BackgroundImageChangeCount { get; set; }

        public GameState()
        {
            Hero = new Hero();
            Enemy = new Enemy();
            IsHeroDebuffed = false;
            IsDefending = false;
            BossesDefeated = 0;
            DefeatedEnemiesCount = 0;
            BackgroundImageChangeCount = 0;
        }
    }
}
