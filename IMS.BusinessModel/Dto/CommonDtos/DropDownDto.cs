using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Dto.CommonDtos
{
    public class DropDownDto
    {
        public string SerialNumber { get; set; }
        public long Id { get; set; }
        public decimal DueAmount { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
