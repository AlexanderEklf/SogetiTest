using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kattaffar
{
    class Cats
    {
        public Cat[] cats;
        public string uri = "cats";

        public Cats(Cat[] cats, string uri)
        {
            this.cats = cats;
            this.uri = uri;
        }

        public Cats()
        {
        }



        public override string ToString()
        {
            string catsString ="";
            foreach (var cat in cats)
            {
                catsString += cat.ToString();
            }
            return catsString;
        }

    }


}
