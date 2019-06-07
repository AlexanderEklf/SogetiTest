using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kattaffar
{
    class Cat
    {
        public string id;
        public string name;
        public double price;

        public Cat(string id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }
        public Cat()
        {
        }

        public override string ToString()
        {
            return $"{id}\t {name}\t {price}\n";
        }

    }
}
