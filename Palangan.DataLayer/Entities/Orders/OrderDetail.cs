using Palangan.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Orders
{
    public  class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }


        [Required]
        public int Count { get; set; }

        [Required]
        public int Price { get; set; }

        #region Relation
        public Order Order { get; set; }

        public Product  Product { get; set; }
        #endregion
    }
}
