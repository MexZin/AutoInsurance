using BLL.Contract;
using Models.DataViewModels;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class OrderManagement : IOrderManagement
    {
        OrderRep ord = new OrderRep();

        public bool AddOrder(string user, string packName)
        {
            try
            {
                if (user.Equals("") || packName.Equals(""))
                    throw new Exception("Invalid Parameters");

                ord.AddOrder(user, packName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<OrderDTO> GetAllOrder()
        {
            return ord.GetAllOrder();
        }

        public OrderDTO GetOrder(string user)
        {
            try
            {
                if (user.Equals(""))
                    throw new Exception("Invalid Parameters");
                return ord.GetOrder(user);
            }
            catch
            {
                return new OrderDTO();
            }
        }

        public IEnumerable<OrderDetailDTO> GetOrderDetails(string user)
        {
            return ord.GetOrderDetails(user);
        }
    }
}
