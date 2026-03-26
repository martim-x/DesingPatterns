using System;
using System.Collections.Generic;

namespace ICelebrity.lib;

public record Celebrity(int Id, string FirstName, string Surname, string PhotoPath);

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
