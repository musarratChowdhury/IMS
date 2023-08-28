using IMS.BusinessModel.Dto.CommonDtos;

namespace IMS.BusinessModel.Dto.Vendor
{
    public sealed class VendorDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long VendorTypeId { get; set; }
        public string VendorTypeName { get; set; }
    }

    public sealed class  VendorFormDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long VendorTypeId { get; set; }
    }
}
