using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MISA.Common.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Unique : Attribute
    {
    }


    public partial class Customer
    {
        [Required]
        [Key]
        public Guid CustomerId { get; set; }

        [Unique]
        [DisplayName("Mã khách hàng")]
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }

        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 0:
                        return "Nữ";
                    case 1:
                        return "Nam";
                    case 2:
                        return "Khác";
                    default:
                        return "Không xác định";
                }
            }
        }
        public string CompanyName { get; set; }
        public string CompanyTaxCode { get; set; }
        [Unique]
        [DisplayName("Số điện thoại di động")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string MemberCardCode { get; set; }
        public double? DebitAmount { get; set; }
        public string Address { get; set; }
        public bool IsStopFollow { get; set; }
        public Guid CustomerGroupId { get; set; }
        public string CustomerGroupName { get; set; }
    }
}
