using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_tk2672.Models
{
    public class EFPurchaseRepository : IPurchaseRepository
    {
        private BookstoreContext context;

        public EFPurchaseRepository(BookstoreContext temp)
        {
            context = temp;
        }

        // then include allows us to not have to merge stuff then include it in one line, it instead allows us to keep things separate and add stuff on
        public IQueryable<Purchase> Purchases => context.Purchases.Include(x =>x.Lines).ThenInclude(x => x.Book);

        public void SavePurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(x => x.Book));

            if (purchase.PurchaseId == 0)
            {
                context.Purchases.Add(purchase);
            }

            context.SaveChanges();
        }
    }
}
