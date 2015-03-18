using System.Web;

namespace Tema2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string[] Authors { get; set; }

        public string URI
        {
            get { return "/api/books/" + Id; }
        }

        public string Rel
        {
            get { return "/api/books"; }
        }
    }
}