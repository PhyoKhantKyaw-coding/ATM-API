using MODEL.Entities;
using MODEL;
using REPOSITORY.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Repositories.Repositories;

internal class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly DataContext _context;
    public TransactionRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
