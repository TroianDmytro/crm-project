using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Business_Layer.DTO
{
    public class UpdateDealDTO
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid ClientId { get; set; }
    }
}
