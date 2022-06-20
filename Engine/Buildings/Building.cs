using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Building // Parent class for stores and misc. buildings.
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CorrespondingButton { get; set; }
        public int UpgradeLvl { get; set; }
        public int Requirements { get; set; }
        public int GoldCost { get; set; }
        public int WoodCost { get; set; }
        public int StoneCost { get; set; }
        public int IronCost { get; set; }

        //public List<Resource> ResourceCostToBuild { get; set; }

        public Building (int id, string name, string button, int upgradeLvl, int requirements,
            int goldCost = 0, int woodCost = 0, int stoneCost = 0, int ironCost = 0)
        {
            ID = id;
            Name = name;
            CorrespondingButton = button;
            UpgradeLvl = upgradeLvl;
            Requirements = requirements;
            GoldCost = goldCost;
            WoodCost = woodCost;
            StoneCost = stoneCost;
            IronCost = ironCost;

            //ResourceCostToBuild = new List<Resource>();
        }

        public string ResourceCost()
        {
            string cost = "";
            if (GoldCost > 0) { cost += GoldCost + " gold"; }
            if (WoodCost > 0) { cost += ", " + WoodCost + " lumber"; }
            if (StoneCost > 0) { cost += ", " + StoneCost + " stone"; }
            if (IronCost > 0) { cost += ", " + IronCost + " iron";  }

            return cost;
        }

        public BuiltBuilding GetBuiltBuilding(Building building, Player player)
        {
            foreach(BuiltBuilding builtBuilding in player.BuildingsBuilt)
            {
                if(builtBuilding.Details == building)
                {
                    return builtBuilding;
                }
            }
            return null;
        }
                
        /*public String BuildingButton(Player _player)
        {
            bool buildingHasBeenBuilt = false;

            foreach (BuiltBuilding building in _player.BuildingsBuilt)
            {
                if (building.Details.ID == World.BuildingByID(World.BUILDING_ID_LUMBER_MILL).ID)
                {
                    buildingHasBeenBuilt = true;
                    break;
                }
            }

            if (buildingHasBeenBuilt == false)
            {
                enterBuilding(false);
                return "You enter an empty plot of land. This would be a good place to build a " + Name + "." + 
                    Environment.NewLine + "Would you like to build the " + Name + " for the cost of " + ResourceCost() + "?" + Environment.NewLine;
            }
            else
            {
                enterBuilding(true);
                return "You enter the Lumber Mill";
            }
        }*/
    }
}
