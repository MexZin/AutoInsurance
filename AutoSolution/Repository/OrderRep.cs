using DAL.EF;
using Models.DataViewModels;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRep : IOrderRep
    {
        private bool disposedValue;
        AutoDBContext db = new AutoDBContext();

        public bool AddOrder(string user, string packName)
        {
            if (!db.Packages.Any(i => i.Pack_Name.Equals(packName)))
                throw new Exception($"Package with name {packName} not found!");

            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var pack = db.Packages.Where(i => i.Pack_Name.Equals(packName)).First();

                    if (db.Orders.Any(i => i.Client.Equals(user)))
                    {
                        var order = db.Orders.Where(i => i.Client.Equals(user)).First();
                        if (!db.OrderDetails.Any(i => i.Id_pack.Equals(pack.Id_pack) && i.Id_order.Equals(order.Id_order)))
                        {
                            OrderDetail odt = new OrderDetail
                            {
                                Id_order = order.Id_order,
                                Id_pack = pack.Id_pack,
                                Price = pack.Price
                            };
                            order.Total = order.Total += pack.Price;
                            order.Order_date = DateTime.Now;
                            db.OrderDetails.Add(odt);
                        }
                    }
                    else
                    {
                        Order udt = new Order
                        {
                            Client = user,
                            Order_date = DateTime.Now,
                            Total = pack.Price
                        };
                        OrderDetail odt = new OrderDetail
                        {
                            Id_order = udt.Id_order,
                            Id_pack = pack.Id_pack,
                            Price = pack.Price
                        };
                        db.Orders.Add(udt);
                        db.OrderDetails.Add(odt);
                    }

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<OrderDTO> GetAllOrder()
        {
            return db.Orders.Select(i => new OrderDTO
            {
                Id_order = i.Id_order,
                Client = i.Client,
                Order_date = i.Order_date,
                Total = i.Total
            });
        }

        public IEnumerable<OrderDetailDTO> GetOrderDetails(string user)
        {
            return from p in db.Packages
                   join od in db.OrderDetails on p.Id_pack equals od.Id_pack
                   join o in db.Orders on od.Id_order equals o.Id_order
                   where o.Client == user
                   select new OrderDetailDTO
                   {
                       Pack_name = p.Pack_Name,
                       Price = p.Price
                   };
        }

        public OrderDTO GetOrder(string user)
        {
            return db.Orders.Where(i => i.Client.Equals(user)).Select(i => new OrderDTO
            {
                Id_order = i.Id_order,
                Client = i.Client,
                Order_date = i.Order_date,
                Total = i.Total
            }).FirstOrDefault();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~OrderRep()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
