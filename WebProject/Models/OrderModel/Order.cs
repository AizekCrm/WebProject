using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models.CartModel;

namespace WebProject.Models.OrderModel
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вы не указали имя!")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Вы не указали адресс!")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Вы не указали телефон!")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerContactPhone { get; set; }


        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }
    }
}
