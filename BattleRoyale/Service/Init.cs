using System;
using System.Collections.Generic;
using System.Text;
using BattleRoyale.Data;
using System.Xml.Linq;

namespace BattleRoyale.Service
{
    class Init
    {
        public static List<Character> createCharacter()
        {
            List<Character> listCharacter = new List<Character> { };

            var path = "./Mock";
            var fullPath = System.IO.Path.GetFullPath(path);
            string goodPath = fullPath.Substring(0, fullPath.Length - 29);

            XElement xelement = XElement.Load(goodPath + "/Mock/Characters.xml");
            IEnumerable<XElement> characters = xelement.Elements();
            foreach (var character in characters)
            {
                var name = character.Element("Name").Value;
                var attack = character.Element("Attack").Value;
                var defense = character.Element("Defense").Value;
                var attackSpeed = character.Element("AttackSpeed").Value;
                var damages = character.Element("Damages").Value;
                var maximumLife = character.Element("MaximumLife").Value;
                var currentLife = character.Element("CurrentLife").Value;
                var powerSpeed = character.Element("PowerSpeed").Value;


                double attackSpeedDouble = Convert.ToDouble(attackSpeed);
                double powerSpeedDouble = Convert.ToDouble(powerSpeed);

                Character newCharacter = new Character(name, int.Parse(attack), int.Parse(defense), attackSpeedDouble, int.Parse(damages), int.Parse(maximumLife), int.Parse(currentLife), powerSpeedDouble, 0, 0, isUndead(name), false, 0);
                listCharacter.Add(newCharacter);
            }

            return listCharacter;
        }

        public static bool isUndead(string name)
        {
            List<string> undeads = new List<string>(new string[] { "Zombie", "Vampire", "Nécromancien" });
            bool isUndead = false;

            undeads.ForEach(delegate (String undead)
            {
                if (undead == name)
                {
                    isUndead = true;
                }
            });

            return isUndead;
        }

        public static void initDelay(Character character)
        {
            var random = new Random();
            int randomnumber = random.Next(1, 100);

            character.speedCast(randomnumber);
        }
    }
}
