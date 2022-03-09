using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Models;

namespace PaylocityProduct.Domain.Interfaces
{
    public interface IDependentService : IDisposable
    {
        Task<IEnumerable<Dependent>> GetAll();
        Task<Dependent> Add(Dependent dependent);
    }
}