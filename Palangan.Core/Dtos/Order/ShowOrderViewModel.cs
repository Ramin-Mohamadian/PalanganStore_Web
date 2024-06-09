using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Dtos.Order
{
    public  class ShowOrderViewModel
    {
        public int OrderDetailId { get; set; }
        public string ImageName { get; set; }

        public string Title { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }
        public int SumOrder { get; set; }
    }
}
