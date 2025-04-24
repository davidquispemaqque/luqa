using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace luqa_backend.Models;

public class LuqaContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Env.Load();

        var connectionString = Env.GetString("DATABASE_URL");

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }
}