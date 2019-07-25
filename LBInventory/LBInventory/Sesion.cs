using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;

namespace LBInventory
{
    public class Sesion
    {
        public static RNUsuario Usuario = null;

        public static bool Acceso(string usuario, string pass)
        {
           Usuario = RNUsuario.Acceso(usuario, pass,out bool snAcceso);
            return snAcceso;
        }

        public static void Cerrar()
        {
            Usuario = null;
        }
    }
}
