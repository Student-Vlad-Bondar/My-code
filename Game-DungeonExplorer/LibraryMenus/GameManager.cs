using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SQLite;

namespace LibraryBatles
{
    public class GameManager
    {
        private Mage mage;
        private Warrior warrior;
        private bool isHeroTurn;
        private Action heroTurnCallback;
        private Action enemyTurnCallback;
        private int userId;
        private SQLite.Database database;
        private Random random;
        public bool IsHeroTurn => isHeroTurn;
        public GameState GameState { get; private set; }


        public GameManager(Action heroTurnCallback, Action enemyTurnCallback, int userId, SQLite.Database database)
        {
            mage = new Mage();
            warrior = new Warrior();
            this.heroTurnCallback = heroTurnCallback;
            this.enemyTurnCallback = enemyTurnCallback;
            this.userId = userId;
            this.database = database;
            isHeroTurn = true;
            random = new Random();
            GameState = new GameState();

            var stats = database.LoadGameStatistics(userId);
            if (stats.HasValue)
            {
                GameState.BossesDefeated = stats.Value.BossesDefeated;
                GameState.DefeatedEnemiesCount = stats.Value.EnemiesDefeated;
            }
        }

        public void PerformMageTurn(int abilityIndex, Action defeatEnemy)
        {
            if (!isHeroTurn)
            {
                return;
            }

            mage.UseMageAbility(abilityIndex, GameState, defeatEnemy);

            database.SaveGameStatistics(userId, GameState.BossesDefeated, GameState.DefeatedEnemiesCount);

            isHeroTurn = false;
            heroTurnCallback?.Invoke();
            enemyTurnCallback?.Invoke();
        }
        public void PerformWarriorTurn(int abilityIndex, Action defeatEnemy)
        {
            if (!isHeroTurn)
            {
                return;
            }

            warrior.UseWarriorAbility(abilityIndex, GameState, defeatEnemy, () =>
            {
                isHeroTurn = true;
                heroTurnCallback?.Invoke();
            });

            if (abilityIndex != 2)
            {
                isHeroTurn = false;
                heroTurnCallback?.Invoke();
                enemyTurnCallback?.Invoke();
            }

            database.SaveGameStatistics(userId, GameState.BossesDefeated, GameState.DefeatedEnemiesCount);
        }
        public void PerformEnemyTurn(Action defeatHero)
        {
            if (isHeroTurn)
            {
                Debug.WriteLine("Enemy turn was attempted, but it is still the hero's turn.");
                return;
            }

            int action = random.Next(3);
            if (GameState.Enemy.Health <= 7)
            {
                action = 0;
            }
            switch (action)
            {
                case 0:
                    Debug.WriteLine("Enemy attacks.");
                    GameState.IsDefending = false;
                    GameState.Hero.ApplyDamage(6);
                    if (GameState.Hero.Health <= 0)
                    {
                        defeatHero();
                    }
                    break;
                case 1:
                    Debug.WriteLine("Enemy defends.");
                    GameState.IsDefending = true;
                    GameState.Enemy.ApplyDefenseBoost(4);
                    break;
                case 2:
                    Debug.WriteLine("Enemy applies debuff.");
                    GameState.Hero.ApplyDamage(1);
                    GameState.IsHeroDebuffed = true;
                    GameState.IsDefending = true;
                    break;
            }
            database.SaveGameStatistics(userId, GameState.BossesDefeated, GameState.DefeatedEnemiesCount);

            isHeroTurn = true;
            heroTurnCallback?.Invoke();
        }
    }
}