using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaylocityProduct.Domain.Interfaces;
using PaylocityProduct.Domain.Models;

namespace PaylocityProduct.Domain.Services
{
    public class DependentService : IDependentService
    {
        private readonly IDependentRepository _dependentRepository;

        public DependentService(IDependentRepository dependentRepository)
        {
            _dependentRepository = dependentRepository;
        }

        public async Task<IEnumerable<Dependent>> GetAll()
        {
            return await _dependentRepository.GetAll();
        }

      

        public async Task<Dependent> Add(Dependent dependent)
        {
         

            await _dependentRepository.Add(dependent);
            return dependent;
        }

      

        public void Dispose()
        {
            _dependentRepository?.Dispose();
        }
    }
}