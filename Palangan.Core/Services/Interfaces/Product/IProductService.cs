using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Palangan.Core.Dtos.Product;
using Palangan.DataLayer.Entities.Comments;
using Palangan.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services.Interfaces.Product
{
    public interface IProductService
    {
        #region AdminProduct
        List<ShowProductForAdminViewModel> GetAllProductAdminList();
        int AddProduct(Palangan.DataLayer.Entities.Products.Product product, IFormFile file);
        List<SelectListItem> GetGroupForAddProduct();
        List<SelectListItem> GetSubGroupForAddProduct(int id);
        Palangan.DataLayer.Entities.Products.Product GetProductlById(int id);

        void EditProduct(Palangan.DataLayer.Entities.Products.Product product, IFormFile file);

        void DeleteProduct(int id);
        #endregion

        #region UI
        List<ShowProductForSiteViewModel> GetAllProductByVisit();
        List<ShowProductForSiteViewModel> GetAllProductByDate();
        List<ShowProductForSiteViewModel> GetAllProductByGroup();

        SingleProductViewModel GetSingleProduct(int id);
        int GetUserIdByName(string name);

        List<ShowProductForSiteViewModel> GetProductByFilter(string filter="",int group=0,string getType="all",int startPrice=0,int endPrice=0);


        void UpdateProduct(Palangan.DataLayer.Entities.Products.Product product);
        #endregion

        #region Comment
        void AddComment(ProductComment comment);

        List<ProductComment> GetAllCommentsById(int productId);
        List<ProductComment> GetAllComment();

        string GetProductNameById(int productId);

        void UpdateComment(ProductComment comment); 

        ProductComment GetCommentById(int id);
        #endregion
    }
}
