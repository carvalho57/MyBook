using System;
using MyBook.Models;
using System.Collections.Generic;
using MyBook.Data;

namespace MyBook
{
    public class MenuOption {
        private static int CountOptions {get;set;}
        public int NumberOption {get;}
        public string Description { get; private set; }
        public Action Function {get; private set;}
        public bool OptionExit {get;set;}

        public MenuOption(string message, Action function, bool optionExit = false) {
            this.NumberOption = CountOptions++;
            this.Description = message;
            this.Function = function;
            this.OptionExit = optionExit;
        }
    }
}