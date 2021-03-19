using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BattleRoyale.Data;

namespace BattleRoyale.Service
{
    class Spell
    {
        public void zombie(Character defender, Character zombie)
        {
            zombie.CurrentLife += defender.MaxLife;
        }

        public void guerrier(Character guerrier)
        {
            Thread th = new Thread(() => {
                guerrier.AttackSpeed += 0.5;
                Thread.Sleep(3000);
                guerrier.AttackSpeed -= 0.5;
            });
        }

        public void paladin(Character paladin)
        {
            // Pas bien compris
        }

        public void robot(Character robot)
        {
            robot.Attack += (robot.Attack * 50) / 100;
        }

        public void vampire(Character vampire)
        {
            
        }

        public void pretre(Character pretre)
        {
            pretre.CurrentLife += (pretre.MaxLife * 10) / 100;
        }

        public bool magicien(Character defender, Character magicien, Dictionary<String, Thread> threadDic, List<Character> characters)
        {
            var random = new Random();

            int defenseRandom = random.Next(0, 100);
            int defenseValue = defender.cast(defenseRandom, defender.Defense);

            int attackRandom = random.Next(0, 100);
            int attackValue = magicien.cast(attackRandom, magicien.Attack * 5);

            int margin = attackValue - defenseValue;

            if (margin > 0)
            {
                int damageTaken = margin * magicien.Damages * 5 / 100;
                bool isDead = magicien.takeDamage(damageTaken, defender, threadDic, characters);
                return isDead;
            }
            return false;
        }

        public void illusioniste(Character illusioniste)
        {
        }

        public void alchimiste(Character alchimiste)
        {
        }

        public void assassin(Character assassin)
        {
        }
    }
}
