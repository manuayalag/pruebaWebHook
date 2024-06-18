using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace pruebaWebHook.Models
{
    public class Datos
    {
        private readonly string _connectionString;

        public Datos(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Insertar(string mensajeRecibido, string idWa, string telefonoWa)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO aspwa.dbo.registro (mensaje_recibido, id_wa, telefono_wa) VALUES (@mensajeRecibido, @idWa, @telefonoWa)";
                    command.Parameters.AddWithValue("@mensajeRecibido", mensajeRecibido);
                    command.Parameters.AddWithValue("@idWa", idWa);
                    command.Parameters.AddWithValue("@telefonoWa", telefonoWa);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public List<MessageRecord> GetReceivedMessages()
        {
            var messages = new List<MessageRecord>();

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT id, fecha_hora, mensaje_recibido, id_wa, telefono_wa FROM aspwa.dbo.registro";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = new MessageRecord
                            {
                                Id = reader.GetInt32(0),
                                FechaHora = reader.GetDateTime(1),
                                MensajeRecibido = reader.GetString(2),
                                IdWa = reader.GetString(3),
                                TelefonoWa = reader.GetString(4)
                            };
                            messages.Add(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return messages;
        }
    }

    public class MessageRecord
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string MensajeRecibido { get; set; }
        public string IdWa { get; set; }
        public string TelefonoWa { get; set; }
    }
}