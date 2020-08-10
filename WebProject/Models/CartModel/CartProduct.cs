using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models.CartModel
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }

        public int PhoneId { get; set; }

        public int Quentity { get; set; }
    }
}
