using GestionVentas.Clases;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GestionVentas.Reportes
{
    public static class ReporteProductosVendidosPdf
    {
        public static void Exportar(string ruta, IList<ProductoVendido> productos, string filtroProducto, string generadoPor)
        {
            var documento = new Document();
            documento.Info.Title = "Reporte de productos vendidos - Tienda de productos electrónicos";
            documento.Info.Author = "Tienda de productos electrónicos";
            var normal = documento.Styles[StyleNames.Normal];
            normal.Font.Name = "Arial";
            normal.Font.Size = 9;

            var seccion = documento.AddSection();
            seccion.PageSetup.PageFormat = PageFormat.A4;
            seccion.PageSetup.Orientation = Orientation.Landscape;
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(2);
            seccion.PageSetup.BottomMargin = Unit.FromCentimeter(1.8);
            seccion.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            seccion.PageSetup.RightMargin = Unit.FromCentimeter(1.5);

            var encabezado = seccion.Headers.Primary.AddParagraph("TIENDA DE PRODUCTOS ELECTRÓNICOS | REPORTE DE PRODUCTOS VENDIDOS");
            encabezado.Format.Font.Bold = true;
            encabezado.Format.Font.Color = Colors.DarkSlateBlue;
            var titulo = seccion.AddParagraph("Reporte de productos vendidos");
            titulo.Format.Font.Size = 18;
            titulo.Format.Font.Bold = true;
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.3);
            seccion.AddParagraph("Producto: " + filtroProducto);
            seccion.AddParagraph("Generado por: " + generadoPor);
            seccion.AddParagraph("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
            var total = seccion.AddParagraph("Productos: " + productos.Count +
                "   |   Unidades vendidas: " + productos.Sum(x => x.CantidadVendida) +
                "   |   Monto total: " + Dinero(productos.Sum(x => x.MontoAcumulado)));
            total.Format.SpaceAfter = Unit.FromCentimeter(0.5);

            var tabla = seccion.AddTable();
            tabla.Borders.Width = 0.5;
            tabla.Borders.Color = Colors.LightGray;
            tabla.TopPadding = 5;
            tabla.BottomPadding = 5;
            double[] anchos = { 9.0, 6.0, 4.5, 6.5 };
            foreach (var ancho in anchos)
                tabla.AddColumn(Unit.FromCentimeter(ancho));
            var cabecera = tabla.AddRow();
            cabecera.HeadingFormat = true;
            cabecera.Shading.Color = Colors.DarkSlateBlue;
            cabecera.Format.Font.Color = Colors.White;
            cabecera.Format.Font.Bold = true;
            string[] columnas = { "Producto", "Marca", "Unidades vendidas", "Monto acumulado" };
            for (int i = 0; i < columnas.Length; i++)
                cabecera.Cells[i].AddParagraph(columnas[i]);
            cabecera.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            cabecera.Cells[3].Format.Alignment = ParagraphAlignment.Right;

            for (int i = 0; i < productos.Count; i++)
            {
                var producto = productos[i];
                var fila = tabla.AddRow();
                fila.VerticalAlignment = VerticalAlignment.Center;
                if (i % 2 == 0)
                    fila.Shading.Color = Colors.WhiteSmoke;
                string[] valores = { producto.Producto, producto.Marca,
                    producto.CantidadVendida.ToString(), Dinero(producto.MontoAcumulado) };
                for (int j = 0; j < valores.Length; j++)
                    fila.Cells[j].AddParagraph(valores[j] ?? "");
                fila.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                fila.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            }

            var pie = seccion.Footers.Primary.AddParagraph();
            pie.Format.Alignment = ParagraphAlignment.Right;
            pie.AddText("Página ");
            pie.AddPageField();
            pie.AddText(" de ");
            pie.AddNumPagesField();
            var renderizador = new PdfDocumentRenderer(true) { Document = documento };
            renderizador.RenderDocument();
            using (var pdf = renderizador.PdfDocument)
                pdf.Save(ruta);
        }

        private static string Dinero(decimal valor)
        {
            return "$ " + valor.ToString("N2", CultureInfo.InvariantCulture);
        }
    }
}
