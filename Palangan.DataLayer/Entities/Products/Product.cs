using Palangan.DataLayer.Entities.Comments;
using Palangan.DataLayer.Entities.Groups;
using Palangan.DataLayer.Entities.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Products
{
    public  class Product
    {
        [Key]
        public int ProductId { get; set; }


        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int GroupId { get; set; }


        
        public int? SubGroup { get; set; }



        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(350, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string Title { get; set; }


        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public string Description { get; set; }


        [Display(Name ="قیمت")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public int Price { get; set; }


        [Display(Name ="تصویر")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string ProductImage { get; set; }


        
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public DateTime CreateDate { get; set; }

        [Display(Name ="برچسب")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(850, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string  Tags { get; set; }



        [Display(Name = "بازدید")]
        public int See { get; set; }

        [Display(Name ="حذف شده")]
        public bool IsDelete { get; set; }


        #region Relations

        [ForeignKey("GroupId")]
        public ProductGroup ArticleGroup { get; set; }

        [ForeignKey("SubGroup")]
        public ProductGroup Group { get; set; }


        public List<OrderDetail>    OrderDetails { get; set; }

        public List<ProductComment> ProductComments { get; set; }
        #endregion
    }
}
