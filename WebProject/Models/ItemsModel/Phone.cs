using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models.ItemsModel
{
    public class Phone
    {
        [Key , DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PhoneId { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
    }
}
