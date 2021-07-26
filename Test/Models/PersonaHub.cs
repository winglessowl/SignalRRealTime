using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Models
{
    [HubName("PersonaHub")]
    public class PersonaHub:Hub
    {

      
        public async Task Notificar() 
        {
          
            await Clients.All.SendAsync("Recibir", "Funcionando async");
        }
    }
}
