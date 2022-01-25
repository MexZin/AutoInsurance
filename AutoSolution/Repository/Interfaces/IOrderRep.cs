using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IOrderRep:IDisposable
    {
        IEnumerable<OrderDTO> GetAllOrder();
        IEnumerable<OrderDetailDTO> GetOrderDetails(string user);
        OrderDTO GetOrder(string user);
        bool AddOrder(string user, string packName);
    }
}
