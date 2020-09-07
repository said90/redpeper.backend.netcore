using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdStringTask(string id);
    }
}
