using FrontB.Classes;
using FrontB.Pages;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FrontB.Helpers
{
    public class Requests
    {
        #region Login
        async public static Task Login()
        {        
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent("admin"), "username");
                form.Add(new StringContent("admin123"), "password");
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        using (HttpResponseMessage response = await client.PostAsync(Urls.URL_Login, form))
                        {
                          
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string http_response = await content.ReadAsStringAsync();
                                    LoginResponse? login_model = JsonConvert.DeserializeObject<LoginResponse>(http_response);

                                    if (login_model == null || login_model.data == null)
                                    {
                                        MessageBox.Show("Serwerden näsazlykly jogap alyndy!", "Näsazlyk", MessageBoxButton.OK, MessageBoxImage.Stop);
                                        return;
                                    }
                                    StaticVariables.access_token = login_model.data.token;
                                    StaticVariables.refresh_token = login_model.data.refresh_token;                
                                   
                                }   
                            }
                            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                MessageBox.Show("Ulanyjy maglumatlaryňyzy dogry giriziň!", "Ulanyjy tapylmady", MessageBoxButton.OK, MessageBoxImage.Stop);
                            }
                            else
                            {
                                MessageBox.Show("Tor ulgamyňyzda ýa-da serwerde näsazlyk bar", "Serwere baglanmady!", MessageBoxButton.OK, MessageBoxImage.Stop);
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
            }
        }

        #endregion

        #region Stats
        async public static Task Get_Stats(int year)
        {

            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Cupertino;
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                }

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                        using (HttpResponseMessage response = await client.GetAsync(Urls.Url_Stats + year))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = await content.ReadAsStringAsync();
                                    var Root = JsonConvert.DeserializeObject<StatsResponse>(result);

                                    if (Root != null)
                                    {
                                        App.Current.Dispatcher.Invoke((Action)delegate
                                        {
                                            if (Blankets.Static_ProgressBar != null && Blankets.Static_ProgressBar1 != null && Blankets.Static_ProgressBar2 != null)
                                            {
                                                Blankets.Static_ProgressBar.Progress = Root.stat1;
                                                Blankets.Static_ProgressBar.Maximum = Root.stat1 * 2;
                                                Blankets.Static_ProgressBar1.Progress = Root.stat2;
                                                Blankets.Static_ProgressBar2.Progress = Root.stat3;
                                            }
                                        });
                                    }
                                }
                            }
                            else if (response.StatusCode == HttpStatusCode.Forbidden)
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Rugsat ýok (Get_Blankets): " + await content.ReadAsStringAsync());
                            }
                            else
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Ýalňyşlyk: " + await content.ReadAsStringAsync());
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
                if (MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }


        }
        async public static Task Get_Years()
        {

            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Cupertino;
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                }
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                        using (HttpResponseMessage response = await client.GetAsync(Urls.URL_StatYears))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = await content.ReadAsStringAsync();
                                    var Root = JsonConvert.DeserializeObject<YearResponse>(result);

                                    if (Root != null)
                                    {
                                        if (Blankets.Static_YearsComboBox != null)
                                            Blankets.Static_YearsComboBox.Items.Clear();
                                        App.Current.Dispatcher.Invoke((Action)delegate
                                        {
                                            if (Root.data != null)
                                            {
                                                foreach (var item in Root.data)
                                                {
                                                    if (Blankets.Static_YearsComboBox != null)
                                                        Blankets.Static_YearsComboBox.Items.Add(new ComboBoxItem() { Content = item });
                                                }
                                            }

                                        });
                                    }
                                }
                            }
                            else if (response.StatusCode == HttpStatusCode.Forbidden)
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Rugsat ýok (Get_Blankets): " + await content.ReadAsStringAsync());
                            }
                            else
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Ýalňyşlyk: " + await content.ReadAsStringAsync());
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
                if (MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }


        }
        async public static Task Get_Colors()
        {
            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Cupertino;
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                }

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                        using (HttpResponseMessage response = await client.GetAsync(Urls.URL_Colors))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = await content.ReadAsStringAsync();
                                    var Root = JsonConvert.DeserializeObject<ColorsResponse>(result);

                                    if (Root != null)
                                    {            
                                        MainWindow.Static_Colors?.Clear();
                                                                                  
                                        App.Current.Dispatcher.Invoke((Action)delegate
                                        {
                                            if (Root.colors != null)
                                            {
                                                foreach (var item in Root.colors)
                                                {                                                  
                                                    MainWindow.Static_Colors?.Add(item.renk);
                                                }
                                            }
                                        });
                                    }
                                }
                            }
                            else if (response.StatusCode == HttpStatusCode.Forbidden)
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Rugsat ýok (Get_Blankets): " + await content.ReadAsStringAsync());
                            }
                            else
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Ýalňyşlyk: " + await content.ReadAsStringAsync());
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
                if (MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }


        }
        async public static Task Get_Owners()
        {
            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Cupertino;
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                }

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                        using (HttpResponseMessage response = await client.GetAsync(Urls.URL_Owners))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = await content.ReadAsStringAsync();
                                    var Root = JsonConvert.DeserializeObject<OwnersResponse>(result);

                                    if (Root != null)
                                    {                                       
                                        MainWindow.Static_Owners?.Clear();
                                        App.Current.Dispatcher.Invoke((Action)delegate
                                        {
                                            if (Root.owners != null)
                                            {
                                                foreach (var item in Root.owners)
                                                {
                                                    MainWindow.Static_Owners?.Add(item.owner);
                                                }
                                            }
                                        });
                                    }
                                }
                            }
                            else if (response.StatusCode == HttpStatusCode.Forbidden)
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Rugsat ýok (Get_Blankets): " + await content.ReadAsStringAsync());
                            }
                            else
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Ýalňyşlyk: " + await content.ReadAsStringAsync());
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
                if (MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }


        }
        async public static Task Get_Maxid()
        {

            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Cupertino;
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                }

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                        using (HttpResponseMessage response = await client.GetAsync(Urls.URL_Maxid))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = await content.ReadAsStringAsync();
                                    var Root = JsonConvert.DeserializeObject<int>(result);
                                    
                                    App.Current.Dispatcher.Invoke((Action)delegate
                                    {                                     
                                      MainWindow.Static_Maxid= Root;
                                       
                                    });
                                    
                                }
                            }
                            else if (response.StatusCode == HttpStatusCode.Forbidden)
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Rugsat ýok (Get_Blankets): " + await content.ReadAsStringAsync());
                            }
                            else
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Ýalňyşlyk: " + await content.ReadAsStringAsync());
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
                if (MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }


        }
        #endregion

        #region Blankets
        async public static Task Get_Blankets(string url)
        {
            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Cupertino;
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                }

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                        using (HttpResponseMessage response = await client.GetAsync(url))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = await content.ReadAsStringAsync();
                                    var Root = JsonConvert.DeserializeObject<BlanketsResponse>(result);

                                    App.Current.Dispatcher.Invoke((Action)delegate
                                    {
                                        Blankets.Blank.Clear();
                                        MainWindow.Static_Blankets?.Clear();
                                        if (Blankets.Static_DgBlankets != null)
                                        {
                                            Blankets.Static_DgBlankets.ItemsSource = null;
                                            if (Root?.total != 0)
                                            {
                                                var filterItems = new List<int?>();
                                                var filterItems2 = new List<string?>();
                                                var filterItems3 = new List<string?>();
                                                var filterItems4 = new List<string?>();
                                                var filterItems5 = new List<string?>();
                                                if (Root?.list != null)
                                                {
                                                    int tb = 1;
                                                    foreach (var item in Root.list)
                                                    {   
                                                        Blankets.Blank.Add(new BlanketsClass(item?.id, tb, item?.ykrarhat,
                                                             item?.sene, item?.atsan, item?.atcount, item?.bellik, item?.horses));

                                                        if (item?.ykrarhat!=null)
                                                        {
                                                            MainWindow.Static_Blankets?.Add(item.ykrarhat); 
                                                        }
                                                        if (!filterItems.Contains(item?.tb))
                                                            filterItems.Add(item?.tb);

                                                        if (!filterItems2.Contains(item?.ykrarhat))
                                                            filterItems2.Add(item?.ykrarhat);

                                                        if (!filterItems3.Contains(item?.sene))
                                                            filterItems3.Add(item?.sene);

                                                        string? sanStr = item?.atsan.ToString();
                                                        if (!filterItems4.Contains(sanStr))
                                                            filterItems4.Add(sanStr);

                                                        string? atsanStr = item?.atcount.ToString();
                                                        if (!filterItems5.Contains(atsanStr))
                                                            filterItems5.Add(atsanStr);
                                                        tb++;
                                                    }

                                                    Blankets.Static_DgBlankets.ItemsSource = Blankets.Blank;
                                                    
                                                    Blankets.Static_DgBlankets.DataContext = new
                                                    {
                                                        FilterItems = filterItems,
                                                        FilterItems2 = filterItems2,
                                                        FilterItems3 = filterItems3,
                                                        FilterItems4 = filterItems4,
                                                        FilterItems5 = filterItems5
                                                    };
                                                }
                                            }
                                        }
                                    });
                                }
                            }
                            else if (response.StatusCode == HttpStatusCode.Forbidden)
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Rugsat ýok (Get_Blankets): " + await content.ReadAsStringAsync());
                            }
                            else
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Ýalňyşlyk: " + await content.ReadAsStringAsync());
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
                if (MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }

            
        }
        async public static Task Add_Blankets(string ykrarhat, string sene, string atsan)
        {           
            using (var form = new MultipartFormDataContent())
            {   
                form.Add(new StringContent(ykrarhat), "ykrarhat");               
                form.Add(new StringContent(sene), "sene");
                form.Add(new StringContent(atsan), "atsan");

                if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                        try
                        {
                            var a = form.ToList<HttpContent>();
                            for (int i = 0; i < a.Count(); i++)
                            {
                                Debug.WriteLine("Form: " + "\nid: " + a[i].ReadAsStringAsync().Id.ToString() + " Result: " + a[i].ReadAsStringAsync().Result.ToString());
                            }
                            if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                            {
                                MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Pen;
                                MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                            }

                            using (HttpResponseMessage response = await client.PostAsync(Urls.URL_Blankets, form))
                            {
                               
                                if (response.IsSuccessStatusCode)
                                {                                   
                                   await Get_Blankets(Urls.URL_Blankets);
                                   MessageBox.Show("Täze ykrarhat goşuldy!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Information);                              
                                    
                                }
                                else
                                {   if (MainWindow.Static_LoadingBorder != null)
                                        MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                    MessageBox.Show("Ýalňyşlyk: \n" + await response.Content.ReadAsStringAsync());
                                }                             
                                
                            }
                        }
                        catch (WebException ex)
                        {  
                            if (MainWindow.Static_LoadingBorder != null)
                                MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                            WebExceptionStatus status = ex.Status;
                            if (status == WebExceptionStatus.ProtocolError)
                            {
                                HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                                MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                            }
                            else
                            {
                                MessageBox.Show("Ошибка WebException: " + ex.Message);
                            }
                        }
                        catch (HttpRequestException request_ex)
                        {
                            if (MainWindow.Static_LoadingBorder != null)
                                MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                            MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
                }
            }
        }
        async public static Task Update_Blankets(string? blanketid,string? ykrarhat, string? sene, string? atsan,string? atcount)
        {      
              using (var form = new MultipartFormDataContent())
              {   if (ykrarhat!=null)               
                    form.Add(new StringContent(ykrarhat), "ykrarhat");
                  if (sene!=null)
                    form.Add(new StringContent(sene), "sene");
                  if (atsan!=null)
                    form.Add(new StringContent(atsan), "atsan");
                  if (atcount!=null)
                    form.Add(new StringContent(atcount), "atcount");

                  if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
                  {
                      using (HttpClient client = new HttpClient())
                      {
                          client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                          try
                          {
                              var a = form.ToList<HttpContent>();
                              for (int i = 0; i < a.Count(); i++)
                              {
                                  Debug.WriteLine("Form: " + "id: " + a[i].ReadAsStringAsync().Id.ToString() + " Result: " + a[i].ReadAsStringAsync().Result.ToString());
                              }
                              
                              if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                              {
                                  MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Pen;
                                  MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                              }                               

                              using (HttpResponseMessage response = await client.PutAsync(Urls.URL_Blankets + blanketid + "/", form))
                              {
                                  using (HttpContent content = response.Content)
                                  {
                                      if(response.IsSuccessStatusCode) 
                                      {
                                         await Get_Blankets(Urls.URL_Blankets);
                                         MessageBox.Show("Maglumatlar üstünlikli üýtgedildi!: ", "Bedew", MessageBoxButton.OK, MessageBoxImage.Information);
                                      }
                                      else
                                      {
                                        if (MainWindow.Static_LoadingBorder != null)
                                            MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                        MessageBox.Show("Ýalňyşlyk: \n" + await response.Content.ReadAsStringAsync());
                                      }
                                  }
                              }
                          }
                          catch (WebException ex)
                          {
                              if (MainWindow.Static_LoadingBorder != null)
                                  MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                              WebExceptionStatus status = ex.Status;
                              if (status == WebExceptionStatus.ProtocolError)
                              {
                                  HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                                  MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                              }
                              else
                              {
                                  MessageBox.Show("Ошибка WebException: " + ex.Message);
                              }
                          }
                          catch (HttpRequestException request_ex)
                          {
                                if(MainWindow.Static_LoadingBorder!=null)
                                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                              MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                          }
                      }
                  }
                  else
                  {
                      MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
                  }
              }
          }
        async public static Task Delete_Blanket(string? blanekt_id)
        {
            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                      if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                        {
                            MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Delete;
                            MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                        }

                        using (HttpResponseMessage response = await client.DeleteAsync(Urls.URL_Blankets + blanekt_id + "/"))
                          {
                              using (HttpContent content = response.Content)
                              {                                  
                                  if (response.IsSuccessStatusCode)
                                  {
                                      await Get_Blankets(Urls.URL_Blankets);
                                      MessageBox.Show("Maglumatlar üstünlikli pozuldy!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Information);
                                  }
                                  else
                                  {     if (MainWindow.Static_LoadingBorder != null)
                                        MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                      MessageBox.Show("Ýalňyşlyk: \n" + await response.Content.ReadAsStringAsync());
                                  }
                              }
                          }
                    }
                    catch (WebException ex)
                    { 
                      if (MainWindow.Static_LoadingBorder != null)
                          MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                          {
                              HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                              MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                          }
                        else
                          {
                              MessageBox.Show("Ошибка WebException: " + ex.Message);
                          }
                    }
                    catch (HttpRequestException request_ex)
                    {   
                        if (MainWindow.Static_LoadingBorder != null)
                          MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }
        }
        #endregion

        #region Ahliatlar
        async public static Task Get_JournalHorses(string url)
        {
            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Cupertino;
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                }
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                        using (HttpResponseMessage response = await client.GetAsync(url))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = await content.ReadAsStringAsync();
                                    var Root = JsonConvert.DeserializeObject<JHorsesResponse>(result);

                                    App.Current.Dispatcher.Invoke((Action)delegate
                                    {
                                        if (JournalHorses.Static_DgJournalHorses != null)
                                        {
                                            JournalHorses.Horseinfo.Clear();
                                            JournalHorses.Static_DgJournalHorses.ItemsSource = null;
                                            if (Root?.total != 0)
                                            {                                                
                                                var filterItems = new List<uint?>();
                                                var filterItems2 = new List<string?>();
                                                var filterItems3 = new List<string?>();
                                                var filterItems4 = new List<string?>();
                                                var filterItems5 = new List<string?>();
                                                var filterItems6 = new List<string?>();
                                                var filterItems7 = new List<string?>();
                                                var filterItems8 = new List<string?>();
                                                var filterItems9 = new List<string?>();
                                                var filterItems10 = new List<string?>();
                                                var filterItems11 = new List<string?>();
                                                if (Root?.list != null)
                                                {
                                                    int tb=Root.total;

                                                    foreach (var item in Root.list)
                                                    {
                                                        JournalHorses.Horseinfo.Add(new JournalHorsesClass(item.id, tb,
                                                            item.lakamy, item.doglanyyl, item.atasy, item.enesi,
                                                            item.jynsy, item.renki, item.biomaterial, item.biosan,
                                                            item.probnomer, item.eyesi, item.nyshanlar, item.bellik,
                                                            item.blanket?.ykrarhat, item.blanket?.sene));

                                                        if (!filterItems.Contains(item?.tb))
                                                            filterItems.Add(item?.tb); 
                                                        if (!filterItems2.Contains(item?.lakamy))
                                                            filterItems2.Add(item?.lakamy);
                                                        if (!filterItems3.Contains(item?.doglanyyl.ToString()))
                                                            filterItems3.Add(item?.doglanyyl.ToString());
                                                        if (!filterItems4.Contains(item?.atasy))
                                                            filterItems4.Add(item?.atasy);
                                                        if (!filterItems5.Contains(item?.enesi))
                                                            filterItems5.Add(item?.enesi);
                                                        if (!filterItems6.Contains(item?.jynsy))
                                                            filterItems6.Add(item?.jynsy);
                                                        if (!filterItems7.Contains(item?.renki))
                                                            filterItems7.Add(item?.renki);
                                                        if (!filterItems8.Contains(item?.blanket?.ykrarhat))
                                                            filterItems8.Add(item?.blanket?.ykrarhat);
                                                        if (!filterItems9.Contains(item?.blanket?.sene))
                                                            filterItems9.Add(item?.blanket?.sene);
                                                        if (!filterItems10.Contains(item?.eyesi))
                                                            filterItems10.Add(item?.eyesi);
                                                        if (!filterItems11.Contains(item?.bellik))
                                                            filterItems11.Add(item?.bellik);

                                                        tb--;
                                                    }
                                                }

                                                JournalHorses.Static_DgJournalHorses.ItemsSource = JournalHorses.Horseinfo;

                                                JournalHorses.Static_DgJournalHorses.DataContext = new
                                                {
                                                    FilterItems = filterItems,
                                                    FilterItems2 = filterItems2,
                                                    FilterItems3 = filterItems3,
                                                    FilterItems4 = filterItems4,
                                                    FilterItems5 = filterItems5,
                                                    FilterItems6 = filterItems6,
                                                    FilterItems7 = filterItems7,
                                                    FilterItems8 = filterItems8,
                                                    FilterItems9 = filterItems9,
                                                    FilterItems10 = filterItems10,
                                                    FilterItems11 = filterItems11
                                                };
                                            }
                                        }

                                    });
                                }
                            }
                            else if (response.StatusCode == HttpStatusCode.Forbidden)
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Rugsat ýok (Get_JournalHorses): " + await content.ReadAsStringAsync());
                            }
                            else
                            {
                                HttpContent content = response.Content;
                                MessageBox.Show("Ýalňyşlyk: " + await content.ReadAsStringAsync());
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
                if (MainWindow.Static_LoadingBorder != null)
                {
                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }
        }
                
        async public static Task Add_JournalHorses(string lakamy,string? doglanyyl, string atasy,string enesi,string jynsy,
            string renki,string biomaterial,string biosan,string probnomer,string eyesi,string nyshanlar,string bellik,string? blanketid,int? atsan)
        {
            if (doglanyyl!=null && blanketid!=null)

            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(lakamy), "lakamy");
                form.Add(new StringContent(doglanyyl), "doglanyyl");
                form.Add(new StringContent(atasy), "atasy");
                form.Add(new StringContent(enesi), "enesi");
                form.Add(new StringContent(jynsy), "jynsy");
                form.Add(new StringContent(renki), "renki");
                form.Add(new StringContent(biomaterial), "biomaterial");
                form.Add(new StringContent(biosan), "biosan");
                form.Add(new StringContent(probnomer), "probnomer");
                form.Add(new StringContent(eyesi), "eyesi");
                form.Add(new StringContent(nyshanlar), "nyshanlar");
                form.Add(new StringContent(bellik), "bellik");
                form.Add(new StringContent(blanketid), "blanketguid");

                if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                        try
                        {
                            var a = form.ToList<HttpContent>();
                            for (int i = 0; i < a.Count(); i++)
                            {
                                Debug.WriteLine("Form: " + "\nid: " + a[i].ReadAsStringAsync().Id.ToString() + " Result: " + a[i].ReadAsStringAsync().Result.ToString());
                            }
                            if (MainWindow.Static_Loader != null && MainWindow.Static_LoadingBorder != null)
                            {
                                MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Pen;
                                MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;
                            }

                            using (HttpResponseMessage response = await client.PostAsync(Urls.URL_JournalHorses, form))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    await Get_Blankets(Urls.URL_Blankets);
                                    MessageBox.Show("Täze ykrarhat goşuldy!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Information);
                                    atsan++;
                                    await Update_Blankets(blanketid,null,null,null,atsan.ToString());
                                }
                                else
                                {   if (MainWindow.Static_LoadingBorder != null)
                                        MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                    MessageBox.Show("Ýalňyşlyk: \n" + await response.Content.ReadAsStringAsync());
                                }
                            }
                        }
                        catch (WebException ex)
                        {   if (MainWindow.Static_LoadingBorder != null)
                                MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                            WebExceptionStatus status = ex.Status;
                            if (status == WebExceptionStatus.ProtocolError)
                            {
                                HttpWebResponse? httpResponse = (HttpWebResponse?)ex.Response;
                                MessageBox.Show("Статусный код ошибки: " + (int?)httpResponse?.StatusCode + " - " + httpResponse?.StatusCode);
                            }
                            else
                            {
                                MessageBox.Show("Ошибка WebException: " + ex.Message);
                            }
                        }
                        catch (HttpRequestException request_ex)
                        {   if (MainWindow.Static_LoadingBorder != null)
                                MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                            MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
                }
            }
        }

        /*async public static Task Update_Blankets(string blanketid, string ykrarhat, string ysene, int san)
        {
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(ykrarhat), "ykrarhat");
                form.Add(new StringContent(ysene), "ysene");
                form.Add(new StringContent(san.ToString()), "san");

                if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                        try
                        {
                            var a = form.ToList<HttpContent>();
                            for (int i = 0; i < a.Count(); i++)
                            {
                                Debug.WriteLine("Form: " + "id: " + a[i].ReadAsStringAsync().Id.ToString() + " Result: " + a[i].ReadAsStringAsync().Result.ToString());
                            }

                            MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Pen;
                            MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;

                            using (HttpResponseMessage response = await client.PutAsync(Urls.URL_Blankets + blanketid + "/", form))
                            {
                                using (HttpContent content = response.Content)
                                {
                                    if (response.IsSuccessStatusCode)
                                    {
                                        await Get_Blankets(Urls.URL_Blankets);
                                        MessageBox.Show("Maglumatlar üstünlikli üýtgedildi!: ", "Bedew", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                    else
                                    {
                                        MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                        MessageBox.Show("Ýalňyşlyk: \n" + await response.Content.ReadAsStringAsync());
                                    }
                                }
                            }
                        }
                        catch (WebException ex)
                        {
                            MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                            WebExceptionStatus status = ex.Status;
                            if (status == WebExceptionStatus.ProtocolError)
                            {
                                HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                                MessageBox.Show("Статусный код ошибки: " + (int)httpResponse.StatusCode + " - " + httpResponse.StatusCode);
                            }
                            else
                            {
                                MessageBox.Show("Ошибка WebException: " + ex.Message);
                            }
                        }
                        catch (HttpRequestException request_ex)
                        {
                            MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                            MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
                }
            }
        }
        async public static Task Delete_Blanket(string blanekt_id)
        {
            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {
                        MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Delete;
                        MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;

                        using (HttpResponseMessage response = await client.DeleteAsync(Urls.URL_Blankets + blanekt_id + "/"))
                        {
                            using (HttpContent content = response.Content)
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    await Get_Blankets(Urls.URL_Blankets);
                                    MessageBox.Show("Maglumatlar üstünlikli pozuldy!", "Bedew", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                    MessageBox.Show("Ýalňyşlyk: \n" + await response.Content.ReadAsStringAsync());
                                }
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                        WebExceptionStatus status = ex.Status;
                        if (status == WebExceptionStatus.ProtocolError)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                            MessageBox.Show("Статусный код ошибки: " + (int)httpResponse.StatusCode + " - " + httpResponse.StatusCode);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка WebException: " + ex.Message);
                        }
                    }
                    catch (HttpRequestException request_ex)
                    {
                        MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }
        } */
        #endregion
    }
}
