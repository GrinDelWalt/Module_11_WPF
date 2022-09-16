using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF
{
    internal class Controller
    {
        public void ReadJson()
        {
            DeserializationJSON deserialization = new DeserializationJSON();
            deserialization.DeserializJSON();

        }
    }
}
