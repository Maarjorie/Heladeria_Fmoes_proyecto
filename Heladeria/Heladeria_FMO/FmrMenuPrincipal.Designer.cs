namespace Heladeria_FMO
{
    partial class FmrMenuPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panelContenedor = new Panel();
            panelSuperior = new Panel();
            lblTitulo = new Label();
            panelMenu = new Panel();
            btnCerrarSesion = new Button();
            btnReportes = new Button();
            btnCaja = new Button();
            btnRutas = new Button();
            btnPedidos = new Button();
            btnClientes = new Button();
            btnVentas = new Button();
            btnProductos = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
            panelSuperior.SuspendLayout();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panelContenedor);
            panel1.Controls.Add(panelSuperior);
            panel1.Controls.Add(panelMenu);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1126, 614);
            panel1.TabIndex = 0;
            // 
            // panelContenedor
            // 
            panelContenedor.Location = new Point(279, 3);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(844, 611);
            panelContenedor.TabIndex = 2;
            // 
            // panelSuperior
            // 
            panelSuperior.Controls.Add(lblTitulo);
            panelSuperior.Location = new Point(3, 0);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(270, 72);
            panelSuperior.TabIndex = 1;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(92, 26);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(109, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Heladeria FMO";
            // 
            // panelMenu
            // 
            panelMenu.Controls.Add(btnCerrarSesion);
            panelMenu.Controls.Add(btnReportes);
            panelMenu.Controls.Add(btnCaja);
            panelMenu.Controls.Add(btnRutas);
            panelMenu.Controls.Add(btnPedidos);
            panelMenu.Controls.Add(btnClientes);
            panelMenu.Controls.Add(btnVentas);
            panelMenu.Controls.Add(btnProductos);
            panelMenu.Controls.Add(button1);
            panelMenu.Location = new Point(3, 76);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(270, 535);
            panelMenu.TabIndex = 0;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(17, 451);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(240, 46);
            btnCerrarSesion.TabIndex = 8;
            btnCerrarSesion.Text = "Cerrar sesión";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            // 
            // btnReportes
            // 
            btnReportes.Location = new Point(17, 399);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(240, 46);
            btnReportes.TabIndex = 7;
            btnReportes.Text = "Reportes";
            btnReportes.UseVisualStyleBackColor = true;
            // 
            // btnCaja
            // 
            btnCaja.Location = new Point(17, 347);
            btnCaja.Name = "btnCaja";
            btnCaja.Size = new Size(240, 46);
            btnCaja.TabIndex = 6;
            btnCaja.Text = "Caja";
            btnCaja.UseVisualStyleBackColor = true;
            // 
            // btnRutas
            // 
            btnRutas.Location = new Point(17, 295);
            btnRutas.Name = "btnRutas";
            btnRutas.Size = new Size(240, 46);
            btnRutas.TabIndex = 5;
            btnRutas.Text = "Rutas";
            btnRutas.UseVisualStyleBackColor = true;
            // 
            // btnPedidos
            // 
            btnPedidos.Location = new Point(17, 243);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.Size = new Size(240, 46);
            btnPedidos.TabIndex = 4;
            btnPedidos.Text = "Pedidos";
            btnPedidos.UseVisualStyleBackColor = true;
            // 
            // btnClientes
            // 
            btnClientes.Location = new Point(17, 191);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(240, 46);
            btnClientes.TabIndex = 3;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = true;
            // 
            // btnVentas
            // 
            btnVentas.Location = new Point(17, 139);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(240, 46);
            btnVentas.TabIndex = 2;
            btnVentas.Text = "Ventas";
            btnVentas.UseVisualStyleBackColor = true;
            // 
            // btnProductos
            // 
            btnProductos.Location = new Point(17, 87);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(240, 46);
            btnProductos.TabIndex = 1;
            btnProductos.Text = "Producto";
            btnProductos.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(17, 34);
            button1.Name = "button1";
            button1.Size = new Size(240, 47);
            button1.TabIndex = 0;
            button1.Text = "Inicio";
            button1.UseVisualStyleBackColor = true;
            // 
            // FmrMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1150, 638);
            Controls.Add(panel1);
            Name = "FmrMenuPrincipal";
            Text = "FmrMenuPrincipal";
            panel1.ResumeLayout(false);
            panelSuperior.ResumeLayout(false);
            panelSuperior.PerformLayout();
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panelContenedor;
        private Panel panelSuperior;
        private Label lblTitulo;
        private Panel panelMenu;
        private Button btnCerrarSesion;
        private Button btnReportes;
        private Button btnCaja;
        private Button btnRutas;
        private Button btnPedidos;
        private Button btnClientes;
        private Button btnVentas;
        private Button btnProductos;
        private Button button1;
    }
}