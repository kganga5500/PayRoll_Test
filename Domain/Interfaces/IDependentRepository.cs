using System.Collections.Generic;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Models;

namespace PaylocityProduct.Domain.Interfaces
{
    public interface IDependentRepository : IRepository<Dependent>
    {
        Task<IEnumerable<Dependent>> GetAll();
        Task Add(Dependent dependent);
    }
}