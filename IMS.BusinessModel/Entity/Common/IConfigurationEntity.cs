using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity.Common
{
    public interface IConfigurationEntity : IBaseEntity
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
