using System;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace RealTimeChat.Hubs
{
    public class WebRtcHub:Hub
    {
        public WebRtcHub()
        {
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("receiveConnId", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task createOffer(string message)
        {
            OfferWithCandidate data = JsonConvert.DeserializeObject<OfferWithCandidate>(message);
            var client = Clients.AllExcept(data.connId);
            await client.SendAsync("receiveOffer", JsonConvert.SerializeObject( data));
        }

        public async Task createAnswer(string message)
        {
            OfferWithCandidate data = JsonConvert.DeserializeObject<OfferWithCandidate>(message);
            var client = Clients.AllExcept(data.connId);
            await client.SendAsync("receiveAnswer", JsonConvert.SerializeObject(data));
        }

    }
}
