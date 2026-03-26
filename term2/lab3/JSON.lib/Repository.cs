using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ICelebrity.lib;

public record Celebrity(int Id, string FirstName, string Surname, string PhotoPath);

public class Repository : ICelebrity<Celebrity>
{
    public static string JSONFileName = "Celebrities.json";

    public string BasePath { get; private set; }

    public string FullBasePath => Path.Combine(this.BasePath, Repository.JSONFileName);

    private List<Celebrity> _celebrities = new();
    private int _scn = 0;

    private Repository(string basePath)
    {
        this.BasePath = basePath;
        this.Load();
    }

    public static ICelebrity<Celebrity> Create(string basePath)
    {
        return new Repository(basePath);
    }

    private void Load()
    {
        if (!File.Exists(this.FullBasePath))
        {
            this._celebrities = new List<Celebrity>();
            return;
        }

        var json = File.ReadAllText(this.FullBasePath);
        var list = JsonSerializer.Deserialize<List<Celebrity>>(json);
        this._celebrities = list ?? new List<Celebrity>();
    }

    public List<Celebrity> GetAll()
    {
        return this._celebrities;
    }

    public Celebrity? GetById(int id)
    {
        return this._celebrities.FirstOrDefault(c => c.Id == id);
    }

    public bool Add(Celebrity celebrity)
    {
        var id = this._celebrities.Count > 0 ? this._celebrities.Max(c => c.Id) + 1 : 1;

        this._celebrities.Add(
            new Celebrity(id, celebrity.FirstName, celebrity.Surname, celebrity.PhotoPath)
        );

        this._scn++;
        return true;
    }

    public bool DelById(int id)
    {
        var idx = this._celebrities.FindIndex(c => c.Id == id);
        if (idx >= 0)
        {
            this._celebrities.RemoveAt(idx);
            this._scn++;
            return true;
        }

        return false;
    }

    public bool UpdateById(int id, Celebrity celebrity)
    {
        var idx = this._celebrities.FindIndex(c => c.Id == id);
        if (idx >= 0)
        {
            this._celebrities[idx] = new Celebrity(
                id,
                celebrity.FirstName,
                celebrity.Surname,
                celebrity.PhotoPath
            );
            this._scn++;
            return true;
        }

        return false;
    }

    public int AddAndGetId(Celebrity celebrity)
    {
        var id = this._celebrities.Count > 0 ? this._celebrities.Max(c => c.Id) + 1 : 1;

        this._celebrities.Add(
            new Celebrity(id, celebrity.FirstName, celebrity.Surname, celebrity.PhotoPath)
        );

        this._scn++;
        return id;
    }

    public int GetIdByName(string celebrityName)
    {
        var cel = this._celebrities.FirstOrDefault(c =>
            ($"{c.FirstName}|{c.Surname}").Contains(
                celebrityName,
                StringComparison.OrdinalIgnoreCase
            )
        );

        return cel != null ? cel.Id : -1;
    }

    public int SaveChanges()
    {
        if (this._scn > 0)
        {
            var json = JsonSerializer.Serialize(
                this._celebrities,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(this.FullBasePath, json);
            this._scn = 0;
        }

        return this._scn;
    }

    public void Dispose()
    {
        this.SaveChanges();
    }
}
