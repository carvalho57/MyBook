using System;
using System.Linq;
using System.Collections;
using MyBook.Data;
using MyBook.Models;

namespace MyBook {
    public class App {
        private readonly  ManagerBook _managerBook;
        public App() {
            _managerBook = new ManagerBook();
        }
       
       public void Run() {
            int option = 0;
            Console.CursorVisible = false;

            do {
                Console.CursorVisible = false;
                option = DisplayMenu();                
                ExecuteOption(option);
                Console.Write("\nPressione Enter para continuar...");


            } while(Console.ReadKey().Key == ConsoleKey.Enter);
       }


        private int DisplayMenu() {
                      
            string[] options = {"Adicionar Livro", "Listrar todos os livros", "Buscar por um livro","Remover livro"}; 

            int currentOption = 0; 
            ConsoleKey key;             
            
            do{

                
                Console.Clear();                
                Console.WriteLine("\n\n\t\tWelcome to myBook\n\n");   

                for(int i = 0; i < options.Length; i++) {
                    if(currentOption == i) {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch(key) {
                    case ConsoleKey.UpArrow:
                        if(currentOption > 0) {
                            currentOption--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if(currentOption < options.Length - 1) {
                            currentOption++;
                        }
                        break;                    
                }

            } while(key != ConsoleKey.Enter);

            return currentOption;
        }
        
        private void ExecuteOption(int option) {
            switch(option) {
                case 0: 
                    AddBook();
                    break;
                case 1:
                    GetBooks();
                    break;
                case 2:
                    GetBook();
                    break;
                case 3:
                    RemoveBook();
                    break;
                default:
                    Console.WriteLine("Informe uma opção válida");
                    break;
            }
            return;
        }
        private void AddBook(bool limparTela = true)
        {   
            LimparTela(limparTela);
            Console.WriteLine("\nInsira as informações do livro \n");
            Console.Write("Título:");
            string title = Console.ReadLine();
            Console.Write("Gênero:");
            string genre = Console.ReadLine();
            Console.Write("Descrição:");
            string description = Console.ReadLine();
            
            var book = new Book(title, genre, description);

            var sucess = _managerBook.Add(book);
            if(!sucess)  {
                Console.WriteLine("Problema ao inserir novo livro, digite as informações novamente");
                AddBook(false);
            }            

            Console.WriteLine($"Book {book.Title} adicionado com sucesso!");
            Console.WriteLine("Pressione backspace para retornar ou enter para continuar adicionando");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                AddBook();
            }
            return;
        }

        private void RemoveBook(bool limparTela = true)
        {
            LimparTela(limparTela);
            Console.Write("Informe o título do livro: ");
            var name = Console.ReadLine();
            var book = _managerBook.GetBookByName(name);

            if (book == null)
            {
                Console.WriteLine($"{name} Não Existe");
                Console.WriteLine("Pressione backspace para retornar");
                Console.ReadKey();
                return;
            }
            PrintBookonScreen(book);
            Console.Write($"Deseja mesmo remover {book.Title} yes/no: ");
            var remove = Console.ReadLine();

            if (remove == "yes")
            {
                _managerBook.Remove(book);
                Console.WriteLine("Livro removido com sucesso");
            }

            return;
        }
        private void LimparTela(bool limpar) {
            if(limpar)
                Console.Clear();
        }
        private void GetBooks()
        {
            var books = _managerBook.GetBooks();
            Console.Clear();
            foreach (var book in books)
            {
                PrintBookonScreen(book);                
            }

            Console.WriteLine("Pressione backspace para retornar");
            Console.ReadKey();
        }

        private void GetBook()
        {
            Console.Clear();
            Console.Write("Informe o nome do livro: ");
            var name = Console.ReadLine();

            var book = _managerBook.GetBookByName(name);
            if (book != null)
            {
                PrintBookonScreen(book);
                return;
            }

            Console.WriteLine("Livro não encontrado");
            Console.WriteLine("Pressione backspace para retornar");
            Console.ReadKey();
        }

        private void PrintBookonScreen(Book book) {
            if(!(book is null)) {
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Gênero: {book.Genre}");
                Console.WriteLine($"Descrição: {book.Description}");
                Console.WriteLine(string.Format("Lido: {0}",book.Readed ? "Sim": "Não"));
                Console.WriteLine("-------------------------------");
            }            
        }
    }
}