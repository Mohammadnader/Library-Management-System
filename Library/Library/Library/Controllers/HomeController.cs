using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _dbContext; 

        public HomeController(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Helper method to get current user from session
        private User? GetCurrentUser()
        {
            var userJson = HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(userJson))
                return null;
            
            return JsonSerializer.Deserialize<User>(userJson);
        }

        // Helper method to set current user in session
        private void SetCurrentUser(User user)
        {
            var userJson = JsonSerializer.Serialize(user);
            HttpContext.Session.SetString("CurrentUser", userJson);
        }

        // Helper method to check if user is admin
        private bool IsAdmin()
        {
            var currentUser = GetCurrentUser();
            return currentUser != null && currentUser.Role == UserRoles.ADMIN;
        }

        // Helper method to check if user is logged in
        private bool IsLoggedIn()
        {
            return GetCurrentUser() != null;
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public async Task<IActionResult> AllBooks(string searchTerm = "", string category = "")
        {
            var books = _dbContext.Books.AsQueryable();

            // Apply search filter if search term is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                books = books.Where(b => 
                    b.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    b.Author.ToLower().Contains(searchTerm.ToLower()) ||
                    b.Category.ToLower().Contains(searchTerm.ToLower()) ||
                    b.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            // Apply category filter if category is provided
            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                books = books.Where(b => b.Category.ToLower() == category.ToLower());
            }

            var bookList = await books.ToListAsync();
            
            // Get all unique categories for the filter dropdown
            var categories = await _dbContext.Books
                .Select(b => b.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            ViewBag.IsAdmin = IsAdmin();
            ViewBag.IsLoggedIn = IsLoggedIn();
            ViewBag.SearchTerm = searchTerm;
            ViewBag.SelectedCategory = category;
            ViewBag.Categories = categories;
            ViewBag.TotalBooks = await _dbContext.Books.CountAsync();
            ViewBag.FilteredBooks = bookList.Count;

            return View(bookList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            ViewBag.IsAdmin = IsAdmin();
            ViewBag.IsLoggedIn = IsLoggedIn();
            return View(book);
        }

        public async Task<IActionResult> Download(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            // In a real application, you would serve the actual file
            // For demo purposes, we'll create a simple text file
            var fileName = $"{book.Title.Replace(" ", "_")}.pdf";
            var content = $"This is a demo PDF file for '{book.Title}' by {book.Author}.\n\n" +
                         $"Description: {book.Description}\n\n" +
                         $"Published: {book.YearPublished}\n" +
                         $"Pages: {book.Pages}\n" +
                         $"Language: {book.Language}\n\n" +
                         "In a real application, this would be the actual book content.";

            var bytes = System.Text.Encoding.UTF8.GetBytes(content);
            return File(bytes, "application/pdf", fileName);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            if (!IsLoggedIn())
                return RedirectToAction("SignIn");

            if (!IsAdmin())
                return RedirectToAction("AccessDenied");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book_characteristics book)
        {
            if (!IsLoggedIn())
                return RedirectToAction("SignIn");

            if (!IsAdmin())
                return RedirectToAction("AccessDenied");

            if (ModelState.IsValid)
            {
                _dbContext.Books.Add(book);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Book added successfully!";
                return RedirectToAction("AllBooks");
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("SignIn");

            if (!IsAdmin())
                return RedirectToAction("AccessDenied");

            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book_characteristics updatedBook)
        {
            if (!IsLoggedIn())
                return RedirectToAction("SignIn");

            if (!IsAdmin())
                return RedirectToAction("AccessDenied");

            if (ModelState.IsValid)
            {
                _dbContext.Books.Update(updatedBook);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Book updated successfully!";
                return RedirectToAction("AllBooks");
            }
            return View(updatedBook);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("SignIn");

            if (!IsAdmin())
                return RedirectToAction("AccessDenied");

            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("SignIn");

            if (!IsAdmin())
                return RedirectToAction("AccessDenied");

            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Book deleted successfully!";
            return RedirectToAction("AllBooks");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            if (IsLoggedIn())
                return RedirectToAction("AllBooks");
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == loginUser.Email.ToLower());

                if (user != null && user.Password == loginUser.Password)
                {
                    SetCurrentUser(user);
                    TempData["SuccessMessage"] = $"Welcome back, {user.Username}!";
                    return RedirectToAction("AllBooks");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            return View(loginUser);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            if (IsLoggedIn())
                return RedirectToAction("AllBooks");
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User newUser)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == newUser.Email.ToLower());

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(newUser);
                }

                // Set default role and maintain backward compatibility
                newUser.Role = UserRoles.USER;
                newUser.Key = UserRoles.USER;
                
                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                SetCurrentUser(newUser);
                TempData["SuccessMessage"] = $"Welcome to the Library, {newUser.Username}!";
                return RedirectToAction("AllBooks");
            }

            return View(newUser);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Welcome");
        }

        public IActionResult AccessDenied()
        {
            ViewBag.Message = "You don't have permission to access this page. Only administrators can perform this action.";
            return View();
        }

        // Action to promote a user to admin (for testing purposes)
        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(string email)
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied");

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                user.Role = UserRoles.ADMIN;
                user.Key = UserRoles.ADMIN; // Maintain backward compatibility
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = $"User {user.Username} has been promoted to Admin.";
            }
            else
            {
                TempData["ErrorMessage"] = "User not found.";
            }

            return RedirectToAction("AllBooks");
        }
    }
}
