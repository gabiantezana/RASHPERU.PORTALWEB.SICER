using System;
using System.Collections.Generic;
using MSS.TAWA.BE;
using MSS.TAWA.DA;

//using System.ServiceProcess;
//using System.Diagnostics;

namespace MSS.TAWA.BC
{
    public class UsuarioBC
    {
        public UsuarioBE LoginUsuario(string username, string password)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.LoginUsuario(username, password);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UsuarioBE> ListarUsuario(int Tipo2, int IdUsuario2, int Tipo3)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.ListarUsuario(Tipo2, IdUsuario2, Tipo3);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UsuarioBE> ListarUsuario2(int Tipo2, int Tipo3, int Tipo4, string Palabra)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.ListarUsuario2(Tipo2, Tipo3, Tipo4, Palabra);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string VerificarContrasena(string Pass, string Usuario, int CuentaNumerico, int CuentaMayusculas,
            int CuentaEspeciales)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.VerificarContrasena(Pass, Usuario, CuentaNumerico, CuentaMayusculas, CuentaEspeciales);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ModificarContrasena(int CardCode, string Pass)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.ModificarContrasena(CardCode, Pass);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int VerificarUsuario(string CardCode)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.VerificarUsuario(CardCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int VerificarUsuarioExiste(int CardCode)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.VerificarUsuarioExiste(CardCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int VerificarUsuario2(int CardCode, string mail)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.VerificarUsuario2(CardCode, mail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<UsuarioBE> ListarUsuarioCorreosTesoreria()
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.ListarUsuarioCorreosTesoreria();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UsuarioBE ObtenerUsuario(int? Id, int Tipo)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.ObtenerUsuario(Id, Tipo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int InsertarUsuario(UsuarioBE objBE, int Tipo2, int IdUsuario2)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.InsertarUsuario(objBE, Tipo2, IdUsuario2);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool InsertarAcceso(int IdLog, DateTime Fecha, string Usuario, string Contraseña, string Operacion)
        {
            try
            {
                var objDA = new UsuarioDA();
                return objDA.InsertarAcceso(IdLog, Fecha, Usuario, Contraseña, Operacion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModificarUsuario(UsuarioBE objBE)
        {
            try
            {
                var objDA = new UsuarioDA();
                objDA.ModificarUsuario(objBE);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarUsuario(int Tipo, int IdUsuario)
        {
            try
            {
                var objDA = new UsuarioDA();
                objDA.EliminarUsuario(Tipo, IdUsuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /*
        public string iniciar_Click()
        {
            ServiceController sc = new ServiceController("MSSQLSERVER", "MSS074PC");
            if ((sc.Status.Equals(ServiceControllerStatus.Stopped)) || (sc.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                //var process = new Process();
                //process.StartInfo.FileName = "net";
                //process.StartInfo.Arguments = "start " + "MSSQLSERVER";
                //process.StartInfo.Verb = "runas";//run as administrator
                //process.Start();
                //process.WaitForExit();

                sc.Start();
                return "el servicio estaba detenido y se inicio";
            }  
            else
                return "el servicio ya estaba iniciado";
        }
        public string detener_Click()
        {
            ServiceController sc = new ServiceController("MSSQLSERVER", "MSS074PC");
           if ((sc.Status.Equals(ServiceControllerStatus.Running)) || (sc.Status.Equals(ServiceControllerStatus.StartPending)))
           {
               //var process = new Process();
               //process.StartInfo.FileName = "net";
               //process.StartInfo.Arguments = "start " + "MSS074PC";
               //process.StartInfo.Verb = "runas";//run as administrator
               //process.stop();
               //process.WaitForExit();

               sc.Stop();
               return "el servicio estaba iniciado y se detuvo";
           }
           else
               return "el servicio ya estaba detenido";
        }
        public string validar_Click()
        {
            ServiceController sc = new ServiceController("MSSQLSERVER", "MSS074PC");
            return sc.Status.ToString();
        }
        */
    }
}