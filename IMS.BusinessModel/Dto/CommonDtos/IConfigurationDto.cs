using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Dto.CommonDtos
{
    public interface IConfigurationDto
    {
        long Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Status { get; set; }
        long CreatedBy { get; set; }
        DateTime CreationDate { get; set; }
        long? ModifiedBy { get; set; }
        DateTime? ModificationDate { get; set; }
        int Rank { get; set; }
        string BusinessId { get; set; }
        int Version { get; set; }
    }
    public interface IConfigurationFormData
    {
        long Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Status { get; set; }
        int Rank { get; set; }
    }
}
