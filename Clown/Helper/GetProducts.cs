using Clown.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clown.Helper
{
    public static class GetProducts
    {
        public static List<NewProduct> GetSec()
        {
            using (ModelBD model = new ModelBD())
            {

                var products = from p in model.Product
                               select new
                               {
                                   Title = p.Title,
                                   Price = p.MinCostForAgent,
                                   Image = p.Image,
                                   ArticleNumber = p.ArticleNumber,
                                   ProductionWorkshopNumber = p.ProductionWorkshopNumber,
                                   PType = p.ProductTypeID
                               };

                NewProduct newProduct = new NewProduct();
                List<NewProduct> newProducts = new List<NewProduct>();

                foreach (var item in products)
                {
                    var pathImage = "";
                    if (item.Image == "none") pathImage = "/products/picture.png";
                    else pathImage = item.Image.Replace("\\", "/").Replace("jpg", "jpeg");

                    newProduct = new NewProduct()
                    {
                        Title = item.Title,
                        ArticleNumber = item.ArticleNumber,
                        Image = pathImage,
                        MinCostForAgent = item.Price,
                        ProductionWorkshopNumber = item.ProductionWorkshopNumber,
                        ProductTypeID = item.PType
                    };

                    newProducts.Add(newProduct);

                }

                return newProducts;

            }
        }
    }
}
