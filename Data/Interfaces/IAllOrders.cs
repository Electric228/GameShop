﻿using GameShop.Data.Models;
using System.Collections.Generic;

namespace GameShop.Data.Interfaces
{
    public interface IAllOrders // repository interface of entity "Order" instances
    {
        IEnumerable<Order> Orders { get; }
        void createOrder(Order order);  // order creation

    }
}
