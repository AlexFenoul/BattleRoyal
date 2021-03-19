using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BattleRoyale;

namespace BattleRoyale.Data
{
    /// <summary>
    /// Classe d'un personnage
    /// </summary>
    class Character
    {
        public string Name;

        public int Attack;

        public int Defense;

        public double AttackSpeed;

        public int Damages;

        public int MaxLife;

        public int CurrentLife;

        public double PowerSpeed;

        public double Delay;

        public int PoisonRate;

        public bool Undead;

        public bool IsHidden;

        public int Score;


        /// <summary>
        /// Permet de faire un jet pour le personnage
        /// </summary>
        /// <param name="random"></param>
        public int cast(int random, int characteristic)
        {
            int castValue  = characteristic + random;
            return castValue;
        }

        /// <summary>
        /// Permet de faire un jet de vitesse pour le personnage
        /// </summary>
        /// <param name="random"></param>
        public void speedCast(int random)
        {
            double speedCastValue = ((double)1000 / AttackSpeed) - random;

            Delay = speedCastValue;
        }

        /// <summary>
        /// Permet de faire un jet de vitesse pour le personnage
        /// </summary>
        /// <param name="random"></param>
        public void addSpeedCast(int addTime)
        {
            Delay += addTime;
        }

        /// <summary>
        /// Attack un ennemie
        /// </summary>
        /// <param name="defender"></param>
        /// <returns></returns>
        public bool attackEnnemy(Character defender, Dictionary<string, Thread> threadDictionary, List<Character> characters)
        {
            if (defender.IsHidden)
            {
                return false;
            }
            var random = new Random();

            int defenseRandom = random.Next(0, 100);
            int defenseValue = defender.cast(defenseRandom, defender.Defense);

            int attackRandom = random.Next(0, 100);
            int attackValue = cast(attackRandom, Attack);

            int margin = attackValue - defenseValue;

            if (margin > 0)
            {
                int damageTaken = margin * Damages / 100;
                bool isDead = takeDamage(damageTaken, defender, threadDictionary, characters);
                return isDead;
            }
            return false;
        }

        /// <summary>
        /// Le personnage prend des dégats
        /// </summary>
        /// <param name="damageTaken"></param>
        public bool takeDamage(int damageTaken, Character defender, Dictionary<string, Thread> threadDictionary, List<Character> characters)
        {
            defender.CurrentLife -= damageTaken;
            defender.Delay += damageTaken;

            if (defender.CurrentLife < 0 )
            {
                Death(defender, threadDictionary);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permet de choisir un defenseur
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public Character chooseDefender(Character attacker, List<Character> characters)
        {
            var random = new Random();
            int randomNumber;
            if (characters.Count > 1)
            {
                randomNumber = random.Next(0, characters.Count - 1);
            }
            else
            {
                randomNumber = random.Next(0, characters.Count);
            }

            if (characters[randomNumber].Name == attacker.Name)
            {
                if(randomNumber == characters.Count)
                {
                    return characters[randomNumber - 1];
                }
                if (randomNumber == 0)
                {

                    return characters[randomNumber + 1];
                }

                return characters[randomNumber];
            }
            return characters[randomNumber];
        }

        /// <summary>
        /// Le personnage meurt
        /// </summary>
        public void Death(Character defender, Dictionary<string, Thread> threadDictionary)
        {
            
            Console.WriteLine("{0} Is Dead", defender.Name);

        }

        /// <summary>
        /// Constructeur d'un personnage complet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <param name="attackSpeed"></param>
        /// <param name="damages"></param>
        /// <param name="maxLife"></param>
        /// <param name="currentLife"></param>
        /// <param name="powerSpeed"></param>
        /// <param name="delay"></param>
        /// <param name="poisonRate"></param>
        /// <param name="undead"></param>
        /// <param name="isHidden"></param>
        /// <param name="score"></param>
        public Character(string name, int attack, int defense, double attackSpeed, int damages, int maxLife, int currentLife, double powerSpeed, double delay, int poisonRate, bool undead, bool isHidden, int score)
            {
                Name = name;
                Attack = attack;
                Defense = defense;
                AttackSpeed = attackSpeed;
                Damages = damages;
                MaxLife = maxLife;
                CurrentLife = currentLife;
                PowerSpeed = powerSpeed;
                Delay = delay;
                PoisonRate = poisonRate;
                Undead = undead;
                IsHidden = isHidden;
                Score = score;
            }
    }
}
