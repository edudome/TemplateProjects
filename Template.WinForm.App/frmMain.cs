namespace Template.WinForm.App
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            comboBoxVerbo.SelectedIndex = 0;
        }

        private void txtControllerName_TextChanged(object sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;

                if (text.Length == 1)
                {
                    textBox.Text = text.Substring(0, 1).ToUpper() + text.Substring(1);
                    textBox.SelectionStart = 1;
                    text = textBox.Text;
                }
                txtLogicName.Text = text + "Logic";
            }
        }

        private void txtControllerMetodo_TextChanged(object sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;

                if (text.Length == 1)
                {
                    textBox.Text = text.Substring(0, 1).ToUpper() + text.Substring(1);
                    textBox.SelectionStart = 1;
                    text = textBox.Text;
                }

                if (text != string.Empty)
                {
                    string modeloName = string.Empty;
                    string logicMetodo = string.Empty;
                    txtCommandName.Text = text;

                    if (text.StartsWith("AddOrUpdate"))
                    {
                        modeloName = text.Replace("AddOrUpdate", "");
                        logicMetodo = "AddOrUpdate";
                    }
                    else if (text.StartsWith("AddRange"))
                    {
                        modeloName = text.Replace("AddRange", "");
                        logicMetodo = "AddRange";
                    }
                    else if (text.StartsWith("Add"))
                    {
                        modeloName = text.Replace("Add", "");
                        logicMetodo = "Add";
                    }
                    else if (text.StartsWith("Update"))
                    {
                        modeloName = text.Replace("Update", "");
                        logicMetodo = "Update";
                    }
                    else if (text.StartsWith("Delete"))
                    {
                        modeloName = text.Replace("Delete", "");
                        logicMetodo = "Delete";

                        if (text.EndsWith("ById"))
                        {
                            modeloName = modeloName.Replace("ById", "");
                            logicMetodo += "ById";
                        }
                        else if (text.EndsWith("All"))
                        {
                            modeloName = modeloName.Replace("All", "");
                            logicMetodo += "All";
                        }
                        else if (text.EndsWith("List"))
                        {
                            modeloName = modeloName.Replace("List", "");
                            logicMetodo += "List";
                        }
                    }
                    else if (text.StartsWith("Remove"))
                    {
                        modeloName = text.Replace("Remove", "");
                        logicMetodo = "Remove";
                    }
                    else if (text.StartsWith("Agregar"))
                    {
                        modeloName = text.Replace("Agregar", "");
                        logicMetodo = "Agregar";
                    }
                    else if (text.StartsWith("Get"))
                    {
                        modeloName = text.Replace("Get", "");
                        logicMetodo = "Get";

                        if (text.EndsWith("ById"))
                        {
                            modeloName = modeloName.Replace("ById", "");
                            logicMetodo += "ById";
                        }
                        else if (text.EndsWith("All"))
                        {
                            modeloName = modeloName.Replace("All", "");
                            logicMetodo += "All";
                        }
                        else if (text.EndsWith("List"))
                        {
                            modeloName = modeloName.Replace("List", "");
                            logicMetodo += "List";
                        }
                    }

                    if (modeloName.EndsWith("s")) modeloName = modeloName.Remove(modeloName.Length - 1);
                    if (text.StartsWith("Get"))
                    {
                        txtLogicResult.Text = modeloName + "Model";
                    }
                    else
                    {
                        txtLogicResult.Text = "int";
                    }
                    txtLogicMetodo.Text = logicMetodo;
                    txtEntity.Text = modeloName;
                    txtModelo.Text = modeloName + "Model";
                    txtEntityResult.Text = modeloName + "EntityResult";
                }
                else
                {
                    txtCommandName.Text = string.Empty;
                    txtLogicMetodo.Text = string.Empty;
                    txtEntity.Text = string.Empty;
                    txtModelo.Text = string.Empty;
                    txtEntityResult.Text = string.Empty;
                    txtLogicResult.Text = "int";
                }
            }
        }

        private void txtCommandPropiedades_TextChanged(object sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;

                if (text != string.Empty)
                {
                    txtLogicParametros.Text = text;
                }
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            btnCrear.Enabled = false;
            GeneradorModel model = new GeneradorModel();
            model.ControllerName = txtControllerName.Text;
            model.ControllerMetodo = txtControllerMetodo.Text;
            model.ControllerVerbo = comboBoxVerbo.SelectedItem?.ToString() ?? string.Empty;
            model.CommandName = txtCommandName.Text;
            model.CommandPropiedades = txtCommandPropiedades.Text;
            model.LogicName = txtLogicName.Text;
            model.LogicMetodo = txtLogicMetodo.Text;
            model.LogicParametros = txtLogicParametros.Text;
            model.LogicResult = txtLogicResult.Text;
            model.Modelo = txtModelo.Text;
            model.EntityResult = txtEntityResult.Text;
            model.Entity = txtEntity.Text;
            Generador.Crear(model);
            btnCrear.Enabled = true;
            MessageBox.Show("Se han generado los archivos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

    }
}
