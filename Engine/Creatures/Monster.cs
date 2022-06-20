using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Monster : LivingCreature // All enemies are built with this class (or, potentially, its children)
    {
        //public string Type { get; set; }
        public int ID { get; set; }
        public int MaximumDamage { get; set; }
        public int XPReward { get; set; }
        public int GoldReward { get; set; }
        public List<LootItem> LootTable { get; set; }

        public Monster (string name, int currentHitPoints, int maxHitPoints, int id, int maximumDamage, int xpReward, int goldReward)
            : base(name, currentHitPoints, maxHitPoints)
        {
            //Type = type;
            ID = id;
            MaximumDamage = maximumDamage;
            XPReward = xpReward;
            GoldReward = goldReward;
            LootTable = new List<LootItem>();
        }
    }
}
