using FrontB.Classes;
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
        public static ComboBox Static_ComboBlanket;
        public static ComboBox Static_ComboColors;
        public static ComboBox Static_ComboOwners;        
        public AddJournalHorse()
        {
            InitializeComponent();
            Static_ComboBlanket= tb_ykrar;
            Static_ComboColors= combo_renki;
            Static_ComboOwners= tb_doglanYeri;           
            tb_doglanyyl.Maximum = DateTime.Now.Year;
            SetupComboBox();          
        }       
        private void tb_ykrar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string ykrar = (((ComboBoxItem)this.tb_ykrar.SelectedItem).Content).ToString();

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

        private void Hasabaalmakbtn_Click(object sender, RoutedEventArgs e)
        {
            if (tb_ykrar.Text != "" && combo_renki.SelectedIndex != -1 && tb_jynsy.SelectedIndex != -1 && Combo_biomaterial.SelectedIndex != -1 && !string.IsNullOrEmpty(tb_doglanYeri.Text))
            {
                try
                {
                    // add journal horse
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
