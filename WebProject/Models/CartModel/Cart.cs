using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models.CartModel
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public List<CartProduct> Products { get; set; }

        public Cart()
        {
            Products = new List<CartProduct>();
        }
    }
}
