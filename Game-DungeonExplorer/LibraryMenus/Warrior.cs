using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBatles
{
    public class Warrior
    {
        public void UseWarriorAbility(int abilityIndex, GameState gameState, Action defeatEnemy, Action heroTurnCallback)
        {
            switch (abilityIndex)
            {
                case 0:
                    gameState.Enemy.ApplyDamage(6);
                    if (gameState.Enemy.Health <= 0)
                    {
                        defeatEnemy();
                    }
                    break;
                case 1:
                    gameState.Hero.ApplyDefenseBoost(6);
                    break;
                case 2:
                    gameState.Enemy.ApplyDamage(2);
                    if (gameState.Enemy.Health <= 0)
                    {
                        defeatEnemy();
                    }
                    heroTurnCallback?.Invoke();
                    break;
            }
        }
    }
}
