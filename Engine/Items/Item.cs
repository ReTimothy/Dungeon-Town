using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }

        public Item(int id, string name, string namePlural, int cost, string description)
        {
            ID = id;
            Name = name;
            NamePlural = namePlural;
            Cost = cost;
            Description = description;
        }
    }
}
