using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Repository
{
    class QueueRepository : ITweetsRepository
    {
        private static List<Tweet> _tweets = new List<Tweet>();
        // Imitates a queue appending a status at the end of the list
        public void Add(Tweet status)
        {
            if (_tweets.Find(t => status.Id == t.Id) == null)
            {
                _tweets.Insert(_tweets.Count, status);
            }
        }

        public void Delete(decimal id)
        {
            _tweets.Remove(_tweets.Find(t => t.Id == id));
        }

        public void Clear()
        {
            _tweets.Clear();
        }

        public List<Tweet> GetRange(int count, decimal lastId)
        {
            List<Tweet> statuses = new List<Tweet>();
            int getCount = count < _tweets.Count ? count : _tweets.Count;
            statuses = _tweets.GetRange(0, getCount);
            _tweets.RemoveRange(0, getCount);
            return statuses;
        }

        public Tweet GetStatus(decimal lastId)
        {
            Tweet status = null;
            if (_tweets.Count > 0)
            {
                status = _tweets[0];
                _tweets.RemoveAt(0);
            }
            return status;
        }
    }
}
