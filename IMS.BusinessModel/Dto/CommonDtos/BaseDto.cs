using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Dto.CommonDtos
{
    public class BaseDto
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int Rank { get; set; }
        public string BusinessId { get; set; }
        public int Version { get; set; }
    }
}
