using System;
using System.Collections.Generic;
using ICelebrity.lib;

internal class Program
{
    private static void Main(string[] args)
    {
        Repository.JSONFileName = "Celebrities.json";

        using (ICelebrity<Celebrity> repo = Repository.Create("Celebrities"))
        {
            Console.WriteLine("-------------- GetAll ----------------------");
            List<Celebrity> celebrities = repo.GetAll();
            celebrities.ForEach(c =>
                Console.WriteLine(
                    $"Id = {c.Id}, Firstname = {c.FirstName}, Surname = {c.Surname}, PhotoPath = {c.PhotoPath} "
                )
            );

            Console.WriteLine("-------------- Add ---------------------------");

            repo.Add(new Celebrity(0, "Marvin", "Minsky", "/Photo/Minsky.jpg"));
            celebrities = repo.GetAll();
            celebrities.ForEach(c =>
                Console.WriteLine(
                    $"Id = {c.Id}, Firstname = {c.FirstName}, Surname = {c.Surname}, PhotoPath = {c.PhotoPath} "
                )
            );

            Console.WriteLine("-------------- GetIdByName, UpdateById ---------------------------");

            int id = repo.GetIdByName("Marvin");
            if (id >= 0)
            {
                repo.UpdateById(id, new Celebrity(id, "Marvin", "Minsky", "/Photo/Minsky.jpg"));
            }
            celebrities = repo.GetAll();
            celebrities.ForEach(c =>
                Console.WriteLine(
                    $"Id = {c.Id}, Firstname = {c.FirstName}, Surname = {c.Surname}, PhotoPath = {c.PhotoPath} "
                )
            );

            Console.WriteLine("-------------- GetIdByName, DelById ---------------------------");

            id = repo.GetIdByName("Minsky");
            if (id >= 0)
            {
                repo.DelById(id);
            }
            celebrities = repo.GetAll();
            celebrities.ForEach(c =>
                Console.WriteLine(
                    $"Id = {c.Id}, Firstname = {c.FirstName}, Surname = {c.Surname}, PhotoPath = {c.PhotoPath} "
                )
            );

            Console.WriteLine("-------------- AddAndGetId ---------------------------");

            id = repo.AddAndGetId(new Celebrity(0, "Marvin", "Minsky", "/Photo/Minsky.jpg"));
            Console.WriteLine($"New Id = {id}");
            celebrities = repo.GetAll();
            celebrities.ForEach(c =>
                Console.WriteLine(
                    $"Id = {c.Id}, Firstname = {c.FirstName}, Surname = {c.Surname}, PhotoPath = {c.PhotoPath} "
                )
            );

            Console.WriteLine("SaveChanges = {0}", repo.SaveChanges());
        }

        using (ICelebrity<Celebrity> repo = Repository.Create("Celebrities"))
        {
            List<Celebrity> celebrities = repo.GetAll();
            celebrities.ForEach(c =>
                Console.WriteLine(
                    $"Id = {c.Id}, Firstname = {c.FirstName}, Surname = {c.Surname}, PhotoPath = {c.PhotoPath} "
                )
            );
        }
    }
}
