using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Negocio
{
    public class RNReporteCompra
    {
        public static XLWorkbook Reporte(DataGridView dataGridCompras,List<string> codigosEscaneados, string proveedor,string cveOrden,string importe)
        {
            var wb = new XLWorkbook();  // se abre el template
            var ws = wb.Worksheets.Add("Orden "+ cveOrden);
            // CABECERA
            ws.Column("A").Width = 2.14;
            ws.Cell("B2").Value = "Orden:";
            ws.Cell("C2").Value = cveOrden;
            ws.Cell("F2").Value = "Proveedor:";
            ws.Cell("G2").Value = proveedor;
            ws.Cell("K2").Value = "Importe:";
            ws.Cell("L2").Value = importe;
            ws.Cell("L2").Style.NumberFormat.SetFormat("$#,##0.00");
            // CABECERAS PRODUCTO
            ws.Cell("B5").Value = "Partida";
            ws.Cell("C5").Value = "Descripción";
            ws.Cell("D5").Value = "codigo";
            ws.Cell("E5").Value = "Caducidad";
            ws.Cell("F5").Value = "Lote";
            //ws.Cell("G5").Value = "Codigo Capturado";
            int i = 6, j = 2;
            foreach (DataGridViewRow dgvRenglon in dataGridCompras.Rows)
            {

                ws.Cell(i, j++).Value = dgvRenglon.Cells[0].Value;
                ws.Cell(i, j++).Value = dgvRenglon.Cells[2].Value;
                ws.Cell(i, j++).Value = dgvRenglon.Cells[6].Value;
                for (int col = 8; col < dataGridCompras.Columns.Count; col++)
                {
                    
                    if (col % 2 == 0 && col > 8)
                    {
                        i++;
                        j = j - 2;
                    }
                    ws.Cell(i, j++).Value = dgvRenglon.Cells[col].Value;
                }
                i++;
                j = 2;
            }

            i = i + 3;
            j = 2;
            ws.Cell(i++, j).Value = "Codigos leidos";
            foreach (var item in codigosEscaneados)
            {
                ws.Cell(i++, j).Value = item;
            }





            return wb;
        }
    }
}
