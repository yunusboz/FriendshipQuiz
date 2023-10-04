using DataAccess.Contexts;
using DataAccess.IRepository;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(BaseDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateOneAppUser(ApplicationUser appUser) => Add(appUser);

        public void DeleteOneAppUser(ApplicationUser appUser) => Remove(appUser);

        public IQueryable<ApplicationUser> GetAllAppUsers(bool trackChanges, string includeProperties = "") 
            => GetAll(trackChanges, includeProperties);

        public ApplicationUser? GetOneAppUser(string id, bool trackChanges, string includeProperties = "")
        {
            return Get(a => a.Id.Equals(id), trackChanges, includeProperties);
        }
    }
}
