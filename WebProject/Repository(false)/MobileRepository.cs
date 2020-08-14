using System.Collections.Generic;
using WebProject.Interface;
using WebProject.Models;
using WebProject.Models.ItemsModel;

namespace WebProject.Repository
{
    public class MobileRepository : IAllMobile
    {
        private readonly MobileContext db;
        public MobileRepository(MobileContext cont)
        {
            db = cont;
        }
        public IEnumerable<Phone> Mobiles => db.Phones;
    }
}
