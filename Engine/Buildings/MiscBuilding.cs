using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class MiscBuilding: Building // Other buildings in the game, for resource production, progression unlocks and/or questgiving
    {
        public string Type { get; set; } // What does this building do?
        public int Amount { get; set; } // Amount of resources produced
        public Resource Resource { get; set; } // Kind of resource it produces (wood/stone/iron/coal?)

        public MiscBuilding(int id, string name, string function, int upgradeLvl, int requirements, int goldCost,
            int woodCost, int stoneCost, int ironCost, string type, int amount = 0, Resource resource = null)
            : base(id, name, function, upgradeLvl, requirements, goldCost, woodCost, stoneCost, ironCost)
        {
            Type = type;
            Amount = amount;
            Resource = resource;
        }

        public bool Upgrade(Player player)
        {
            double goldUpgradeCost = ((GoldCost + 50) * UpgradeLvl) * (UpgradeLvl * 1.5);
            double lumberUpgradeCost = ((WoodCost + 50) * UpgradeLvl) * (UpgradeLvl * 1.5);
            double stoneUpgradeCost = ((StoneCost + 50) * UpgradeLvl) * (UpgradeLvl * 1.5);
            double ironUpgradeCost = ((IronCost + 50) * UpgradeLvl) * (UpgradeLvl * 1.5);

            if(player.Gold >= goldUpgradeCost && player.Lumber >= lumberUpgradeCost && 
                player.Stone >= stoneUpgradeCost && player.Iron >= ironUpgradeCost && UpgradeLvl < 5)
            {
                player.Gold -= (int)goldUpgradeCost;
                player.Lumber -= (int)lumberUpgradeCost;
                player.Stone -= (int)stoneUpgradeCost;
                player.Iron -= (int)ironUpgradeCost;
                UpgradeLvl++;
                Amount *= 2;
                return true;
            }
            return false;
        }

        public string UpgradeCostString()
        {
            string cost = "";
            cost += ((GoldCost + 50) * UpgradeLvl) * (UpgradeLvl * 1.5) + " gold"; 
            cost += ", " + ((WoodCost + 50) * UpgradeLvl) * (UpgradeLvl * 1.5) + " lumber"; 
            cost += ", " + ((StoneCost + 50) * UpgradeLvl) * (UpgradeLvl * 1.5) + " stone"; 
            cost += " and " + ((IronCost + 50) * UpgradeLvl) * (UpgradeLvl * 1.5) + " iron"; 

            return cost;
        }
    }
}
