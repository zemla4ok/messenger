using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class SearchObject
    {
        public string Element { get; private set; }
        public string ByWhat { get; private set; }

        public SearchObject(string element, string byWhat)
        {
            this.Element = element;
            this.ByWhat = byWhat;
        }

        public override string ToString()
        {
            return this.ByWhat + ":" + this.Element;
        }
    }
}