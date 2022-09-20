using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetododoBurbuja.models
{
    public class Sismo
    {
        public string sat_unimed { get; set; }
        public string descripcion { get; set; }
        public string simbolo { get; set; }

        public string print()
        {
            return "Id: " + this.sat_unimed +" Descripción " + this.descripcion + " Simbolo " + this.simbolo;
        }
    }
}
