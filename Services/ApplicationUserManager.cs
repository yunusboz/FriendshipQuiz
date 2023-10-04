using DataAccess.IRepository;
using Entities.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ApplicationUserManager : IApplicationUserService
    {
        private readonly IRepositoryManager _manager;

        public ApplicationUserManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public void CreateOneAppUser(ApplicationUser appUser)
        {
            _manager.ApplicationUser.CreateOneAppUser(appUser);
            _manager.Save();
        }

        public void DeleteOneAppUser(string id)
        {
            ApplicationUser? entity = _manager.ApplicationUser.GetOneAppUser(id, false);
            if (entity is not null)
            {
                _manager.ApplicationUser.DeleteOneAppUser(entity);
                _manager.Save();
            }
        }

        public IEnumerable<ApplicationUser> GetAllAppUsers(bool trackChanges, string includeProperties = "")
        {
            return _manager.ApplicationUser.GetAllAppUsers(trackChanges, includeProperties);
        }

        public ApplicationUser? GetOneAppUsers(string id, bool trackChanges, string includeProperties = "")
        {
            ApplicationUser? entity = _manager.ApplicationUser.GetOneAppUser(id, false);
            if(entity is null)
                throw new Exception($"User with {id} not found");

            return entity;
        }
    }
}
