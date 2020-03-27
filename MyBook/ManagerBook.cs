using System;
using MyBook.Models;
using System.Collections.Generic;
using MyBook.Data;

namespace MyBook
{
    public class ManagerBook
    {
        private readonly BookAccess _bookAccess;
        public ManagerBook()
        {
            _bookAccess = new BookAccess();
        }


        public void Add(Book book)
        {
            if(isBookValid(book)) {
                try {
                    _bookAccess.Add(book);
                }catch(Exception ex) {
                    Console.WriteLine("Erro add",ex.Message);
                }
                
            }
        }

        public void Remove(Book book)
        {
            var existBook = GetBookByName(book.Title);
            if(existBook != null) {
                try {
                    _bookAccess.Remove(book);
                }catch(Exception ex) {
                    Console.WriteLine("Erro Remove",ex.Message);
                }
                
            }
        }
        public ICollection<Book> GetBooks()
        {
            return _bookAccess.GetBooks();
        }
        public Book GetBookByName(string name)
        {
            return _bookAccess.GetBookByName(name);
        }

        private bool isBookValid(Book book) {
            return true;
        }
    }
}