using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebProject.Interface;
using WebProject.Models;
using WebProject.Models.ItemsModel;
using WebProject.ViewModel;

namespace WebProject.Service.CartServ
{
    public class CartService
    {
        private readonly IServiceProvider Service;

        public CartService (IServiceProvider service)
        {
            Service = service;
        }

        /// <summary>
        /// Создание сессии 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public ISession CreateSession()
        {
            ISession sesia = Service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string IdItem = sesia.GetString("IdSession") ?? Guid.NewGuid().ToString(); 
            sesia.SetString("IdSession", IdItem);
            return sesia;
        }

        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="quentity"></param>
        public void AddToCart(Phone phone)
        {
            var session = CreateSession();
            var Product = JsonConvert.SerializeObject(phone);

            if (session.Keys.Contains("IdSession"))
            {
                if (!session.Keys.Contains("Item"))
                {
                    session.SetString("Item", Product);
                }
                else
                {
                    var GetSession = session.GetString("Item");
                    var SessionValue = string.Join(";", GetSession);

                    #region Получение товаров из сессии
                    string[] ArrayObject = SessionValue.Split(";");
                    var ListProduct = new List<string>();

                    if (ArrayObject.Length >= 1)
                    {
                        for (int i = 0; i < ArrayObject.Length; i++)
                        {
                            string ValueArray = ArrayObject[i];
                            ListProduct.Add(ValueArray);
                        }
                    }
                    #endregion

                    #region Добавление товара
                    string NewProduct = Product;
                    ListProduct.Add(NewProduct);

                    var SerialProduct = new List<string>();
                    for (int i = 0; i < ListProduct.Count; i++)
                    {
                        string SerialString = ListProduct[i];
                        SerialProduct.Add(SerialString);
                    }
                    #endregion

                    #region Обновление сессии
                    var Result = string.Join(";", SerialProduct);

                    session.Remove("Item");
                    session.SetString("Item", Result);
                    #endregion
                }
            }
        }

        /// <summary>
        /// Удаление товара из корзины
        /// </summary>
        public void DeleteToCart(Phone phone)
        {
            var Session = CreateSession();
            var GetSession = Session.GetString("Item");

            #region Получение товаров из сессии
            string SessionValue = string.Join(";", GetSession);
            string[] ArrayObject = SessionValue.Split(";");

            var ListProducts = new List<List<string>>();

            for (int i = 0; i < ArrayObject.Length; i++)
            {
                var Product = ArrayObject[i];
                if (ListProducts.Exists(c => c.Exists(q => q.Contains(Product))))
                {
                    for (int n = 0; n < ListProducts.Count; n++)
                    {
                        if (ListProducts[n].Exists(c => c.Contains(Product)))
                        {
                            ListProducts[n].Add(Product);
                        }
                    }
                }
                else
                {
                    ListProducts.Add(new List<string> { Product });
                }
            }
            #endregion

            #region Удаление товара
            var Products = new List<string>();
            for (int q = 0; q < ListProducts.Count; q++)
            {
                var DeleteProduct = JsonConvert.SerializeObject(phone);
                ListProducts[q].Remove(DeleteProduct);

                for (int x = 0; x < ListProducts[q].Count; x++)
                {
                    Products.Add(ListProducts[q][x]);
                }
            }
            #endregion

            #region Обновление сессии
            string Result = string.Join(";", Products);
            if (Result == "")
            {
                Session.Remove("Item");
            }
            else
            {
                Session.Remove("Item");
                Session.SetString("Item", Result);
            }
            #endregion
        }
    }
}
