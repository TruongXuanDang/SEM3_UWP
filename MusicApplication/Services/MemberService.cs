using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using MusicApplication.Constant;
using MusicApplication.Entities;
using Newtonsoft.Json;

namespace MusicApplication.Services
{
    class MemberService:IMemberService
    {
        public string Login(LoginMember member)
        {
            HttpClient httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member),Encoding.UTF8,"application/json");
            var httpRequestMessage = httpClient.PostAsync(ApiUrl.API_LOGIN, content);
            var jsonResult = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }

        public string Register(User user)
        {
            HttpClient httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(user),Encoding.UTF8,"application/json");
            var httpRequestMessage = httpClient.PostAsync(ApiUrl.API_REGISTER, content);
            var jsonResult = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            MessageDialog dialog = new MessageDialog(jsonResult);
            dialog.ShowAsync().GetAwaiter().GetResult();
            return jsonResult;
        }

        public string GetInformation(string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var httpRequestMessage = httpClient.GetAsync(ApiUrl.API_GET_INFO);
            var jsonResult = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            return jsonResult;
        }
    }
}
