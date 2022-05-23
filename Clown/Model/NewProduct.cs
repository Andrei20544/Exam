using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clown.Model
{
    public class NewProduct
    {
        public string Title { get; set; }
        public int ArticleNumber { get; set; }
        public decimal MinCostForAgent { get; set; }
        public string Image { get; set; }
        public double? ProductionWorkshopNumber { get; set; }
        public int? ProductTypeID { get; set; }
    }
}
