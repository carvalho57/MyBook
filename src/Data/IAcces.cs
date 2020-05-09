using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using MyBook.Models;

namespace MyBook.Data {
    public interface IAcess<T>
    {
        void Add(T item);
        void Update(T item);
        void Remove(int id);
        T GetById(int id);
        bool Contains(T item);
        ICollection<T> GetAll();
    }    

}