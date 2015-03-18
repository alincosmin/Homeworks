using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using Tema2.Models;

namespace Tema2.Controllers
{
    public class BooksController : ApiController
    {
        private IList<Book> _books
        {
            get { return HttpRuntime.Cache["books"] as IList<Book>; }
        }

        [AcceptVerbs("GET","HEAD")]
        public Book[] GetAll()
        {
            return _books.ToArray();
        }

        [AcceptVerbs("GET", "HEAD")]
        public Book Get(int id)
        {
            var book = _books.SingleOrDefault(x => x.Id == id);

            if (book == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return book;
        }

        [HttpPost]
        public HttpResponseMessage AddBook([FromBody] AddBookRequest newBook)
        {
            var id = _books.Max(x => x.Id);

            if(newBook.Authors == null || newBook.Name == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var book = new Book()
            {
                Authors = newBook.Authors,
                BookName = newBook.Name,
                Id = id + 1
            };

            _books.Add(book);

            return Request.CreateResponse(HttpStatusCode.Created, book);
        }

        [HttpPut]
        public HttpResponseMessage AddOrUpdateBook(int id, [FromBody] Book newBook)
        {
            Book existing;
            var isNew = false;

            if (IsValid(newBook))
            {
                existing = _books.SingleOrDefault(x => x.Id == id);
                if (existing == null)
                {
                    existing = newBook;
                    existing.Id = id;
                    _books.Add(existing);
                    isNew = true;
                }

                existing.Authors = newBook.Authors;
                existing.BookName = newBook.BookName;
            }
            else throw new HttpResponseException(HttpStatusCode.BadRequest);

            var retCode = isNew ? HttpStatusCode.Created : HttpStatusCode.OK;
            return Request.CreateResponse(retCode, existing);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteBook(int id)
        {
            var existing = _books.SingleOrDefault(x => x.Id == id);
            if (existing == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _books.Remove(existing);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        private bool IsValid(Book newBook)
        {
            var ok = !string.IsNullOrEmpty(newBook.BookName);
            ok &= newBook.Authors!=null && newBook.Authors.Length != 0;
            ok &= newBook.Authors != null && !newBook.Authors.Any(string.IsNullOrEmpty);
            return ok;
        }
    }
}