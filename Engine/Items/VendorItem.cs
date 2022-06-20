using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class VendorItem
    {
        public Item Details { get; set; }
        public int Cost { get; set; }
        public bool CanBeSold { get; set; }

        public VendorItem(Item details, int cost, bool canBeSold)
        {
            Details = details;
            Cost = cost;
            CanBeSold = canBeSold;
        }
    }
}
