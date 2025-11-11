using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Models.DTO
{
    public class ProductSerialsDTO
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<ProductSerials> data { get; set; }
        public string error { get; set; }
    }
}
