using System;
using MyBook.Models;
using MyBook.Data;
using System.Collections.Generic;
namespace MyBook
{
    class Program
    {
        private static BookAccess contextBook { get; set;}
        public Program() {
            contextBook = new BookAccess();
        }
        static void Main(string[] args)
        {
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
            } while(Console.ReadKey().Key == ConsoleKey.Enter);
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
        }
        static void AddBook() {
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

        }
        static IEnumerable<Book> GetBooks() {
            return new List<Book>();
        }

        static Book GetBook() {
            return new Book();
        }

        
    }
}
