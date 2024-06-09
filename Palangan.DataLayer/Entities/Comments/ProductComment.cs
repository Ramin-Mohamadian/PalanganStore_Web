using Palangan.DataLayer.Entities.Products;
using Palangan.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Entities.Comments
{
    public class ProductComment
    {
        [Key]
        public int CommentId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        [MaxLength(750, ErrorMessage = "تعداد کاراکتر های وارد شده بیش از حد مجاز است")]
        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsDelete { get; set; }

        public bool IsAdminRead { get; set; }


        #region Relation
        public Product  Product { get; set; }

        public User User { get; set; }

        #endregion


    }
}
