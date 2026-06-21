using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Heladeria_FMO.Utileria
{
    internal class Estilos
    {
        public static Color ChocolateOscuro = Color.FromArgb(32, 18, 12);
        public static Color CafeElegante = Color.FromArgb(55, 32, 22);
        public static Color Dorado = Color.FromArgb(212, 175, 55);
        public static Color DoradoClaro = Color.FromArgb(245, 215, 120);
        public static Color Crema = Color.FromArgb(245, 235, 215);

        public static void Formulario(Form form)
        {
            form.BackColor = ChocolateOscuro;
            form.ForeColor = Crema;
            form.Font = new Font("Segoe UI", 10);
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        public static void Titulo(Guna2HtmlLabel label)
        {
            label.ForeColor = Dorado;
            label.BackColor = Color.Transparent;
            label.TextAlignment = ContentAlignment.MiddleCenter;
        }

        public static void Subtitulo(Guna2HtmlLabel label)
        {
            label.ForeColor = Dorado;
            label.BackColor = Color.Transparent;
        }

        public static void PanelElegante(Guna2Panel panel)
        {
            panel.BackColor = CafeElegante;
        }

        public static void BotonDorado(Guna2Button boton)
        {
            boton.FillColor = Dorado;
            boton.ForeColor = ChocolateOscuro;
            boton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.Height = 40;
            boton.BorderRadius = 0;
=======
            boton.FillColor = Dorado;
            boton.ForeColor = ChocolateOscuro;
            boton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.Height = 40;
            boton.BorderRadius = 0;
>>>>>>> ab54ff87b97c77c76c8fb775facf67563c44696c
        }

        public static void BotonOscuro(Guna2Button boton)
        {
            boton.FillColor = ChocolateOscuro;
            boton.ForeColor = Dorado;
            boton.BorderColor = Dorado;
            boton.BorderThickness = 1;
            boton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.Height = 40;
=======
            boton.FillColor = ChocolateOscuro;
            boton.ForeColor = Dorado;
            boton.BorderColor = Dorado;
            boton.BorderThickness = 1;
            boton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.Height = 40;
>>>>>>> ab54ff87b97c77c76c8fb775facf67563c44696c
            boton.BorderRadius = 0;
        }

        public static void CajaTexto(Guna2TextBox txt)
        {
            txt.BackColor = Crema;
            txt.ForeColor = ChocolateOscuro;
            txt.BorderStyle = (System.Drawing.Drawing2D.DashStyle)BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 10);
        }

        public static void Combo(Guna2ComboBox cmb)
        {
            cmb.BackColor = Crema;
            cmb.ForeColor = ChocolateOscuro;
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Font = new Font("Segoe UI", 10);
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public static void Tabla(Guna2DataGridView dgv)
        {
            dgv.BackgroundColor = ChocolateOscuro;
            dgv.GridColor = Dorado;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = CafeElegante;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Dorado;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.BackColor = Crema;
            dgv.DefaultCellStyle.ForeColor = ChocolateOscuro;
            dgv.DefaultCellStyle.SelectionBackColor = Dorado;
            dgv.DefaultCellStyle.SelectionForeColor = ChocolateOscuro;
        }

        public static void PanelMenu(Guna2Panel panel)
        {
            panel.BackColor = ChocolateOscuro;
            panel.Dock = DockStyle.Left;
            panel.Width = 230;
        }

        public static void PanelSuperior(Guna2Panel panel)
        {
            panel.BackColor = CafeElegante;
            panel.Dock = DockStyle.Top;
            panel.Height = 75;
        }

        public static void PanelContenedor(Guna2Panel panel)
        {
            panel.BackColor = Crema;
            panel.Dock = DockStyle.Fill;
        }

        public static void TituloMenu(Guna2HtmlLabel label)
        {
            label.ForeColor = Dorado;
            label.BackColor = Color.Transparent;
            label.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            label.Dock = DockStyle.Fill;
            label.TextAlignment = ContentAlignment.MiddleCenter;
        }

        public static void BotonMenu(Guna2Button boton)
        {
<<<<<<< HEAD
            boton.Dock = DockStyle.Top;
            boton.Height = 50;
            boton.FillColor = ChocolateOscuro;
            boton.ForeColor = Dorado;
            boton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.TextAlign = HorizontalAlignment.Left;
            boton.TextOffset = new Point(25, 0); // Reemplaza de forma nativa al Padding izquierdo
            boton.BorderRadius = 0;

            boton.HoverState.FillColor = CafeElegante;
=======
            boton.Dock = DockStyle.Top;
            boton.Height = 50;
            boton.FillColor = ChocolateOscuro;
            boton.ForeColor = Dorado;
            boton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.TextAlign = HorizontalAlignment.Left;
            boton.TextOffset = new Point(25, 0); // Reemplaza de forma nativa al Padding izquierdo
            boton.BorderRadius = 0;

            boton.HoverState.FillColor = CafeElegante;
            boton.HoverState.ForeColor = DoradoClaro;
        }

        public static void EfectoBotonMenu(Guna2Button boton)
        {
            boton.MouseEnter += (s, e) =>
            {
                boton.BackColor = CafeElegante;
                boton.ForeColor = Color.FromArgb(245, 215, 120);
            };

            boton.MouseLeave += (s, e) =>
            {
                boton.BackColor = ChocolateOscuro;
                boton.ForeColor = Dorado;
            };
        }
    }
}



