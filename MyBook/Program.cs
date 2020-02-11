using System;
using MyBook.Models;
using MyBook.Data;
using System.Collections.Generic;
namespace MyBook
{
    class Program
    {
        private static BookAccess contextBook { get; set;}        
        static void Main(string[] args)
        {
            contextBook = new BookAccess();

            int options;

            do {

                DisplayMenu();

                if(int.TryParse(Console.ReadLine(), out options)) {
                    ChooseOption(options);
                } else  {
                    Console.WriteLine("Informe um valor Válido");
                    continue;
                }
                


                Console.Write("Press Enter to quit");
            } while(Console.ReadKey().Key != ConsoleKey.Enter);
        }

        static void DisplayMenu() {
            Console.Clear();
            Console.WriteLine("1 - Adicionar Livro");
            Console.WriteLine("2 - Listar todos os livros");
            Console.WriteLine("3 - Buscar por um livro");
            Console.WriteLine("4 - Remover Livro");
            Console.Write("Selecione uma opção: ");
        }

        static void ChooseOption(int option) {
            switch(option) {
                case 1: 
                    AddBook();
                    break;
                case 2:
                    GetBooks();
                    break;
                case 3:
                    GetBook();
                    break;
                case 4:
                    RemoveBook();
                    break;
                default:
                    Console.WriteLine("Informe uma opção válida");
                    break;
            }
            return;
        }
        static void AddBook() {

            Console.Clear();
            Console.WriteLine("Insira as informações do livro \n");
            Console.Write("Title:")    ;
            string title = Console.ReadLine();
            Console.Write("Genre:");
            string genre = Console.ReadLine();
            Console.Write("Description:");
            string description = Console.ReadLine();

            var book = new Book(title,genre,description);

            contextBook.Add(book);

            Console.WriteLine($"Book {book.Title} added");
        }
        static void RemoveBook() {
            Console.Write("Informe o título do livro: ");
            var name = Console.ReadLine();
            
            var book = contextBook.GetBookByName(name);

            if(book == null) {
                Console.WriteLine($"{name} Não Existe");
                return;
            }
            Console.Write($"Deseja mesmo remover {book.Title} yes/no: ");
            var remove = Console.ReadLine();
            
            if(remove == "yes") {
                contextBook.Remove(book);
                Console.WriteLine("Livro removido com sucesso");                
            }
            
            return;
        }
        static void GetBooks() {
            var books =  contextBook.GetBooks();

            foreach(var book in books) {
                Console.WriteLine($"Title: {book.Title}");
            }
        }

        static void GetBook() {
            Console.Write("Informe o nome do livro: ");
            var name = Console.ReadLine();

            var book = contextBook.GetBookByName(name);
            
            Console.WriteLine($"Título: {book.Title}");
            Console.WriteLine($"Gênero: {book.Genre}");
            Console.WriteLine($"Descrição: {book.Description}");            
        }

        
    }
}
