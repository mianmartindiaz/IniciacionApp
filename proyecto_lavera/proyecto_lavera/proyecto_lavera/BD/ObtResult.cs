using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace proyecto_lavera.BD
{
    class ObtResult
    {
        public async Task<List<T>> GetList<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                var uri = new Uri(url);
                var response = await client.GetAsync(uri);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var  jsonstring = await response.Content.ReadAsStringAsync();



                    return JsonConvert.DeserializeObject<List<T>>(jsonstring);
                }
                else { return default (List<T>); }

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return default(List<T>);
            }

        }

        public async Task<T> GetObject<T>(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                var uri = new Uri(url);
                var response = await client.GetAsync(uri);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonstring = await response.Content.ReadAsStringAsync();


                    return JsonConvert.DeserializeObject<T>(jsonstring);
                }
                else { return default(T); }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("fallo");
                System.Diagnostics.Debug.WriteLine(e.Message);
                return default(T);
            }

        }
    }
}

