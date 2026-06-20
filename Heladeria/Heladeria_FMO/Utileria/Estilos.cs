using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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

        public static void Titulo(Label label)
        {
            label.ForeColor = Dorado;
            label.BackColor = Color.Transparent;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }

        public static void Subtitulo(Label label)
        {
            label.ForeColor = Dorado;
            label.BackColor = Color.Transparent;
        }

        public static void PanelElegante(Panel panel)
        {
            panel.BackColor = CafeElegante;
        }

        public static void BotonDorado(Button boton)
        {
            boton.BackColor = Dorado;
            boton.ForeColor = ChocolateOscuro;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.Height = 40;
        }

        public static void BotonOscuro(Button boton)
        {
            boton.BackColor = ChocolateOscuro;
            boton.ForeColor = Dorado;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderColor = Dorado;
            boton.FlatAppearance.BorderSize = 1;
            boton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.Height = 40;
        }

        public static void CajaTexto(TextBox txt)
        {
            txt.BackColor = Crema;
            txt.ForeColor = ChocolateOscuro;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 10);
        }

        public static void Combo(ComboBox cmb)
        {
            cmb.BackColor = Crema;
            cmb.ForeColor = ChocolateOscuro;
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.Font = new Font("Segoe UI", 10);
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public static void Tabla(DataGridView dgv)
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

        public static void PanelMenu(Panel panel)
        {
            panel.BackColor = ChocolateOscuro;
            panel.Dock = DockStyle.Left;
            panel.Width = 230;
        }

        public static void PanelSuperior(Panel panel)
        {
            panel.BackColor = CafeElegante;
            panel.Dock = DockStyle.Top;
            panel.Height = 75;
        }

        public static void PanelContenedor(Panel panel)
        {
            panel.BackColor = Crema;
            panel.Dock = DockStyle.Fill;
        }

        public static void TituloMenu(Label label)
        {
            label.ForeColor = Dorado;
            label.BackColor = Color.Transparent;
            label.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }

        public static void BotonMenu(Button boton)
        {
            boton.Dock = DockStyle.Top;
            boton.Height = 50;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = ChocolateOscuro;
            boton.ForeColor = Dorado;
            boton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.TextAlign = ContentAlignment.MiddleLeft;
            boton.Padding = new Padding(25, 0, 0, 0);
        }

        public static void EfectoBotonMenu(Button boton)
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



