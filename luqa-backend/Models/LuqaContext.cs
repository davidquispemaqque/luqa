using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace luqa_backend.Models;

public class LuqaContext : DbContext
{
    public LuqaContext() { }  // Opcional: puedes mantenerlo si quieres

    public LuqaContext(DbContextOptions<LuqaContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            Env.Load();
            var connectionString = Env.GetString("DATABASE_URL");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }
}