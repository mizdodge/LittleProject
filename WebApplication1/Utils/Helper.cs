using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Utils
{
    public class Helper
    {
        public static async Task<string> PostDataAsync(dynamic jsonString,string path)
        {
            var json = JsonConvert.SerializeObject(jsonString);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage result = await client.PostAsync(path, content);
            var jsonresult = await result.Content.ReadAsStringAsync();
            return jsonresult;
        }

        public static async Task<string> GetDataAsync(dynamic dataString, string path)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage result = await client.GetAsync(path+dataString);
            var jsonresult = await result.Content.ReadAsStringAsync();
            return jsonresult;
        }

        public static async Task<string> DeleteDataAsync(dynamic dataString, string path)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage result = await client.DeleteAsync(path+dataString);
            var jsonresult = await result.Content.ReadAsStringAsync();
            return jsonresult;
        }

        public static async Task<string> PutDataAsync(dynamic jsonString, string path)
        {
            var json = JsonConvert.SerializeObject(jsonString);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage result = await client.PutAsync(path,content);
            var jsonresult = await result.Content.ReadAsStringAsync();
            return jsonresult;
        }
    }
}
