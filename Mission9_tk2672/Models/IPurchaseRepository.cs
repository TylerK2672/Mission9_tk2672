﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_tk2672.Models
{
    public interface IPurchaseRepository
    {
        IQueryable<Purchase> Purchases { get; }

        void SavePurchase(Purchase purchase);
    }
}
