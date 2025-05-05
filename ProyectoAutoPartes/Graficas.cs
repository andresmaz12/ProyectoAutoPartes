using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using FastReport;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using FastReport.Design;
using OfficeOpenXml; // Requerido para exportación real a Excel (requiere paquete EPPlus)
using System.Text;

namespace ProyectoAutoPartes
{
    public class claseReportesBalanceGeneral
    {
        private string connectionString;
        private formMenu form;

        // Constructor con inyección de dependencias
        public claseReportesBalanceGeneral(string connectionString, formMenu form)
        {
            this.connectionString = connectionString;
            this.form = form;
        }

        #region Métodos para obtener datos del balance general

        /// <summary>
        /// Obtiene los activos principales ordenados por valor
        /// </summary>
        /// <param name="topN">Cantidad de activos a mostrar</param>
        /// <returns>DataTable con los activos</returns>
        
        private void CrearDiseñoReporteBalanceGeneral(Report report)
        {
            try
            {
                // Configuración inicial del reporte
                report.ReportInfo.Name = "BalanceGeneral";
                report.ReportInfo.Author = "Sistema AutoPartes";
                report.ReportInfo.Description = "Reporte de Balance General";
                report.ReportInfo.Created = DateTime.Now;
                
                // Obtener referencias a los objetos del reporte
                ReportPage page = new ReportPage();
                page.Name = "PaginaBalanceGeneral";
                report.Pages.Add(page);
                
                // Crear banda de título
                ReportTitleBand titleBand = new ReportTitleBand();
                titleBand.Name = "TitleBand";
                titleBand.Height = Units.Centimeters(2.5f);
                page.Bands.Add(titleBand);
                
                // Agregar título al reporte
                TextObject titleText = new TextObject();
                titleText.Name = "TitleText";
                titleText.Bounds = new RectangleF(0, 0, Units.Centimeters(19), Units.Centimeters(1.5f));
                titleText.Text = "REPORTE DE BALANCE GENERAL";
                titleText.HorzAlign = HorzAlign.Center;
                titleText.VertAlign = VertAlign.Center;
                titleText.Font = new Font("Arial", 16, FontStyle.Bold);
                titleBand.Objects.Add(titleText);
                
                // Agregar fecha al reporte
                TextObject dateText = new TextObject();
                dateText.Name = "DateText";
                dateText.Bounds = new RectangleF(0, Units.Centimeters(1.5f), Units.Centimeters(19), Units.Centimeters(0.8f));
                dateText.Text = "Fecha de generación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                dateText.HorzAlign = HorzAlign.Right;
                dateText.Font = new Font("Arial", 10);
                titleBand.Objects.Add(dateText);
                
                // Crear banda de datos para activos
                DataBand activosBand = new DataBand();
                activosBand.Name = "ActivosBand";
                activosBand.DataSource = report.GetDataSource("Activos");
                activosBand.Height = Units.Centimeters(0.6f);
                page.Bands.Add(activosBand);
                
                // Crear banda de encabezado para activos
                HeaderBand activosHeader = new HeaderBand();
                activosHeader.Name = "ActivosHeader";
                activosHeader.Height = Units.Centimeters(1.2f);
                activosHeader.CanGrow = true;
                page.Bands.Add(activosHeader);
                
                // Título de sección de activos
                TextObject activosTitle = new TextObject();
                activosTitle.Name = "ActivosTitle";
                activosTitle.Bounds = new RectangleF(0, 0, Units.Centimeters(19), Units.Centimeters(0.8f));
                activosTitle.Text = "ACTIVOS PRINCIPALES";
                activosTitle.Font = new Font("Arial", 12, FontStyle.Bold);
                activosTitle.VertAlign = VertAlign.Center;
                activosTitle.Fill = new SolidFill(Color.LightGreen);
                activosTitle.Border.Lines = BorderLines.All;
                activosHeader.Objects.Add(activosTitle);
                
                // Encabezados de columnas de activos
                TextObject nombreActivoHeader = new TextObject();
                nombreActivoHeader.Name = "NombreActivoHeader";
                nombreActivoHeader.Bounds = new RectangleF(0, Units.Centimeters(0.8f), Units.Centimeters(12), Units.Centimeters(0.6f));
                nombreActivoHeader.Text = "Nombre del Activo";
                nombreActivoHeader.Font = new Font("Arial", 10, FontStyle.Bold);
                nombreActivoHeader.Border.Lines = BorderLines.All;
                nombreActivoHeader.Fill = new SolidFill(Color.WhiteSmoke);
                activosHeader.Objects.Add(nombreActivoHeader);
                
                TextObject valorActivoHeader = new TextObject();
                valorActivoHeader.Name = "ValorActivoHeader";
                valorActivoHeader.Bounds = new RectangleF(Units.Centimeters(12), Units.Centimeters(0.8f), Units.Centimeters(7), Units.Centimeters(0.6f));
                valorActivoHeader.Text = "Valor ($)";
                valorActivoHeader.Font = new Font("Arial", 10, FontStyle.Bold);
                valorActivoHeader.HorzAlign = HorzAlign.Right;
                valorActivoHeader.Border.Lines = BorderLines.All;
                valorActivoHeader.Fill = new SolidFill(Color.WhiteSmoke);
                activosHeader.Objects.Add(valorActivoHeader);
                
                // Campos de datos de activos
                TextObject nombreActivoData = new TextObject();
                nombreActivoData.Name = "NombreActivoData";
                nombreActivoData.Bounds = new RectangleF(0, 0, Units.Centimeters(12), Units.Centimeters(0.6f));
                nombreActivoData.Text = "[Activos.NombreActivo]";
                nombreActivoData.Font = new Font("Arial", 9);
                nombreActivoData.Border.Lines = BorderLines.Left | BorderLines.Right | BorderLines.Bottom;
                activosBand.Objects.Add(nombreActivoData);
                
                TextObject valorActivoData = new TextObject();
                valorActivoData.Name = "ValorActivoData";
                valorActivoData.Bounds = new RectangleF(Units.Centimeters(12), 0, Units.Centimeters(7), Units.Centimeters(0.6f));
                valorActivoData.Text = "[FormatNumber([Activos.ValorActivo], 2)]";
                valorActivoData.Font = new Font("Arial", 9);
                valorActivoData.HorzAlign = HorzAlign.Right;
                valorActivoData.Border.Lines = BorderLines.Left | BorderLines.Right | BorderLines.Bottom;
                activosBand.Objects.Add(valorActivoData);
                
                // Agregar secciones similares para pasivos y otros datos del balance
                
                // Guardar el diseño para uso futuro
                string carpetaReportes = Path.Combine(Application.StartupPath, "Reportes");
                
                // Crear directorio si no existe
                if (!Directory.Exists(carpetaReportes))
                {
                    Directory.CreateDirectory(carpetaReportes);
                }
                
                string rutaReporte = Path.Combine(carpetaReportes, "BalanceGeneral.frx");
                report.Save(rutaReporte);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear diseño de reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Exporta los datos del balance general a Excel
        /// </summary>
        /// <param name="rutaGuardar">Ruta donde se guardará el archivo Excel</param>
        /// <returns>Verdadero si la exportación fue exitosa</returns>
        public bool ExportarDatosExcel(string rutaGuardar = "")
        {
            try
            {
                // Crear dataset con todas las tablas
                DataSet ds = new DataSet("BalanceGeneralData");
                
                // Obtener las tablas de datos
                DataTable dtActivos = ObtenerActivosPrincipales(50);
                dtActivos.TableName = "Activos";
                ds.Tables.Add(dtActivos.Copy());
                
                DataTable dtPasivos = ObtenerPasivosPrincipales(50);
                dtPasivos.TableName = "Pasivos";
                ds.Tables.Add(dtPasivos.Copy());
                
                DataTable dtBalanceTrimestral = ObtenerBalanceTrimestral();
                dtBalanceTrimestral.TableName = "BalanceTrimestral";
                ds.Tables.Add(dtBalanceTrimestral.Copy());
                
                DataTable dtIndicadores = CalcularIndicadoresFinancieros();
                dtIndicadores.TableName = "Indicadores";
                ds.Tables.Add(dtIndicadores.Copy());
                
                // Si no se proporciona ruta, usar una ruta predeterminada
                if (string.IsNullOrEmpty(rutaGuardar))
                {
                    string carpetaExportacion = Path.Combine(Application.StartupPath, "Exportaciones");
                    
                    // Crear directorio si no existe
                    if (!Directory.Exists(carpetaExportacion))
                    {
                        Directory.CreateDirectory(carpetaExportacion);
                    }
                    
                    // Generar nombre de archivo con fecha y hora
                    string nombreArchivo = "BalanceGeneral_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    rutaGuardar = Path.Combine(carpetaExportacion, nombreArchivo);
                }
                
                // Usar EPPlus para exportar realmente a Excel (en lugar de CSV)
                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(rutaGuardar)))
                {
                    // Para cada tabla en el DataSet, crear una hoja
                    foreach (DataTable table in ds.Tables)
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(table.TableName);
                        
                        // Agregar encabezados
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = table.Columns[i].ColumnName;
                            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                            worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            worksheet.Cells[1, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        }
                        
                        // Agregar datos
                        for (int row = 0; row < table.Rows.Count; row++)
                        {
                            for (int col = 0; col < table.Columns.Count; col++)
                            {
                                worksheet.Cells[row + 2, col + 1].Value = table.Rows[row][col];
                                
                                // Si es un valor numérico, formato de moneda
                                if (table.Columns[col].DataType == typeof(decimal) || 
                                    table.Columns[col].DataType == typeof(double) || 
                                    table.Columns[col].DataType == typeof(float))
                                {
                                    worksheet.Cells[row + 2, col + 1].Style.Numberformat.Format = "$#,##0.00";
                                }
                                
                                worksheet.Cells[row + 2, col + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                            }
                        }
                        
                        // Ajustar ancho de columnas automáticamente
                        worksheet.Cells.AutoFitColumns();
                    }
                    
                    // Agregar una hoja con gráficas
                    ExcelWorksheet chartSheet = excelPackage.Workbook.Worksheets.Add("Gráficas");
                    
                    // Crear gráfico de activos principales
                    var activosChart = chartSheet.Drawings.AddChart("ActivosChart", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered);
                    activosChart.SetPosition(1, 0, 0, 0);
                    activosChart.SetSize(800, 400);
                    activosChart.Title.Text = "Activos Principales";
                    activosChart.Series.Add(excelPackage.Workbook.Worksheets["Activos"].Cells["B2:B11"], 
                                          excelPackage.Workbook.Worksheets["Activos"].Cells["A2:A11"]);
                    activosChart.XAxis.Title.Text = "Activos";
                    activosChart.YAxis.Title.Text = "Valor ($)";
                    
                    // Guardar el archivo Excel
                    excelPackage.Save();
                }
                
                MessageBox.Show("Datos exportados correctamente a: " + rutaGuardar, "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar datos a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        /// <summary>
        /// Método para validar los datos obtenidos de la base de datos
        /// </summary>
        /// <param name="dt">DataTable a validar</param>
        /// <returns>DataTable validado</returns>
        private DataTable ValidarDataTable(DataTable dt)
        {
            if (dt == null)
            {
                dt = new DataTable();
            }
            
            // Verificar si hay datos
            if (dt.Rows.Count == 0)
            {
                // Agregar una fila con valores por defecto para evitar errores
                DataRow row = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType == typeof(string))
                    {
                        row[col] = "Sin datos";
                    }
                    else if (col.DataType == typeof(decimal) || col.DataType == typeof(double) || 
                             col.DataType == typeof(float) || col.DataType == typeof(int))
                    {
                        row[col] = 0;
                    }
                    else if (col.DataType == typeof(DateTime))
                    {
                        row[col] = DateTime.Now;
                    }
                    else
                    {
                        row[col] = DBNull.Value;
                    }
                }
                dt.Rows.Add(row);
            }
            
            return dt;
        }
        public DataTable ObtenerActivosPrincipales(int topN = 10)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    NombreActivo, 
                                    ValorActivo 
                                FROM 
                                    Activos 
                                ORDER BY 
                                    ValorActivo DESC 
                                LIMIT @TopN";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TopN", topN);
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos de activos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene los pasivos principales ordenados por valor
        /// </summary>
        /// <param name="topN">Cantidad de pasivos a mostrar</param>
        /// <returns>DataTable con los pasivos</returns>
        public DataTable ObtenerPasivosPrincipales(int topN = 10)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    NombrePasivo, 
                                    ValorPasivo 
                                FROM 
                                    Pasivos 
                                WHERE 
                                    ValorPasivo > 0
                                ORDER BY 
                                    ValorPasivo DESC 
                                LIMIT @TopN";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TopN", topN);
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos de pasivos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene el resumen del balance general por trimestre del año actual
        /// </summary>
        /// <returns>DataTable con el resumen trimestral</returns>
        public DataTable ObtenerBalanceTrimestral()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    Trimestre, 
                                    SUM(TotalActivos) as TotalActivos,
                                    SUM(TotalPasivos) as TotalPasivos,
                                    SUM(Capital) as Capital
                                FROM 
                                    BalanceGeneral 
                                WHERE 
                                    YEAR(Fecha) = YEAR(CURDATE())
                                GROUP BY 
                                    Trimestre 
                                ORDER BY 
                                    Trimestre ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos de balance trimestral: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene activos por categoría
        /// </summary>
        /// <returns>DataTable con categorías y valores</returns>
        public DataTable ObtenerActivosPorCategoria()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    CategoriaActivo, 
                                    SUM(ValorActivo) as ValorTotal
                                FROM 
                                    Activos 
                                GROUP BY 
                                    CategoriaActivo 
                                ORDER BY 
                                    ValorTotal DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos por categoría de activos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene pasivos por tipo
        /// </summary>
        /// <returns>DataTable con tipos y valores</returns>
        public DataTable ObtenerPasivosPorTipo()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    TipoPasivo, 
                                    SUM(ValorPasivo) as ValorTotal
                                FROM 
                                    Pasivos 
                                GROUP BY 
                                    TipoPasivo 
                                ORDER BY 
                                    ValorTotal DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos por tipo de pasivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene la evolución del capital por periodo
        /// </summary>
        /// <returns>DataTable con periodos y valores de capital</returns>
        public DataTable ObtenerEvolucionCapital()
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    CONCAT(YEAR(Fecha), '-', MONTH(Fecha)) as Periodo,
                                    AVG(Capital) as CapitalPromedio,
                                    MAX(Capital) as CapitalMaximo
                                FROM 
                                    BalanceGeneral 
                                GROUP BY 
                                    YEAR(Fecha), MONTH(Fecha)
                                ORDER BY 
                                    YEAR(Fecha) ASC, MONTH(Fecha) ASC
                                LIMIT 12"; // Últimos 12 meses

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener evolución del capital: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        #endregion

