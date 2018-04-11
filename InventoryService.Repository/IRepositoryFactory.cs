using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;
        IRepositoryAsync<T> GetRepositoryAsync<T>() where T : class;
    }
}
