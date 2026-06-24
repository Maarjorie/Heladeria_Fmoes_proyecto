using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Heladeria_FMO.Utileria
{
    public enum TipoMensaje { Info, Exito, Advertencia, Error, Pregunta }

    // Cuadro de diálogo oscuro y estilizado (GUNA) para reemplazar el MessageBox
    // estándar de Windows y conservar la estética del programa.
    // Uso:
    //   MensajeFmo.Info("Guardado.");
    //   MensajeFmo.Error("No se pudo conectar.");
    //   if (MensajeFmo.Confirmar("¿Eliminar el registro?")) { ... }
    public static class MensajeFmo
    {
        public static void Info(string mensaje, string titulo = "Información")
            => Mostrar(mensaje, titulo, TipoMensaje.Info, false);

        public static void Exito(string mensaje, string titulo = "Listo")
            => Mostrar(mensaje, titulo, TipoMensaje.Exito, false);

        public static void Advertencia(string mensaje, string titulo = "Atención")
            => Mostrar(mensaje, titulo, TipoMensaje.Advertencia, false);

        public static void Error(string mensaje, string titulo = "Error")
            => Mostrar(mensaje, titulo, TipoMensaje.Error, false);

        // Devuelve true si el usuario confirma (Sí).
        public static bool Confirmar(string mensaje, string titulo = "Confirmar")
            => Mostrar(mensaje, titulo, TipoMensaje.Pregunta, true) == DialogResult.Yes;

        private static DialogResult Mostrar(string mensaje, string titulo, TipoMensaje tipo, bool pregunta)
        {
            using var dialogo = Construir(mensaje, titulo, tipo, pregunta);
            Form owner = Form.ActiveForm;
            return owner != null && !owner.IsDisposed ? dialogo.ShowDialog(owner) : dialogo.ShowDialog();
        }

        private static Form Construir(string mensaje, string titulo, TipoMensaje tipo, bool pregunta)
        {
            Color acento = ColorAcento(tipo);
            string glifo = Glifo(tipo);

            const int ancho = 460;
            const int margen = 24;
            const int xTexto = 80;
            int anchoTexto = ancho - xTexto - margen;

            var form = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterParent,
                ShowInTaskbar = false,
                BackColor = EstilosFmo.Superficie,
                ClientSize = new Size(ancho, 200)
            };

            var marco = new Guna2Panel { Dock = DockStyle.Fill };
            EstilosFmo.Tarjeta(marco);
            form.Controls.Add(marco);

            // Franja de acento superior.
            var franja = new Guna2Panel
            {
                Location = new Point(0, 0),
                Size = new Size(ancho, 6),
                FillColor = acento,
                BorderRadius = 0
            };

            // Icono (glifo) en un círculo del color de acento.
            var icono = new Guna2HtmlLabel
            {
                Text = glifo,
                Location = new Point(margen, 28),
                Size = new Size(40, 40),
                Font = EstilosFmo.Fuente(20F, FontStyle.Bold),
                ForeColor = acento,
                BackColor = Color.Transparent,
                TextAlignment = ContentAlignment.MiddleCenter
            };

            var lblTitulo = new Guna2HtmlLabel
            {
                Text = titulo,
                Location = new Point(xTexto, 28),
                Size = new Size(anchoTexto, 26),
                Font = EstilosFmo.Fuente(14F, FontStyle.Bold),
                ForeColor = EstilosFmo.TextoFuerte,
                BackColor = Color.Transparent
            };

            var fuenteMsg = EstilosFmo.Fuente(10F);
            int altoMsg = TextRenderer.MeasureText(
                mensaje, fuenteMsg, new Size(anchoTexto, int.MaxValue), TextFormatFlags.WordBreak).Height;

            var lblMensaje = new Label
            {
                Text = mensaje,
                AutoSize = false,
                Location = new Point(xTexto, 60),
                Size = new Size(anchoTexto, altoMsg + 6),
                Font = fuenteMsg,
                ForeColor = EstilosFmo.TextoCuerpo,
                BackColor = Color.Transparent
            };

            int yBotones = lblMensaje.Bottom + 22;

            var botones = new Control[pregunta ? 2 : 1];
            if (pregunta)
            {
                var btnNo = new Guna2Button { Text = "No", Size = new Size(110, 40), DialogResult = DialogResult.No };
                EstilosFmo.BotonContorno(btnNo);
                btnNo.Location = new Point(ancho - margen - 110, yBotones);

                var btnSi = new Guna2Button { Text = "Sí", Size = new Size(110, 40), DialogResult = DialogResult.Yes };
                EstilosFmo.BotonPrimario(btnSi);
                btnSi.FillColor = acento;
                btnSi.Location = new Point(ancho - margen - 110 - 10 - 110, yBotones);

                botones[0] = btnNo;
                botones[1] = btnSi;
                form.AcceptButton = btnSi;
                form.CancelButton = btnNo;
            }
            else
            {
                var btnOk = new Guna2Button { Text = "Aceptar", Size = new Size(120, 40), DialogResult = DialogResult.OK };
                EstilosFmo.BotonPrimario(btnOk);
                btnOk.FillColor = acento;
                btnOk.Location = new Point(ancho - margen - 120, yBotones);
                botones[0] = btnOk;
                form.AcceptButton = btnOk;
            }

            form.ClientSize = new Size(ancho, yBotones + 40 + margen);

            marco.Controls.Add(franja);
            marco.Controls.Add(icono);
            marco.Controls.Add(lblTitulo);
            marco.Controls.Add(lblMensaje);
            marco.Controls.AddRange(botones);

            return form;
        }

        private static Color ColorAcento(TipoMensaje tipo) => tipo switch
        {
            TipoMensaje.Exito => EstilosFmo.Menta,
            TipoMensaje.Advertencia => EstilosFmo.Mango,
            TipoMensaje.Error => EstilosFmo.Cereza,
            TipoMensaje.Pregunta => EstilosFmo.Fresa,
            _ => EstilosFmo.Arandano
        };

        private static string Glifo(TipoMensaje tipo) => tipo switch
        {
            TipoMensaje.Exito => "✓",
            TipoMensaje.Advertencia => "!",
            TipoMensaje.Error => "✕",
            TipoMensaje.Pregunta => "?",
            _ => "i"
        };
    }
}
