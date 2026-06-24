using System.Drawing;
using Guna.UI2.WinForms;

namespace Heladeria_FMO.Utileria
{
    // Paleta y estilos del nuevo diseño "Heladería FMO" en tono oscuro.
    // Toma los acentos del sistema de diseño (fresa, menta, mango, arándano)
    // y los coloca sobre superficies oscuras y cálidas para no cansar la vista.
    // Reutilizable por todos los UserControl de la interfaz nueva.
    public static class EstilosFmo
    {
        // ---- Superficies / fondos (cacao oscuro) -------------------------------
        public static readonly Color Fondo = Color.FromArgb(30, 27, 26);     // app background
        public static readonly Color Superficie = Color.FromArgb(46, 36, 34);    // tarjetas / paneles
        public static readonly Color SuperficieHundida = Color.FromArgb(38, 30, 28);    // wells, filas zebra
        public static readonly Color Borde = Color.FromArgb(78, 64, 59);     // bordes / divisores

        // ---- Texto -------------------------------------------------------------
        public static readonly Color TextoFuerte = Color.FromArgb(252, 248, 244);
        public static readonly Color TextoCuerpo = Color.FromArgb(236, 226, 218);
        public static readonly Color TextoTenue = Color.FromArgb(183, 166, 155);

        // ---- Acentos de marca --------------------------------------------------
        public static readonly Color Fresa = Color.FromArgb(232, 69, 107);   // primario
        public static readonly Color FresaHover = Color.FromArgb(206, 47, 87);
        public static readonly Color Menta = Color.FromArgb(22, 168, 138);   // secundario / éxito
        public static readonly Color MentaClaro = Color.FromArgb(52, 196, 164);
        public static readonly Color Mango = Color.FromArgb(245, 166, 35);   // advertencia / promo
        public static readonly Color Arandano = Color.FromArgb(79, 98, 216);   // info / enlaces
        public static readonly Color Cereza = Color.FromArgb(229, 72, 77);    // peligro

        // ---- Tipografías --------------------------------------------------------
        public static Font Fuente(float tam, FontStyle estilo = FontStyle.Regular)
            => new Font("Segoe UI", tam, estilo);

        // Botón primario relleno (acción principal: cobrar, guardar, etc.)
        public static void BotonPrimario(Guna2Button boton)
        {
            boton.FillColor = Fresa;
            boton.ForeColor = Color.White;
            boton.Font = Fuente(11F, FontStyle.Bold);
            boton.BorderRadius = 10;
            boton.Cursor = System.Windows.Forms.Cursors.Hand;
            boton.HoverState.FillColor = FresaHover;
        }

        // Botón secundario (acción positiva alterna: confirmar/cobrar en verde menta)
        public static void BotonSecundario(Guna2Button boton)
        {
            boton.FillColor = Menta;
            boton.ForeColor = Color.White;
            boton.Font = Fuente(11F, FontStyle.Bold);
            boton.BorderRadius = 10;
            boton.Cursor = System.Windows.Forms.Cursors.Hand;
            boton.HoverState.FillColor = MentaClaro;
        }

        // Botón "fantasma" con borde (cancelar / acciones suaves)
        public static void BotonContorno(Guna2Button boton)
        {
            boton.FillColor = Superficie;
            boton.ForeColor = TextoCuerpo;
            boton.BorderColor = Borde;
            boton.BorderThickness = 1;
            boton.Font = Fuente(10F, FontStyle.Bold);
            boton.BorderRadius = 10;
            boton.Cursor = System.Windows.Forms.Cursors.Hand;
            boton.HoverState.FillColor = SuperficieHundida;
        }

        // Caja de texto sobre fondo oscuro
        public static void CajaTexto(Guna2TextBox txt)
        {
            txt.FillColor = SuperficieHundida;
            txt.ForeColor = TextoFuerte;
            txt.BorderColor = Borde;
            txt.BorderRadius = 10;
            txt.Font = Fuente(10F);
            txt.PlaceholderForeColor = TextoTenue;
            txt.FocusedState.BorderColor = Fresa;
            txt.HoverState.BorderColor = Fresa;
        }

        // Panel tipo "tarjeta" oscura con borde sutil
        public static void Tarjeta(Guna2Panel panel)
        {
            panel.FillColor = Superficie;
            panel.BorderColor = Borde;
            panel.BorderThickness = 1;
            panel.BorderRadius = 14;
        }

