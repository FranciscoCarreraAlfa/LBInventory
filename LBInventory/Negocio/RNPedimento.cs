using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RNPedimento
    {
        public string numPedimento { get; set; }
        public string Aduana { get; set; }
        public DateTime Fecha { get; set; }
        public string Ciudad { get; set; }
        public string Frontera { get; set; }
        public string GLN { get; set; }
    }
}
