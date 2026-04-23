using System;

namespace ICelebrity.lib;

public class Celebrity
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? Surname { get; set; }
    public string? PhotoPath { get; set; }

    public Celebrity() { }

    public Celebrity(int id, string firstName, string surname, string photoPath)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        PhotoPath = photoPath;
    }
}

public interface ICelebrity<T> : IDisposable
{
    List<T> GetAll();
    T? GetById(int id);

    bool Add(T celebrity);
    bool DelById(int id);
    bool UpdateById(int id, T celebrity);

    int AddAndGetId(T celebrity);

    int GetIdByName(string celebrityName);

    int SaveChanges();
}
