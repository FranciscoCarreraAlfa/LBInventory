using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using System.Data;
using Umbrella.Aspel.Sae.Datos;

namespace Negocio
{
    public class RNRemision
    {
        public static void GenerarRemision(RNOrdenRecepcion recepcion, List<RNPartidasRecepcion> partidas, List<RNRecepcion.Lotes> lotes)
        {
            // generar la remision en la inportadora
            int numEmpresa = RNLbInventory.ObtenerImportadora().NumEmpresa;
            int folio = 0;
            string cveDocRecpcion = GenerarCveRemision("R", null, numEmpresa, out folio);
            List<RNPartidasRemision> partidasRemision = null;
            RNOrdenRemision remision = RNOrdenRemision.GenerarRemision(cveDocRecpcion,recepcion,folio ,numEmpresa, partidas,lotes, out partidasRemision);
            // actualizar las existencias
            foreach(var part in partidasRemision)
            {
                RNLbInventory.ActualizarExistencias(part.CVE_ART, recepcion.NUM_ALMA, -1, part.CANT, numEmpresa);
            }

        }

        public static string GenerarCveRemision(string tipoDoc, string serie, int numEmpresa, out int folio)
        {
            serie = string.IsNullOrEmpty(serie) ? "STAND." : serie;
            string cveDoc = RNLbInventory.ObtenerCveDoc(tipoDoc, serie, numEmpresa, "F", out folio);
            return cveDoc;
        }





    }

}
