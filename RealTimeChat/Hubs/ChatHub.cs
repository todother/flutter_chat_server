using System;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RealTimeChat.Hubs
{
    public class ChatHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.Client(Context.ConnectionId).SendAsync("receiveConnId", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {

            return base.OnDisconnectedAsync(exception);
        }

        public async Task receiveMsgAsync(string msg)
        {
            MessageTemplate temp = JsonConvert.DeserializeObject<MessageTemplate>(msg);
            string toWho = temp.toWho; ///userId --> connId

            //await Clients.Client(temp.fromWho).SendAsync("receiveMsg", temp.message);

            var client = Clients.AllExcept(temp.fromWho);
            await client.SendAsync("receiveMsg", temp.message);
        }



        public async Task notifyVoice(string msg)
        {
            try
            {
                MessageTemplate temp = JsonConvert.DeserializeObject<MessageTemplate>(msg);
                string toWho = temp.toWho; ///userId --> connId
                var client = Clients.Client(toWho);
                await client.SendAsync("receiveVoice", temp.message,temp.voiceLength);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        

    }
}
