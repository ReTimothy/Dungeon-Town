using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class DungeonDelve
    {
        int Level { get; set; }

        public DungeonDelve(int level)
        {
            Level = level;
        }

        public int NextRoom() //Returns an event/potion/treasure/monster
        {
            int roomContains = RandomNumberGenerator.NumberBetween(0, 100);
            
            if(roomContains <= 20)
            {
                // Empty room, fancy description
                return 1;
            }
            else if(roomContains <= 45)
            {
                if(roomContains == 21 || roomContains == 22)
                {
                    // free potion
                    return 2;
                }
                if(roomContains == 39 || roomContains == 40)
                {
                    // grand treasure
                    return 3;
                }
                else
                {
                    // regular treasure
                    return 4;
                }                
            }
            else
            {
                // MONSTER TIME!
                return 5;
            }
        }

        public int[] MonsterIDPicker(int level)
        {
            if (level > World.Monsters.Count) 
            { 
                level = World.Monsters.Count; 
            }
            int monsterID = RandomNumberGenerator.NumberBetween(1, level);
            int monsterAmount = 1;
            if(monsterID * 2 <= level)
            {
                monsterAmount = 3;
            }
            else if(monsterID + 2 <= level)
            {
                monsterAmount = 2;
            }

            return new int[] { monsterID, monsterAmount };
            //return new int[] { 1, 2 }; // DON'T FORGET TO CHANGE THIS BACK! FOR TESTING PURPOSES ONLY!
        }
        public Dictionary<string, Monster> CreateMonsters(int ID, int amount)
        {
            Dictionary<string, Monster> monsters = new Dictionary<string, Monster>();
            Monster standardMonster = World.MonsterByID(ID);

            Monster monster1 = new Monster(standardMonster.Name, standardMonster.CurrentHitPoints, standardMonster.MaxHitPoints,
                standardMonster.ID, standardMonster.MaximumDamage, standardMonster.XPReward, standardMonster.GoldReward);

            foreach (LootItem lootItem in standardMonster.LootTable)
            {
                monster1.LootTable.Add(lootItem);
            }
            string monster1Name = monster1.Name + " 1";
            monsters.Add(monster1.Name, monster1);

            if(amount > 1)
            {
                Monster monster2 = new Monster(standardMonster.Name, standardMonster.CurrentHitPoints, standardMonster.MaxHitPoints,
                standardMonster.ID, standardMonster.MaximumDamage, standardMonster.XPReward, standardMonster.GoldReward);

                foreach (LootItem lootItem in standardMonster.LootTable)
                {
                    monster2.LootTable.Add(lootItem);
                }
                string monster2Name = monster2.Name + " 2";
                monsters.Add(monster2Name, monster2);
            }
            if(amount > 2)
            {
                Monster monster3 = new Monster(standardMonster.Name, standardMonster.CurrentHitPoints, standardMonster.MaxHitPoints,
                standardMonster.ID, standardMonster.MaximumDamage, standardMonster.XPReward, standardMonster.GoldReward);

                foreach (LootItem lootItem in standardMonster.LootTable)
                {
                    monster3.LootTable.Add(lootItem);
                }
                string monster3Name = monster3.Name + " 3";
                monsters.Add(monster3Name, monster3);
            }

            return monsters;
        }

        public void Battle(List<Monster> monsters)
        {

        }

        public Item PotionReward(Player player)
        {
            int playerLevel = player.Level;
            if(playerLevel < 5) { return World.ItemByID(100); } // Minor Healing Potion
            if(playerLevel < 9) { return World.ItemByID(100); } // Lesser Healing Potion
            if(playerLevel < 12) { return World.ItemByID(100); } // Basic Healing Potion
            if(playerLevel < 15) { return World.ItemByID(100); } // Greater Healing Potion
            return World.ItemByID(100); // Superior Healing Potion            
        }
    }
}
