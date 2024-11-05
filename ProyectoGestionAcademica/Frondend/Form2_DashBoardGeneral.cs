﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend
{
    public partial class Form2_DashboardGeneral : Form
    {
        private int id_perfil;
        private string titulo = "MENU PRINCIPAL";
        private string backUpTitulo = "MENU PRINCIPAL";
        private string nombreUsuario;

        public int Id_perfil { get => id_perfil; set => id_perfil = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string BackUpTitulo { get => backUpTitulo; set => backUpTitulo = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }

        public Form2_DashboardGeneral(int N_DePerfilElegido, string NomUsuarioAceptado)
        {
            InitializeComponent();
            Id_perfil = N_DePerfilElegido;
            Form2_DashboardGeneral_Labell_Titulo.Text = Titulo;
            NombreUsuario = NomUsuarioAceptado;
            EstablecerPerfil();
            Configuracion();
            this.Refresh();
        }
        private void EstablecerPerfil()
        {
            Form2_DashboardGeneral_Button_Perfil.Text = NombreUsuario;
        }
        private void Form2_DashboardGeneral_LinkLabell_CerrarApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult = MessageBox.Show("¿ Seguro que queres salir ?", "Salir...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void Form2_DashboardGeneral_LinkLabell_MinimizarApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                DevolverTituloOriginal();
            }
            else
            {
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = true;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
                AbrirFormulario<Form3_DashBoardAlumnos>();
            }
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Agregar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_1_Agregar>();
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Editar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_2_Editar>();
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Eliminar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_3_Eliminar>();
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Asignar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_4_Asignar>();
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Informacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_5_Informacion>();
        }
        private void Form2_DashboardGeneral_Button_Carreras_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {

                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = true;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
            }
        }

        private void Form2_DashboardGeneral_Button_Materias_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {

                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = true;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
            }
        }

        private void Form2_DashboardGeneral_Button_Examenes_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {

                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = true;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
            }
        }
        private void Form2_DashboardGeneral_Button_Usuarios_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {

                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = true;
            }
        }
        //Metodo generico para regular que formulario se muestra y si debe crearse o no
        //<MiForm>{Nombre del tipo generico} () where {Condicion} MiForm : Form {El tipo generico debe heredar de la class Form}, new () {Su contructor debe ser vacio}
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            //Variable de tipo Form
            Form Formularios;
            Formularios = Form2_DashboardGeneral_Panel_Derecho_Principal.Controls.OfType<MiForm>().FirstOrDefault();
            if (Formularios == null)
            {
                Formularios = new MiForm();
                Formularios.TopLevel = false;//Determina que el nuevo formulario no se vuelva el formulario principal
                Formularios.Dock = DockStyle.Fill;
                Formularios.FormBorderStyle = FormBorderStyle.None;
                Form2_DashboardGeneral_Panel_Derecho_Principal.Controls.Add(Formularios); //Agrega a la coleccion del panel el formulario
                Form2_DashboardGeneral_Panel_Derecho_Principal.Tag = Formularios;
                Form2_DashboardGeneral_Labell_Titulo.Text = ((IConfiguracion)Formularios).Titulo; //Toma el valor de titulo del formulario que utiliza la interface y
                                                                                                  //lo ingresa en la propiedad del formulario general
                Form2_DashboardGeneral_Labell_Titulo.Location = new Point((1056 - Form2_DashboardGeneral_Labell_Titulo.Size.Width) / 2, Form2_DashboardGeneral_Labell_Titulo.Location.Y);
                Formularios.BackColor = Color.FromArgb(177, 173, 189);
                Formularios.Show();
                Formularios.BringToFront();

            }
            else
            {
                Formularios.BringToFront();//Si el formulatrio existe lo trae al frente
                Form2_DashboardGeneral_Labell_Titulo.Text = ((IConfiguracion)Formularios).Titulo; //Toma el valor de titulo del formulario que utiliza la interface y lo ingresa en la propiedad del formulario general
                Form2_DashboardGeneral_Labell_Titulo.Location = new Point((1056 - Form2_DashboardGeneral_Labell_Titulo.Size.Width) / 2, Form2_DashboardGeneral_Labell_Titulo.Location.Y);//Centra el componente basado en el nuevo tamaño de texto.
            }
        }
        private void DevolverTituloOriginal()
        {
            Form2_DashboardGeneral_Labell_Titulo.Text = BackUpTitulo; //Toma el valor de titulo del formulario que utiliza la interface y lo ingresa en la propiedad del formulario general
            Form2_DashboardGeneral_Labell_Titulo.Location = new Point((1056 - Form2_DashboardGeneral_Labell_Titulo.Size.Width) / 2, Form2_DashboardGeneral_Labell_Titulo.Location.Y);
        }
        private void Configuracion()
        {
            switch (Id_perfil)
            {
                case 1:
                    Form2_DashboardGeneral_Button_Alumnos.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Carreras.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_DefinirCursada.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Materias.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Examenes.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Usuarios.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Informacion.Visible = true;
                    break;
                case 2:
                    Form2_DashboardGeneral_Button_Alumnos.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Carreras.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_DefinirCursada.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Materias.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Examenes.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Usuarios.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Informacion.Visible = true;
                    break;
                case 3:
                    Form2_DashboardGeneral_Button_Examenes.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Informacion.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Informacion.Visible = true;
                    break;
                case 4:
                    Form2_DashboardGeneral_Button_Examenes.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Informacion.Visible = true;
                    break;
            }
        }

    }
}
