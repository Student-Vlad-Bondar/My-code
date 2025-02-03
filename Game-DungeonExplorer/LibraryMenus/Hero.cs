using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBatles
{
    public class Hero
    {
        public int Health { get; set; }
        public int Defense { get; set; }
        public bool IsDebuffed { get; set; }

        public Hero()
        {
            Health = 70;
            Defense = 0;
            IsDebuffed = false;
        }

        public void ApplyDefenseBoost(int amount)
        {
            Defense += amount;
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
