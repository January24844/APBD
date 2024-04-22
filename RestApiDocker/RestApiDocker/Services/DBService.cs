using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RestApiDocker.Models;

namespace RestApiDocker.Services
{
    public class DBService : IDBService
    {
        private readonly string connectionString = @"Data Source=db-mssql,1433;Initial Catalog=s24844;User Id=marshall;Password=banana;";

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        private bool IsExecuted(int rows)
        {
            return rows >= 1;
        }

        public void CreateAnimal(Animal animal)
        {
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO Animal(Name, Description, Category, Area) VALUES(@name, @description, @category, @area)";
                command.Parameters.AddWithValue("name", animal.Name);
                command.Parameters.AddWithValue("description", animal.Description);
                command.Parameters.AddWithValue("category", animal.Category);
                command.Parameters.AddWithValue("area", animal.Area);
                connection.Open();
            }
        }

        public void ChangeAnimal(int idAnimal, Animal animal)
        {
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE idAnimal = @idAnimal";
                command.Parameters.AddWithValue("name", animal.Name);
                command.Parameters.AddWithValue("description", animal.Description);
                command.Parameters.AddWithValue("category", animal.Category);
                command.Parameters.AddWithValue("area", animal.Area);
                command.Parameters.AddWithValue("idAnimal", idAnimal);
                connection.Open();
            }
        }

        public void DeleteAnimal(int idAnimal)
        {
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Animal WHERE idAnimal = @idAnimal";
                command.Parameters.AddWithValue("idAnimal", idAnimal);
                connection.Open();
            }
        }

        public List<Animal> GetAnimals(string orderBy)
        {
            var animals = new List<Animal>();
            using (var connection = GetConnection())
            using (var command = connection.CreateCommand())
            {
                string[] columnsNames = { "name", "description", "category", "area" };
                bool isMatched = false;

                if (!string.IsNullOrEmpty(orderBy))
                {
                    foreach (var columnName in columnsNames)
                    {
                        if (orderBy.ToLower().Equals(columnName))
                        {
                            isMatched = true;
                            break;
                        }
                    }

                    if (isMatched)
                    {
                        command.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy} ASC";
                    }
                }
                else
                {
                    command.CommandText = "SELECT * FROM Animal ORDER BY Name ASC";
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    animals.Add(new Animal
                    {
                        IdAnimal = int.Parse(reader["IdAnimal"].ToString()),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Category = reader["Category"].ToString(),
                        Area = reader["Area"].ToString()
                    });
                }
            }
            return animals;
        }
    }
}
