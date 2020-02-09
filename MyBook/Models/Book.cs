using System; 

namespace MyBook.Models {
    public class Book {
        
        public int BookID { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public bool Favorite { get; set; }

        public Book() {

        }
        public Book(string title, string genre, string description, bool status = false, bool favorite = false) {
            Title = title;
            Genre = genre;
            Description = description;
            Status = status;
            Favorite = favorite;            
        }
    }
}