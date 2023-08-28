using IMS.BusinessModel.Dto.CommonDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Dto.Customer
{
    public sealed class CustomerDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
    }

    public sealed class  CustomerFormDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long CustomerTypeId { get; set; }
    }
}
