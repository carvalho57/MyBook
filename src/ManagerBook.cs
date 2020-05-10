using System;
using MyBook.Models;
using System.Collections.Generic;
using MyBook.Data;

namespace MyBook.Business
{
    public class ManagerBook
    {
        private readonly IBookAcess _bookAccess;
        public ManagerBook()
        {
            _bookAccess = new BookAccess();
        }

        public bool Add(Book book)
        {            
            if(!isBookValid(book)) 
                return false;

            try {
                _bookAccess.Add(book);
                return true;
            }catch(Exception) {                
                return false;
            }           
        }

        public bool Update(Book book) {
            if(book.BookID == null) 
                return false;

            if(!BookExist(book)) 
                return false;

            try {
                _bookAccess.Update(book);
                return true;
            }catch(Exception) {                
                return false;
            }        
        }
        public bool Remove(Book book)
        {
            if(!BookExist(book)) 
                return false;

            try {

                _bookAccess.Remove((int)book.BookID);
                return true;   
            }catch(Exception) {                
                return false;
            }
            
        }
        public ICollection<Book> GetAll()
        {
            return _bookAccess.GetAll();
        }

        public Book GetBookById(int id)
        {               
            return  _bookAccess.GetById(id);            
        }   
        public Book GetBookByTitle(string title) {
            if(String.IsNullOrEmpty(title)) 
                return null;

            try {
                return  _bookAccess.GetBookByTitle(title);                
            }catch(Exception) {
                return null;
            }
        }
        
        public ICollection<Book> GetAllByGenre(string genre) {
            if(String.IsNullOrEmpty(genre)) 
                return null;
            try {
                return  _bookAccess.GetAllByGenre(genre);                
            }catch(Exception) {
                return null;
            }

        }
        public ICollection<Book> GetAllByReaded(bool readed) {
            try {
                return  _bookAccess.GetAllByReaded(readed);                
            }catch(Exception) {
                return null;
            }
        }
        public ICollection<Book> GetAllByFavorite(bool favorite) {
            try {
                return  _bookAccess.GetAllByFavorite(favorite);                
            }catch(Exception) {
                return null;
            }
        }
        private bool BookExist(Book book) {            
            if(book.BookID == null)
                return false;
            try {
                var result =  _bookAccess.Contains(book);
                return result;
            }catch(Exception ex) {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }

        private bool isBookValid(Book book) {
            ICollection<string> erros = new List<string>();
            if(book.Title.Length > 50) {
                erros.Add("Título: Não pode ter mais de 50 caracteres");
            }
            if(book.Genre.Length > 30) {
                erros.Add("Gênero: Não pode ter mais de 30 caracteres");
            }
            if(book.Description.Length > 200) {
                erros.Add("Descrição: Não pode ter mais de 200 caracters");
            }

            return erros.Count == 0;
        }
    }
}