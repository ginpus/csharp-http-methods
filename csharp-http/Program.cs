using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace csharp_http
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //var url = "https://jsonplaceholder.typicode.com/posts?id=20";
            var url = "https://jsonplaceholder.typicode.com/posts/20";
            var httpClient = new HttpClient(); // metodas leidziantis sufomuoti http uzklausa
                                               //-------------------------GET---------------------------------

            /*            var request = new HttpRequestMessage();

                        request.Method = HttpMethod.Get;
                        request.RequestUri = new Uri(url );

                        var response = await httpClient.SendAsync(request);

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception("request failed!");
                        }

                        var responseString = await response.Content.ReadAsStringAsync(); //resposeString yra stringu listas. reikia deserializuoti

                        var posts = JsonSerializer.Deserialize<List<PostResponse>>(responseString); // metodas yra case sensitive - jei is html grazina kitaip, tada nesumapina

                        //Pilnai Async:
                        //var responseString = await response.Content.ReadAsStreamAsync();

                        //var posts = await JsonSerializer.DeserializeAsync<List<PostResponse>>(responseString);

                        //var post = JsonSerializer.Deserialize<List<PostResponse>>(responseString).First(); // First paima pirma liste esanti

                        foreach (var post in posts)
                        {
                            Console.WriteLine(post);
                        }

                        //Console.WriteLine(responseString);*/

            //SUPAPRASTINIMAS:

            /*            var response = await httpClient.GetAsync(url);

                        var posts = await response.Content.ReadFromJsonAsync<List<PostResponse>>(); // JSONas deserializuojasi i PostResponse klases lista. Sitas metodas yra case insesitive, todel net nereikia klaseje sumapinimo properciu

                        foreach (var post in posts)
                        {
                            Console.WriteLine(post);
                        }*/

            //DAR DIDESNIS SUPAPRASTINIMAS

            /*            var posts = await httpClient.GetFromJsonAsync<List<PostResponse>>(url);

                        foreach (var post in posts)
                        {
                            Console.WriteLine(post);
                        }*/

            //-------------------------POST---------------------------------
            var post = new PostRequest
            {
                UserId = 1,
                Title = "foo",
                Body = "bar"
            };

            /*            var postJson = JsonSerializer.Serialize(post);

                        var request = new HttpRequestMessage();

                        request.RequestUri = new Uri(url);
                        request.Method = HttpMethod.Post;
                        request.Content = new StringContent(postJson, Encoding.UTF8, "application/json"); // body/turinys
                        //HEADER IS A MUST

                        var response = await httpClient.SendAsync(request);

                        //var postResponse = response.Content.ReadFromJsonAsync<PostResponse>();

                        //Console.WriteLine(postResponse);

                        Console.WriteLine(await response.Content.ReadAsStringAsync());*/

            //SUPAPRASTINIMAS:

            /*            var response = await httpClient.PostAsJsonAsync(url, post);
                        Console.WriteLine(response.EnsureSuccessStatusCode());*/

            //-------------------------PATCH---------------------------------

            /* //without using data model
             var patchJson = JsonSerializer.Serialize(new
                        {
                            title = "new title this is. Yoda",
                            body = "also changed body"
                        });*/

            var patchJson = JsonSerializer.Serialize(new PatchTitleRequestModel
            {
                Title = "new title this is. Yoda"
            });

            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Patch;
            request.Content = new StringContent(patchJson, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            //-------------------------DELETE---------------------------------

            //-------------------------PUT---------------------------------

            /*            var putRequest = new PutRequestModel
                        {
                            //Id = 222, // does not make sense to be changed
                            Title = "new title lalala",
                            Body = "new body too",
                            UserId = 45
                        };

                        var putJson = JsonSerializer.Serialize(putRequest);

                        var request = new HttpRequestMessage
                        {
                            RequestUri = new Uri(url),
                            Method = HttpMethod.Put,
                            Content = new StringContent(putJson, Encoding.UTF8, "application/json")
                        };

                        //same as:

            *//*            var request = new HttpRequestMessage();

                        request.RequestUri = new Uri(url);
                        request.Method = HttpMethod.Put;
                        request.Content = new StringContent(putJson, Encoding.UTF8, "application/json");*//*

                        var response = await httpClient.SendAsync(request);

                        Console.WriteLine(await response.Content.ReadAsStringAsync());*/
        }
    }

    internal class PatchTitleRequestModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    internal class PostResponse
    {
        //[JsonPropertyName("userId")]
        public int UserId { get; set; }

        //[JsonPropertyName("id")]
        public int Id { get; set; }

        //[JsonPropertyName("title")]
        public string Title { get; set; }

        //[JsonPropertyName("body")]
        public string Body { get; set; }

        public override string ToString()
        {
            return $"{UserId} {Id} {Title} {Body}";
        }
    }

    internal class PostRequest
    {
        public int UserId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }

    internal class PutRequestModel
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        public override string ToString()
        {
            return $"{UserId} {Id} {Title} {Body}";
        }
    }
}