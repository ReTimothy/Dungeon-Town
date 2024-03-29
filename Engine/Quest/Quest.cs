﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int XPReward { get; set; }
        public int GoldReward { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set; }
        public Item RewardItem { get; set; }

        public Quest(int id, string name, string description, int xpreward, int goldreward)
        {
            ID = id;
            Name = name;
            Description = description;
            XPReward = xpreward;
            GoldReward = goldreward;
            QuestCompletionItems = new List<QuestCompletionItem>();
        }
    }
}
