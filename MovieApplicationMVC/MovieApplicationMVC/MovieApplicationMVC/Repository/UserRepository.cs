using MovieApplicationMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MovieApplicationMVC.Repositories
{
    public class UserRepository
    {
        private readonly HttpClient _client;

        public UserRepository()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:49681/api/User/") };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public bool Register(User user)
        {
            // Send data to API's Register endpoint
            var response = _client.PostAsJsonAsync("Register", user).Result;
            return response.IsSuccessStatusCode;
        }
    



public User Login(string email, string password)
        {
            var loginRequest = new { Email = email, Password = password };
            var response = _client.PostAsJsonAsync("Login", loginRequest).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<dynamic>().Result;
                var user = result.User.ToObject<User>();
               return user;
            }

            return null;
        }

        public User GetUserById(string userId)
        {
            var response = _client.GetAsync($"GetById/{userId}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<User>().Result;
            }
            return null;
        }

        public bool UpdateProfile(User user)
        {
            var response = _client.PutAsJsonAsync("UpdateProfile", user).Result;
            return response.IsSuccessStatusCode;
        }
        public async Task<List<User>> GetAllUsers()

        {

            var response = await _client.GetAsync("GetAll"); // Call the GetAllUsers API endpoint            if (response.IsSuccessStatusCode)

            {

                var jsonData = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<User>>(jsonData); // Deserialize the JSON response            }

                return null;

            }
        }
        public bool DeleteUser(string userId)
        {
            var response = _client.DeleteAsync($"DeleteUser/{userId}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
