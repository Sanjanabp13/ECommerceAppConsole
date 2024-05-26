using System;
using Microsoft.Extensions.Configuration;

namespace hexaware_casestudy.Util
{
	public class ConnectionUtil
	{
		

             private static IConfiguration _iconfiguration;
        static ConnectionUtil()
        {
            GetAppSettingsFile();
        }

        public static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json");
            _iconfiguration = builder.Build();

        }

        public static string GetConnectionString()
        {
            return _iconfiguration.GetConnectionString("LocalConnectionString");
        }
    }
}


