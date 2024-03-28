using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEEK2.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WEEK2.Controllers
{
    public class HomeController : Controller
    {
        private IBookInterface _bookInterface;

        public HomeController(IBookInterface bookInterface)
        {
            _bookInterface = bookInterface;
        }

        public ViewResult Index()
        {
            var model = _bookInterface.GetAllBooks();
            return View(model);

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Book book)
        {
            _bookInterface.AddBook(book);
            return RedirectToAction("Index");
            
        }

        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Update(Book book)
        { 
            _bookInterface.UpdateBook(book);
            return RedirectToAction("Index");

        }


        [HttpPost]
        public IActionResult DeleteUser(string ISBN)
        {
            //Book bookToDelete = book.Find((Book b) => b.ISBN == ISBN);
            var bookToDelete = _bookInterface.GetBook(ISBN);
            _bookInterface.DeleteBook(bookToDelete);
            return RedirectToAction("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
