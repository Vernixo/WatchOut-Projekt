using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WatchOut.Data;
using System;
using System.Linq;

namespace WatchOut.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new WatchOutContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<WatchOutContext>>()))
        {
            // Look for any movies.
            if (context.Watch.Any())
            {
                return;   // DB has been seeded
            }
            context.Watch.AddRange(
                new Watch
                {
                    Brand = "Orient",
                    Name = "Bambino",
                    Price = 200,
                    Size = 40,
                    UserGender = "Male",
                    Style = "Elegant",
                    Quantity = 15,
                    PhotoPath = ""
                },
                new Watch
                {
                    Brand = "Orient",
                    Name = "Open Heart Contemporary",
                    Price = 300,
                    Size = 42,
                    UserGender = "Male",
                    Style = "Elegant",
                    Quantity = 10,
                    PhotoPath = ""
                },
                new Watch
                {
                    Brand = "Seiko",
                    Name = "Cocktail Time",
                    Price = 500,
                    Size = 39,
                    UserGender = "Male",
                    Style = "Elegant",
                    Quantity = 5,
                    PhotoPath = ""
                },
                new Watch
                {
                    Brand = "Seiko",
                    Name = "5 Sports Automatic SKX Midi",
                    Price = 300,
                    Size = 38,
                    UserGender = "Male",
                    Style = "Casual",
                    Quantity = 15,
                    PhotoPath = ""
                }
            );
            context.SaveChanges();
        }
    }
}