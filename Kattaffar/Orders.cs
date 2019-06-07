using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kattaffar
{
    class Orders
    {
        public List<Order> orders { get; set; }

        public Orders()
        {
        }

        public Orders(List<Order> orders)
        {
            this.orders = orders;
        }

        public override string ToString()
        {
            string ordersString = "";
            foreach (var order in orders)
            {
                ordersString += order.ToString();
            }
            return ordersString;
        }
    }
}
