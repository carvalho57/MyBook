using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MyBook.Models;

namespace MyBook.Data {

    public class BookAccess {
        
        private readonly string ConnectionString = "DataSource=book.db";

        public void Add(Book book) {
            using(var connection = new SqliteConnection(ConnectionString)) {
                var command = connection.CreateCommand();

                var query = $"INSERT INTO Book( Title, Genre, Status, Favorite, Description) VALUES (\"{book.Title}\", \"{book.Genre}\", {book.Status},{book.Favorite}, \"{book.Description}\" )";

                command.CommandText = query;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void Update(Book book) {
            using(var connection = new SqliteConnection(ConnectionString)) {
                var command = connection.CreateCommand();

                var query = $"UPDATE Book SET Title = \"{book.Title}\", Genre = \"{book.Genre}\", Status = {book.Status}, Favorite = {book.Favorite}, Description = \"{book.Description}\" WHERE BookID = {book.BookID};";

                command.CommandText = query;

                connection.Open();

                command.ExecuteNonQuery();
            }

        }

        public void Remove(Book book) {
            using(var connection = new SqliteConnection(ConnectionString)) {
                var command = connection.CreateCommand();

                var query = $"DELETE FROM Book WHERE BookID = {book.BookID}";

                command.CommandText = query;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public List<Book> GetBooks() {
            using(var connection = new SqliteConnection(ConnectionString)) {

                var command = connection.CreateCommand();

                var query = "SELECT BookID, Title, Genre, Status, Favorite, Description FROM Book";

                command.CommandText = query;

                connection.Open();
                
                var reader = command.ExecuteReader();
                var books = new List<Book>();
                while(reader.Read()) {
                    books.Add( 
                        new Book() {
                            BookID = Convert.ToInt32(reader["BookID"]),
                            Title = Convert.ToString(reader["Title"]),
                            Genre = Convert.ToString(reader["Genre"]),
                            Status = Convert.ToBoolean(reader["Status"]),
                            Favorite = Convert.ToBoolean(reader["Favorite"]),
                            Description = Convert.ToString(reader["Description"])
                        }
                    );
                }
                
                return books;
            }
        }
    }
}