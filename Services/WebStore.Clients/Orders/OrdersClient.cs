﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration config) : base(config, WebAPI.Orders) { }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => Get<List<OrderDTO>>($"user/{UserName}");

        public OrderDTO GetOrderById(int id) => GetById<OrderDTO>(id);

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName) =>
            Post(UserName, OrderModel)
               .Content
               .ReadAsAsync<OrderDTO>()
               .Result;
    }
}
