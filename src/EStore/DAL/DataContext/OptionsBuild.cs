using DAL.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public class OptionsBuild
    {
        public DbContextOptionsBuilder<DatabaseContext> OptionsBuilder { get; set; }
        public DbContextOptions<DatabaseContext> DatabaseOptions { get; set; }
        private AppConfigration _appConfigration { get; }

        public OptionsBuild()
        {
            _appConfigration = new AppConfigration();
            OptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            OptionsBuilder.UseSqlServer(_appConfigration.SqlConnectionString);
            OptionsBuilder.EnableSensitiveDataLogging(true);
            DatabaseOptions = OptionsBuilder.Options;
        }

        public static void OptionsConfigure(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(new AppConfigration().SqlConnectionString);
            options.EnableSensitiveDataLogging(true);
        }
    }
}
