using BLL.Service;
using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutoService.Controllers
{
    public class OrderController : ApiController
    {
        OrderManagement od = new OrderManagement();

        [Route("api/GetAllOrder")]
        [HttpGet]
        public IEnumerable<OrderDTO> GetAllOrder() => od.GetAllOrder();

        [Route("api/GetOrder/{user}")]
        [HttpGet]
        public OrderDTO GetOrder(string user) => od.GetOrder(user);

        [Route("api/GetOrderDetails/{user}")]
        [HttpGet]
        public IEnumerable<OrderDetailDTO> GetOrderDetails(string user) => od.GetOrderDetails(user);

        [Route("api/AddOrder/{user}/{packName}")]
        [HttpGet]
        public bool AddOrder(string user, string packName) => od.AddOrder(user, packName);
    }
}
