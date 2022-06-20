using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Potion : Item
    {
        public int AmountToHeal { get; set; }

        public Potion (int id, string name, string namePlural, int cost, string description, int amountToHeal)
            : base(id, name, namePlural, cost, description)
        {
            AmountToHeal = amountToHeal;
        }
    }
}
