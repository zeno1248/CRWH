using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = ConfigurationManager.AppSettings["ApiURL"];
            string consumerType = ConfigurationManager.AppSettings["ConsumerType"].ToLower();
            IApiConsumer consumer = null;

            if (consumerType == "console")
                consumer = new ConsoleConsumer();
            else if (consumerType == "Database")
                consumer = new DBConsumer();

            ApiClient client = new ApiClient(url, consumer);
            client.CallApi();
        }
    }

    public class ApiClient
    {
        IApiConsumer _consumer;
        string _apiURL;

        public ApiClient(string apiURL, IApiConsumer consumer)
        {
            _apiURL = apiURL;
            _consumer = consumer;
        }

        public void CallApi()
        {
            string message = GetMessageAsync(_apiURL).GetAwaiter().GetResult();
            _consumer.WriteMessage(message);
        }

        async Task<string> GetMessageAsync(string path)
        { 
            string message = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                message = await response.Content.ReadAsStringAsync();
            }
            return message;
        }
    }

    public interface IApiConsumer
    {
        void WriteMessage(string message);
    }

    public class ConsoleConsumer : IApiConsumer
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class DBConsumer : IApiConsumer
    {
        public void WriteMessage(string message)
        {
            //Connect to database and write the message into database
        }
    }
}
