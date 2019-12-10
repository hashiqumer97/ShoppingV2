using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingV2.Order
{
    public class OrderLineQuantityNotAvailable : Exception
    {
        public OrderLineQuantityNotAvailable():base("Your Order cannot be added because the quantity is over the limit!")
        {

        }
    }
}
