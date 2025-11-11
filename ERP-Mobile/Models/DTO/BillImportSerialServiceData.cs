using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Models.DTO
{
    public class BillImportSerialServiceData
    {
        public BillImportDetails details { get; set; } = new();
        public List<ProductSerials> serials { get; set; } = [];
    }
}
