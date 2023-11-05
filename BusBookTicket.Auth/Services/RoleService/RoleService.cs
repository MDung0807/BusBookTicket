using BusBookTicket.Auth.Specification;
using BusBookTicket.Core.Models.Entity;

using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.Auth.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Role> _repository;
        
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GenericRepository<Role>();
        }
        public async Task<Role> getRole(string roleName)
        {
            RoleSpecification roleSpecification = new RoleSpecification(roleName);
            return await _repository.Get(roleSpecification);
        }
    }
}
