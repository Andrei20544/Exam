using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Clown.Helper
{
    public static class SetComboBox
    {
        public static string[] _sortList = new string[6] { "Наименование по воз.", "Наименование по убыв.", 
                                                           "№ Цеха по воз.", "№ Цеха по убыв.", 
                                                           "Мин. стоимость по воз.", "Мин стоимость по убыв." };

        public static List<ProductType> GetOrdList()
        {
            using (ModelBD model = new ModelBD())
            {
                var types = from t in model.ProductType
                            select t;

                return types.ToList();
            }
        }
    }
}
