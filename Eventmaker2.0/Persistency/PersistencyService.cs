using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Automation;
using Eventmaker2._0.Model;
using Newtonsoft.Json;

namespace Eventmaker2._0.Persistency
{

    //Notice: NuGet - Json.Net from Newtonsoft is installed, provides: JsonConvert 

    class PersistencyService
    {
        const string ServerUrl = "HTTP://localhost:6463";
        //private static string eventFileName = "EventsAsJson.dat";

        public static async void SaveEventsAsJsonAsync (Event eEvent)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                // Sætter base adress
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                // Fortæller hvilken fil der skal retureres i dette tilfælde Json
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));

                // Post = Create
                // Put = Update
                try
                {
                    await client.PostAsJsonAsync("api/Events", eEvent);


                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }

        public static async Task<List<Event>> LoadEventsFromJsonAsync()
        {
             HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                // Sætter base adress
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                // Bestemmer hvilken fil type vi gemmer i
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Applicaton/json"));

                try
                {
                    var response = client.GetAsync("api/events").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var eventdata = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
                            return  eventdata.ToList();
                    }
                }
                catch (Exception)
                { 
                    
                    throw;
                }
                return null;
            }

        }

        public static async void UpdateEvent(Event uEvent)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                // Sætter base adress
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                // Bestemmer hvilken fil type vi gemmer i
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Applicaton/json"));

                try
                {
                    var response = client.PutAsJsonAsync(ServerUrl, uEvent).Result;
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
                }
            }

        public static async void RemoveEvent(Event rEvent)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                // Sætter base adress
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                // Bestemmer hvilken fil type vi gemmer i
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Applicaton/json"));

                try
                {
                   await client.DeleteAsync("api/events/"+ rEvent.Id);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                }
        }
        // Metode til at gemme i fil
        //public static async void SaveEventsAsJsonAsync(ObservableCollection<Event> events)
        //{
        //    string eventsJsonString = JsonConvert.SerializeObject(events);
        //    SerializeEventsFileAsync(eventsJsonString, eventFileName);
        //}

        //public static async Task<List<Event>> LoadEventsFromJsonAsync()
        //{
        //    string eventsJsonString = await DeSerializeEventsFileAsync(eventFileName);
        //    if (eventsJsonString != null)
        //        return (List<Event>)JsonConvert.DeserializeObject(eventsJsonString, typeof(List<Event>));
        //    return null;
        //}


        //public static async void SerializeEventsFileAsync(string eventsString, string fileName)
        //{
        //    StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        //    await FileIO.WriteTextAsync(localFile, eventsString);
        //}

        //public static async Task<string> DeSerializeEventsFileAsync(String fileName)
        //{
        //    try
        //    {
        //        StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
        //        return await FileIO.ReadTextAsync(localFile);
        //    }
        //    catch (FileNotFoundException ex)
        //    {

        //        MessageDialogHelper.Show("File of Events not found! - Loading for the first time?", "File not found!");
        //        return null;
        //    }
        //}

        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }



    }


}
