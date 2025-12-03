using FrontB.Classes;
using FrontB.Helpers;
using FrontB.Pages;
using Syncfusion.Windows.Controls.Notification;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace FrontB
{
    public partial class MainWindow : Window
    {
        public static Border Static_LoadingBorder;
        public static SfBusyIndicator Static_Loader;        
        readonly Blankets blankets = new Blankets();
        public static Frame Static_Main_Frame;
        public MainWindow()
        {
            InitializeComponent();
            Static_LoadingBorder = LoadingPanel;
            Static_Loader = loader;
            Static_Main_Frame = Main;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {   int currentyear = DateTime.Now.Year;
            await Requests.Login();
            string url = Urls.URL_Blankets;
            await Requests.Get_Blankets(url+"?search=");
            await Requests.Get_Stats(currentyear);
            await Requests.Get_Years();
            Main.Content = blankets;        
        }
        private void Expander1_Expanded(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ExpanderIcon.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Expander2.IsExpanded = false;
            Expander3.IsExpanded = false;
            Expander4.IsExpanded = false;

        }
        private void Expander1_Collapsed(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ExpanderIcon.Foreground = new SolidColorBrush(Colors.Black);
        }
        private async void Blanketbtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Ykrarhatlar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Blanketbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));
            await Requests.Get_Blankets(Urls.URL_Blankets);
            Main.Content = blankets;
        }
        private void Hasabaalmakbtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Hasabalmak.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Hasabaalmakbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));
        }
        private void Ahliatlarbtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Ahliatlar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Ahliatlarbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));
            
        }
        private void Expander2_Expanded(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ExpanderIcon2.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Expander1.IsExpanded = false;
            Expander3.IsExpanded = false;
            Expander4.IsExpanded = false;
        }
        private void Expander2_Collapsed(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ExpanderIcon2.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void DNKbtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            DNK.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            DNKbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));

        }
        private void Bazabtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Garysyk.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Bazabtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));
        }
        private void Konebtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Kone.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Konebtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));            
        }
        private void Tazebtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Taze.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Tazebtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));            
        }
        private void Karantinbtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Karantin.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Karantinbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));
        }
        private void Expander3_Expanded(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ExpanderIcon3.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Expander1.IsExpanded = false;
            Expander2.IsExpanded = false;
            Expander4.IsExpanded = false;
        }
        private void Expander3_Collapsed(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ExpanderIcon3.Foreground = new SolidColorBrush(Colors.Black);
        }   

        private void Dernewbtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Dernewtb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Dernewbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));           
        }  

        private void Hasabatbtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            Hasabat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Hasabatbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));
        }
        private void Expander4_Expanded(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ExpanderIcon4.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Expander1.IsExpanded = false;
            Expander2.IsExpanded = false;
            Expander3.IsExpanded = false;
        }
        private void Expander4_Collapsed(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ExpanderIcon4.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void Ulanyjylarbtn_Click(object sender, RoutedEventArgs e)
        {
            ColorsBlack();
            ulanyjylar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#007602"));
            Ulanyjylarbtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ECFFED"));
            
        }
       
        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }

        private void Logout()
        {
            var result = MessageBox.Show("Hasabyňyzdan çykmak isleýärsiňizmi?", "Bedew",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                //   ConnectionClass.Logout();
                //   ShowLoginWindow();
            }
        }

        private void ColorsBlack()
        {
            Ykrarhatlar.Foreground = new SolidColorBrush(Colors.Black);
            Hasabalmak.Foreground = new SolidColorBrush(Colors.Black);
            Ahliatlar.Foreground = new SolidColorBrush(Colors.Black);

            DNK.Foreground = new SolidColorBrush(Colors.Black);
            Kone.Foreground = new SolidColorBrush(Colors.Black);
            Taze.Foreground = new SolidColorBrush(Colors.Black);
            Garysyk.Foreground = new SolidColorBrush(Colors.Black);
            Karantin.Foreground = new SolidColorBrush(Colors.Black);
            Dernewtb.Foreground = new SolidColorBrush(Colors.Black);
            ulanyjylar.Foreground = new SolidColorBrush(Colors.Black);
            Hasabat.Foreground = new SolidColorBrush(Colors.Black);
           
            ExpanderIcon.Background = new SolidColorBrush(Colors.Transparent);
            ExpanderIcon2.Background = new SolidColorBrush(Colors.Transparent);
            ExpanderIcon3.Background = new SolidColorBrush(Colors.Transparent);
            ExpanderIcon4.Background = new SolidColorBrush(Colors.Transparent);

            Blanketbtn.Background = new SolidColorBrush(Colors.Transparent);
            Hasabaalmakbtn.Background = new SolidColorBrush(Colors.Transparent);
            Ahliatlarbtn.Background = new SolidColorBrush(Colors.Transparent);
            DNKbtn.Background = new SolidColorBrush(Colors.Transparent);
            Bazabtn.Background = new SolidColorBrush(Colors.Transparent);
            Konebtn.Background = new SolidColorBrush(Colors.Transparent);
            Tazebtn.Background = new SolidColorBrush(Colors.Transparent);
            Karantinbtn.Background = new SolidColorBrush(Colors.Transparent);
            Dernewbtn.Background = new SolidColorBrush(Colors.Transparent);           
            Hasabatbtn.Background = new SolidColorBrush(Colors.Transparent);
            Ulanyjylarbtn.Background = new SolidColorBrush(Colors.Transparent);
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
        private void Maximizebtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Minimizebtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Closebtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Programmadan çykmak isleýärsiňizmi?", "Bedew",
               MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }
    }
}