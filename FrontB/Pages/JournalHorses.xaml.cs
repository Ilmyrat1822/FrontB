using FrontB.Classes;
using FrontB.Helpers;
using FrontB.Wins;
using Syncfusion.Windows.Tools.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FrontB.Pages
{
    /// <summary>
    /// Логика взаимодействия для JournalHorses.xaml
    /// </summary>
    public partial class JournalHorses : Page
    {
        public static List<JournalHorsesClass> Horseinfo = new List<JournalHorsesClass>();
        public static DataGrid? Static_DgJournalHorses;
        FilterDataContext dataContext;
        FilterHelper<JournalHorsesClass> filterHelper;
        public static string? Id;
        public static int biomaterialsan = 0;
        private Dictionary<string, ComboBoxAdv> comboBoxes = new Dictionary<string, ComboBoxAdv>();
        private Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();
        private Dictionary<string, Expander> expanders = new Dictionary<string, Expander>();
        public JournalHorses()
        {
            InitializeComponent();            
            Static_DgJournalHorses= dataGrid_Ahliatlar;          
            dataContext = new FilterDataContext(11);
            filterHelper = new FilterHelper<JournalHorsesClass>(dataContext, comboBoxes, textBoxes, expanders);
            InitFilterDictionaries();            
        }
        private void InitFilterDictionaries()
        {
            filterHelper = new FilterHelper<JournalHorsesClass>(dataContext, comboBoxes, textBoxes, expanders)
            {
                PropertySelectors = new Dictionary<string, Func<JournalHorsesClass, object>>()
                {
                    ["TB"] = x => x.TB.ToString() ?? "",
                    ["Lakamy"] = x => x.Lakamy?.ToString() ?? "",
                    ["Doglanyyl"] = x => x.Doglanyyl?.ToString() ?? "",
                    ["Atasy"] = x => x.Atasy?.ToString() ?? "",
                    ["Enesi"] = x => x.Enesi?.ToString() ?? "",
                    ["Renki"] = x => x.Renki?.ToString() ?? "",
                    ["Jynsy"] = x => x.Jynsy?.ToString() ?? "",
                    ["Eyesi"] = x => x.Eyesi?.ToString() ?? "",
                    ["Bellik"] = x => x.Bellik?.ToString() ?? "",
                    ["Yhat"] = x => x.Yhat?.ToString() ?? "",
                    ["Ysene"] = x => x.Ysene?.ToString() ?? ""
                },
                TextBoxToColumn = new Dictionary<string, string>()
                {
                    ["Searchtb"] = "TB",
                    ["Searchtb2"] = "Lakamy",
                    ["Searchtb3"] = "Doglanyyl",
                    ["Searchtb4"] = "Atasy",
                    ["Searchtb5"] = "Enesi",
                    ["Searchtb6"] = "Renki",
                    ["Searchtb7"] = "Jynsy",
                    ["Searchtb8"] = "Eyesi",
                    ["Searchtb9"] = "Bellik",
                    ["Searchtb10"] = "Yhat",
                    ["Searchtb11"] = "Ysene"
                },
                ComboBoxToColumn = new Dictionary<string, string>()
                {
                    ["Tbcombo"] = "TB",
                    ["Tbcombo2"] = "Lakamy",
                    ["Tbcombo3"] = "Doglanyyl",
                    ["Tbcombo4"] = "Atasy",
                    ["Tbcombo5"] = "Enesi",
                    ["Tbcombo6"] = "Renki",
                    ["Tbcombo7"] = "Jynsy",
                    ["Tbcombo8"] = "Eyesi",
                    ["Tbcombo9"] = "Bellik",
                    ["Tbcombo10"] = "Yhat",
                    ["Tbcombo11"] = "Ysene"
                },
                HeaderToColumn = new Dictionary<string, string>()
                {
                    ["T/b"] = "TB",
                    ["Lakamy"] = "Lakamy",
                    ["Doglan ýyly"] = "Doglanyyl",
                    ["Atasy"] = "Atasy",
                    ["Enesi"] = "Enesi",
                    ["Reňki"] = "Renki",
                    ["Jynsy"] = "Jynsy",
                    ["Eýesi we hojalygy"] = "Eyesi",
                    ["Bellik"] = "Bellik",
                    ["Ykrar haty"] = "Yhat",
                    ["Y/hat sene"] = "Ysene"
                },
                NullableColumns = new HashSet<string>() { "Lakamy", "Atasy", "Enesi" },
                IntegerColumns = new HashSet<string>() { "TB", "Doglanyyl" },
                DescendingOrderColumns = new HashSet<string>() { "TB", "Doglanyyl", "Renki", "Jynsy", "Yhat", "Ysene" },
                DataGrid = dataGrid_Ahliatlar,
                GetOriginalData = () => Horseinfo
            };

            foreach (var key in filterHelper.PropertySelectors.Keys)
            {
                filterHelper.ComboBoxFilters[key] = new List<string>();
            }
            filterHelper.OnFilterApplied = filteredData =>
            {
                dataGrid_Ahliatlar.ItemsSource = null;
                dataGrid_Ahliatlar.ItemsSource = filteredData;
            };
        }        
        private  void View_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parentWindow = Window.GetWindow(this);
                var messageWindow = new HorseInfo
                {
                    Owner = parentWindow,
                    ShowInTaskbar = false,
                };
                Button? button = sender as Button;
                if (button != null)
                {
                    JournalHorsesClass? selectedItem = button.DataContext as JournalHorsesClass;
                    if (selectedItem != null)
                    {
                        Id = selectedItem.Id.ToString();
                        messageWindow.tb_lakamy.Text = selectedItem.Lakamy;
                        messageWindow.tb_doglanYyly.Text = selectedItem.Doglanyyl.ToString();
                        messageWindow.tb_atasy.Text = selectedItem.Atasy;
                        messageWindow.tb_enesi.Text = selectedItem.Enesi;                        
                        messageWindow.tb_jynsy.Text = selectedItem.Jynsy;                       
                        messageWindow.tb_ysene.Text = selectedItem.Ysene;
                        messageWindow.tb_probnomer.Text = selectedItem.Probnomer;
                        messageWindow.tb_biosan.Text = selectedItem.Biosan.ToString();
                        messageWindow.Combo_biomaterial.Text = selectedItem.Biomaterial;
                        messageWindow.tb_doglanYeri.Text = selectedItem.Eyesi;                        
                        messageWindow.tb_nyshanlar.Text = selectedItem.Nyshanlar;
                        messageWindow.tb_bellik.Text = selectedItem.Bellik;                        
                        messageWindow.combo_renki.ItemsSource =MainWindow.Static_Colors;
                        messageWindow.combo_renki.Text = selectedItem.Renki;
                        messageWindow.tb_doglanYeri.ItemsSource = MainWindow.Static_Owners;
                        messageWindow.tb_doglanYeri.ItemsSource = selectedItem.Eyesi;
                        messageWindow.tb_ykrar.ItemsSource = MainWindow.Static_Blankets;
                        messageWindow.tb_ykrar.Text = selectedItem.Yhat;
                    }
                }
                messageWindow.ShowDialog();                
                filterHelper.ClearAllFilters();               
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
