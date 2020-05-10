using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MyBook.Models;

namespace MyBook.Data {
    public interface IBookAcess : IAcess<Book>
    {
        Book GetBookByTitle(string title);
        
        ICollection<Book> GetAllByGenre(string genre);

        ICollection<Book> GetAllByReaded(bool readed);

        ICollection<Book> GetAllByFavorite(bool favorite);
    }    

}