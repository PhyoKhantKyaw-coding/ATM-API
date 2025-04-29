using MODEL;
using REPOSITORY.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Repositories.Repositories
{
    internal class TransferRepository : GenericRepository<MODEL.Entities.Transfer>, ITransferRepository
    {
        private readonly DataContext _context;
        public TransferRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
