using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Specialized;
using System.Net;

namespace CursValutar.Services
{
    public class ApiService
    {
        private static string ApiKey { get; set; }
        private static IRestClient restClient;

        public ApiService()
        {
            ApiKey = "f954f2d65a3500c9d6b4fbe2eeb9ada0a9d85abb2b0f5e4c54b57bfef420f0ee";

            restClient = new RestClient();
        }

        public IRestResponse GetCountries()
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_countries&APIkey={ApiKey}", Method.POST);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response;
        }

        public IRestResponse GetAllLeagues(int countryId)
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_leagues&country_id={countryId}&APIkey={ApiKey}", Method.POST);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response;
        }

        public IRestResponse GetLeagueByName(string country)
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_leagues&country_name={country}&APIkey={ApiKey}", Method.POST);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response;
        }

        public IRestResponse GetTeamsByLeagueId(string leagueId)
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_teams&league_id={leagueId}&APIkey={ApiKey}", Method.POST);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response;
        }

        public IRestResponse GetPlayerData(string playerName)
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_players&player_name=={playerName}&APIkey={ApiKey}", Method.POST);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response;
        }

    }
}

