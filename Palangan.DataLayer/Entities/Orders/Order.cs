using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Orders
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }


        [Display(Name ="کدام کاربر")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int UserId { get; set; }


        [Required]
        public DateTime CreateDate { get; set; }


        [Display(Name ="جمع فاکتور")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int Sum { get; set; }


        [Display(Name ="پرداخت شده")]
        public bool IsFinaly { get; set; }


        [Display(Name = "تحویل داده شده؟")]
        public bool IsDelivered { get; set; }

        #region Relation
        public List<OrderDetail> OrderDetails { get; set; }
        #endregion
    }
}
