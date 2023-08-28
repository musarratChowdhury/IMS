using System;

namespace IMS.BusinessModel.Entity.Common
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        long CreatedBy { get; set; }
        DateTime CreationDate { get; set; }
        long? ModifiedBy { get; set; }
        DateTime? ModificationDate { get; set; }
        int Rank { get; set; }
        string BusinessId { get; set; }
        int Version { get; set; }
        int Status { get; set; }
    }
}