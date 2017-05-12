using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace APIService.Models
{
    public static class SampleData
    {
        public static void InitializeOrderDatabase(IServiceProvider serviceProvider)
        {

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<OrdersContext>();
                db.Database.EnsureCreated();
            }
            InsertTestData(serviceProvider);
        }

        private static void InsertTestData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<OrdersContext>();
                if (DataExists<Order>(db))
                    return;

                AddData<Order>(db, new Order() {AccountId = 0,
                                                InstrumentId = 0,
                                                TNumber = 0,
                                                TVersion = 0,
                                                TAction = "S",
                                                CorrectFlag = "N",
                                                CancelFlag = "N",
                                                NDDFlag = "N" });
                db.SaveChanges();
            }
        }

        private static bool DataExists<TData>(DbContext db) where TData: class
        {
            var existingData = db.Set<TData>().ToList();
            if (existingData.Count > 0)
                return true;
            return false;
        }

        private static void AddData<TData>(DbContext db, object item) where TData: class
        {
            db.Entry(item).State = EntityState.Added;
        }

    }
}
