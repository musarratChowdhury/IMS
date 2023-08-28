using System;

namespace IMS.BusinessModel.Dto.CommonDtos
{
    public class ConfigurationDto : IConfigurationDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int Rank { get; set; }
        public string BusinessId { get; set; }
        public int Version { get; set; }
    }

    public class ConfigurationFormData : IConfigurationFormData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Rank { get; set; }
    }
}
