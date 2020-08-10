using System.Collections.Generic;
using WebProject.Models.ItemsModel;

namespace WebProject.Interface
{
    public interface IAllMobile
    {
        IEnumerable<Phone> Mobiles { get; }
    }
}
