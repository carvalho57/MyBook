using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MyBook.Models;

namespace MyBook.Data {

    public class BookAccess : IBookAcess {        
        protected readonly string ConnectionString = "DataSource=book.db";
        public BookAccess() {
            using(var databaseCommand = new DAL(this.ConnectionString)) {
                var query = "CREATE TABLE IF NOT EXISTS  Book (BookID INTEGER PRIMARY KEY AUTOINCREMENT,Title VARCHAR(50),Genre VARCHAR(30),Readed INTEGER, Favorite INTEGER,Description VARCHAR(200));";
                
                databaseCommand.ExecuteNonQuery(query);
            }
        }
        public void Add(Book book) {        
            using(var databaseCommand = new DAL(this.ConnectionString)) {
                var query = @"INSERT INTO Book( Title, Genre, Readed, Favorite, Description) VALUES (@Title, @Genre, @Readed, @Favorite, @Description )";

                databaseCommand.AddParameter("@Title",book.Title);
                databaseCommand.AddParameter("@Genre",book.Genre);
                databaseCommand.AddParameter("@Readed",book.Readed);
                databaseCommand.AddParameter("@Favorite",book.Favorite);
                databaseCommand.AddParameter("@Description",book.Description);
                                
                databaseCommand.ExecuteNonQuery(query);
            }
        }

        public void Update(Book book) {
            using(var databaseCommand = new DAL(this.ConnectionString)) {
                
                var query = @"UPDATE Book SET Title = @Title, Genre = @Genre, Readed = @Readed , Favorite = @Favorite, Description = @Description WHERE BookID = @BookID ;";
        
                databaseCommand.AddParameter("@Title",book.Title);
                databaseCommand.AddParameter("@Genre",book.Genre);
                databaseCommand.AddParameter("@Readed",book.Readed);
                databaseCommand.AddParameter("@Favorite",book.Favorite);
                databaseCommand.AddParameter("@Description",book.Description);
                databaseCommand.AddParameter("@BookID",book.BookID);

                databaseCommand.ExecuteNonQuery(query);
            }

        }

        public void Remove(int bookId) {
            using(var databaseCommand = new DAL(this.ConnectionString)) {
            
                var query = @"DELETE FROM Book WHERE BookID = @BookID";
                databaseCommand.AddParameter("@BookID",bookId);

                databaseCommand.ExecuteNonQuery(query);
            }
        }

        public Book GetById(int id) {
            using(var databaseCommand = new DAL(this.ConnectionString)) {
            
                var query = @"SELECT BookID, Title, Genre, Readed, Favorite, Description FROM Book WHERE BookID = @id";

                databaseCommand.AddParameter("@id",id);

                var reader = databaseCommand.ExecuteReader(query);
                
                while(reader.Read()) {   
                        return ReadLineOfDataSet(reader);                        
                }
                
                return null;
            }
        }

    

         public Book GetBookByTitle(string title) {
            using(var databaseCommand = new DAL(this.ConnectionString)) {
            
                var query = @"SELECT BookID, Title, Genre, Readed, Favorite, Description FROM Book WHERE Title LIKE '@title%' LIMIT 1";

                databaseCommand.AddParameter("@title",title);

                var reader = databaseCommand.ExecuteReader(query);
                
                while(reader.Read()) {   
                        return ReadLineOfDataSet(reader);                        
                }
                
                return null;
            }
        }

        public bool Contains(Book book) {
             using(var databaseCommand = new DAL(this.ConnectionString)) {
            
                var query = @"SELECT COUNT(*) FROM Book WHERE BookID = @id";

                databaseCommand.AddParameter("@id",book.BookID);

                var count = Convert.ToInt32(databaseCommand.ExecuteScalar(query));
                return count > 0;                     
            }
        }
        public ICollection<Book> GetAll() {
            using(var databaseCommand = new DAL(this.ConnectionString)) {   
                /*Melhorias -> Retornar apenas um limite de objetos, fazendo paginação*/     
                var query = @"SELECT BookID, Title, Genre, Readed, Favorite, Description FROM Book";
                
                var reader = databaseCommand.ExecuteReader(query);
                var books = new List<Book>();
                while(reader.Read()) {
                    books.Add(ReadLineOfDataSet(reader));
                }
                
                return books;
            }
        }
 

        public ICollection<Book> GetAllByGenre(string genre)
        {
             using(var databaseCommand = new DAL(this.ConnectionString)) {        
                var query = @"SELECT BookID, Title, Genre, Readed, Favorite, Description FROM Book WHERE Genre = @genre";

                databaseCommand.AddParameter("@genre",genre);
                var reader = databaseCommand.ExecuteReader(query);
                var books = new List<Book>();
                while(reader.Read()) {
                    books.Add(ReadLineOfDataSet(reader));
                }

                return books;
            }
        }

        public ICollection<Book> GetAllByReaded(bool readed)
        {
             using(var databaseCommand = new DAL(this.ConnectionString)) {                        
                var query = @"SELECT BookID, Title, Genre, Readed, Favorite, Description FROM Book WHERE Readed = @readed";

                databaseCommand.AddParameter("@readed", Convert.ToInt32(readed));
                var reader = databaseCommand.ExecuteReader(query);
                var books = new List<Book>();
                while(reader.Read()) {
                    books.Add(ReadLineOfDataSet(reader));
                }
                
                return books;
            }
        }

        public ICollection<Book> GetAllByFavorite(bool favorite)
        {
            using(var databaseCommand = new DAL(this.ConnectionString)) {        
                var query = @"SELECT BookID, Title, Genre, Readed, Favorite, Description FROM Book WHERE Favorite = @favorite";

                databaseCommand.AddParameter("@favorite", Convert.ToInt32(favorite));
                var reader = databaseCommand.ExecuteReader(query);
                var books = new List<Book>();
                while(reader.Read()) {
                    books.Add(ReadLineOfDataSet(reader));
                }
                
                return books;
            }
        }

        private Book ReadLineOfDataSet(SqliteDataReader reader) {
            return new Book(
                            Convert.ToInt32(reader["BookID"]),                
                            Convert.ToString(reader["Title"]),
                            Convert.ToString(reader["Genre"]),
                            Convert.ToString(reader["Description"]),
                            Convert.ToBoolean(reader["Readed"]),
                            Convert.ToBoolean(reader["Favorite"])
                        );
        }
    }
}