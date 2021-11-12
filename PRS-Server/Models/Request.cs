using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PRS_Server.Models
{
    public class Request
    {
        
        public int Id { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; } = "Pickup";
        public string Status { get; set; } = "NEW";
        public decimal Total { get; set; } = 0m;
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<RequestLine> RequestLines { get; set; }
        public Request() { }
    }
}
