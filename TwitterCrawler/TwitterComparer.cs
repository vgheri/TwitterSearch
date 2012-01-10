using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class TwitterComparer : IComparer<Tweet>
    {
        public int Compare(Tweet x, Tweet y)
        {
            return x.Id.CompareTo(y.Id);            
        }
    }
}