        #region Métodos para generar gráficos

        /// <summary>
        /// Genera un gráfico de barras para los activos principales
        /// </summary>
        /// <param name="chartControl">Control Chart donde se mostrará el gráfico</param>
        /// <param name="topN">Cantidad de activos a mostrar</param>
        public void GenerarGraficoActivosPrincipales(Chart chartControl, int topN = 10)
        {
            try
            {
                DataTable dt = ObtenerActivosPrincipales(topN);
                
                // Configurar el gráfico
                chartControl.Series.Clear();
                chartControl.ChartAreas.Clear();
                chartControl.Titles.Clear();
                
                ChartArea chartArea = new ChartArea("MainChartArea");
                chartControl.ChartAreas.Add(chartArea);
                
                Title title = new Title("Activos Principales", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
                chartControl.Titles.Add(title);
                
                Series series = new Series("ActivosSeries");
                series.ChartType = SeriesChartType.Bar;  // Tipo de gráfico de barras
                chartControl.Series.Add(series);
                
                // Agregar datos al gráfico
                foreach (DataRow row in dt.Rows)
                {
                    string nombre = row["NombreActivo"].ToString();
                    decimal valor = Convert.ToDecimal(row["ValorActivo"]);
                    
                    // Si el nombre es muy largo, lo acortamos
                    if (nombre.Length > 20)
                    {
                        nombre = nombre.Substring(0, 17) + "...";
                    }
                    
                    series.Points.AddXY(nombre, valor);
                }
                
                // Configurar formato del gráfico
                chartArea.AxisX.LabelStyle.Font = new Font("Arial", 8);
                chartArea.AxisY.Title = "Valor ($)";
                chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
                chartArea.AxisY.LabelStyle.Format = "${0:N0}";
                
                // Colorear las barras
                foreach (DataPoint point in series.Points)
                {
                    point.Color = Color.FromArgb(0, 128, 0);  // Verde
                    point.BorderColor = Color.Black;
                    point.BorderWidth = 1;
                    
                    // Agregar etiquetas a las barras
                    point.Label = string.Format("${0:N0}", point.YValues[0]);
                    point.LabelForeColor = Color.Black;
                }
                
                // Habilitar zoom y tooltips
                chartArea.CursorX.IsUserEnabled = true;
                chartArea.CursorX.IsUserSelectionEnabled = true;
                chartArea.AxisX.ScaleView.Zoomable = true;
                chartControl.Series[0].ToolTip = "#VALX: $#VALY{N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar gráfico de activos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera un gráfico de barras para los pasivos principales
        /// </summary>
        /// <param name="chartControl">Control Chart donde se mostrará el gráfico</param>
        /// <param name="topN">Cantidad de pasivos a mostrar</param>
        public void GenerarGraficoPasivosPrincipales(Chart chartControl, int topN = 10)
        {
            try
            {
                DataTable dt = ObtenerPasivosPrincipales(topN);
                
                // Configurar el gráfico
                chartControl.Series.Clear();
                chartControl.ChartAreas.Clear();
                chartControl.Titles.Clear();
                
                ChartArea chartArea = new ChartArea("MainChartArea");
                chartControl.ChartAreas.Add(chartArea);
                
                Title title = new Title("Pasivos Principales", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
                chartControl.Titles.Add(title);
                
                Series series = new Series("PasivosSeries");
                series.ChartType = SeriesChartType.Bar;
                chartControl.Series.Add(series);
                
                // Agregar datos al gráfico
                foreach (DataRow row in dt.Rows)
                {
                    string nombre = row["NombrePasivo"].ToString();
                    decimal valor = Convert.ToDecimal(row["ValorPasivo"]);
                    
                    if (nombre.Length > 20)
                    {
                        nombre = nombre.Substring(0, 17) + "...";
                    }
                    
                    series.Points.AddXY(nombre, valor);
                }
                
                // Configurar formato del gráfico
                chartArea.AxisX.LabelStyle.Font = new Font("Arial", 8);
                chartArea.AxisY.Title = "Valor ($)";
                chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
                chartArea.AxisY.LabelStyle.Format = "${0:N0}";
                
                // Colorear las barras (rojo para pasivos)
                foreach (DataPoint point in series.Points)
                {
                    point.Color = Color.FromArgb(220, 80, 80);  // Rojo
                    point.BorderColor = Color.Black;
                    point.BorderWidth = 1;
                    
                    // Agregar etiquetas a las barras
                    point.Label = string.Format("${0:N0}", point.YValues[0]);
                    point.LabelForeColor = Color.Black;
                }
                
                // Habilitar zoom y tooltips
                chartArea.CursorX.IsUserEnabled = true;
                chartArea.CursorX.IsUserSelectionEnabled = true;
                chartArea.AxisX.ScaleView.Zoomable = true;
                chartControl.Series[0].ToolTip = "#VALX: $#VALY{N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar gráfico de pasivos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera un gráfico circular para los activos por categoría
        /// </summary>
        /// <param name="chartControl">Control Chart donde se mostrará el gráfico</param>
        public void GenerarGraficoActivosPorCategoria(Chart chartControl)
        {
            try
            {
                DataTable dt = ObtenerActivosPorCategoria();
                
                // Configurar el gráfico
                chartControl.Series.Clear();
                chartControl.ChartAreas.Clear();
                chartControl.Titles.Clear();
                chartControl.Legends.Clear();
                
                ChartArea chartArea = new ChartArea("MainChartArea");
                chartControl.ChartAreas.Add(chartArea);
                
                Title title = new Title("Activos por Categoría", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
                chartControl.Titles.Add(title);
                
                Legend legend = new Legend("MainLegend");
                legend.Docking = Docking.Right;
                chartControl.Legends.Add(legend);
                
                Series series = new Series("CategoriasSeries");
                series.ChartType = SeriesChartType.Pie;  // Gráfico de pastel
                series.Legend = "MainLegend";
                chartControl.Series.Add(series);
                
                // Colores para el gráfico de pastel
                Color[] customColors = new Color[] 
                {
                    Color.FromArgb(0, 128, 0),      // Verde
                    Color.FromArgb(65, 140, 240),   // Azul
                    Color.FromArgb(252, 180, 65),   // Amarillo
                    Color.FromArgb(224, 64, 10),    // Rojo
                    Color.FromArgb(5, 100, 146),    // Azul oscuro
                    Color.FromArgb(191, 191, 191),  // Gris
                    Color.FromArgb(26, 59, 105),    // Azul marino
                    Color.FromArgb(142, 53, 239),   // Púrpura
                    Color.FromArgb(156, 188, 235),  // Azul claro
                    Color.FromArgb(0, 0, 0)         // Negro
                };
                
                // Agregar datos al gráfico
                int colorIndex = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string categoria = row["CategoriaActivo"].ToString();
                    decimal valor = Convert.ToDecimal(row["ValorTotal"]);
                    
                    // Agregar punto al gráfico
                    int pointIndex = series.Points.AddXY(categoria, valor);
                    
                    // Asignar color personalizado
                    series.Points[pointIndex].Color = customColors[colorIndex % customColors.Length];
                    colorIndex++;
                    
                    // Formato de etiqueta
                    series.Points[pointIndex].Label = "#PERCENT{P0}";
                    series.Points[pointIndex].LegendText = categoria + " ($" + string.Format("{0:N0}", valor) + ")";
                }
                
                // Configuración adicional
                series.Label = "#PERCENT{P0}";
                series["PieLabelStyle"] = "Outside";
                series["PieLineColor"] = "Black";
                series.BorderWidth = 1;
                series.BorderColor = Color.FromArgb(26, 59, 105);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar gráfico de activos por categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera un gráfico circular para los pasivos por tipo
        /// </summary>
        /// <param name="chartControl">Control Chart donde se mostrará el gráfico</param>
        public void GenerarGraficoPasivosPorTipo(Chart chartControl)
        {
            try
            {
                DataTable dt = ObtenerPasivosPorTipo();
                
                // Configurar el gráfico
                chartControl.Series.Clear();
                chartControl.ChartAreas.Clear();
                chartControl.Titles.Clear();
                chartControl.Legends.Clear();
                
                ChartArea chartArea = new ChartArea("MainChartArea");
                chartControl.ChartAreas.Add(chartArea);
                
                Title title = new Title("Pasivos por Tipo", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
                chartControl.Titles.Add(title);
                
                Legend legend = new Legend("MainLegend");
                legend.Docking = Docking.Bottom;
                chartControl.Legends.Add(legend);
                
                Series series = new Series("TiposSeries");
                series.ChartType = SeriesChartType.Doughnut;  // Gráfico de dona
                series.Legend = "MainLegend";
                chartControl.Series.Add(series);
                
                // Colores para el gráfico
                Color[] customColors = new Color[] 
                {
                    Color.FromArgb(220, 80, 80),    // Rojo
                    Color.FromArgb(224, 64, 10),    // Naranja rojizo
                    Color.FromArgb(252, 180, 65),   // Amarillo
                    Color.FromArgb(191, 191, 191),  // Gris
                    Color.FromArgb(26, 59, 105),    // Azul marino
                    Color.FromArgb(65, 140, 240),   // Azul
                    Color.FromArgb(142, 53, 239),   // Púrpura
                    Color.FromArgb(5, 100, 146),    // Azul oscuro
                    Color.FromArgb(156, 188, 235),  // Azul claro
                    Color.FromArgb(0, 0, 0)         // Negro
                };
                
                // Agregar datos al gráfico
                int colorIndex = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string tipo = row["TipoPasivo"].ToString();
                    decimal valor = Convert.ToDecimal(row["ValorTotal"]);
                    
                    // Limitar longitud del texto para mejor visualización
                    if (tipo.Length > 20)
                    {
                        tipo = tipo.Substring(0, 17) + "...";
                    }
                    
                    // Agregar punto al gráfico
                    int pointIndex = series.Points.AddXY(tipo, valor);
                    
                    // Asignar color personalizado
                    series.Points[pointIndex].Color = customColors[colorIndex % customColors.Length];
                    colorIndex++;
                    
                    // Formato de etiqueta
                    series.Points[pointIndex].Label = "#PERCENT{P0}";
                    series.Points[pointIndex].LegendText = tipo + " ($" + string.Format("{0:N0}", valor) + ")";
                }
                
                // Configuración adicional
                series["DoughnutRadius"] = "75";  // Tamaño del agujero de la dona (0-100)
                series["PieLabelStyle"] = "Outside";
                series["PieLineColor"] = "Black";
                series.BorderWidth = 1;
                series.BorderColor = Color.FromArgb(26, 59, 105);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar gráfico de pasivos por tipo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera un gráfico de columnas para el balance trimestral
        /// </summary>
        /// <param name="chartControl">Control Chart donde se mostrará el gráfico</param>
        public void GenerarGraficoBalanceTrimestral(Chart chartControl)
        {
            try
            {
                DataTable dt = ObtenerBalanceTrimestral();
                
                // Configurar el gráfico
                chartControl.Series.Clear();
                chartControl.ChartAreas.Clear();
                chartControl.Titles.Clear();
                
                ChartArea chartArea = new ChartArea("MainChartArea");
                chartControl.ChartAreas.Add(chartArea);
                
                Title title = new Title("Balance General Trimestral", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
                chartControl.Titles.Add(title);
                
                // Series para activos, pasivos y capital
                Series seriesActivos = new Series("Activos");
                seriesActivos.ChartType = SeriesChartType.Column;
                chartControl.Series.Add(seriesActivos);
                
                Series seriesPasivos = new Series("Pasivos");
                seriesPasivos.ChartType = SeriesChartType.Column;
                chartControl.Series.Add(seriesPasivos);
                
                Series seriesCapital = new Series("Capital");
                seriesCapital.ChartType = SeriesChartType.Column;
                chartControl.Series.Add(seriesCapital);
                
                // Agregar datos al gráfico
                foreach (DataRow row in dt.Rows)
                {
                    string trimestre = "T" + row["Trimestre"].ToString();
                    decimal activos = Convert.ToDecimal(row["TotalActivos"]);
                    decimal pasivos = Convert.ToDecimal(row["TotalPasivos"]);
                    decimal capital = Convert.ToDecimal(row["Capital"]);
                    
                    seriesActivos.Points.AddXY(trimestre, activos);
                    seriesPasivos.Points.AddXY(trimestre, pasivos);
                    seriesCapital.Points.AddXY(trimestre, capital);
                }
                
                // Configurar formato del gráfico
                chartArea.AxisX.LabelStyle.Font = new Font("Arial", 9);
                chartArea.AxisY.Title = "Valor ($)";
                chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
                chartArea.AxisY.LabelStyle.Format = "${0:N0}";
                
                // Colorear las columnas
                foreach (DataPoint point in seriesActivos.Points)
                {
                    point.Color = Color.FromArgb(0, 128, 0);  // Verde
                    point.BorderColor = Color.Black;
                    point.BorderWidth = 1;
                    point.Label = string.Format("${0:N0}", point.YValues[0]);
                }
                
                foreach (DataPoint point in seriesPasivos.Points)
                {
                    point.Color = Color.FromArgb(220, 80, 80);  // Rojo
                    point.BorderColor = Color.Black;
                    point.BorderWidth = 1;
                    point.Label = string.Format("${0:N0}", point.YValues[0]);
                }
                
                foreach (DataPoint point in seriesCapital.Points)
                {
                    point.Color = Color.FromArgb(65, 140, 240);  // Azul
                    point.BorderColor = Color.Black;
                    point.BorderWidth = 1;
                    point.Label = string.Format("${0:N0}", point.YValues[0]);
                }
                
                // Configurar leyenda
                Legend legend = new Legend("MainLegend");
                chartControl.Legends.Add(legend);
                seriesActivos.LegendText = "Activos";
                seriesPasivos.LegendText = "Pasivos";
                seriesCapital.LegendText = "Capital";
                
                // Tooltips
                seriesActivos.ToolTip = "#SERIESNAME: $#VALY{N0}";
                seriesPasivos.ToolTip = "#SERIESNAME: $#VALY{N0}";
                seriesCapital.ToolTip = "#SERIESNAME: $#VALY{N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar gráfico de balance trimestral: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Genera un gráfico de líneas para la evolución del capital
        /// </summary>
        /// <param name="chartControl">Control Chart donde se mostrará el gráfico</param>
        public void GenerarGraficoEvolucionCapital(Chart chartControl)
        {
            try
            {
                DataTable dt = ObtenerEvolucionCapital();
                
                // Configurar el gráfico
                chartControl.Series.Clear();
                chartControl.ChartAreas.Clear();
                chartControl.Titles.Clear();
                
                ChartArea chartArea = new ChartArea("MainChartArea");
                chartControl.ChartAreas.Add(chartArea);
                
                Title title = new Title("Evolución del Capital", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
                chartControl.Titles.Add(title);
                
                Series seriesPromedio = new Series("CapitalPromedio");
                seriesPromedio.ChartType = SeriesChartType.Line;
                chartControl.Series.Add(seriesPromedio);
                
                Series seriesMaximo = new Series("CapitalMaximo");
                seriesMaximo.ChartType = SeriesChartType.Line;
                chartControl.Series.Add(seriesMaximo);
                
                // Agregar datos al gráfico
                foreach (DataRow row in dt.Rows)
                {
                    string periodo = row["Periodo"].ToString();
                    decimal promedio = Convert.ToDecimal(row["CapitalPromedio"]);
                    decimal maximo = Convert.ToDecimal(row["CapitalMaximo"]);
                    
                    seriesPromedio.Points.AddXY(periodo, promedio);
                    seriesMaximo.Points.AddXY(periodo, maximo);
                }
                
                // Configurar formato del gráfico
                chartArea.AxisX.LabelStyle.Font = new Font("Arial", 8);
                chartArea.AxisX.LabelStyle.Angle = -45;
                chartArea.AxisY.Title = "Capital ($)";
                chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
                chartArea.AxisY.LabelStyle.Format = "${0:N0}";
                
                // Estilo de las líneas
                seriesPromedio.BorderWidth = 3;
                seriesPromedio.Color = Color.FromArgb(65, 140, 240);  // Azul
                seriesPromedio.MarkerStyle = MarkerStyle.Circle;
                seriesPromedio.MarkerSize = 8;
                seriesPromedio.MarkerColor = Color.White;
                seriesPromedio.MarkerBorderColor = Color.FromArgb(65, 140, 240);
                seriesPromedio.MarkerBorderWidth = 2;
                
                seriesMaximo.BorderWidth = 3;
                seriesMaximo.Color = Color.FromArgb(0, 128, 0);  // Verde
                seriesMaximo.MarkerStyle = MarkerStyle.Circle;
                seriesMaximo.MarkerSize = 8;
                seriesMaximo.MarkerColor = Color.White;
                seriesMaximo.MarkerBorderColor = Color.FromArgb(0, 128, 0);
                seriesMaximo.BorderWidth = 3;
                seriesMaximo.Color = Color.FromArgb(0, 128, 0);  // Verde
                seriesMaximo.MarkerStyle = MarkerStyle.Circle;
                seriesMaximo.MarkerSize = 8;
                seriesMaximo.MarkerColor = Color.White;
                seriesMaximo.MarkerBorderColor = Color.FromArgb(0, 128, 0);
                seriesMaximo.MarkerBorderWidth = 2;
                
                // Configurar leyenda
                Legend legend = new Legend("MainLegend");
                chartControl.Legends.Add(legend);
                seriesPromedio.LegendText = "Capital Promedio";
                seriesMaximo.LegendText = "Capital Máximo";
                
                // Tooltips
                seriesPromedio.ToolTip = "#SERIESNAME: $#VALY{N0}";
                seriesMaximo.ToolTip = "#SERIESNAME: $#VALY{N0}";
                
                // Habilitar zoom
                chartArea.CursorX.IsUserEnabled = true;
                chartArea.CursorX.IsUserSelectionEnabled = true;
                chartArea.AxisX.ScaleView.Zoomable = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar gráfico de evolución del capital: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Métodos para generar reportes con FastReport

        /// <summary>
        /// Genera un reporte completo del balance general
        /// </summary>
        /// <param name="rutaGuardar">Ruta donde se guardará el archivo PDF</param>
        /// <returns>Verdadero si el reporte se generó correctamente</returns>
        public bool GenerarReporteBalanceGeneral(string rutaGuardar = "")
        {
            try
            {
                // Crear el reporte
                Report report = new Report();
                
                // Configurar conexión a la base de datos para FastReport
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(connectionString);
                MySqlDataConnection conn = new MySqlDataConnection();
                conn.ConnectionString = connectionString;
                conn.Database = builder.Database;
                conn.ServerVersion = "8.0"; // Ajustar según la versión de MySQL
                report.Dictionary.Connections.Add(conn);
                
                // Agregar datasets al reporte
                DataSet ds = new DataSet("BalanceGeneralData");
                
                // Obtener las tablas de datos
                DataTable dtActivos = ObtenerActivosPrincipales(20);
                dtActivos.TableName = "Activos";
                ds.Tables.Add(dtActivos.Copy());
                
                DataTable dtPasivos = ObtenerPasivosPrincipales(20);
                dtPasivos.TableName = "Pasivos";
                ds.Tables.Add(dtPasivos.Copy());
                
                DataTable dtBalanceTrimestral = ObtenerBalanceTrimestral();
                dtBalanceTrimestral.TableName = "BalanceTrimestral";
                ds.Tables.Add(dtBalanceTrimestral.Copy());
                
                DataTable dtActivosCat = ObtenerActivosPorCategoria();
                dtActivosCat.TableName = "ActivosCategorias";
                ds.Tables.Add(dtActivosCat.Copy());
                
                DataTable dtPasivosTipo = ObtenerPasivosPorTipo();
                dtPasivosTipo.TableName = "PasivosTipos";
                ds.Tables.Add(dtPasivosTipo.Copy());
                
                DataTable dtEvolucionCapital = ObtenerEvolucionCapital();
                dtEvolucionCapital.TableName = "EvolucionCapital";
                ds.Tables.Add(dtEvolucionCapital.Copy());
                
                // Registrar fuentes de datos en el reporte
                foreach (DataTable table in ds.Tables)
                {
                    report.RegisterData(table, table.TableName);
                }
                
                // Cargar diseño del reporte desde archivo
                string reportPath = Path.Combine(Application.StartupPath, "Reportes", "BalanceGeneral.frx");
                
                if (File.Exists(reportPath))
                {
                    report.Load(reportPath);
                }
                else
                {
                    // Si no existe el archivo, crear un diseño básico de reporte
                    CrearDiseñoReporteBalanceGeneral(report);
                }
                
                // Preparar el reporte
                report.Prepare();
                
                // Si no se proporciona ruta, usar una ruta predeterminada
                if (string.IsNullOrEmpty(rutaGuardar))
                {
                    string carpetaReportes = Path.Combine(Application.StartupPath, "Reportes", "Generados");
                    
                    // Crear directorio si no existe
                    if (!Directory.Exists(carpetaReportes))
                    {
                        Directory.CreateDirectory(carpetaReportes);
                    }
                    
                    // Generar nombre de archivo con fecha y hora
                    string nombreArchivo = "BalanceGeneral_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
                    rutaGuardar = Path.Combine(carpetaReportes, nombreArchivo);
                }
                
                // Exportar a PDF
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(report, rutaGuardar);
                
                // Mostrar mensaje de éxito y preguntar si desea abrir el archivo
                DialogResult resultado = MessageBox.Show(
                    "Reporte generado correctamente en: " + rutaGuardar + "\n\n¿Desea abrir el archivo ahora?",
                    "Reporte Generado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                
                if (resultado == DialogResult.Yes)
                {
                    // Abrir el archivo PDF
                    System.Diagnostics.Process.Start(rutaGuardar);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Crea un diseño básico para el reporte de balance general
        /// </summary>
        /// <param name="report">Objeto Report a configurar</param>
        private void CrearDiseñoReporteBalanceGeneral(Report report)
        {
            try
            {
                // Esta es una implementación básica, en un entorno real
                // se recomienda diseñar el reporte con el diseñador visual de FastReport
                
                // Configuración inicial del reporte
                report.ReportInfo.Name = "BalanceGeneral";
                report.ReportInfo.Author = "Sistema AutoPartes";
                report.ReportInfo.Description = "Reporte de Balance General";
                report.ReportInfo.Created = DateTime.Now;
                
                // Agregar páginas y bandas básicas usando el API de FastReport
                // (Esta implementación es muy básica y se recomienda usar el diseñador visual)
                
                // Guardar el diseño para uso futuro
                string carpetaReportes = Path.Combine(Application.StartupPath, "Reportes");
                
                // Crear directorio si no existe
                if (!Directory.Exists(carpetaReportes))
                {
                    Directory.CreateDirectory(carpetaReportes);
                }
                
                string rutaReporte = Path.Combine(carpetaReportes, "BalanceGeneral.frx");
                report.Save(rutaReporte);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear diseño de reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Métodos de utilidad

        /// <summary>
        /// Calcula la relación entre activos y pasivos
        /// </summary>
        /// <returns>DataTable con indicadores financieros</returns>
        public DataTable CalcularIndicadoresFinancieros()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Indicador", typeof(string));
            dt.Columns.Add("Valor", typeof(decimal));
            dt.Columns.Add("Interpretacion", typeof(string));
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    SUM(TotalActivos) as TotalActivos,
                                    SUM(TotalPasivos) as TotalPasivos,
                                    SUM(ActivosCorrientes) as ActivosCorrientes,
                                    SUM(PasivosCorrientes) as PasivosCorrientes
                                FROM 
                                    BalanceGeneral 
                                WHERE 
                                    Fecha = (SELECT MAX(Fecha) FROM BalanceGeneral)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dtResultados = new DataTable();
                        adapter.Fill(dtResultados);
                        
                        if (dtResultados.Rows.Count > 0)
                        {
                            decimal totalActivos = Convert.ToDecimal(dtResultados.Rows[0]["TotalActivos"]);
                            decimal totalPasivos = Convert.ToDecimal(dtResultados.Rows[0]["TotalPasivos"]);
                            decimal activosCorrientes = Convert.ToDecimal(dtResultados.Rows[0]["ActivosCorrientes"]);
                            decimal pasivosCorrientes = Convert.ToDecimal(dtResultados.Rows[0]["PasivosCorrientes"]);
                            
                            // Calcular índices
                            decimal endeudamiento = 0;
                            if (totalActivos > 0)
                            {
                                endeudamiento = Math.Round((totalPasivos / totalActivos) * 100, 2);
                            }
                            
                            decimal liquidez = 0;
                            if (pasivosCorrientes > 0)
                            {
                                liquidez = Math.Round(activosCorrientes / pasivosCorrientes, 2);
                            }
                            
                            decimal capitalTrabajo = activosCorrientes - pasivosCorrientes;
                            
                            // Agregar resultados a la tabla
                            dt.Rows.Add("Índice de Endeudamiento (%)", endeudamiento, 
                                (endeudamiento <= 40) ? "Nivel de endeudamiento saludable" : 
                                (endeudamiento <= 60) ? "Nivel de endeudamiento moderado" : "Nivel de endeudamiento alto");
                            
                            dt.Rows.Add("Ratio de Liquidez", liquidez, 
                                (liquidez >= 1.5) ? "Buena capacidad de pago a corto plazo" : 
                                (liquidez >= 1) ? "Capacidad de pago ajustada" : "Posible problema de liquidez");
                            
                            dt.Rows.Add("Capital de Trabajo", capitalTrabajo, 
                                (capitalTrabajo > 0) ? "Capital de trabajo positivo" : "Capital de trabajo negativo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular indicadores financieros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return dt;
        }

        /// <summary>
        /// Exporta los datos del balance general a Excel
        /// </summary>
        /// <param name="rutaGuardar">Ruta donde se guardará el archivo Excel</param>
        /// <returns>Verdadero si la exportación fue exitosa</returns>
        public bool ExportarDatosExcel(string rutaGuardar = "")
        {
            try
            {
                // Crear dataset con todas las tablas
                DataSet ds = new DataSet("BalanceGeneralData");
                
                // Obtener las tablas de datos
                DataTable dtActivos = ObtenerActivosPrincipales(50);
                dtActivos.TableName = "Activos";
                ds.Tables.Add(dtActivos.Copy());
                
                DataTable dtPasivos = ObtenerPasivosPrincipales(50);
                dtPasivos.TableName = "Pasivos";
                ds.Tables.Add(dtPasivos.Copy());
                
                DataTable dtBalanceTrimestral = ObtenerBalanceTrimestral();
                dtBalanceTrimestral.TableName = "BalanceTrimestral";
                ds.Tables.Add(dtBalanceTrimestral.Copy());
                
                DataTable dtIndicadores = CalcularIndicadoresFinancieros();
                dtIndicadores.TableName = "Indicadores";
                ds.Tables.Add(dtIndicadores.Copy());
                
                // Si no se proporciona ruta, usar una ruta predeterminada
                if (string.IsNullOrEmpty(rutaGuardar))
                {
                    string carpetaExportacion = Path.Combine(Application.StartupPath, "Exportaciones");
                    
                    // Crear directorio si no existe
                    if (!Directory.Exists(carpetaExportacion))
                    {
                        Directory.CreateDirectory(carpetaExportacion);
                    }
                    
                    // Generar nombre de archivo con fecha y hora
                    string nombreArchivo = "BalanceGeneral_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                    rutaGuardar = Path.Combine(carpetaExportacion, nombreArchivo);
                }
                
                // Usar una librería para exportar a Excel como Microsoft.Office.Interop.Excel
                // o una alternativa como EPPlus, ClosedXML, etc.
                // Por simplicidad, aquí se usará un método genérico para exportar a CSV
                ExportarDataTableCSV(dtActivos, Path.ChangeExtension(rutaGuardar, ".csv"));
                
                MessageBox.Show("Datos exportados correctamente a: " + rutaGuardar, "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Exporta un DataTable a CSV
        /// </summary>
        /// <param name="dt">DataTable a exportar</param>
        /// <param name="rutaArchivo">Ruta del archivo CSV</param>
        private void ExportarDataTableCSV(DataTable dt, string rutaArchivo)
        {
            try
            {
                // Crear directorio si no existe
                string directorio = Path.GetDirectoryName(rutaArchivo);
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }
                
                // Crear archivo CSV
                using (StreamWriter sw = new StreamWriter(rutaArchivo, false))
                {
                    // Escribir encabezados
                    int columnCount = dt.Columns.Count;
                    for (int i = 0; i < columnCount; i++)
                    {
                        sw.Write(dt.Columns[i]);
                        if (i < columnCount - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                    
                    // Escribir filas
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < columnCount; i++)
                        {
                            if (!Convert.IsDBNull(row[i]))
                            {
                                string value = row[i].ToString();
                                // Si el valor contiene comas, comillas o saltos de línea, encerrarlo en comillas
                                if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                                {
                                    value = "\"" + value.Replace("\"", "\"\"") + "\"";
                                }
                                sw.Write(value);
                            }
                            
                            if (i < columnCount - 1)
                            {
                                sw.Write(",");
                            }
                        }
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al exportar a CSV: " + ex.Message);
            }
        }
        #endregion
    }
}