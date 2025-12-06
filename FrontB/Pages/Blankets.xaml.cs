using FrontB.Classes;
using FrontB.Helpers;
using Syncfusion.UI.Xaml.ProgressBar;
using Syncfusion.Windows.Tools.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FrontB.Pages
{    
    public partial class Blankets : Page
    {
        public static List<BlanketsClass> Blank=new List<BlanketsClass>();
        public static List<JournalHorsesClass> Horseinfo=new List<JournalHorsesClass>();
        public static DataGrid? Static_DgBlankets;
        public static SfCircularProgressBar? Static_ProgressBar;
        public static SfCircularProgressBar? Static_ProgressBar1;
        public static SfCircularProgressBar? Static_ProgressBar2;
        public static ComboBox? Static_YearsComboBox;
        int opacitycounter;
        string? mysqlDateFormat;
        int currentyear= DateTime.Now.Year;
        private bool isedit = false;
        public static string? blanketid;
        FilterDataContext dataContext;
        FilterHelper<BlanketsClass> filterHelper;
        private Dictionary<string, ComboBoxAdv> comboBoxes = new Dictionary<string, ComboBoxAdv>();
        private Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();
        private Dictionary<string, Expander> expanders = new Dictionary<string, Expander>();
        public Blankets()
        {
            InitializeComponent();          
            Static_DgBlankets = dataGrid_Ykrarhatlar;
            Static_ProgressBar = Progressbar;
            Static_ProgressBar1 = Progressbar1;
            Static_ProgressBar2 = Progressbar2;
            Static_YearsComboBox = YearComboBox;
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
                    ["Counter"] = x => x.Counter.ToString() ?? "",
                    ["Yhat"] = x => x.Ykrarhat ?? "",
                    ["Ysene"] = x => x.Ysene ?? "",
                    ["San"] = x => x.San.ToString() ?? "",
                    ["Atsan"] = x => x.Atsan.ToString() ?? ""
                },
                TextBoxToColumn = new Dictionary<string, string>()
                {
                    ["Searchtb"] = "Counter",
                    ["Searchtb2"] = "Yhat",
                    ["Searchtb3"] = "Ysene",
                    ["Searchtb4"] = "San",
                    ["Searchtb5"] = "Atsan"
                },
                ComboBoxToColumn = new Dictionary<string, string>()
                {
                    ["Tbcombo"] = "Counter",
                    ["Tbcombo2"] = "Yhat",
                    ["Tbcombo3"] = "Ysene",
                    ["Tbcombo4"] = "San",
                    ["Tbcombo5"] = "Atsan"
                },
                HeaderToColumn = new Dictionary<string, string>()
                {
                    ["T/b"] = "Counter",
                    ["Y/hat №"] = "Yhat",
                    ["Y/hat sene"] = "Ysene",
                    ["At Sany"] = "San",
                    ["Girizlen at san"] = "Atsan"
                },
                NullableColumns = new HashSet<string>() { },
                IntegerColumns = new HashSet<string>() { "Counter", "Yhat", "San", "Atsan" },
                DescendingOrderColumns = new HashSet<string>() { "Counter", "Yhat", "Ysene", "San", "Atsan" },
                DataGrid = dataGrid_Ykrarhatlar,
                GetOriginalData = () => Blank
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
        private  async void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_hat.Text == "" || tb_ysene.Text == null || tb_san.Text == "")
                {
                    MessageBox.Show("Ähli boşluklary dolduryň!","Bedew",MessageBoxButton.OK,MessageBoxImage.Warning);
                    return;
                }
                if (tb_ysene.SelectedDate != null) 
                {
                    mysqlDateFormat = tb_ysene.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                string ykrarhat = tb_hat.Text;                
                string san = tb_san.Text;           

                await Requests.Add_Blankets(ykrarhat, mysqlDateFormat, Convert.ToInt32(san));
                Arassala();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid_Ykrarhatlar.SelectedIndex < 0)
                {
                    MessageBox.Show("Düzediş girizmek üçin ilki saýlaň!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Addbtn.Visibility = Visibility.Collapsed;
                Editbtn.Visibility = Visibility.Visible;

                Editbtn.IsEnabled = false;
                Editbtn.Opacity = 0.2;
                Button? button = sender as Button;
                if (button != null)
                {
                    BlanketsClass? selectedItem = button.DataContext as BlanketsClass;
                    if (selectedItem != null)
                    {
                        tb_hat.Text = selectedItem?.Ykrarhat?.ToString();
                        tb_ysene.SelectedDate = Convert.ToDateTime(selectedItem?.Ysene);
                        tb_san.Text = selectedItem?.San.ToString();

                    }
                }
                isedit = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void Editbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isedit = false;            
                
                if (tb_hat.Text == "" || tb_ysene.Text == null || tb_san.Text == "")
                {
                    MessageBox.Show("Ähli boşluklary dolduryň!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (tb_ysene.SelectedDate != null)
                {
                    mysqlDateFormat = tb_ysene.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                string? blanketid = Blank[dataGrid_Ykrarhatlar.SelectedIndex].Guid;
                string? ykrarhat = tb_hat.Text;               
                string? san = tb_san.Text;

                await Requests.Update_Blankets(blanketid,ykrarhat, mysqlDateFormat, san);
                Arassala();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void tb_hat_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(tb_hat.Text, "[^0-9]"))
                {

                    tb_hat.Text = tb_hat.Text.Remove(tb_hat.Text.Length - 1);
                }
                else
                {
                    opacitycounter++;
                    if (opacitycounter > 1 && isedit)
                    {
                        Editbtn.IsEnabled = true;
                        Editbtn.Opacity = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void tb_ysene_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (tb_ysene.SelectedDate == null)
                {
                    return;
                }
                else
                {                  
                   string mysqlDateFormat = tb_ysene.SelectedDate.Value.ToString("yyyy-MM-dd");                                      
                    opacitycounter++;
                    if (opacitycounter > 1 && isedit)
                    {
                        Editbtn.IsEnabled = true;
                        Editbtn.Opacity = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void tb_san_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(tb_san.Text, "[^0-9]"))
                {
                    tb_san.Text = tb_san.Text.Remove(tb_san.Text.Length - 1);
                }
                else
                {
                    opacitycounter++;
                    if (opacitycounter > 1 && isedit)
                    {
                        Editbtn.IsEnabled = true;
                        Editbtn.Opacity = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private async void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (YearComboBox.SelectedIndex != -1)
                {
                    currentyear = Convert.ToInt32((((ComboBoxItem)this.YearComboBox.SelectedItem).Content).ToString());

                    await Requests.Get_Stats(currentyear);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Arassala();
            var result = MessageBox.Show("Siz hakykatdanam bu ykrar haty pozmak isleýärsiňizmi?", "Bedew", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    string? blanketid = Blank[dataGrid_Ykrarhatlar.SelectedIndex].Guid;
                    await Requests.Delete_Blanket(blanketid);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        private void View_Click(object sender, RoutedEventArgs e)
        {
            Arassala();
            try
            {
                Button? button = sender as Button;
                if (button != null)
                {
                    BlanketsClass? selectedItem = button.DataContext as BlanketsClass;
                    if (selectedItem != null)
                    {   int counter = 0;
                        blanketid = selectedItem.Guid;
                        if (selectedItem.Horses == null || selectedItem.Horses.Count == 0)
                        {
                            MessageBox.Show("Bu ykrar hat degişli at ýok!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        Horseinfo.Clear();
                        var filterItems = new List<int>();
                        var filterItems2 = new List<string>();
                        var filterItems3 = new List<int?>();
                        var filterItems4 = new List<string>();
                        var filterItems5 = new List<string>();
                        var filterItems6 = new List<string>();
                        var filterItems7 = new List<string>();
                        var filterItems8 = new List<string>();
                        foreach (var horse in selectedItem.Horses)
                        {   counter++;
                            Horseinfo.Add(new JournalHorsesClass(counter, horse.guid, horse.lakamy, horse.doglanyyl, horse.atasy, horse.enesi,horse.jynsy, horse.renki,
                                horse.biomaterial, horse.biosan, horse.probnomer, horse.eyesi, horse.nyshanlar, horse.bellik,selectedItem.Ykrarhat,selectedItem.Ysene));
                        
                            if (!filterItems.Contains(counter))
                                filterItems.Add(counter);

                            if (!filterItems2.Contains(horse.lakamy))
                                filterItems2.Add(horse.lakamy);

                            if (!filterItems3.Contains(horse.doglanyyl))
                                filterItems3.Add(horse.doglanyyl);
                       
                            if (!filterItems4.Contains(horse.atasy))
                                filterItems4.Add(horse.atasy);

                            if (!filterItems5.Contains(horse.enesi))
                                filterItems5.Add(horse.enesi);

                            if (!filterItems6.Contains(horse.jynsy))
                                filterItems6.Add(horse.jynsy);

                            if (!filterItems7.Contains(horse.renki))
                                filterItems7.Add(horse.renki);
                            
                            if (!filterItems8.Contains(horse.eyesi))
                                filterItems8.Add(horse.eyesi);
                        } 
                        var parentWindow = Window.GetWindow(this);
                        var messageWindow = new BlanketHorses
                        {
                            Owner = parentWindow,
                            ShowInTaskbar = false,
                        };
                        if (BlanketHorses.Static_DgBlanketHorses != null)
                        {
                            BlanketHorses.Static_DgBlanketHorses.ItemsSource = null;
                            BlanketHorses.Static_DgBlanketHorses.ItemsSource = Horseinfo;
                            BlanketHorses.Static_DgBlanketHorses.DataContext = new
                            {
                                FilterItems = filterItems,
                                FilterItems2 = filterItems2,
                                FilterItems3 = filterItems3,
                                FilterItems4 = filterItems4,
                                FilterItems5 = filterItems5,
                                FilterItems6 = filterItems6,
                            };
                            messageWindow.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Arassala()
        {
            try
            {
                tb_san.Clear();
                tb_ysene.SelectedDate = null;
                tb_hat.Clear();
                opacitycounter = 0;
                Editbtn.Visibility = Visibility.Collapsed;
                Addbtn.Visibility = Visibility.Visible;
                Editbtn.IsEnabled = false;

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