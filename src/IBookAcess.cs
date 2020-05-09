using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MyBook.Models;

namespace MyBook.Data {
    public interface IBookAcess : IAcess<Book>
    {
        Book GetBookByTitle(string title);
    }    

}