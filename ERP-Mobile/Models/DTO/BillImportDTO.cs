using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Models.DTO
{
    public class BillImportDTO
    {
        public int status { get; set; }
        public List<BillImport> data { get; set; }
        public int totalPage { get; set; }
    }
}
