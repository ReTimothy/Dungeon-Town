using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class NPC : PlayerSide // Class for friendly (hireable) NPCs, to be implemented.
    {
        public int CostToHire { get; set; }

        public NPC(string name, int currentHitPoints, int maxHitPoints, int experiencePoints, int level, int costToHire)
            : base(name, currentHitPoints, maxHitPoints, experiencePoints, level)
        {
            CostToHire = costToHire;
        }
    }
}
