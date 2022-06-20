using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class BuiltBuilding
    {
        public Building Details { get; set; }

        public BuiltBuilding(Building building)
        {
            Details = building;
        }

        public bool buildingUpgradeable(BuiltBuilding builtBuilding)
        {
            if (builtBuilding.Details is MiscBuilding)
            {
                MiscBuilding miscBuilding = (MiscBuilding)builtBuilding.Details;
                if (miscBuilding.Type == "Produces Resources" && miscBuilding.UpgradeLvl < 5)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
