using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class PlayerSide : LivingCreature // Parent class for Player and NPCs.
    {
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }
        //Equipment and (future) magic unlocks are stored here.

        public PlayerSide (string name, int currentHitPoints, int maxHitPoints, int experiencePoints, int level) 
            : base(name, currentHitPoints, maxHitPoints)
        {
            ExperiencePoints = experiencePoints;
            Level = level;
        }
    }
}
