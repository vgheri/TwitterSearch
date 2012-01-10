using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public ITweetsRepository GetTwitterRepository()
        {
            ITweetsRepository repository = new QueueRepository();
            return repository;
        }
    }    
}
