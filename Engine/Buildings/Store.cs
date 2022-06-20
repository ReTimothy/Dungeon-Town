using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Store : Building // Class to build all store buildings, where the player can buy things
    {
        public string Type { get; set; } // Item type sold in this store
        public List <VendorItem> Stock { get; set; }

        public Store(int id, string name, string function, int upgradeLvl, int requirements, 
            int goldCost, int woodCost, int stoneCost, int ironCost, string type)
            : base(id, name, function, upgradeLvl, requirements, goldCost, woodCost, stoneCost, ironCost)
        {
            Type = type;
            Stock = new List<VendorItem>();
        }
    }
}
