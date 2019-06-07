using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kattaffar
{
    class NewOrder
    {
        public List<String> catIds { get; set; }
        public NewOrder(List<String> catIds)
        {
            this.catIds = catIds;
        }

        public NewOrder()
        {
            catIds = new List<String>();
        }

        public override string ToString()
        {
            int count = 1;
            string _catIds = "";
            foreach (var catId in catIds)
            {
                _catIds += $"Cat{count} + {catId}\n";
                count++;
            }
            return _catIds;
        }
    }
}
