using System;
using System.Collections.Generic;

namespace MyBook
{
    public class App {

        private readonly  BookController _controllerBook;
        private Menu _menu;
        public App() {
            _controllerBook = new BookController();
            _menu = new Menu();
            _menu.AddOptions(   
                    new List<MenuOption>() {
                        new MenuOption
                            ( "Adicionar Livro", _controllerBook.AddBook ),
                        new MenuOption
                            ( "Atualizar Livro", _controllerBook.UpdateBook ),
                        new MenuOption
                            ( "Listar todos os livros", _controllerBook.GetAllBooks ),
                        new MenuOption
                            ( "Buscar por um livro", _controllerBook.GetBook ),
                        new MenuOption
                            ( "Remover livro", _controllerBook.RemoveBook ),
                        new MenuOption
                            ( "Sair", this.Sair,true)
            });

        }

        public void Run() {
            MenuOption menuOption;
            Console.CursorVisible = false;

            do {                
                menuOption = _menu.DisplayMenu();         

                do {
                    Console.Clear();
                    menuOption.Function.Invoke();
                    Console.Write("\n\nPressione Enter para voltar ao menu...");  
                }while(Console.ReadKey().Key != ConsoleKey.Enter );                
            } while(!menuOption.OptionExit.Equals(true));            
       }

        public void Sair() {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("\t\t Obrigado! Volte Sempre!!!!");
            Console.WriteLine("----------------------------------------------");     
            Environment.Exit(0);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}