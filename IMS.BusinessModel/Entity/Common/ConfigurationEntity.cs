using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Entity.Common
{
    public class ConfigurationEntity : BaseEntity,IConfigurationEntity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public static T CreateInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
