using Microsoft.EntityFrameworkCore;
using System.IO;

namespace NostrLib;

public class MyDbContext : DbContext
{
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
            "NostrSharp.db3");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}