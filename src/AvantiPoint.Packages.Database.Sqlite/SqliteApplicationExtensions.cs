using System;
using AvantiPoint.Packages.Core;
using AvantiPoint.Packages.Database.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AvantiPoint.Packages
{
    public static class SqliteApplicationExtensions
    {
        public static NuGetApiApplication AddSqliteDatabase(this NuGetApiApplication app)
        {
            app.Services.AddAPPackagesDbContextProvider<SqliteContext>("Sqlite", (provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptionsSnapshot<DatabaseOptions>>();

                options.UseSqlite(databaseOptions.Value.ConnectionString);
            });

            return app;
        }

        public static NuGetApiApplication AddSqliteDatabase(
            this NuGetApiApplication app,
            Action<DatabaseOptions> configure)
        {
            app.AddSqliteDatabase();
            app.Services.Configure(configure);
            return app;
        }
    }
}