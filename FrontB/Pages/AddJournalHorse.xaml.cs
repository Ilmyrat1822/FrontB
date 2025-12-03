using FrontB.Classes;
using FrontB.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace FrontB.Pages
{
    /// <summary>
    /// Логика взаимодействия для Hasabalmak.xaml
    /// </summary>
    public partial class AddJournalHorse : Page
    {
        string sql;
        List<BlanketsClass> yinfo;
        List<UserClass> colorInfo;
        private List<string> allItems = new List<string>();
        public AddJournalHorse()
        {
            InitializeComponent();
            yinfo = new List<BlanketsClass>();
            colorInfo = new List<UserClass>();
            tb_doglanyyl.Maximum = DateTime.Now.Year;
            SetupComboBox();
        }

        public async Task Init()
        {
            try
            {  
                //blankets
                //owners
                //colors
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void tb_ykrar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
               /* if (tb_ykrar.SelectedIndex != -1)
                {
                    string mysqlDateFormat = yinfo[tb_ykrar.SelectedIndex].Ysene;
                    tb_ysene.Text = mysqlDateFormat;
                    tb_ykrar.Tag = yinfo[tb_ykrar.SelectedIndex].TB.ToString();
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Hasabaalmakbtn_Click(object sender, RoutedEventArgs e)
        {
            if (tb_ykrar.Text != "" && combo_renki.SelectedIndex != -1 && tb_jynsy.SelectedIndex != -1 && Combo_biomaterial.SelectedIndex != -1 && !string.IsNullOrEmpty(tb_doglanYeri.Text))
            {
                try
                {
                    // add journal horse
                    Arassala();
                    await Init();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else { MessageBox.Show("Ähli maglumatlary giriziň!!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }
        private void SetupComboBox()
        {

            tb_doglanYeri.IsEditable = true;
            tb_doglanYeri.IsTextSearchEnabled = false;

            tb_doglanYeri.AddHandler(TextBoxBase.TextChangedEvent,
            new TextChangedEventHandler(ComboBox_TextChanged));

            tb_doglanYeri.PreviewKeyDown += ComboBox_PreviewKeyDown;
            tb_doglanYeri.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string typedText = comboBox.Text;

            if (string.IsNullOrEmpty(typedText))
            {
                comboBox.ItemsSource = allItems;
                comboBox.IsDropDownOpen = false;
                return;
            }

            var filteredItems = allItems
                .Where(item => item.ToLower().Contains(typedText.ToLower()))
                .ToList();

            comboBox.ItemsSource = filteredItems;

            if (filteredItems.Count > 0)
            {
                comboBox.IsDropDownOpen = true;
            }
            else
            {
                comboBox.IsDropDownOpen = false;
            }

            TextBox textBox = comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox;
            if (textBox != null)
            {
                textBox.Select(typedText.Length, 0);
            }
        }

        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (e.Key == Key.Enter)
            {
                comboBox.IsDropDownOpen = false;
            }
            else if (e.Key == Key.Escape)
            {
                comboBox.IsDropDownOpen = false;
                comboBox.SelectedIndex = -1;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox.SelectedItem != null)
            {
                comboBox.IsDropDownOpen = false;
            }
        }

        public void Arassala()
        {
            try
            {
                tb_belgi.Clear();
                tb_lakamy.Clear();
                Combo_biomaterial.SelectedIndex = 0;
                tb_jynsy.SelectedIndex = -1;
                combo_renki.SelectedIndex = -1;
                tb_doglanyyl.Value = 0;
                tb_enesi.Clear();
                tb_atasy.Clear();
                tb_doglanYeri.ItemsSource = null;
                tb_nyshanlar.Clear();
                tb_bellik.Clear();
                tb_probnomer.Clear();
                tb_ysene.SelectedDate = null;
                tb_ykrar.Items.Clear();
                tb_biosan.Clear();
                tb_doglanYeri.Text = "";
                tb_ykrar.Tag = null;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
