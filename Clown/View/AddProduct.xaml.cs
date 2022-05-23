using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Clown.View
{
    public partial class AddProduct : Window
    {
        public string ImagePath = "";

        public AddProduct()
        {
            InitializeComponent();
            SetComboBox();
        }

        private void SetComboBox()
        {

            using (ModelBD model = new ModelBD())
            {

                var types = from t in model.ProductType
                            select new
                            {
                                ID = t.ID,
                                Title = t.Title
                            };

                foreach (var type in types) TypeComboBox.Items.Add(type.ID + ". " + type.Title);

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                ImagePath = @"\products\" + openFileDialog.SafeFileName;

                ImgBTN.Content = ImagePath;
            }
        }
    }
}
