using System;
using MyBook.Models;
using MyBook.Data;
using System.Collections.Generic;
namespace MyBook
{
    class Program
    {
        private static App Aplication { get; set;}        
        static void Main(string[] args)
        {
            App Aplication = new App();
            Aplication.Run();
        }
    }
}
