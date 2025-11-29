using FrontB.Classes;
using FrontB.Pages;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

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
                                    LoginResponse login_model = JsonConvert.DeserializeObject<LoginResponse>(http_response);

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
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }
            }
        }

        #endregion

        #region Blankets
        async public static Task Get_Blankets(string url)
        {
            
            if (!string.IsNullOrWhiteSpace(StaticVariables.access_token))
            {
                MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Cupertino;
                MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", StaticVariables.access_token);
                    try
                    {   using (HttpResponseMessage response = await client.GetAsync(url))
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    string result = await content.ReadAsStringAsync();
                                    var Root = JsonConvert.DeserializeObject<BlanketsResponse>(result);

                                    App.Current.Dispatcher.Invoke((Action)delegate
                                    {
                                        Blankets.blank.Clear();

                                        Blankets.Static_DgBlankets.ItemsSource = null;
                                        if (Root.total != 0)
                                        {
                                            int counter = 1;

                                            var filterItems = new List<int>();
                                            var filterItems2 = new List<string>();
                                            var filterItems3 = new List<string>();
                                            var filterItems4 = new List<string>();
                                            var filterItems5 = new List<string>();

                                            foreach (var item in Root.list)
                                            {
                                                Blankets.blank.Add(new BlanketsClass(counter, item.guid, item.ykrarhat, item.ysene, item.san, item.atsan, item.bellik));
                                                

                                                if (!filterItems.Contains(counter))
                                                    filterItems.Add(counter);

                                                if (!filterItems2.Contains(item.ykrarhat))
                                                    filterItems2.Add(item.ykrarhat);

                                                if (!filterItems3.Contains(item.ysene))
                                                    filterItems3.Add(item.ysene);

                                                string sanStr = item.san.ToString();
                                                if (!filterItems4.Contains(sanStr))
                                                    filterItems4.Add(sanStr);

                                                string atsanStr = item.atsan.ToString();
                                                if (!filterItems5.Contains(atsanStr))
                                                    filterItems5.Add(atsanStr);
                                                counter++;
                                            }

                                            Blankets.Static_DgBlankets.ItemsSource = Blankets.blank;

                                            Blankets.Static_DgBlankets.DataContext = new
                                            {
                                                FilterItems = filterItems,   
                                                FilterItems2 = filterItems2, 
                                                FilterItems3 = filterItems3, 
                                                FilterItems4 = filterItems4, 
                                                FilterItems5 = filterItems5  
                                            };
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
                        MessageBox.Show("Ошибка HttpRequestException: " + request_ex.Message);
                    }
                }

                MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Tokeniň wagty gutardy, programmany täzeden açyň!");
            }

            
        }

        /*   async public static void Add_Rank(string name, string full_name)
          {
              using (var form = new MultipartFormDataContent())
              {
                  form.Add(new StringContent(name), "name");
                  form.Add(new StringContent(full_name), "full_name");

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

                              MainWindow.Static_Loader.AnimationType = Syncfusion.Windows.Controls.Notification.AnimationTypes.Pen;
                              MainWindow.Static_LoadingBorder.Visibility = Visibility.Visible;

                              using (HttpResponseMessage response = await client.PostAsync(Urls.URL_Blankets, form))
                              {
                                  using (HttpContent content = response.Content)
                                  {
                                      string answer = await content.ReadAsStringAsync();
                                      if (answer.Contains("\"answer\":\"ok\""))
                                      {
                                          Get_Blankets();
                                          MessageBox.Show("Täze ykrarhat goşuldy!: \n" + answer, "Goşuldy", MessageBoxButton.OK, MessageBoxImage.Information);
                                      }
                                      else
                                      {
                                          MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                          MessageBox.Show("Ýalňyşlyk: \n" + answer);
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

         async public static void Update_Rank(string name, string full_name, string rank_id)
          {
              using (var form = new MultipartFormDataContent())
              {
                  form.Add(new StringContent(name), "name");
                  form.Add(new StringContent(full_name), "full_name");

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

                              using (HttpResponseMessage response = await client.PutAsync(Urls.URL_Blankets + rank_id + "/", form))
                              {
                                  using (HttpContent content = response.Content)
                                  {
                                      string answer = await content.ReadAsStringAsync();
                                      if (answer.Contains("\"answer\":\"ok\""))
                                      {
                                          Get_Ranks();
                                          //MessageBox.Show("Harby adyň maglumatlary üýtgedildi!: " + answer, "Üýtgedildi", MessageBoxButton.OK, MessageBoxImage.Information);
                                      }
                                      else
                                      {
                                          MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                          MessageBox.Show("Ýalňyşlyk: \n" + answer);
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

          async public static void Delete_Rank(string rank_id)
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

                          using (HttpResponseMessage response = await client.DeleteAsync(Urls.URL_Blankets + rank_id + "/"))
                          {
                              using (HttpContent content = response.Content)
                              {
                                  string answer = await content.ReadAsStringAsync();
                                  if (answer.Contains("\"answer\":\"ok\""))
                                  {
                                      Get_Blankets();
                                      MessageBox.Show("Maglumatlar üstünlikli pozuldy!: " + answer, "Üýtgedildi", MessageBoxButton.OK, MessageBoxImage.Information);
                                  }
                                  else
                                  {
                                      MainWindow.Static_LoadingBorder.Visibility = Visibility.Collapsed;
                                      MessageBox.Show("Ýalňyşlyk: \n" + answer);
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
