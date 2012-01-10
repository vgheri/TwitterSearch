using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Repository
{
    public interface ITweetsRepository
    {        
        void Add(Tweet status);

        void Delete(decimal id);

        void Clear();

        List<Tweet> GetRange(int count, decimal lastId);

        Tweet GetStatus(decimal lastId);
    }
}
