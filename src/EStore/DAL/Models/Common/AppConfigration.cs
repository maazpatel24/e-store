﻿using Microsoft.Extensions.Configuration;

namespace DAL.Models.Common
{
    public class AppConfigration
    {
        public string SqlConnectionString { get; set; }

        public AppConfigration()
        {
            #region Init

            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();

            #endregion Init

            SqlConnectionString = root.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
    }
}