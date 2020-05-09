using System;
using MyBook.Models;
using System.Collections.Generic;
using MyBook.Data;

namespace MyBook
{
    public class Menu {
        private List<MenuOption> MenuOptions{get;set;}
        private int CountOption{get;set;}
        public Menu() {
            MenuOptions = new List<MenuOption>();
        }
        public void AddOption(MenuOption options) {
            MenuOptions.Add(options);            
        }
        public void AddOptions(ICollection<MenuOption> options) {
            MenuOptions.AddRange(options);
        }

        private void DisplayMenuOptions(ICollection<MenuOption> options, int selectedOption){                    
            Console.WriteLine("\n\n\t\tWelcome to myBook\n\n");   
            foreach(MenuOption option in options) {
                if(option.NumberOption == selectedOption) {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"\t\t{option.Description}");
                Console.ResetColor();
            }            
        }

        public MenuOption DisplayMenu() {                           
            int currentOption = 0; 
            ConsoleKey key;             
            
            do{                
                Console.Clear();       
                DisplayMenuOptions(MenuOptions, currentOption);
                key = Console.ReadKey(true).Key;

                switch(key) {
                    case ConsoleKey.UpArrow:
                        if(currentOption > 0) {
                            currentOption--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if(currentOption < MenuOptions.Count - 1) {
                            currentOption++;
                        }
                        break;                    
                }

            } while(key != ConsoleKey.Enter);

            return MenuOptions[currentOption];
        }
        public Action GetMethodOption(int option) {
            return MenuOptions[option].Function;
        }
    }
}