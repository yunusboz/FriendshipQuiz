using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        IQueryable<ApplicationUser> GetAllAppUsers(bool trackChanges, string includeProperties = "");
        ApplicationUser? GetOneAppUser(string id, bool trackChanges, string includeProperties = "");
        void CreateOneAppUser(ApplicationUser appUser);
        void DeleteOneAppUser(ApplicationUser appUser);
    }
}
