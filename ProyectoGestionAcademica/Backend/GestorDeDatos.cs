﻿using ProyectoGestionAcademica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGestionAcademica.Backend
{
    internal class GestorDeDatos
    {
        Conexion Instancia_SQL = new Conexion();


        //Log in
        public int Form_LogIn_BuscarUsuario(string Usuario, string Contraseña) 
        {
            int respuesta = 0;
            int perfil;
            if (Usuario.Length >= 9)
            {
                respuesta = 5;
            }
            else if (Contraseña.Length >= 35) 
            {
                respuesta = 6;
            }
            else
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@Usuario_Alumno", Usuario },
                    { "@Contraseña_Alumno", Contraseña }
                };
                perfil = Instancia_SQL.EjecutarProcedimiento("sp_StoreAlumnos",parametros);
                if (perfil == 4)
                {
                    respuesta = perfil;
                }
                else
                {
                    var parametros2 = new Dictionary<string, object>
                    {
                        { "@Usuario_Empleado", Usuario },
                        { "@Contraseña_Empleado", Contraseña }
                    };
                    perfil = Instancia_SQL.EjecutarProcedimiento("sp_StoreEmpleados", parametros);
                    if (perfil >= 1) 
                    {
                        respuesta = perfil;
                    } 
                    else 
                    {
                        respuesta = 8;
                    }
                }
            }
            if (Usuario.Length >= 9 && Contraseña.Length >= 35) { respuesta = 7; }
            return respuesta;           
        }
    }
}
