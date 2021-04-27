using MISA.Common.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Resources;

namespace MISA.Common.Models
{
    public partial class Employee
    {
        public string _name;
        public Employee()
        {
            EmployeeId = Guid.NewGuid();
        }
        [Key]
        public Guid EmployeeId { get; set; }

        [Unique]
        [DisplayName("Mã nhân viên")]
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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

        public string Email { get; set; }

        [Unique]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Guid? PositionId { get; set; }
        public string PositionName { get; set; }
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Guid? QualificationId { get; set; }
        public string QualificationName { get; set; }
        public int EducationalBackground { get; set; }
        public int MaritalStatus { get; set; }
        public string PersonalTaxCode { get; set; }
        public double? Salary { get; set; }
        public DateTime? JoinDate { get; set; }
        public int? WorkStatus { get; set; }
        public string WorkStatusName
        {
            get
            {
                if (WorkStatus == null)
                    return string.Empty;
                switch ((WorkStatus)WorkStatus)
                {
                    case Models.WorkStatus.Stopped:
                        return ResourcesVN.Enum_WorkStatus_Stopped;
                    case Models.WorkStatus.Working:
                        return ResourcesVN.Enum_WorkStatus_Working;
                    case Models.WorkStatus.Waiting:
                        return ResourcesVN.Enum_WorkStatus_Waitiing;
                    default:
                        return string.Empty;
                }
            }
        }

        [Unique]
        [DisplayName("Số chứng minh thư")]
        public string IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string IdentityPlace { get; set; }

    }
}
