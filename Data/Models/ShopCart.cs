﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameShop.Data.Models
{
    public class ShopCart
    {
        private readonly AppDBContent _content;
        public uint InTotal { get; set; }   // sum of cart items prices

        public ShopCart(AppDBContent _content)  // get data from database
        {
            this._content = _content;
        }
        public string ShopCartId { get; set; }
        public List<ShopCartItem> ListShopItems { get; set; }
        public static ShopCart GetCart(IServiceProvider services)   // get user cart
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shopCartId);
            return new ShopCart(context) { ShopCartId = shopCartId };
        }

        public void AddToCart(Game game)
        {
            InTotal += game.Price;
            _content.DbShopCartItem.Add(new ShopCartItem // create and add entity "ShopCart Item" instance to DbShopCartItem
            {
                ShopCartId = ShopCartId,
                Game = game
            });

            _content.SaveChanges();
        }

        public void DeleteFromCart(int id)
        {
            var itemToDelete = _content.DbShopCartItem.Where(c => c.Id == id).First(); // search item to delete by ID
            _content.DbShopCartItem.Remove(itemToDelete);
            _content.SaveChanges();
        }

        public List<ShopCartItem> getShopItems()
        {
            return _content.DbShopCartItem.Where(c => c.ShopCartId == ShopCartId).Include(s => s.Game).ToList();
        }
    }
}
