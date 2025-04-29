using MODEL.ApplicationConfig;
using REPOSITORY.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ITransactionRepository Transaction { get; }
    IUserRepository User { get; }
    ITransferRepository Transfer { get; }
    // Add other repositories here
    AppSettings AppSettings { get; }
    Task<int> SaveChangesAsync();
}
