using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MISA.Common.Models
{
    public class CustomerGroup
    {
        [Key]
        public Guid CustomerGroupId { get; set; }
        public string CustomerGroupCode { get; set; }
        public string CustomerGroupName { get; set; }
        public string Description { get; set; }
    }
}
