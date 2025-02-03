using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBatles
{
    public class Mage
    {
        public void UseMageAbility(int abilityIndex, GameState gameState, Action defeatEnemy)
        {
            switch (abilityIndex)
            {
                case 0:
                    gameState.Enemy.ApplyDamage(5);
                    if (gameState.Enemy.Health <= 0)
                    {
                        gameState.Hero.Health += 2;
                        defeatEnemy();
                    }
                    break;
                case 1:
                    gameState.Hero.ApplyDefenseBoost(4);
                    break;
                case 2:
                    gameState.Enemy.ApplyDamage(7);
                    if (gameState.Enemy.Health <= 0)
                    {
                        defeatEnemy();
                    }
                    break;
            }
        }
    }
}