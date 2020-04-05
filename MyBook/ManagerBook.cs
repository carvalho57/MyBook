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


        public bool Add(Book book)
        {
            
            if(isBookValid(book)) {
                try {
                    _bookAccess.Add(book);
                }catch(Exception ex) {
                    Console.WriteLine("Erro add",ex.Message);
                }                
                return true;
            }
            return false;
            
        }

        public bool Remove(Book book)
        {
            var existBook = GetBookByName(book.Title);
            if(existBook != null) {
                try {
                    _bookAccess.Remove(book);
                }catch(Exception ex) {
                    Console.WriteLine("Erro Remove",ex.Message);
                }
                return true;    
            }
            return false;
            
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
            ICollection<string> erros = new string[5];
            if(book.Title.Length > 50) {
                erros.Add("Título: Não pode ter mais de 50 caracteres");
            }
            if(book.Genre.Length > 30) {
                erros.Add("Gênero: Não pode ter mais de 30 caracteres");
            }
            if(book.Description.Length > 200) {
                erros.Add("Descrição: Não pode ter mais de 200 caracters");
            }

            return erros.Count <= 0;
        }
    }
}