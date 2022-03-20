namespace Template.WinForm.App
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtControllerName = new System.Windows.Forms.TextBox();
            this.txtControllerMetodo = new System.Windows.Forms.TextBox();
            this.txtCommandName = new System.Windows.Forms.TextBox();
            this.comboBoxVerbo = new System.Windows.Forms.ComboBox();
            this.txtLogicName = new System.Windows.Forms.TextBox();
            this.txtEntityResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCommandPropiedades = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEntity = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLogicMetodo = new System.Windows.Forms.TextBox();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLogicParametros = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLogicResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtControllerName
            // 
            this.txtControllerName.Location = new System.Drawing.Point(161, 25);
            this.txtControllerName.Name = "txtControllerName";
            this.txtControllerName.Size = new System.Drawing.Size(195, 27);
            this.txtControllerName.TabIndex = 0;
            this.txtControllerName.TextChanged += new System.EventHandler(this.txtControllerName_TextChanged);
            // 
            // txtControllerMetodo
            // 
            this.txtControllerMetodo.Location = new System.Drawing.Point(517, 22);
            this.txtControllerMetodo.Name = "txtControllerMetodo";
            this.txtControllerMetodo.Size = new System.Drawing.Size(195, 27);
            this.txtControllerMetodo.TabIndex = 1;
            this.txtControllerMetodo.TextChanged += new System.EventHandler(this.txtControllerMetodo_TextChanged);
            // 
            // txtCommandName
            // 
            this.txtCommandName.Location = new System.Drawing.Point(161, 72);
            this.txtCommandName.Name = "txtCommandName";
            this.txtCommandName.Size = new System.Drawing.Size(195, 27);
            this.txtCommandName.TabIndex = 3;
            // 
            // comboBoxVerbo
            // 
            this.comboBoxVerbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVerbo.FormattingEnabled = true;
            this.comboBoxVerbo.Items.AddRange(new object[] {
            "GET",
            "POST"});
            this.comboBoxVerbo.Location = new System.Drawing.Point(787, 20);
            this.comboBoxVerbo.Name = "comboBoxVerbo";
            this.comboBoxVerbo.Size = new System.Drawing.Size(137, 28);
            this.comboBoxVerbo.TabIndex = 2;
            // 
            // txtLogicName
            // 
            this.txtLogicName.Location = new System.Drawing.Point(161, 119);
            this.txtLogicName.Name = "txtLogicName";
            this.txtLogicName.Size = new System.Drawing.Size(195, 27);
            this.txtLogicName.TabIndex = 5;
            // 
            // txtEntityResult
            // 
            this.txtEntityResult.Location = new System.Drawing.Point(161, 316);
            this.txtEntityResult.Name = "txtEntityResult";
            this.txtEntityResult.Size = new System.Drawing.Size(195, 27);
            this.txtEntityResult.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Controller:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(369, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Controller Metodo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(730, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Verbo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Command:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Logic:";
            // 
            // txtCommandPropiedades
            // 
            this.txtCommandPropiedades.Location = new System.Drawing.Point(517, 72);
            this.txtCommandPropiedades.Name = "txtCommandPropiedades";
            this.txtCommandPropiedades.PlaceholderText = "int Id, string Nombre, int Edad";
            this.txtCommandPropiedades.Size = new System.Drawing.Size(407, 27);
            this.txtCommandPropiedades.TabIndex = 4;
            this.txtCommandPropiedades.TextChanged += new System.EventHandler(this.txtCommandPropiedades_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(369, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Propiedades:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 323);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "EntityResult:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Modelo:";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(161, 274);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(195, 27);
            this.txtModelo.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 363);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Entity:";
            // 
            // txtEntity
            // 
            this.txtEntity.Location = new System.Drawing.Point(161, 360);
            this.txtEntity.Name = "txtEntity";
            this.txtEntity.Size = new System.Drawing.Size(195, 27);
            this.txtEntity.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 20);
            this.label10.TabIndex = 19;
            this.label10.Text = "Logic Metodo:";
            // 
            // txtLogicMetodo
            // 
            this.txtLogicMetodo.Location = new System.Drawing.Point(161, 168);
            this.txtLogicMetodo.Name = "txtLogicMetodo";
            this.txtLogicMetodo.Size = new System.Drawing.Size(195, 27);
            this.txtLogicMetodo.TabIndex = 6;
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(707, 360);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(94, 29);
            this.btnCrear.TabIndex = 12;
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Location = new System.Drawing.Point(830, 359);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(94, 29);
            this.btnBorrar.TabIndex = 13;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(369, 171);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 20);
            this.label11.TabIndex = 23;
            this.label11.Text = "Parametros:";
            // 
            // txtLogicParametros
            // 
            this.txtLogicParametros.Location = new System.Drawing.Point(517, 168);
            this.txtLogicParametros.Name = "txtLogicParametros";
            this.txtLogicParametros.PlaceholderText = "int id, string nombre, int edad";
            this.txtLogicParametros.Size = new System.Drawing.Size(407, 27);
            this.txtLogicParametros.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(369, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 20);
            this.label12.TabIndex = 25;
            this.label12.Text = "Resultado:";
            // 
            // txtLogicResult
            // 
            this.txtLogicResult.Location = new System.Drawing.Point(517, 221);
            this.txtLogicResult.Name = "txtLogicResult";
            this.txtLogicResult.Size = new System.Drawing.Size(407, 27);
            this.txtLogicResult.TabIndex = 8;
            this.txtLogicResult.Text = "int";
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnCrear;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 400);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtLogicResult);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtLogicParametros);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtLogicMetodo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEntity);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCommandPropiedades);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEntityResult);
            this.Controls.Add(this.txtLogicName);
            this.Controls.Add(this.comboBoxVerbo);
            this.Controls.Add(this.txtCommandName);
            this.Controls.Add(this.txtControllerMetodo);
            this.Controls.Add(this.txtControllerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generador";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtControllerName;
        private TextBox txtControllerMetodo;
        private TextBox txtCommandName;
        private ComboBox comboBoxVerbo;
        private TextBox txtLogicName;
        private TextBox txtEntityResult;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtCommandPropiedades;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtModelo;
        private Label label9;
        private TextBox txtEntity;
        private Label label10;
        private TextBox txtLogicMetodo;
        private Button btnCrear;
        private Button btnBorrar;
        private Label label11;
        private TextBox txtLogicParametros;
        private Label label12;
        private TextBox txtLogicResult;
    }
}