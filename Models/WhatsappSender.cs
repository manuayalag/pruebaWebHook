using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace pruebaWebHook.Models
{
    public class WhatsAppSender
    {
        private readonly string _apiUrl = "https://graph.facebook.com/v19.0/347322555126894/"; // Ajusta según la versión de la API
        private readonly string _accessToken = "EAAGUeVCimEsBO3HHYZCWUk3jw0WTdygZA3lsZBKaoUeVs0HiGwsXVyr7KSZCJW3qGonK3uG1drxhrUyy9wGB0sDg8hTIe23lZB6X2dfmZChZAHITkdz0Xu57pGyzPVZAc5hLIve0FdyMp8aMKTVR4H43yCVT8u6F5j366yYarlb1fZCBzTQVImNZBNAc8VgAaFHA14kCvzIBiZBBvRUuhNDREjZCUkQyqtYZD"; // Reemplaza con tu token de acceso

        public async Task EnviarMensaje(string phoneNumber, string messageBody)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                var message = new
                {
                    messaging_product = "whatsapp",
                    to = phoneNumber,
                    type = "text",
                    text = new
                    {
                        body = messageBody
                    }
                };

                var content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_apiUrl}me/messages", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error sending message: " + response.ReasonPhrase);
                }
            }
        }
    }
}
