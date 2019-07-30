using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio
{
    public class RNCodigo
    {
        public string compañia { get; set; }
        public string producto { get; set; }
        public string control { get; set; }
        public string contador { get; set; }
        public DateTime caducidad { get; set; }
        public string lote { get; set; }

        public static List<RNCodigo> Desfracmentar(ListBox list)
        {
            List<RNCodigo> codigos = new List<RNCodigo>();
            foreach(var item in list.Items)
            {
                codigos.Add(Decodificar(item.ToString()));
            }
            return codigos;
        }

        public static RNCodigo Decodificar(string codigo)
        {
            //0108431673 41002 5 30 8027 17 201028 10 LIQ646A
            //0108431673 41202 9 30 2804 17 201228 10 LIQ403B
            //0108431673 01200 7 30 2889 17 190828 10 140
            //0108431673 01260 1 30 8337 17 201028 10 2344TA
            //0108431673 41022 3 30 2348 17 210128 10 LIQ332A
            RNCodigo validado = new RNCodigo();
            validado.compañia = codigo.Substring(2,8);
            validado.producto = codigo.Substring(10,5);
            validado.control = codigo.Substring(15, 1);
            validado.contador = codigo.Substring(18,4);
            validado.lote = codigo.Substring(32,codigo.Length-32);
            var cd = codigo.Substring(24, 4);
            validado.caducidad = Convert.ToDateTime("01/"+cd.Substring(2,2)+"/20"+ cd.Substring(0, 2));
            return validado;
        }
    }
}
