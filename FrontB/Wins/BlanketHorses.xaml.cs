using FrontB.Classes;
using Syncfusion.Windows.Tools.Controls;
using System.Windows;
using System.Windows.Controls;

namespace FrontB.Pages
{
    public partial class BlanketHorses : Window
    {       
        FilterDataContext dataContext;
        FilterHelper<JournalHorses> filterHelper;        
        private Dictionary<string, ComboBoxAdv> comboBoxes = new Dictionary<string, ComboBoxAdv>();
        private Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();
        private Dictionary<string, Expander> expanders = new Dictionary<string, Expander>();
        public static DataGrid? Static_DgBlanketHorses;
        public BlanketHorses()
        {
            InitializeComponent();   
            Static_DgBlanketHorses= dataGrid_YkrarhatAtlar;
            dataContext = new FilterDataContext(8);            
            filterHelper= new FilterHelper<JournalHorses>(dataContext, comboBoxes, textBoxes, expanders);
            InitFilterDictionaries();
        }
        private void InitFilterDictionaries()
        {
            filterHelper = new FilterHelper<JournalHorses>(dataContext, comboBoxes, textBoxes, expanders)
            {
                PropertySelectors = new Dictionary<string, Func<JournalHorses, object>>()
                {
                    ["Counter"] = x => x.Counter.ToString()?? "",
                    ["Lakamy"] = x => x.Lakamy?.ToString() ?? "",
                    ["Doglanyyl"] = x => x.Doglanyyl?.ToString() ?? "",
                    ["Atasy"] = x => x.Atasy?.ToString() ?? "",
                    ["Enesi"] = x => x.Enesi?.ToString() ?? "",
                    ["Renki"] = x => x.Renki?.ToString() ?? "",
                    ["Jynsy"] = x => x.Jynsy?.ToString() ?? "",
                    ["Eyesi"] = x => x.Eyesi?.ToString() ?? ""
                },
                TextBoxToColumn = new Dictionary<string, string>()
                {
                    ["Searchtb"] = "Counter",
                    ["Searchtb2"] = "Lakamy",
                    ["Searchtb3"] = "Doglanyyl",
                    ["Searchtb4"] = "Atasy",
                    ["Searchtb5"] = "Enesi",
                    ["Searchtb6"] = "Renki",
                    ["Searchtb7"] = "Jynsy",
                    ["Searchtb8"] = "Eyesi"
                },
                ComboBoxToColumn = new Dictionary<string, string>()
                {
                    ["Tbcombo"] = "Counter",
                    ["Tbcombo2"] = "Lakamy",
                    ["Tbcombo3"] = "Doglanyyl",
                    ["Tbcombo4"] = "Atasy",
                    ["Tbcombo5"] = "Enesi",
                    ["Tbcombo6"] = "Renki",
                    ["Tbcombo7"] = "Jynsy",
                    ["Tbcombo8"] = "Eyesi"
                },
                HeaderToColumn = new Dictionary<string, string>()
                {
                    ["T/b"] = "Counter",
                    ["Lakamy"] = "Lakamy",
                    ["Doglan ýyly"] = "Doglanyyl",
                    ["Atasy"] = "Atasy",
                    ["Enesi"] = "Enesi",
                    ["Reňki"] = "Renki",
                    ["Jynsy"] = "Jynsy",
                    ["Eýesi we hojalygy"] = "Eyesi"
                },
                NullableColumns = new HashSet<string>() { "Lakamy", "Atasy", "Enesi" },
                DescendingOrderColumns = new HashSet<string>() { "Doglanyyl", "Renki", "Jynsy" },
                DataGrid = dataGrid_YkrarhatAtlar,
                GetOriginalData = () => Blankets.Atinfo
            };

            foreach (var key in filterHelper.PropertySelectors.Keys)
            {
                filterHelper.ComboBoxFilters[key] = new List<string>();
            }
            filterHelper.OnFilterApplied = filteredData =>
            {
                dataGrid_YkrarhatAtlar.ItemsSource = null;
                dataGrid_YkrarhatAtlar.ItemsSource = filteredData;
            };
        }       

        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Searchtb_TextChanged(object sender, TextChangedEventArgs e)
           => filterHelper.Searchtb_TextChanged(sender, e);

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
             => filterHelper.ComboBox_SelectionChanged(sender, e);

        private void OKbtn_Click(object sender, RoutedEventArgs e)
            => filterHelper.OKbtn_Click(sender, e);

        private void Expander_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
            => filterHelper.Expander_MouseRightButtonDown(sender, e);

        private void SearchTextBox_Loaded(object sender, RoutedEventArgs e)
            => filterHelper.SearchTextBox_Loaded(sender, e);

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
            => filterHelper.ComboBox_Loaded(sender, e);

        private void Expander_Loaded(object sender, RoutedEventArgs e)
            => filterHelper.Expander_Loaded(sender, e);
    }
}
