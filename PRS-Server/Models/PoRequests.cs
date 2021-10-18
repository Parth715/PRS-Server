using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PRS_Server.Models
{
    public class PoRequests
    {
        
        public Vendor Vendor { get; set; }
        public List<PoLines> Header{ get; set; }
        public decimal GrandTotal { get; set; }

        public PoRequests()
        {
            
            Header = new List<PoLines>();
        }
    }
}

