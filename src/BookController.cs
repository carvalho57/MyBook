using System;
using System.Linq;
using System.Collections.Generic;
using MyBook.Data;
using MyBook.Models;
using MyBook.Business;

namespace MyBook {
    public class BookController {

        private readonly ManagerBook _managerBook;

        public BookController() {
            _managerBook = new ManagerBook();
        }

        public void AddBook()
        {                         
            Console.WriteLine("\nInsira as informações do livro \n");            
            string title = ReadInputUser("Título: ");            
            string genre = ReadInputUser("Gênero:");            
            string description = ReadInputUser("Descrição:");
            Book newBook = new Book(title,genre,description);

            if(!_managerBook.Add(newBook)) { 
                Write("Problema ao inserir o livro");
                return;
            }

            Write($"Livro {newBook.Title} Inserido com sucesso!");
        }
        public void UpdateBook() {        
            Write("Insira os Dados do Livro para serem atualizados");
            string title = ReadInputUser("Título: ");            
            string genre = ReadInputUser("Gênero:");            
            string description = ReadInputUser("Descrição:");            

            Book book = _managerBook.GetBookByTitle(title);
            if(book == null) {
                Write($"O livro {title} não foi encontrado!");
                return;
            }
            
            var bookUpdate = new Book((int)book.BookID,title,genre,description);

            if(!_managerBook.Update(bookUpdate)) { 
                Write("Problema ao atualizar o  livro");
                return;
            }
            
            Write("Livro atualizado com sucesso");
            
        }
        public void RemoveBook()
        {
            string title = ReadInputUser("Informe o Título do livro a ser removido: "); 
            var book = _managerBook.GetBookByTitle(title);
            if(!_managerBook.Remove(book)){
                Write("Problema ao remover livro, tente novamente!");
                return;
            }
            Write("Livro removido com sucesso!");
        }
        public void GetAllBooks()
        {   
            var books = _managerBook.GetAll();            
            if(books != null)
                PrintAllBook(books);
        }

        public void GetBook()
        {                     
            var title = ReadInputUser("Informe o título do livro: ");
            var book = _managerBook.GetBookByTitle(title);

            if(!(book == null)) {
                PrintBookScreen(book);
                return;
            }
            Write("Livro não encontrado!");            
        }

        public void GetAllByGenre() {
            string genre = ReadInputUser("Informe o gênero do livro:");
            var books = _managerBook.GetAllByGenre(genre);

            if(!PrintAllBook(books)) 
                Write("Nenhum livro com base no gênero foi encontrado!");
        }
        public void GetAllByReaded() {
            bool readed = false;
            if(ReadInputUser("Você deseja os livros que ja foram lidos (yes/no):") == "yes")
                readed = true;
            
            var books = _managerBook.GetAllByReaded(readed);

            if(!PrintAllBook(books)) 
                Write("Nenhum livro encontrado!");    
        }
        public void GetAllByFavorite() {
            var message = "Você deseja todos os livro favoritos(yes/no):";
            bool favorite = ReadInputUser(message) == "yes"
                                                ? true
                                                : false;
                
            
            var books = _managerBook.GetAllByReaded(favorite);

            if(!PrintAllBook(books)) 
                Write("Nenhum livro encontrado!");    
        }

        private void PrintBookScreen(Book book) {
           if(book is null)
                return;

            Write($"Título: {book.Title}");
            Write($"Gênero: {book.Genre}");
            Write($"Descrição: {book.Description}");            
            Write("-------------------------------");
        }

        private bool PrintAllBook(ICollection<Book> books) {
            if(books == null)
                return false;

             foreach(Book book in books)
                PrintBookScreen(book);
            
            return true;
        }
        private string ReadInputUser(string message) {
            Console.Write($"{message}");
            return Console.ReadLine();
        }

        private void Write(string message) {
            Console.WriteLine($"{message}");
        }
    }
}   