using FrontB.Classes;
using FrontB.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace FrontB.Wins
{
    public partial class HorseInfo : Window
    {   
        public HorseInfo()
        {
            InitializeComponent();
            SetupComboBox();          
        }      
        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Hasabaalmakbtn_Click(object sender, RoutedEventArgs e)
        {

            if (tb_ykrar.Text != "" && combo_renki.SelectedIndex != -1 && tb_jynsy.SelectedIndex != -1 && Combo_biomaterial.SelectedIndex != -1 && !string.IsNullOrEmpty(tb_doglanYeri.Text))
            {
                try
                {
                   //Update Horseinfo();
                    Arassala();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else { MessageBox.Show("Ähli maglumatlary giriziň!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Warning); }

        }
        private void SetupComboBox()
        {
            try
            {
                tb_doglanYeri.IsEditable = true;
                tb_doglanYeri.IsTextSearchEnabled = false;

                tb_doglanYeri.AddHandler(TextBoxBase.TextChangedEvent,
                new TextChangedEventHandler(ComboBox_TextChanged));

                tb_doglanYeri.PreviewKeyDown += ComboBox_PreviewKeyDown;
                tb_doglanYeri.SelectionChanged += ComboBox_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ComboBox? comboBox = sender as ComboBox;
                if (comboBox != null)
                {

                    string typedText = comboBox.Text;

                    if (string.IsNullOrEmpty(typedText))
                    {
                        comboBox.ItemsSource = MainWindow.Static_Owners;
                        comboBox.IsDropDownOpen = false;
                        return;
                    }
                    if (MainWindow.Static_Owners != null)
                    {
                        var filteredItems = MainWindow.Static_Owners
                       .Cast<string>().Where(item => item.ToLower().Contains(typedText.ToLower())).ToList();

                        comboBox.ItemsSource = filteredItems;

                        if (filteredItems.Count > 0 && tb_doglanYeri.SelectedIndex!=-1 )
                        {
                            comboBox.IsDropDownOpen = true;
                        }
                        else
                        {
                            comboBox.IsDropDownOpen = false;
                        }

                        TextBox? textBox = comboBox.Template.FindName("PART_EditableTextBox", comboBox) as TextBox;

                        if (textBox != null)
                        {
                            textBox.Select(typedText.Length, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ComboBox? comboBox = sender as ComboBox;                
                if (e.Key == Key.Enter && comboBox!=null)
                {
                    comboBox.IsDropDownOpen = false;
                }
                else if (e.Key == Key.Escape && comboBox != null)
                {
                    comboBox.IsDropDownOpen = false;
                    comboBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox? comboBox = sender as ComboBox;               
                if (comboBox!=null && comboBox.SelectedItem != null )
                {
                    comboBox.IsDropDownOpen = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
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
                tb_doglanYyly.Value = 0;
                tb_enesi.Clear();
                tb_atasy.Clear();
                tb_doglanYeri.SelectedIndex=-1;
                tb_nyshanlar.Clear();
                tb_bellik.Clear();
                tb_probnomer.Clear();
                tb_ysene.SelectedDate = null;
                tb_ykrar.Items.Clear();
                tb_biosan.Clear();
                tb_doglanYeri.Text = "";
                tb_ykrar.Tag = null;
                Hasabaalmakbtn.IsEnabled = false;
                Hasabaalmakbtn.Opacity = 0.2;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bedew", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TBGotFocus(object sender, RoutedEventArgs e)
        {
            Hasabaalmakbtn.IsEnabled = true;
            Hasabaalmakbtn.Opacity = 1;

        }
    }
}