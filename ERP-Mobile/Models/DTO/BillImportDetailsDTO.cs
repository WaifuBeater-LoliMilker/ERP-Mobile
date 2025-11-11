using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Models.DTO
{
    public class BillImportDetailsDTO
    {
        public int status { get; set; }
        public List<BillImportDetails> data { get; set; } = [];
    }
}
