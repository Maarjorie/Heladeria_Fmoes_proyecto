using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Heladeria_FMO.Modelos;
using Heladeria_FMO.Servicio;
using Heladeria_FMO.Utileria;

namespace Heladeria_FMO.Intefaz.Autorizaciones
{
    // Centro de autorizaciones con secciones: Pedidos mayoristas (aprobar/rechazar),
    // Arqueos de caja (autorizar) y Notificaciones del sistema (marcar leídas).
    public partial class ucAutorizaciones : UserControl
    {
        private enum Seccion { Pedidos, Arqueos, Promociones, Notificaciones }

        private Seccion _seccion = Seccion.Pedidos;
        private string _colId;

        private Guna2Button btnUsuarios;
        private Guna2Button btnSecPedidos, btnSecArqueos, btnSecPromos, btnSecNotif;
        private Guna2Button btnAprobar, btnRechazar, btnAutorizar;

        public ucAutorizaciones()
        {
            InitializeComponent();
            AplicarTema();
            ConstruirControles();
            CargarSeccion(Seccion.Pedidos);
        }

        private void AplicarTema()
        {
            BackColor = EstilosFmo.Fondo;
            pnlHeader.FillColor = EstilosFmo.Fondo;
            pnlHeader.Height = 116;
            EstilosFmo.Tarjeta(pnlCard);
            EstilosFmo.Tabla(dgvNotificaciones);

            lblTitulo.Font = EstilosFmo.Fuente(18F, FontStyle.Bold);
            lblTitulo.ForeColor = EstilosFmo.TextoFuerte;
            lblSub.Font = EstilosFmo.Fuente(9.5F);
            lblSub.ForeColor = EstilosFmo.TextoTenue;

            EstilosFmo.BotonContorno(btnRefrescar);
            btnRefrescar.Location = new Point(600, 74);
            btnRefrescar.Size = new Size(100, 36);
            btnRefrescar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private void ConstruirControles()
        {
            // Botones de sección (izquierda).
            btnSecPedidos = CrearSeccionBtn("Pedidos", 4, 74, 140);
            btnSecArqueos = CrearSeccionBtn("Arqueos", 150, 74, 140);
            btnSecPromos = CrearSeccionBtn("Promociones", 296, 74, 150);
            btnSecNotif = CrearSeccionBtn("Notificaciones", 452, 74, 160);
            btnSecPedidos.Click += (s, e) => CargarSeccion(Seccion.Pedidos);
            btnSecArqueos.Click += (s, e) => CargarSeccion(Seccion.Arqueos);
            btnSecPromos.Click += (s, e) => CargarSeccion(Seccion.Promociones);
            btnSecNotif.Click += (s, e) => CargarSeccion(Seccion.Notificaciones);

            // Acciones (derecha, ancladas).
            btnAprobar = CrearAccionBtn("Aprobar", 710, 74, 120, EstilosFmo.Menta);
            btnAprobar.Click += btnAprobar_Click;
            btnRechazar = CrearAccionBtn("Rechazar", 840, 74, 120, EstilosFmo.Cereza);
            btnRechazar.Click += btnRechazar_Click;
            btnAutorizar = CrearAccionBtn("Autorizar", 840, 74, 120, EstilosFmo.Menta);
            btnAutorizar.Click += btnAutorizar_Click;

            // Reubico el botón heredado "Marcar leída".
            EstilosFmo.BotonSecundario(btnMarcarLeida);
            btnMarcarLeida.Text = "Marcar leída";
            btnMarcarLeida.Location = new Point(820, 74);
            btnMarcarLeida.Size = new Size(160, 36);
            btnMarcarLeida.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Gestión de usuarios (arriba a la derecha).
            btnUsuarios = new Guna2Button
            {
                Text = "Usuarios",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(950, 10),
                Size = new Size(120, 36)
            };
            EstilosFmo.BotonContorno(btnUsuarios);
            btnUsuarios.Click += (s, e) => { using var d = new FrmUsuarios(); d.ShowDialog(this.FindForm()); };
            pnlHeader.Controls.Add(btnUsuarios);
        }

        private Guna2Button CrearSeccionBtn(string texto, int x, int y, int w)
        {
            var b = new Guna2Button { Text = texto, Location = new Point(x, y), Size = new Size(w, 36) };
            EstilosFmo.BotonContorno(b);
            pnlHeader.Controls.Add(b);
            return b;
        }

        private Guna2Button CrearAccionBtn(string texto, int x, int y, int w, Color color)
        {
            var b = new Guna2Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(w, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            EstilosFmo.BotonContorno(b);
            b.ForeColor = color;
            pnlHeader.Controls.Add(b);
            return b;
        }

        // ───────────────────────── Secciones ─────────────────────────
        private void CargarSeccion(Seccion seccion)
        {
            _seccion = seccion;

            // Resalta la sección activa.
            foreach (var (b, sec) in new[] { (btnSecPedidos, Seccion.Pedidos), (btnSecArqueos, Seccion.Arqueos), (btnSecPromos, Seccion.Promociones), (btnSecNotif, Seccion.Notificaciones) })
            {
                bool on = sec == seccion;
                if (on) EstilosFmo.BotonPrimario(b); else EstilosFmo.BotonContorno(b);
            }

            // Botones de acción según la sección.
            bool aprobables = seccion == Seccion.Pedidos || seccion == Seccion.Promociones;
            btnAprobar.Visible = aprobables;
            btnRechazar.Visible = aprobables;
            btnAutorizar.Visible = seccion == Seccion.Arqueos;
            btnMarcarLeida.Visible = seccion == Seccion.Notificaciones;

            switch (seccion)
            {
                case Seccion.Pedidos: CargarPedidos(); break;
                case Seccion.Arqueos: CargarArqueos(); break;
                case Seccion.Promociones: CargarPromos(); break;
                case Seccion.Notificaciones: CargarNotificaciones(); break;
            }
        }

        private void CargarPedidos()
        {
            try
            {
                DataTable t = PedidoMayoristaServicio.ListarPedidos();
                t.DefaultView.RowFilter = "estado = 'pendiente'";
                dgvNotificaciones.DataSource = t.DefaultView;
                _colId = EstilosFmo.ColumnaPorCandidatos(t, "id_pedido", "idpedido", "id");

                EstilosFmo.MostrarSoloColumnas(dgvNotificaciones,
                    ("nombre_comercial", "Cliente"),
                    ("fecha_pedido", "Fecha"),
                    ("codigo_retiro", "Código retiro"),
                    ("estado", "Estado"));

                lblTitulo.Text = "Autorizaciones · Pedidos";
                lblSub.Text = $"{t.DefaultView.Count} pedido(s) pendiente(s)";
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Autorizaciones"); }
        }

        private void CargarArqueos()
        {
            try
            {
                DataTable t = CajaServicio.ListarArqueosPendientes();
                dgvNotificaciones.DataSource = t;
                _colId = EstilosFmo.ColumnaPorCandidatos(t, "id_arqueo", "idarqueo", "id");

                EstilosFmo.MostrarSoloColumnas(dgvNotificaciones,
                    ("id_caja", "Caja"),
                    ("realizado_por", "Cajero"),
                    ("monto_esperado", "Esperado"),
                    ("monto_contado", "Contado"),
                    ("diferencia", "Diferencia"),
                    ("fecha_registro", "Fecha"));

                lblTitulo.Text = "Autorizaciones · Arqueos";
                lblSub.Text = $"{t.Rows.Count} arqueo(s) por autorizar";
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Autorizaciones"); }
        }

        private void CargarPromos()
        {
            try
            {
                DataTable t = PromocionServicio.ListarPendientes();
                dgvNotificaciones.DataSource = t;
                _colId = EstilosFmo.ColumnaPorCandidatos(t, "id_promocion", "idpromocion", "id");

                EstilosFmo.MostrarSoloColumnas(dgvNotificaciones,
                    ("nombre", "Promoción"),
                    ("objetivo", "Aplica a"),
                    ("tipo_descuento", "Tipo"),
                    ("valor_descuento", "Valor"),
                    ("fecha_inicio", "Desde"),
                    ("fecha_fin", "Hasta"));

                lblTitulo.Text = "Autorizaciones · Promociones";
                lblSub.Text = $"{t.Rows.Count} promoción(es) pendiente(s)";
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Autorizaciones"); }
        }

        private void CargarNotificaciones()
        {
            try
            {
                List<Notificacion> noLeidas = NotificacionServicio.ListarNoLeidas();

                var tabla = new DataTable();
                tabla.Columns.Add("Id", typeof(int));
                tabla.Columns.Add("Tipo");
                tabla.Columns.Add("Mensaje");
                tabla.Columns.Add("Registrado");
                tabla.Columns.Add("Enviado");

                foreach (var n in noLeidas)
                    tabla.Rows.Add(n.IdNotificacion, TipoLegible(n.Tipo), n.Mensaje,
                        n.FechaRegistro.ToString("yyyy-MM-dd HH:mm"), n.Enviado ? "Sí" : "No");

                dgvNotificaciones.DataSource = tabla;
                _colId = "Id";

                EstilosFmo.MostrarSoloColumnas(dgvNotificaciones,
                    ("Tipo", "Tipo"),
                    ("Mensaje", "Mensaje"),
                    ("Registrado", "Registrado"),
                    ("Enviado", "Enviado"));

                lblTitulo.Text = "Autorizaciones · Notificaciones";
                lblSub.Text = noLeidas.Count == 0 ? "No hay notificaciones sin leer" : $"{noLeidas.Count} notificación(es) sin leer";
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Autorizaciones"); }
        }

        private static string TipoLegible(string tipo) => tipo switch
        {
            "arqueo_inconsistente" => "Arqueo con diferencia",
            "bajo_stock" => "Stock bajo",
            "prox_vencer" => "Producto por vencer",
            _ => string.IsNullOrEmpty(tipo) ? "Notificación" : tipo.Replace("_", " ")
        };

        private int ObtenerId()
        {
            if (dgvNotificaciones.CurrentRow == null || _colId == null || !dgvNotificaciones.Columns.Contains(_colId))
                return 0;
            object v = dgvNotificaciones.CurrentRow.Cells[_colId].Value;
            return v != null && int.TryParse(v.ToString(), out int id) ? id : 0;
        }

        private int IdUsuario => Sesion.UsuarioActivo?.id_Usuario ?? 0;

        // ───────────────────────── Acciones ─────────────────────────
        private void btnAprobar_Click(object sender, EventArgs e)
        {
            int id = ObtenerId();
            if (id <= 0) { MensajeFmo.Info("Selecciona un registro.", "Autorizaciones"); return; }

            try
            {
                if (_seccion == Seccion.Promociones)
                {
                    if (!MensajeFmo.Confirmar("¿Aprobar la promoción seleccionada?", "Autorizaciones")) return;
                    bool ok = PromocionServicio.AprobarPromocion(id, IdUsuario);
                    if (ok) MensajeFmo.Exito("Promoción aprobada.", "Autorizaciones"); else MensajeFmo.Advertencia("No se pudo aprobar.", "Autorizaciones");
                    CargarPromos();
                }
                else
                {
                    if (!MensajeFmo.Confirmar("¿Aprobar (confirmar) el pedido seleccionado?", "Autorizaciones")) return;
                    bool ok = PedidoMayoristaServicio.ConfirmarPedido(new Pedido_mayorista { IdPedido = id });
                    if (ok) MensajeFmo.Exito("Pedido aprobado.", "Autorizaciones"); else MensajeFmo.Advertencia("No se pudo aprobar.", "Autorizaciones");
                    CargarPedidos();
                }
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Autorizaciones"); }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            int id = ObtenerId();
            if (id <= 0) { MensajeFmo.Info("Selecciona un registro.", "Autorizaciones"); return; }

            try
            {
                if (_seccion == Seccion.Promociones)
                {
                    if (!MensajeFmo.Confirmar("¿Rechazar la promoción seleccionada?", "Autorizaciones")) return;
                    bool ok = PromocionServicio.RechazarPromocion(id, IdUsuario);
                    if (ok) MensajeFmo.Exito("Promoción rechazada.", "Autorizaciones"); else MensajeFmo.Advertencia("No se pudo rechazar.", "Autorizaciones");
                    CargarPromos();
                }
                else
                {
                    if (!MensajeFmo.Confirmar("¿Rechazar (cancelar) el pedido seleccionado?", "Autorizaciones")) return;
                    bool ok = PedidoMayoristaServicio.CancelarPedido(id);
                    if (ok) MensajeFmo.Exito("Pedido rechazado.", "Autorizaciones"); else MensajeFmo.Advertencia("No se pudo rechazar.", "Autorizaciones");
                    CargarPedidos();
                }
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Autorizaciones"); }
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            int id = ObtenerId();
            if (id <= 0) { MensajeFmo.Info("Selecciona un arqueo.", "Autorizaciones"); return; }
            if (!MensajeFmo.Confirmar("¿Autorizar el arqueo seleccionado?", "Autorizaciones")) return;

            try
            {
                bool ok = CajaServicio.AutorizarArqueo(id, IdUsuario);
                if (ok) MensajeFmo.Exito("Arqueo autorizado.", "Autorizaciones"); else MensajeFmo.Advertencia("No se pudo autorizar.", "Autorizaciones");
                CargarArqueos();
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Autorizaciones"); }
        }

        private void btnMarcarLeida_Click(object sender, EventArgs e)
        {
            int id = ObtenerId();
            if (id <= 0) { MensajeFmo.Info("Selecciona una notificación.", "Autorizaciones"); return; }

            try
            {
                bool ok = NotificacionServicio.MarcarLeida(id);
                if (!ok) MensajeFmo.Advertencia("No se pudo marcar la notificación.", "Autorizaciones");
                CargarNotificaciones();
            }
            catch (Exception ex) { MensajeFmo.Error(ex.Message, "Autorizaciones"); }
        }

        private void btnRefrescar_Click(object sender, EventArgs e) => CargarSeccion(_seccion);
    }
}