        // ComboBox sobre fondo oscuro
        public static void Combo(Guna2ComboBox cmb)
        {
            cmb.FillColor = SuperficieHundida;
            cmb.ForeColor = TextoFuerte;
            cmb.BorderColor = Borde;
            cmb.BorderRadius = 8;
            cmb.Font = Fuente(10F);
            cmb.FocusedState.BorderColor = Fresa;
            cmb.ItemHeight = 28;
        }

        // Tabla (Guna2DataGridView) en tono oscuro
        public static void Tabla(Guna2DataGridView dgv)
        {
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgv.BackgroundColor = Superficie;
            dgv.GridColor = Borde;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersHeight = 38;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = SuperficieHundida;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextoTenue;
            dgv.ColumnHeadersDefaultCellStyle.Font = Fuente(9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = SuperficieHundida;

            dgv.DefaultCellStyle.BackColor = Superficie;
            dgv.DefaultCellStyle.ForeColor = TextoCuerpo;
            dgv.DefaultCellStyle.Font = Fuente(9.5F);
            dgv.DefaultCellStyle.SelectionBackColor = Fresa;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            dgv.ThemeStyle.RowsStyle.Height = 34;
            dgv.ThemeStyle.HeaderStyle.BackColor = SuperficieHundida;
            dgv.ThemeStyle.HeaderStyle.ForeColor = TextoTenue;
            dgv.ThemeStyle.RowsStyle.BackColor = Superficie;
            dgv.ThemeStyle.RowsStyle.ForeColor = TextoCuerpo;
            dgv.ThemeStyle.RowsStyle.SelectionBackColor = Fresa;
            dgv.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;
            dgv.ThemeStyle.AlternatingRowsStyle.BackColor = SuperficieHundida;
        }

        // Muestra solo las columnas indicadas (con encabezado legible y en ese orden)
        // y oculta el resto. Llamar después de asignar el DataSource.
        public static void MostrarSoloColumnas(Guna2DataGridView dgv, params (string columna, string encabezado)[] columnas)
        {
            var mapa = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase);
            foreach (var c in columnas) mapa[c.columna] = c.encabezado;

            foreach (System.Windows.Forms.DataGridViewColumn col in dgv.Columns)
            {
                if (mapa.TryGetValue(col.Name, out string header))
                {
                    col.Visible = true;
                    col.HeaderText = header;
                }
                else
                {
                    col.Visible = false;
                }
            }

            int orden = 0;
            foreach (var c in columnas)
                if (dgv.Columns.Contains(c.columna))
                    dgv.Columns[c.columna].DisplayIndex = orden++;
        }

        // Agrega (si no existe) una columna de botón de acción a la tabla.
        // El manejo del clic se hace con el evento CellContentClick del grid.
        public static void AgregarColumnaBoton(Guna2DataGridView dgv, string nombre, string texto, Color fondo)
        {
            if (dgv.Columns.Contains(nombre)) return;

            var col = new System.Windows.Forms.DataGridViewButtonColumn
            {
                Name = nombre,
                HeaderText = "",
                Text = texto,
                UseColumnTextForButtonValue = true,
                Width = 96,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Resizable = System.Windows.Forms.DataGridViewTriState.False
            };
            col.DefaultCellStyle.BackColor = fondo;
            col.DefaultCellStyle.ForeColor = Color.White;
            col.DefaultCellStyle.SelectionBackColor = fondo;
            col.DefaultCellStyle.SelectionForeColor = Color.White;
            col.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col);
        }

        // Busca el nombre real de una columna en un DataTable probando varios
        // candidatos (sin distinción de mayúsculas, por coincidencia parcial).
        // Útil porque los procedimientos devuelven columnas con nombres variables.
        public static string ColumnaPorCandidatos(System.Data.DataTable tabla, params string[] candidatos)
        {
            foreach (var candidato in candidatos)
                foreach (System.Data.DataColumn col in tabla.Columns)
                    if (col.ColumnName.Replace("_", "").Equals(candidato.Replace("_", ""), System.StringComparison.OrdinalIgnoreCase))
                        return col.ColumnName;

            foreach (var candidato in candidatos)
                foreach (System.Data.DataColumn col in tabla.Columns)
                    if (col.ColumnName.IndexOf(candidato, System.StringComparison.OrdinalIgnoreCase) >= 0)
                        return col.ColumnName;

            return null;
        }
    }
}
