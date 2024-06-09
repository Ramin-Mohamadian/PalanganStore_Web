using Microsoft.EntityFrameworkCore;
using Palangan.Core.Services.Interfaces.ProductGroup;
using Palangan.DataLayer.Context;
using Palangan.DataLayer.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services
{
    public class GroupService : IGroupService
    {
        MyContext _context;
        public GroupService(MyContext myContext)
        {
            _context = myContext;
        }

       

        public List<ProductGroup> GetAllGroup()
        {
            return _context.productGroups.Include(c=>c.productGroups).ToList();
        }


        public void AddGroup(ProductGroup group)
        {
            _context.productGroups.Add(group);
            _context.SaveChanges();
        }


        public void UpdateGroup(ProductGroup group)
        {
            _context.productGroups.Update(group);
            _context.SaveChanges();
        }

        public ProductGroup GetGroupById(int id)
        {
            return _context.productGroups.Find(id);
        }

        public void DeleteGroup(int id)
        {
           var group= GetGroupById(id);

            group.IsDelete=true;
            UpdateGroup(group);
            _context.SaveChanges();
        }
    }
}
