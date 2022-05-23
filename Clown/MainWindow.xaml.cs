using Clown.Helper;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clown
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetCombo();
        }

        private void SetCombo()
        {
            foreach (var item in SetComboBox._sortList) SortComboName.Items.Add(item);

            foreach (var item in SetComboBox.GetOrdList()) OrdComboName.Items.Add(item.Title);
            OrdComboName.Items.Add("Все типы");
        }
    }
}
