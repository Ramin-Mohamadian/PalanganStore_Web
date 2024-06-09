using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Palangan.Core.Dtos.Product;
using Palangan.Core.Services.Interfaces.Product;
using Palangan.DataLayer.Context;
using Palangan.DataLayer.Entities.Comments;
using Palangan.DataLayer.Entities.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services
{
    public class ProductService : IProductService
    {
        private MyContext _context;
        public ProductService(MyContext context)
        {
            _context = context;
        }



        #region Admin
        public List<ShowProductForAdminViewModel> GetAllProductAdminList()
        {
            return _context.Products.Select(p => new ShowProductForAdminViewModel()
            {
                ProductId = p.ProductId,
                ProductImage = p.ProductImage,
                Price = p.Price,
                Title= p.Title,
            }).ToList();
        }


        public int AddProduct(Product product, IFormFile imgfile)
        {
            product.CreateDate = DateTime.Now;
            product.IsDelete = false;
            product.See=1;

            if (imgfile == null)
            {
                product.ProductImage="Default.jpg";
            }
            else
            {
                product.ProductImage=Guid.NewGuid().ToString()+Path.GetExtension(imgfile.FileName);
                var imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/ProductImage", product.ProductImage);
                using (var stream = new FileStream(imgpath, FileMode.Create))
                {
                    imgfile.CopyTo(stream);
                }
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return product.ProductId;

        }



        public List<SelectListItem> GetGroupForAddProduct()
        {
            return _context.productGroups.Where(g => g.ParentId==null).Select(g => new SelectListItem()
            {
                Text=g.GroupTitle,
                Value=g.GroupId.ToString(),

            }).ToList();
        }

        public List<SelectListItem> GetSubGroupForAddProduct(int id)
        {

            return _context.productGroups.Where(g => g.ParentId==id).Select(g => new SelectListItem()
            {
                Text = g.GroupTitle,
                Value=g.GroupId.ToString(),
            }).ToList();
        }


        public Product GetProductlById(int id)
        {
            return _context.Products.Find(id);

        }

        public void EditProduct(Product product, IFormFile file)
        {
            string imagePath = "";
            if (file !=null)
            {
                if (product.ProductImage!="default.jpg")
                {
                    imagePath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/ProductImage", product.ProductImage);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                product.ProductImage=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                var imgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admin/ProductImage", product.ProductImage);
                using (var stream = new FileStream(imgpath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }


            _context.Products.Update(product);
            _context.SaveChanges();

        }

        public void DeleteProduct(int id)
        {
            var product = GetProductlById(id);
            product.IsDelete = true;
            _context.Update(product);
            _context.SaveChanges();
        }


        #endregion

        #region UI
        public List<ShowProductForSiteViewModel> GetAllProductByVisit()
        {
            return _context.Products.OrderByDescending(p => p.See).Select(p => new ShowProductForSiteViewModel()
            {
                ProductId=p.ProductId,
                Price = p.Price,
                ProductImage=p.ProductImage,
                Title=p.Title
            }).Take(15).ToList();
        }

        public List<ShowProductForSiteViewModel> GetAllProductByDate()
        {
            return _context.Products.OrderByDescending(p => p.CreateDate).Select(p => new ShowProductForSiteViewModel()
            {
                ProductId=p.ProductId,
                Price = p.Price,
                ProductImage=p.ProductImage,
                Title=p.Title
            }).ToList();
        }

        public List<ShowProductForSiteViewModel> GetAllProductByGroup()
        {
            string groupName = "صنایع دستی";
            int groupId = _context.productGroups.Where(g => g.GroupTitle==groupName).SingleOrDefault().GroupId;
            return _context.Products.Where(p => p.GroupId==groupId).Select(p => new ShowProductForSiteViewModel()
            {
                ProductId=p.ProductId,
                Price = p.Price,
                ProductImage=p.ProductImage,
                Title=p.Title
            }).ToList();
        }

        public SingleProductViewModel GetSingleProduct(int id)
        {
            return _context.Products.Where(p => p.ProductId==id).Select(p => new SingleProductViewModel()
            {
                ProductId = p.ProductId,
                Price = p.Price,
                ProductImage=p.ProductImage,
                Title=p.Title,
                Description=p.Description,
                Tags=p.Tags
            }).SingleOrDefault();
        }

        public int GetUserIdByName(string name)
        {
            return _context.Users.Where(u=>u.UserName==name).SingleOrDefault().UserId;
        }


        public List<ShowProductForSiteViewModel> GetProductByFilter(string filter = "", int group = 0, string getType = "all", int startPrice = 0, int endPrice = 0)
        {
            IQueryable<Product> result = _context.Products;

            if(!string.IsNullOrEmpty(filter))
            {
                result=result.Where(p=>p.Title.Contains(filter)||p.Tags.Contains(filter));
            }

            if(group!=0)
            {
                result=result.Where(p=>p.GroupId==group||p.SubGroup==group);
            }

            switch (getType)
            {
                case "all":
                    break;

                case "see":
                    result=result.OrderByDescending(p => p.See);
                    break;
                case "newest":
                    result=result.OrderByDescending(p => p.CreateDate);
                    break;
                case "cheap":
                    result=result.OrderBy(p => p.Price);
                    break;
                case "expensive":
                    result=result.OrderByDescending(p => p.Price);
                    break;

            }

            if(startPrice>0)
            {
                result=result.Where(p=>p.Price>=startPrice);
            }

            if (endPrice>0)
            {
                result=result.Where(p => p.Price<startPrice);
            }

            return result.Select(p=>new ShowProductForSiteViewModel()
            {
                Price = p.Price,
                ProductId = p.ProductId,
                ProductImage=p.ProductImage,
                Title=p.Title,
            }).ToList();
        }

        public void UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
        }



        #endregion

        #region Comment


        public void AddComment(ProductComment comment)
        {
            _context.ProductComments.Add(comment);
            _context.SaveChanges();
        }


        public List<ProductComment> GetAllCommentsById(int productId)
        {
           return _context.ProductComments.Include(c=>c.User).Where(c=>!c.IsDelete&&c.ProductId==productId).ToList();
        }

        public string GetProductNameById(int productId)
        {
            return _context.Products.Find(productId).Title;
        }

        public List<ProductComment> GetAllComment()
        {
            return _context.ProductComments.Where(c=>!c.IsAdminRead&&!c.IsDelete).OrderByDescending(c=>c.CreateDate).ToList();
        }

        public void UpdateComment(ProductComment comment)
        {
            _context.ProductComments.Update(comment);
            _context.SaveChanges();
        }

        public ProductComment GetCommentById(int id)
        {
            return _context.ProductComments.Find(id);
        }

       
        #endregion
    }
}
