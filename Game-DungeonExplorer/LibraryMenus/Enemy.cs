using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBatles
{
    public class Enemy
    {
        public int Health { get; set; }
        public int Defense { get; set; }
        public bool IsDefending { get; set; }

        public Enemy(int initialHealth = 20)
        {
            Health = initialHealth;
            Defense = 0;
            IsDefending = false;
        }

        public void ApplyDefenseBoost(int amount)
        {
            Defense += amount;
            IsDefending = true;
        }

        public void ApplyDamage(int damage)
        {
            if (Defense > 0)
            {
                if (Defense >= damage)
                {
                    Defense -= damage;
                }
                else
                {
                    damage -= Defense;
                    Defense = 0;
                    Health -= damage;
                }
            }
            else
            {
                Health -= damage;
            }
        }
    }
}