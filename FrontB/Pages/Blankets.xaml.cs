using FrontB.Classes;
using FrontB.Helpers;
using Syncfusion.Windows.Tools.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FrontB.Pages
{    
    public partial class Blankets : Page
    {
        public static List<BlanketsClass> blank=new List<BlanketsClass>();
        public static DataGrid Static_DgBlankets;
        FilterDataContext dataContext;
        FilterHelper<BlanketsClass> filterHelper;
        private Dictionary<string, ComboBoxAdv> comboBoxes = new Dictionary<string, ComboBoxAdv>();
        private Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();
        private Dictionary<string, Expander> expanders = new Dictionary<string, Expander>();
        public Blankets()
        {
            InitializeComponent();          
            Static_DgBlankets = dataGrid_Ykrarhatlar;
            dataContext = new FilterDataContext(5);
            filterHelper = new FilterHelper<BlanketsClass>(dataContext, comboBoxes, textBoxes, expanders);
            InitFilterDictionaries();
        }
        private void InitFilterDictionaries()
        {
            filterHelper = new FilterHelper<BlanketsClass>(dataContext, comboBoxes, textBoxes, expanders)
            {
                PropertySelectors = new Dictionary<string, Func<BlanketsClass, object>>()
                {
                    ["TB"] = x => x.Counter,
                    ["Yhat"] = x => x.Ykrarhat,
                    ["Ysene"] = x => x.Ysene,
                    ["San"] = x => x.San,
                    ["Atsan"] = x => x.Atsan
                },
                TextBoxToColumn = new Dictionary<string, string>()
                {
                    ["Searchtb"] = "TB",
                    ["Searchtb2"] = "Yhat",
                    ["Searchtb3"] = "Ysene",
                    ["Searchtb4"] = "San",
                    ["Searchtb5"] = "Atsan"
                },
                ComboBoxToColumn = new Dictionary<string, string>()
                {
                    ["Tbcombo"] = "TB",
                    ["Tbcombo2"] = "Yhat",
                    ["Tbcombo3"] = "Ysene",
                    ["Tbcombo4"] = "San",
                    ["Tbcombo5"] = "Atsan"
                },
                HeaderToColumn = new Dictionary<string, string>()
                {
                    ["T/b"] = "TB",
                    ["Y/hat №"] = "Yhat",
                    ["Y/hat sene"] = "Ysene",
                    ["At Sany"] = "San",
                    ["Girizlen at san"] = "Atsan"
                },
                NullableColumns = new HashSet<string>() { },
                IntegerColumns = new HashSet<string>() { "TB", "Yhat", "San", "Atsan" },
                DescendingOrderColumns = new HashSet<string>() { "TB", "Yhat", "Ysene", "San", "Atsan" },
                DataGrid = dataGrid_Ykrarhatlar,
                GetOriginalData = () => blank
            };

            foreach (var key in filterHelper.PropertySelectors.Keys)
            {
                filterHelper.ComboBoxFilters[key] = new List<string>();
            }

            filterHelper.OnFilterApplied = filteredData =>
            {
                dataGrid_Ykrarhatlar.ItemsSource = null;
                dataGrid_Ykrarhatlar.ItemsSource = filteredData;
            };
        }
        private  void Addbtn_Click(object sender, RoutedEventArgs e)
        { 
          
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        { 
           
        }
        private  void Editbtn_Click(object sender, RoutedEventArgs e)
        {
          
        }
        private void tb_hat_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        private void tb_ysene_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void tb_san_TextChanged(object sender, TextChangedEventArgs e)
        {
         
        }
        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            
        }
        private  void Delete_Click(object sender, RoutedEventArgs e)
        { 
           
        }
        private void View_Click(object sender, RoutedEventArgs e)
        {
          
        }
        private void Searchtb_TextChanged(object sender, TextChangedEventArgs e)
           => filterHelper.Searchtb_TextChanged(sender, e);
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => filterHelper.ComboBox_SelectionChanged(sender, e);
        private void OKbtn_Click(object sender, RoutedEventArgs e)
            => filterHelper.OKbtn_Click(sender, e);
        private void Expander_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
            => filterHelper.Expander_MouseRightButtonDown(sender, e);
        private void SearchTextBox_Loaded(object sender, RoutedEventArgs e)
            => filterHelper.SearchTextBox_Loaded(sender, e);
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
            => filterHelper.ComboBox_Loaded(sender, e);
        private void Expander_Loaded(object sender, RoutedEventArgs e)
            => filterHelper.Expander_Loaded(sender, e);
    }
}