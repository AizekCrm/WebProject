using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebProject.Models.CartModel;
using WebProject.Models.ItemsModel;
using WebProject.Models.OrderModel;
using WebProject.ViewModel;

namespace WebProject.Service.CartServ
{
    public class ItemsInCart
    {
        private readonly CartService CartService;
        
        public ItemsInCart(CartService basketService)
        {
            CartService = basketService;
        }

        public ProductView Products()
        {
            #region Получение товара из сессии
            var Session = CartService.CreateSession();
            var GetSession = Session.GetString("Item");
            #endregion

            #region Вывод товара на страницу

            var ListProduct = new List<List<Phone>>();

            if (GetSession != null)
            {
                string[] ArrayValue = GetSession.Split(";");

                for (int i = 0; i < ArrayValue.Length; i++)
                {
                    Phone NewPhone = JsonConvert.DeserializeObject<Phone>(ArrayValue[i]);
                    if (ListProduct.Exists(c => c.Exists(b => b.PhoneId == NewPhone.PhoneId)))
                    {
                        for (int n = 0; n < ListProduct.Count; n++)
                        {
                            if (ListProduct[n].Exists(x => x.PhoneId == NewPhone.PhoneId))
                            {
                                ListProduct[n].Add(NewPhone);
                            }
                        }
                    }
                    else
                    {
                        ListProduct.Add(new List<Phone> { {NewPhone} });
                    }
                }
                return new ProductView { Products = ListProduct };
            }
            else
            {
                return new ProductView { Products = null };
            }

            #endregion
        }
        public Order GetItemForOrder(Order Order)
        {
            var ItemInCart = SetProducts().ListItems;

            var _Cart = new Cart();

            foreach (var ItemsIn in ItemInCart)
            {
                if (ItemsIn.Count > 1)
                {
                    ItemsIn[0].Quentity = ItemsIn.Count;
                    _Cart.Products.Add(ItemsIn[0]);
                }
                else
                {
                    _Cart.Products.Add(ItemsIn[0]);
                }
                Order.Cart = _Cart;
            }
            return Order;
        }
        public SetCart SetProducts()
        {
            #region Получение товара из сессии
            var Session = CartService.CreateSession();
            var GetSession = Session.GetString("Item");
            #endregion

            #region Сохранение товара в БД

            var ListProduct = new List<List<CartProduct>>();

            if (GetSession != null)
            {
                string[] ArrayValue = GetSession.Split(";");
                int _Quentity = 1;

                for (int i = 0; i < ArrayValue.Length; i++)
                {
                    Phone NewPhone = JsonConvert.DeserializeObject<Phone>(ArrayValue[i]);
                    if (ListProduct.Exists(c => c.Exists(b => b.PhoneId == NewPhone.PhoneId)))
                    {
                        for (int n = 0; n < ListProduct.Count; n++)
                        {
                            if (ListProduct[n].Exists(x => x.PhoneId == NewPhone.PhoneId))
                            {
                                ListProduct[n].Add(new CartProduct { PhoneId = NewPhone.PhoneId, Quentity = _Quentity });
                            }
                        }
                    }
                    else
                    {
                        ListProduct.Add(new List<CartProduct> { new CartProduct { PhoneId = NewPhone.PhoneId, Quentity = _Quentity } });
                    }
                }
            }

            return new SetCart { ListItems = ListProduct };

            #endregion
        }
    }
}
