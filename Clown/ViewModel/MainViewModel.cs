using Clown.Helper;
using Clown.Model;
using Clown.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Clown.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource collectionViewSource;
        public ICollectionView collectionView => collectionViewSource.View;

        public MainViewModel()
        {
            var products = GetProducts.GetSec();

            ObservableCollection<NewProduct> productsCollection = new ObservableCollection<NewProduct>();
            foreach (var product in products) productsCollection.Add(product);

            collectionViewSource = new CollectionViewSource { Source = productsCollection };
            collectionViewSource.Filter += Items_Filter;
        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                collectionViewSource.View.Refresh();
                OnPropertyChanged("FilterText");
            }
        }

        private string ordText;
        public string OrdText
        {
            get => ordText;
            set
            {
                ordText = value;
                collectionViewSource.View.Refresh();
                OnPropertyChanged("OrdText");
            }
        }

        private string sortText;
        public string SortText
        {
            get => sortText;
            set
            {
                sortText = value;

                collectionViewSource.SortDescriptions.Clear();

                if (sortText.Equals("Наименование по воз."))
                    collectionViewSource.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
                else if (sortText.Equals("Наименование по убыв."))
                    collectionViewSource.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Descending));
                else if (sortText.Equals("№ Цеха по воз."))
                    collectionViewSource.SortDescriptions.Add(new SortDescription("ProductionWorkshopNumber", ListSortDirection.Ascending));
                else if (sortText.Equals("№ Цеха по убыв."))
                    collectionViewSource.SortDescriptions.Add(new SortDescription("ProductionWorkshopNumber", ListSortDirection.Descending));
                else if (sortText.Equals("Мин. стоимость по воз."))
                    collectionViewSource.SortDescriptions.Add(new SortDescription("MinCostForAgent", ListSortDirection.Ascending));
                else if (sortText.Equals("Мин стоимость по убыв."))
                    collectionViewSource.SortDescriptions.Add(new SortDescription("MinCostForAgent", ListSortDirection.Descending));

                collectionViewSource.View.Refresh();
                OnPropertyChanged("SortText");
            }
        }

        private void Items_Filter(object sender, FilterEventArgs e)
        {
            NewProduct _product = e.Item as NewProduct;

            var productType = SetComboBox.GetOrdList().Where(p => p.Title.Equals(OrdText)).FirstOrDefault();

            if (!string.IsNullOrEmpty(FilterText) && string.IsNullOrEmpty(OrdText))
            {
                if (string.IsNullOrEmpty(FilterText))
                {
                    e.Accepted = true;
                    return;
                }

                SortOrdIF(_product, e);
            }
            else if (!string.IsNullOrEmpty(OrdText) && string.IsNullOrEmpty(FilterText))
            {
                if (string.IsNullOrEmpty(OrdText))
                {
                    e.Accepted = true;
                    return;
                }

                if (!OrdText.Equals("Все типы"))
                {
                    if (_product.ProductTypeID == productType.ID)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(OrdText) && string.IsNullOrEmpty(FilterText))
                {
                    e.Accepted = true;
                    return;
                }

                if (!OrdText.Equals("Все типы"))
                {
                    SortOrdIF(_product, e, productType, true);
                }
                else
                {
                    SortOrdIF(_product, e);
                }
            }
        }

        private void SortOrdIF(NewProduct _product, FilterEventArgs e, ProductType productType = null, bool multi = false)
        {
            if (multi == false)
            {
                if (_product.Title.ToUpper().Contains(FilterText.ToUpper()))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            else
            {
                if (productType != null)
                {
                    if (_product.Title.ToUpper().Contains(FilterText.ToUpper()) && _product.ProductTypeID == productType.ID)
                    {
                        e.Accepted = true;
                    }
                    else
                    {
                        e.Accepted = false;
                    }
                }
            }
        }

        private RelayCommand addProduct;
        public RelayCommand AddProduct
        {
            get
            {
                return addProduct ??
                    (addProduct = new RelayCommand(obj =>
                    {
                        try
                        {
                            AddProduct addProduct = new AddProduct();
                            Product product = new Product();
                            if (addProduct.ShowDialog() == true)
                            {
                                product.Title = addProduct.TitleText.Text;
                                product.ProductTypeID = int.Parse(addProduct.TypeComboBox.SelectedItem?.ToString().Split('.')[0]);
                                product.ArticleNumber = int.Parse(addProduct.ArticText.Text);
                                product.Description = addProduct.DescText.Text;
                                product.Image = addProduct.ImagePath;
                                product.ProductionPersonCount = int.Parse(addProduct.PersonCount.Text);
                                product.ProductionWorkshopNumber = int.Parse(addProduct.WorkNumber.Text);
                                product.MinCostForAgent = int.Parse(addProduct.MinCost.Text);
                            }

                            using (ModelBD model = new ModelBD())
                            {

                                model.Product.Add(product);
                                model.SaveChanges();

                            }

                        }
                        catch(Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
