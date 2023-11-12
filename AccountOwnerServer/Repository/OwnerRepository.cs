using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
        :base(repositoryContext)
        {
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FindAll()
                .OrderBy(ow => ow.Name)
                .ToList();
        }

        public Owner GetOwnerById(Guid id)
        {
            return FindByCondition(owner => owner.Id.Equals(id))
                .FirstOrDefault();
        }

        public Owner GetOwnerWithDetails(Guid id)
        {
            return FindByCondition(owner => owner.Id.Equals(id))
                .Include(ac => ac.Accounts)
                .FirstOrDefault();
        }
        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }
    }
}
