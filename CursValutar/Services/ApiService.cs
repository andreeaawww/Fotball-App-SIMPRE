using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

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

        public dynamic GetCountries()
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_countries&APIkey={ApiKey}", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response.Content.ToString();
        }

        public dynamic GetAllLeagues()
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_leagues&APIkey={ApiKey}", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response.Content.ToString();
        }

        public dynamic GetLeagueByName(string country)
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_leagues&country_name={country}&APIkey={ApiKey}", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response.Content.ToString();
        }

        public dynamic GetTeamsByLeagueId(string leagueId)
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_teams&league_id={leagueId}&APIkey={ApiKey}", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response.Content.ToString();
        }

        public dynamic GetPlayerData(string playerName)
        {
            var request = new RestRequest($"https://apiv2.apifootball.com/?action=get_players&player_name=={playerName}&APIkey={ApiKey}", Method.GET);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = restClient.Execute(request);

            return response.Content.ToString();
        }

    }
}

