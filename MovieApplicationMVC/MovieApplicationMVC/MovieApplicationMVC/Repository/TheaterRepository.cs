using MovieApplicationMVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MovieApplicationMVC.Repositories
{
    public class TheaterRepository
    {
        private readonly HttpClient _client;

        public TheaterRepository()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:49681/api/Theater/") };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Theater> GetAllTheaters()
        {
            var response = _client.GetAsync("GetAllTheaters").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<List<Theater>>().Result;
        }

        public Theater GetTheaterById(string id)
        {
            var response = _client.GetAsync($"GetTheaterById/{id}").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<Theater>().Result;
        }

        public List<Theater> GetTheatersByLocation(string location)
        {
            var response = _client.GetAsync($"GetTheatersByLocation/{location}").Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<List<Theater>>().Result;
        }

        public bool AddTheater(Theater theater)
        {
            var response = _client.PostAsJsonAsync("AddTheater", theater).Result;
            return response.IsSuccessStatusCode;
        }

        public bool UpdateTheater(Theater theater)
        {
            var response = _client.PutAsJsonAsync("UpdateTheater", theater).Result;
            return response.IsSuccessStatusCode;
        }

        public bool DeleteTheater(string id)
        {
            var response = _client.DeleteAsync($"DeleteTheater/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
