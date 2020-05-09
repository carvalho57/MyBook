using System; 

namespace MyBook.Models {
    public class Book {
        
        public int? BookID { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public bool Readed { get; set; }

        public bool Favorite { get; set; }

        public Book() {

        }
        public Book(string title, string genre, string description, bool readed = false, bool favorite = false) {
            Title = title;
            Genre = genre;
            Description = description;
            Readed = readed;
            Favorite = favorite;            
        }

         public Book(int id, string title, string genre, string description, bool readed = false, bool favorite = false) {
            BookID = (int)id; 
            Title = title;
            Genre = genre;
            Description = description;
            Readed = readed;
            Favorite = favorite;            
        }


        public override string ToString() {
            return $"{BookID}{Title}\t{Genre}\t{Description}";
        }
    }
}