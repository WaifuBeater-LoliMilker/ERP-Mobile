using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Models.DTO
{
    public class ProductItemsDTO
    {
        public int status { get; set; }
        public List<ProductItems> data { get; set; } = [];
    }
}
