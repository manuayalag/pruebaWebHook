using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using pruebaWebHook.Models;
using System.Collections.Generic;

namespace pruebaWebHook.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<MessageRecord> Messages { get; private set; }

        public void OnGet()
        {
            Datos datos = new Datos(_configuration);
            Messages = datos.GetReceivedMessages();
        }
    }
}
