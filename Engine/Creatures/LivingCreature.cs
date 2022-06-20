using System;

namespace Engine
{
    public class LivingCreature //Parent class for every living creature; hostile, friendly or player.
    {
        public string Name { get; set; }
        public int CurrentHitPoints { get; set; }
        public int MaxHitPoints { get; set; }


        public LivingCreature (string name, int currentHitPoints, int maxHitPoints)
        {
            Name = name;
            CurrentHitPoints = currentHitPoints;
            MaxHitPoints = maxHitPoints;
        }
    }
}
