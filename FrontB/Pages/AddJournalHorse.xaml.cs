using FrontB.Classes;
using FrontB.Helpers;
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
        public static ComboBox Static_ComboColors;
        public static ComboBox Static_ComboOwners;        
        public AddJournalHorse()
        {
            InitializeComponent();            
            Static_ComboColors= combo_renki;
            Static_ComboOwners= tb_doglanYeri;           
            tb_doglanyyl.Maximum = DateTime.Now.Year;
            SetupComboBox();          
        }       
        private void tb_ykrar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string? ykrar = this.tb_ykrar.SelectedItem as string;

                if (string.IsNullOrEmpty(ykrar))
                {
                    return;
                }

                foreach (BlanketsClass b in Blankets.Blank)
                {
                    if (b.Ykrarhat == ykrar)
                    {
                        tb_ysene.Text = b.Ysene;
                        tb_ykrar.Tag = b.Guid;                        
                    }
                }

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
                {   string lakamy = tb_lakamy.Text;
                    string doglanyl = tb_doglanyyl.Value.ToString();
                    string atasy = tb_atasy.Text;
                    string enesi = tb_enesi.Text;
                    string jynsy = tb_jynsy.Text;
                    string renki = combo_renki.Text;
                    string biomaterial = Combo_biomaterial.Text;
                    string biosan = tb_biosan.Text;
                    string probnomer = tb_probnomer.Text;
                    string eyesi = tb_doglanYeri.Text;
                    string nyshanlar = tb_nyshanlar.Text;
                    string bellik = tb_bellik.Text;
                    string guid = tb_ykrar.Tag.ToString();                   

                    await Requests.Add_JournalHorses(lakamy,doglanyl,atasy,enesi,jynsy,renki,biomaterial,biosan,probnomer,eyesi,nyshanlar,bellik,guid);
                    Arassala();                   
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
                comboBox.ItemsSource = tb_doglanYeri.ItemsSource;
                comboBox.IsDropDownOpen = false;
                return;
            }

            if (tb_doglanYeri.ItemsSource == null)
            {
                comboBox.IsDropDownOpen = false;
                return;
            }

            var filteredItems = tb_doglanYeri.ItemsSource
                .Cast<string>().Where(item => item.ToLower().Contains(typedText.ToLower())).ToList();

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
                tb_lakamy.Clear();
                tb_doglanyyl.Value = 0;
                combo_renki.SelectedIndex = -1;
                tb_jynsy.SelectedIndex = -1;
                tb_atasy.Clear();
                tb_enesi.Clear();

                tb_belgi.Clear();
                tb_ykrar.SelectedIndex = -1;
                tb_ykrar.Tag = null;
                tb_ysene.SelectedDate = null;
                tb_probnomer.Value = 0;
                Combo_biomaterial.SelectedIndex = -1;
                tb_biosan.Value = 1;

                tb_doglanYeri.Text = "";
                tb_doglanYeri.SelectedIndex = -1;
                tb_nyshanlar.Clear();
                tb_bellik.Clear();        
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
