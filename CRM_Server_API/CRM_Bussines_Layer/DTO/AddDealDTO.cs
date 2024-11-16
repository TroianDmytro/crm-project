using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Business_Layer.DTO
{
    public class AddDealDTO
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public Guid ClientId { get; set; }
    }
}
