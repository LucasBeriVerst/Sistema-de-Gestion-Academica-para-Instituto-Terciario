﻿using ProyectoGestionAcademica.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend.Form7_Usuarios
{
    public partial class Form7_DashBoardUsuarios_1_Agregar : Form, IConfiguracion
    {
        private GestorDeDatos GestorDeDatos = new GestorDeDatos();
        private string titulo = "USUARIOS: AGREGAR";
        public Form7_DashBoardUsuarios_1_Agregar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }

        private void Form7_DashBoardUsuarios_1_Agregar_Load(object sender, EventArgs e)
        {
            //llamamos al metodo generico y le pasamos el comboBox que va a cargar, el nombre de la tabla, la columna ID y la columna Nombre de donde va sacar los datos
            CargarComboBoxConIDsNombresGenericos(Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil, "Perfiles", "ID_Perfil", "Nombre_Perfil");
        }

        private void Form7_DashBoardUsuarios_1_Agregar_PanelInferior_Button_Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult resutl = MessageBox.Show("Seguro que quiere cerrar la pestaña actual? Se perderan los datos ingresados...", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resutl == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_PanelInferior_Button_Agregar_Click(object sender, EventArgs e)
        {
            //ProbarYRestablecerCamposConPlaceholders();
            ProbarYRestablecerCampos();
            //falta pasarle cmb_tipoPerfil, fecha alta y fecha baja
            if (GestorDeDatos.ValidarCamposDeTexto(Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil.SelectedItem?.ToString(), Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text))
            {
                int error = GestorDeDatos.Form_Usuarios_AgregarUsuario(Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil.SelectedItem?.ToString(), Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text, Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text);

                switch (error)
                {
                    case -20:
                        MessageBox.Show("Por favor seleccione un 'Tipo de Perfil' del campo desplegable.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case -21:
                        MessageBox.Show("Por favor complete el campo 'Nombre' correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -22:
                        MessageBox.Show("Por favor complete el campo 'Apellido' correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -9:
                        MessageBox.Show("El campo 'DNI' solo puede contener numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -23:
                        MessageBox.Show("Por favor complete el campo 'Domicilio (Calle)' correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -24:
                        MessageBox.Show("El campo 'Domicilio (Numero)' solo puede contener numeros.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -10:
                        MessageBox.Show("El campo 'Email' no tiene el formato correcto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -25:
                        MessageBox.Show("Por favor complete el campo 'Usuario' correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -26:
                        MessageBox.Show("Por favor ingrese una contraseña valida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    // Case:1 seria correcto, xq al ejecutar la query devuelve el numero de filas afectadas. Y al ser una insercion
                    // la fila afectada/ingresada seria siempre 1 en este caso. Lo que se traduce a una insercion exitosa
                    case 1:
                        MessageBox.Show("El Usuario se ha ingresado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        MessageBox.Show("Ocurrió un error desconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Faltan completar campos de texto para agregar un nuevo Usuario... Haga un chequeo de los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //ProbarYRestablecerCamposConPlaceholders();
            ProbarYRestablecerCampos();
        }

        //a los placeholders se les pasa el texto de los textbox y directamente los alterna cuando el usuario hace
        //click o sale del textbox [codigo mas legible y facil de escribir que ProbarYRestablecerCampos()]
        private void ProbarYRestablecerCamposConPlaceholders()  //AL PARECER NO FUNCIONA (los hice abajo a mano)
        {
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.PlaceholderText = "NOMBRE";
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.PlaceholderText = "APELLIDO";
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.PlaceholderText = "DNI";
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.PlaceholderText = "DOMICILIO (CALLE)";
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.PlaceholderText = "DOMICILIO (NUMERO)";
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.PlaceholderText = "TELEFONO";
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.PlaceholderText = "EMAIL";
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.PlaceholderText = "USUARIO";
            Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.PlaceholderText = "CONTRASEÑA";
        }

        private void ProbarYRestablecerCampos()
        {
            #region Restablecer campos

            if (Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil.SelectedItem?.ToString() == "Administrador")
            {
                Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil.SelectedItem = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil.SelectedItem?.ToString() == "Docente")
            {
                Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil.SelectedItem = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil.SelectedItem?.ToString() == "Personal Administrativo")
            {
                Form7_DashBoardUsuarios_1_Agregar_ComboBox_Perfil.SelectedItem = string.Empty;
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text == "NOMBRE")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text = "NOMBRE";
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text == "APELLIDO")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text = "APELLIDO";
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text == "DNI")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text = "DNI";
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text == "DOMICILIO (CALLE)")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text = "DOMICILIO (CALLE)";
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text == "DOMICILIO (NUMERO)")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text = "DOMICILIO (NUMERO)";
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text == "TELEFONO")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text = "TELEFONO";
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text == "EMAIL")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text = "EMAIL";
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text == "USUARIO")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text = "USUARIO";
            }

            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text == "CONTRASEÑA")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text = string.Empty;
            }
            else if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text = "CONTRASEÑA";
            }
            #endregion
        }

        #region Texto en los TextBox
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text == "NOMBRE")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.Text = "NOMBRE";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Nombre.ForeColor = Color.DimGray;
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text == "APELLIDO")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.Text = "APELLIDO";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Apellido.ForeColor = Color.DimGray;
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text == "DNI")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.Text = "DNI";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Dni.ForeColor = Color.DimGray;
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text == "DOMICILIO (CALLE)")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.Text = "DOMICILIO (CALLE)";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Calle.ForeColor = Color.DimGray;
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text == "DOMICILIO (NUMERO)")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.Text = "DOMICILIO (NUMERO)";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Numero.ForeColor = Color.DimGray;
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text == "TELEFONO")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.Text = "TELEFONO";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Telefono.ForeColor = Color.DimGray;
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Email_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text == "EMAIL")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Email_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.Text = "EMAIL";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Email.ForeColor = Color.DimGray;
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text == "USUARIO")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.Text = "USUARIO";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Usuario.ForeColor = Color.DimGray;
            }
        }

        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña_Enter(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text == "CONTRASEÑA")
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text = string.Empty;
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.ForeColor = Color.Black;
            }
        }
        private void Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña_Leave(object sender, EventArgs e)
        {
            if (Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text == string.Empty)
            {
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.Text = "CONTRASEÑA";
                Form7_DashBoardUsuarios_1_Agregar_TextBox_Contraseña.ForeColor = Color.DimGray;
            }
        }
        #endregion

        private void CargarComboBoxConIDsNombresGenericos(ComboBox ComboBoxGenerico, string nombreTabla, string columnaID, string columnaNombre)
        {
            var listaIDsNombres = GestorDeDatos.ObtenerIDsyNombresGenericos(nombreTabla, columnaID, columnaNombre);

            ComboBoxGenerico.DisplayMember = "Value";  //Especifica qué propiedad del objeto de la lista se muestra en el ComboBox
            ComboBoxGenerico.ValueMember = "Key";      //qué propiedad está asociada al valor seleccionado. Aquí es Key, el ID.
            ComboBoxGenerico.DataSource = null;     //Limpia el ComboBox antes de asignar la fuente de datos
            ComboBoxGenerico.DataSource = listaIDsNombres;    //Asigna la lista de datos al ComboBox
        }
    }
}
