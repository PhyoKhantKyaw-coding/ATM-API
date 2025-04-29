using Microsoft.Extensions.Options;
using MODEL.ApplicationConfig;
using MODEL;
using REPOSITORY.Repositories.IRepositories;
using REPOSITORY.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Entities;

namespace REPOSITORY.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private DataContext _dataContext;
    public UnitOfWork(DataContext dataContext, IOptions<AppSettings> appsettings)
    {
        _dataContext = dataContext;
        AppSettings = appsettings.Value;
        Transaction = new TransactionRepository(_dataContext);
        User = new UserRepository(_dataContext);
        Transfer = new TransferRepository(_dataContext);
    }

    public AppSettings AppSettings { get; private set; }
    public ITransactionRepository Transaction { get; private set; }
    public IUserRepository User { get; private set; }
    public ITransferRepository Transfer { get; private set; }


    public void Dispose()
    {
        _dataContext.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }
}
