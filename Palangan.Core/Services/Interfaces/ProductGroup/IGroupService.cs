using Palangan.DataLayer.Entities.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services.Interfaces.ProductGroup
{
    public  interface IGroupService
    {
        List<Palangan.DataLayer.Entities.Groups.ProductGroup> GetAllGroup();


        void AddGroup(Palangan.DataLayer.Entities.Groups.ProductGroup group);


        void DeleteGroup(int id);

        void UpdateGroup(Palangan.DataLayer.Entities.Groups.ProductGroup group);

        Palangan.DataLayer.Entities.Groups.ProductGroup GetGroupById(int id);

        
    }
}
