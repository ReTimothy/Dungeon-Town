using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Player : PlayerSide
    {
        public int Gold { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        public List<BuiltBuilding> BuildingsBuilt { get; set; }
        public int Lumber { get; set; }
        public int Stone { get; set; }
        public int Iron { get; set; }

        public Player(string name, int currentHitPoints, int maxHitPoints, int experiencePoints,
            int level, int gold, int Lumber = 0, int Stone = 0, int Iron = 0) 
            : base(name, currentHitPoints, maxHitPoints, experiencePoints, level)
        {
            Gold = gold;
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
            BuildingsBuilt = new List<BuiltBuilding>();
        }

        public bool HasThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CompletedThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }

            return false;
        }

        public bool HasAllQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                bool foundItemInPlayersInventory = false;

                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == qci.Details.ID)
                    {
                        foundItemInPlayersInventory = true;

                        if (ii.Quantity < qci.Quantity)
                        {
                            return false;
                        }
                    }
                }

                if (!foundItemInPlayersInventory)
                {
                    return false;
                }
            }

            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == qci.Details.ID)
                    {
                        ii.Quantity -= qci.Quantity;
                        break;
                    }
                }
            }
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == itemToAdd.ID)
                {
                    ii.Quantity++;

                    return;
                }
            }

            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }

        public void MarkQuestCompleted(Quest quest)
        {
            foreach (PlayerQuest pq in Quests)
            {
                if (pq.Details.ID == quest.ID)
                {
                    pq.IsCompleted = true;

                    return;
                }
            }
        }

        public bool LevelUp()
        {
            // XP required for next level up (level 2 and onward): current xp + 20 + (level * 3)
            int requiredXP = 20;
            for(int i = 1; i < Level; i++)
            {
                requiredXP = requiredXP + 20 + (Level * 3);
            }
            if(ExperiencePoints >= requiredXP)
            {
                Level++;
                MaxHitPoints += Level;
                CurrentHitPoints = MaxHitPoints;
                return true;
            }
            return false;
        }
    }
}
