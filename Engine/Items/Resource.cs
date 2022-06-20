using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Resource
    {
        public Item Details { get; set; }

        public Resource (Item details)
            //: base(id, name, namePlural, cost, description)
        {
            Details = details;
        }
    }
}
