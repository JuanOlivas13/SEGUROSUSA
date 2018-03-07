namespace SEGUROSUSA
{
    partial class CardUsuarios
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this._btnEditar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this._lblCuenta = new System.Windows.Forms.Label();
            this._lblContrasena = new System.Windows.Forms.Label();
            this._lblNombre = new System.Windows.Forms.Label();
            this._txtNombre = new System.Windows.Forms.TextBox();
            this._txtCuenta = new System.Windows.Forms.TextBox();
            this._txtContrasena = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._txtTipoUsuario = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._txtTipoUsuario);
            this.panel1.Controls.Add(this._btnEditar);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this._lblCuenta);
            this.panel1.Controls.Add(this._lblContrasena);
            this.panel1.Controls.Add(this._lblNombre);
            this.panel1.Controls.Add(this._txtNombre);
            this.panel1.Controls.Add(this._txtCuenta);
            this.panel1.Controls.Add(this._txtContrasena);
            this.panel1.Location = new System.Drawing.Point(35, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 172);
            this.panel1.TabIndex = 22;
            // 
            // _btnEditar
            // 
            this._btnEditar.Location = new System.Drawing.Point(206, 135);
            this._btnEditar.Name = "_btnEditar";
            this._btnEditar.Size = new System.Drawing.Size(87, 23);
            this._btnEditar.TabIndex = 21;
            this._btnEditar.Text = "Editar";
            this._btnEditar.UseVisualStyleBackColor = true;
            this._btnEditar.Click += new System.EventHandler(this._btnEditar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Datos del Usuario";
            // 
            // _lblCuenta
            // 
            this._lblCuenta.AutoSize = true;
            this._lblCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblCuenta.Location = new System.Drawing.Point(14, 55);
            this._lblCuenta.Name = "_lblCuenta";
            this._lblCuenta.Size = new System.Drawing.Size(51, 13);
            this._lblCuenta.TabIndex = 1;
            this._lblCuenta.Text = "Cuenta:";
            // 
            // _lblContrasena
            // 
            this._lblContrasena.AutoSize = true;
            this._lblContrasena.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblContrasena.Location = new System.Drawing.Point(14, 79);
            this._lblContrasena.Name = "_lblContrasena";
            this._lblContrasena.Size = new System.Drawing.Size(75, 13);
            this._lblContrasena.TabIndex = 2;
            this._lblContrasena.Text = "Contraseña:";
            // 
            // _lblNombre
            // 
            this._lblNombre.AutoSize = true;
            this._lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblNombre.Location = new System.Drawing.Point(14, 31);
            this._lblNombre.Name = "_lblNombre";
            this._lblNombre.Size = new System.Drawing.Size(54, 13);
            this._lblNombre.TabIndex = 0;
            this._lblNombre.Text = "Nombre:";
            // 
            // _txtNombre
            // 
            this._txtNombre.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this._txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtNombre.Location = new System.Drawing.Point(129, 31);
            this._txtNombre.Name = "_txtNombre";
            this._txtNombre.ReadOnly = true;
            this._txtNombre.Size = new System.Drawing.Size(184, 13);
            this._txtNombre.TabIndex = 1;
            this._txtNombre.TabStop = false;
            this._txtNombre.Text = ".";
            // 
            // _txtCuenta
            // 
            this._txtCuenta.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this._txtCuenta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtCuenta.Location = new System.Drawing.Point(129, 55);
            this._txtCuenta.Name = "_txtCuenta";
            this._txtCuenta.ReadOnly = true;
            this._txtCuenta.Size = new System.Drawing.Size(184, 13);
            this._txtCuenta.TabIndex = 2;
            this._txtCuenta.TabStop = false;
            this._txtCuenta.Text = ".";
            // 
            // _txtContrasena
            // 
            this._txtContrasena.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this._txtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtContrasena.Location = new System.Drawing.Point(129, 79);
            this._txtContrasena.Name = "_txtContrasena";
            this._txtContrasena.ReadOnly = true;
            this._txtContrasena.Size = new System.Drawing.Size(184, 13);
            this._txtContrasena.TabIndex = 3;
            this._txtContrasena.TabStop = false;
            this._txtContrasena.Text = ".";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tipo de Usuario";
            // 
            // _txtTipoUsuario
            // 
            this._txtTipoUsuario.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this._txtTipoUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._txtTipoUsuario.Location = new System.Drawing.Point(129, 107);
            this._txtTipoUsuario.Name = "_txtTipoUsuario";
            this._txtTipoUsuario.ReadOnly = true;
            this._txtTipoUsuario.Size = new System.Drawing.Size(184, 13);
            this._txtTipoUsuario.TabIndex = 24;
            this._txtTipoUsuario.TabStop = false;
            this._txtTipoUsuario.Text = ".";
            // 
            // CardUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CardUsuarios";
            this.Size = new System.Drawing.Size(383, 192);
            this.Load += new System.EventHandler(this.CardUsuarios_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _btnEditar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _lblCuenta;
        private System.Windows.Forms.Label _lblContrasena;
        private System.Windows.Forms.Label _lblNombre;
        private System.Windows.Forms.TextBox _txtNombre;
        private System.Windows.Forms.TextBox _txtCuenta;
        private System.Windows.Forms.TextBox _txtContrasena;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _txtTipoUsuario;
    }
}
