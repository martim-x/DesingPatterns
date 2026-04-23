using System;
using System.Collections.Generic;
using System.Linq;
using ICelebrity.lib;
using Microsoft.EntityFrameworkCore;

namespace ICelebrity.lib;

public class CelebrityContext : DbContext
{
    private readonly string _connectionString;

    public CelebrityContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Celebrity> Celebrities => Set<Celebrity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Celebrity>(entity =>
        {
            entity.ToTable("Celebrities");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            entity
                .Property(e => e.FirstName)
                .HasColumnName("Firstname")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Surname).HasColumnName("Surname").HasMaxLength(100).IsRequired();

            entity
                .Property(e => e.PhotoPath)
                .HasColumnName("PhotoPath")
                .HasMaxLength(200)
                .IsRequired();
        });
    }
}

public class EfDbRepository : ICelebrity<Celebrity>
{
    private readonly CelebrityContext _ctx;
    private int _scn = 0;

    private EfDbRepository(string connectionString)
    {
        _ctx = new CelebrityContext(connectionString);
        _ctx.Database.EnsureCreated(); // аналог EnsureCreated в твоём ADO-варианте
        FlushFill(); // если хочешь автозаполнение
    }

    public static ICelebrity<Celebrity> Create(string connectionString) =>
        new EfDbRepository(connectionString);

    public List<Celebrity> GetAll() => _ctx.Celebrities.OrderBy(c => c.Id).AsNoTracking().ToList();

    public Celebrity? GetById(int id) =>
        _ctx.Celebrities.AsNoTracking().FirstOrDefault(c => c.Id == id);

    public bool Add(Celebrity celebrity)
    {
        _ctx.Celebrities.Add(celebrity);
        _scn++;
        return true;
    }

    public bool DelById(int id)
    {
        var entity = _ctx.Celebrities.Find(id);
        if (entity == null)
            return false;

        _ctx.Celebrities.Remove(entity);
        _scn++;
        return true;
    }

    public bool UpdateById(int id, Celebrity celebrity)
    {
        var entity = _ctx.Celebrities.Find(id);
        if (entity == null)
            return false;

        entity.FirstName = celebrity.FirstName;
        entity.Surname = celebrity.Surname;
        entity.PhotoPath = celebrity.PhotoPath;

        _scn++;
        return true;
    }

    public int AddAndGetId(Celebrity celebrity)
    {
        _ctx.Celebrities.Add(celebrity);
        _scn++;
        _ctx.SaveChanges();
        _scn = 0;
        return celebrity.Id;
    }

    public int GetIdByName(string celebrityName)
    {
        var cel = _ctx
            .Celebrities.AsNoTracking()
            .FirstOrDefault(c =>
                EF.Functions.ILike(c.FirstName + "|" + c.Surname, $"%{celebrityName}%")
            );

        return cel?.Id ?? -1;
    }

    public int SaveChanges()
    {
        if (_scn <= 0)
            return 0;

        var affected = _ctx.SaveChanges();
        _scn = 0;
        return affected;
    }

    public void Dispose()
    {
        SaveChanges();
        _ctx.Dispose();
    }

    private void FlushFill()
    {
        if (_ctx.Celebrities.Any())
            return;

        var seed = new List<Celebrity>
        {
            new()
            {
                FirstName = "Noam",
                Surname = "Chomsky",
                PhotoPath = "/Photo/Chomsky.jpg",
            },
            new()
            {
                FirstName = "Tim",
                Surname = "Berners-Lee",
                PhotoPath = "/Photo/Berners-Lee.jpg",
            },
            new()
            {
                FirstName = "Edgar",
                Surname = "Codd",
                PhotoPath = "/Photo/Codd.jpg",
            },
            new()
            {
                FirstName = "Donald",
                Surname = "Knuth",
                PhotoPath = "/Photo/Knuth.jpg",
            },
            new()
            {
                FirstName = "Linus",
                Surname = "Torvalds",
                PhotoPath = "/Photo/Torvalds.jpg",
            },
            new()
            {
                FirstName = "John",
                Surname = "Neumann",
                PhotoPath = "/Photo/Neumann.jpg",
            },
            new()
            {
                FirstName = "Edsgar",
                Surname = "Dijkstra",
                PhotoPath = "/Photo/Dijkstra.jpg",
            },
            new()
            {
                FirstName = "Marvin",
                Surname = "Minsky",
                PhotoPath = "/Photo/Minsky.jpg",
            },
        };

        _ctx.Celebrities.AddRange(seed);
        _ctx.SaveChanges();
        _scn = 0;
    }
}
