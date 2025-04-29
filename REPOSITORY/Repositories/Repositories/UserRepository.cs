using MODEL;
using MODEL.Entities;
using REPOSITORY.Repositories.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REPOSITORY.Repositories.Repositories;

internal class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context) { }
}
