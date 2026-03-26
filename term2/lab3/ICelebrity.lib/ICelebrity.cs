using System;

namespace ICelebrity.lib;

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
