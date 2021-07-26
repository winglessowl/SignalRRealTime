using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Models
{
    public class SignalRService: ISignalRService
    {
        public SqlConnection connection;
        public readonly IHubContext<PersonaHub> contexto;

        private string conexionStr = @"Data Source=THOMASPC\SQLEXPRESS;Initial Catalog=TestMarcelo;Integrated Security=True";
        private Persona pers;
        private List<Persona> lista;
        public SignalRService(IHubContext<PersonaHub> context) 
        {
            contexto = context;
            this.lista = new List<Persona>();
        }
        public void Conectar() 
        {
            
            using (var connection = new SqlConnection(this.conexionStr))
            using (var cmd = new SqlCommand("SELECT [id] ,[nombre],[apellido],[fechaYhora] FROM dbo.persona;", connection))
            {


                cmd.Notification = null;
  
                var dependency = new SqlDependency(cmd);
             
                connection.Open();
                var reader = cmd.ExecuteNonQuery();
               
                dependency.OnChange += new OnChangeEventHandler(Cambio);




            }

        }

        private void Cambiar()
        {
            using (var connection = new SqlConnection(this.conexionStr))
            using (var cmd = new SqlCommand("SELECT TOP 1 [id] ,[nombre],[apellido],[fechaYhora] FROM dbo.persona ORDER BY [fechaYhora] DESC ;", connection))
            {



                connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Persona perso = new Persona();
                    perso.id = Convert.ToInt32(reader["id"]);
                    perso.apellido = reader["apellido"].ToString();
                    perso.nombre = reader["nombre"].ToString();
                    perso.fecha = reader["fechaYhora"].ToString();
                    if (this.lista.Count >= 50)
                    {
                        this.lista.RemoveAt(0);
                    }
                    this.lista.Add(perso);

                }
                if (this.lista.Count > 0)
                {
                    this.pers = this.lista[this.lista.Count - 1];
                }
                contexto.Clients.All.SendAsync("Recibir", this.pers);
            }
        }

        private void Cambio(Object sender,SqlNotificationEventArgs eve) 
        {
            if (eve.Info == SqlNotificationInfo.Insert) 
            {
                this.Cambiar();
               
                contexto.Clients.All.SendAsync("RecibirInsert", "Registro insertado");
            }
            this.Conectar();





        }
    }
}
