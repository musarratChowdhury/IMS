using IMS.BusinessModel.Dto.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.BusinessModel.Dto.GridData
{
    public class DataResult<T>
    {
        public List<T> result { get; set; } = new List<T>();
        public int count { get; set; } = 0;
    }
}
