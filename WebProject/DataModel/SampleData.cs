
using System.Linq;
using WebProject.Models;
using WebProject.Models.ItemsModel;

namespace WebProject
{
    public static class SampleData
    {
        public static void Initialize(MobileContext context)
        {
            if (!context.Phones.Any())
            {
                context.Phones.AddRange(
                    new Phone
                    {
                        Name = "iPhone 5",
                        Company = "Apple",
                        Price = 63787,
                        Photo = "/Img/Iphone 5.jpg"
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy Edge 7",
                        Company = "Samsung",
                        Price = 35777,
                        Photo = "/Img/Samsung-Galaxy-S7-Edge-32GB-Factory-Refurbished-Gold-Platinum-15102018-01-p.jpg"
                    },
                    new Phone
                    {
                        Name = "Pixel 2",
                        Company = "Google",
                        Price = 53555,
                        Photo = "/Img/Google-Pixel-2-black.jpg"
                    },
                    new Phone
                    {
                        Name = "iPhone 3",
                        Company = "Apple",
                        Price = 35553,
                        Photo = "/Img/iphone_3g.jpg"
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy Edge 5",
                        Company = "Samsung",
                        Price = 35255,
                        Photo = "/Img/Samsung Galaxy Edge 5.jpg"
                    },
                    new Phone
                    {
                        Name = "Pixel 6",
                        Company = "Google",
                        Price = 66633,
                        Photo = "/Img/Pixel 6.jpg"
                    },
                    new Phone
                    {
                        Name = "iPhone 7",
                        Company = "Apple",
                        Price = 25166,
                        Photo = "/Img/iPhone 7.jpg"
                    },
                    new Phone
                    {
                        Name = "Samsung Galaxy 8",
                        Company = "Samsung",
                        Price = 23773,
                        Photo = "/Img/Samsung Galaxy 8.png"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
