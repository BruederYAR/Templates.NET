using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceDuendeTemplate.Base.Reposytory
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
    }
}
