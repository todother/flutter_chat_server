using System;
namespace RealTimeChat.Hubs
{
    public class OfferWithCandidate
    {
        public OfferWithCandidate()
        {
           
        }

        public string sessionId { get; set; }
        public string connId { get; set; }
        public string sdp { get; set; }
        public string type { get; set; }
        public string candidate { get; set; }
        public string sdpMid { get; set; }
        public int sdpMlineIndex { get; set; }
    }
}
