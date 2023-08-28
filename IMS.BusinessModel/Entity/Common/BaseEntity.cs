using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity.Common
{
    public class BaseEntity : IBaseEntity
    {
        public virtual long Id { get; set; }
        public virtual long CreatedBy { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual long? ModifiedBy { get; set; }
        public virtual DateTime? ModificationDate { get; set; }
        public virtual int Rank { get; set; }
        public virtual string BusinessId { get; set; } = string.Empty;
        public virtual int Version { get; set; }
        public virtual int Status { get; set; }
    }
}
