﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public interface IRepositoryFactory
    {
        ITweetsRepository GetTwitterRepository();
    }
}
