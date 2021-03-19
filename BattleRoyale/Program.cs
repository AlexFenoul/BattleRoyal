using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using BattleRoyale.Data;
using BattleRoyale.Service;

namespace BattleRoyale
{

    class Program
    {
        static List<Thread> characterTasks = new List<Thread>();
        static Dictionary<string, Thread> threadDictionary = new Dictionary<string, Thread>();

        static private Object thisLock = new Object();

        static void Main(string[] args)
        {
            Console.WriteLine("Let the battle begin !");
            InitGame();

            Console.ReadKey();
        }

        public static void InitGame()
        {
            List<Character> characters = Init.createCharacter();
            foreach (Character character in characters)
            {
                Init.initDelay(character);
                CreateThreads(character, characters);
            }

            StartMyThreads();

        }

        public static void CreateThreads(Character character, List<Character> characters)
        {
            Thread th = new Thread(() => { ThreadLoop(character, characters); });
            characterTasks.Add(th);
            threadDictionary.Add(character.Name, th);

        }

        private static void StartMyThreads()
        {
            foreach (Thread th in characterTasks)
            {
                th.Start();
            }
        }

        public static void ThreadLoop(Character character, List<Character> characters)
        {
            bool stopthread = false;
            while (!stopthread)
            {
                lock (thisLock)
                {
                    int sleep = Convert.ToInt32(character.Delay);
                    Thread.Sleep(sleep);

                    Character defender = character.chooseDefender(character, characters);

                    bool isDead = character.attackEnnemy(defender, threadDictionary, characters);
                    if (isDead)
                    {
                        characters.Remove(defender);
                        threadDictionary[defender.Name].Join();
                        stopthread = true;
                    }

                    var random = new Random();
                    int randomnumber = random.Next(1, 100);

                    character.speedCast(randomnumber);
                }
            }
        }
    }
}