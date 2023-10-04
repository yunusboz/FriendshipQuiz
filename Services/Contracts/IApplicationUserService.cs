using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IApplicationUserService
    {
        IEnumerable<ApplicationUser> GetAllAppUsers(bool trackChanges, string includeProperties = "");
        ApplicationUser? GetOneAppUsers(string id, bool trackChanges, string includeProperties = "");
        void CreateOneAppUser(ApplicationUser appUser);
        void DeleteOneAppUser(string id);
    }
}
