using System;
using System.Collections.Generic;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

namespace GestionVentas.Clases
{
    public static class ReportePdf
    {
        //colores: una cifra por fila -> 0 = normal, 1 = rojo (stock critico), 2 = ambar (stock bajo)
        public static void Generar(string ruta, string titulo, string detalle,
                                   List<string> encabezados, List<double> anchos, List<bool> alineadoDerecha,
                                   List<string[]> filas, List<int> colores)
        {
            Document doc = new Document();
            doc.Info.Title = titulo;
            doc.Styles["Normal"].Font.Name = "Arial";
            doc.Styles["Normal"].Font.Size = 9;

            Section seccion = doc.AddSection();
            seccion.PageSetup.PageFormat = PageFormat.A4;
            seccion.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            seccion.PageSetup.RightMargin = Unit.FromCentimeter(1.5);
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(2);
            seccion.PageSetup.BottomMargin = Unit.FromCentimeter(2);

            //Encabezado del documento
            Paragraph p = seccion.AddParagraph("Tienda de Productos Electrónicos");
            p.Format.Font.Size = 10;
            p.Format.Font.Color = Colors.Gray;

            p = seccion.AddParagraph(titulo);
            p.Format.Font.Size = 16;
            p.Format.Font.Bold = true;
            p.Format.Font.Color = new Color(31, 56, 100);
            p.Format.SpaceAfter = Unit.FromPoint(4);

            p = seccion.AddParagraph(detalle);
            p.Format.Font.Size = 9;
            p.Format.SpaceAfter = Unit.FromPoint(10);

            //Tabla (el ancho util de la pagina es 18 cm)
            Table tabla = seccion.AddTable();
            tabla.Borders.Width = 0.5;
            tabla.Borders.Color = Colors.Gray;
            tabla.TopPadding = 3;
            tabla.BottomPadding = 3;
            tabla.LeftPadding = 4;
            tabla.RightPadding = 4;

            double suma = anchos.Sum();
            for (int i = 0; i < encabezados.Count; i++)
            {
                Column col = tabla.AddColumn(Unit.FromCentimeter(18.0 * anchos[i] / suma));
                col.Format.Alignment = alineadoDerecha[i] ? ParagraphAlignment.Right : ParagraphAlignment.Left;
            }

            Row cabecera = tabla.AddRow();
            cabecera.HeadingFormat = true;
            cabecera.Shading.Color = new Color(31, 56, 100);
            cabecera.Format.Font.Color = Colors.White;
            cabecera.Format.Font.Bold = true;
            for (int i = 0; i < encabezados.Count; i++)
                cabecera.Cells[i].AddParagraph(encabezados[i]);

            for (int f = 0; f < filas.Count; f++)
            {
                Row fila = tabla.AddRow();
                if (colores[f] == 1) fila.Shading.Color = new Color(255, 205, 210);
                else if (colores[f] == 2) fila.Shading.Color = new Color(255, 236, 179);

                for (int c = 0; c < encabezados.Count; c++)
                    fila.Cells[c].AddParagraph(filas[f][c] ?? "");
            }

            //Leyenda del semaforo (solo si hay filas coloreadas)
            if (colores.Any(x => x != 0))
            {
                p = seccion.AddParagraph("Semáforo de stock:  ROJO = crítico (5 o menos)   |   ÁMBAR = bajo (de 6 a 10)");
                p.Format.SpaceBefore = Unit.FromPoint(8);
                p.Format.Font.Size = 8;
                p.Format.Font.Italic = true;
            }

            //Pie de pagina
            Paragraph pie = seccion.Footers.Primary.AddParagraph();
            pie.Format.Alignment = ParagraphAlignment.Center;
            pie.Format.Font.Size = 8;
            pie.AddText("Generado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "   -   Página ");
            pie.AddPageField();
            pie.AddText(" de ");
            pie.AddNumPagesField();

            PdfDocumentRenderer renderizador = new PdfDocumentRenderer(true);
            renderizador.Document = doc;
            renderizador.RenderDocument();
            renderizador.PdfDocument.Save(ruta);
        }
    }
}