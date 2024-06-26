﻿using Microsoft.Extensions.Configuration;

namespace DAL.Models.Common
{
    public class AppConfigration
    {
        public string SecretJwt { get; set; }
        public string SqlConnectionString { get; set; }
        public string ApiBaseUrlSSL { get; set; }
        public string ApiBaseUrl { get; set; }

        public AppConfigration()
        {
            #region Init

            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();

            #endregion Init

            SecretJwt = root.GetSection("Secret:Jwt").Value;
            SqlConnectionString = root.GetSection("ConnectionStrings:DefaultConnection").Value;
            ApiBaseUrlSSL = root.GetSection("Api:BaseUrlSSL").Value;
            ApiBaseUrl = root.GetSection("Api:BaseUrl").Value;
        }
    }
}