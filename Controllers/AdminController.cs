using ListBlog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ListBlog.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Login, string Password)
        {
            if (string.IsNullOrEmpty(Login))
            {
                ModelState.AddModelError("Login", "Нет login!");
            }
            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Нет password!");
            }

            if (ModelState.IsValid)
            {
                AdminDAO dao = new AdminDAO();
                var admin = dao.GetAdmin(Login, dao.GetMD5(Password));
                if (admin != null)
                {
                    var cookie = FormsAuthentication.GetAuthCookie(Login, true);
                    Response.Cookies.Add(cookie);
                    return View();
                }
                ViewBag.Result = "Неправильный логин или пароль!";
            }

            return View();
        }

        [Authorize]
        public ActionResult Exit()
        {
            if (Request.Cookies["Admin"] != null)
            {
                HttpCookie myCookie = new HttpCookie("Admin");
                myCookie.Expires = DateTime.Now.AddMinutes(-60);
                Response.Cookies.Add(myCookie);
            }
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }



        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(string Password, string Password2)
        {
            if (!Password.Equals(Password2))
            {
                ViewBag.Result = "Пароли не совпадают!";
                return View();
            }

            AdminDAO dao = new AdminDAO();
            string adminName = FormsAuthentication.Decrypt(Request.Cookies["Admin"].Value.ToString()).Name;
            dao.ChangePassword(adminName, Password);
            return RedirectToAction("Index");

        }

        [Authorize]
        public ActionResult AddArticle()
        {
            SectionDAO sectionDAO = new SectionDAO();
            ViewBag.Section = sectionDAO.GetListSection();
            ViewBag.Count = sectionDAO.GetSectionCount();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddArticle(string Section, Article article)
        {
            SectionDAO sectionDAO = new SectionDAO();
            var section = sectionDAO.GetSection(Section);
            Article addArticle = new Article()
            {
                Section_articlesID = section.Id,
                Title = article.Title,
                Description = article.Description,
                Body = article.Body,
                Author = article.Author,
                Date = article.Date

            };

            ArticleDAO articleDAO = new ArticleDAO();
            articleDAO.AddArticle(addArticle);
            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult ChangeArticle(int id)
        {
            ArticleDAO articleDAO = new ArticleDAO();
            var article = articleDAO.GetArticle(id);
            SectionDAO sectionDAO = new SectionDAO();
            ViewBag.Section = sectionDAO.GetListSection();
            ViewBag.Count = sectionDAO.GetSectionCount();
            return View(article);

        }


        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangeArticle(string Section, Article article)
        {
            ArticleDAO articleDAO = new ArticleDAO();
            articleDAO.ChangeArticle(Section, article);

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult DeleteArticle(int id)
        {
            ArticleDAO articleDAO = new ArticleDAO();
            articleDAO.DeleteArticle(id);
            return Redirect("/Home/Articles/");
        }


        [Authorize]
        public ActionResult DeleteJournal(int id)
        {
            JournalDAO journaldao = new JournalDAO();
            journaldao.DeleteJournal(id);
            return Redirect("/Home/Journals/");
        }

        [Authorize]
        public ActionResult AddJournal()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddJournal(string Title, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string filename = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/Content/journal/" + filename));
                Journal journal = new Journal()
                {
                    Title = Title,
                    File = upload.FileName,
                    Date = DateTime.Now.Date
                };

                JournalDAO journaldao = new JournalDAO();
                journaldao.AddJournal(journal);
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
                       
        }
        
        [Authorize]
        public ActionResult AddSection()
        {
           return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddSection(Section_articles section)
        {
            SectionDAO dao = new SectionDAO();
            dao.AddSection(section);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult DeleteSection()
        {
            SectionDAO dao = new SectionDAO();
            var list_section = dao.GetListSection();
            ViewBag.Count = dao.GetSectionCount();
            return View(list_section);

        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteSection(string Section)
        {
            SectionDAO dao = new SectionDAO();
            dao.DeleteSection(Section);
            return RedirectToAction("Index");
        }

        
        [Authorize]
        public ActionResult AddPoetry()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPoetry(Poetry poetry)
        {
            PoetryDAO dao = new PoetryDAO();
            dao.AddPoetry(poetry);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult DeletePoem(int Id)
        {
            PoetryDAO dao = new PoetryDAO();
            dao.DeletePoem(Id);
            return Redirect("/Home/Poetry/");
        }

        [Authorize]
        public ActionResult ChangePoem(int Id)
        {
            PoetryDAO dao = new PoetryDAO();
            var poem = dao.SelectPoem(Id);
            return View(poem);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChangePoem(Poetry poetry)
        {
            PoetryDAO dao = new PoetryDAO();
            dao.ChangePoetry(poetry);
            return Redirect("/Home/Poetry/");
        }

        [Authorize]
        public ActionResult AddCategoryGallery()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddCategoryGallery(CategoryGallery category)
        {
            CategoryGalleryDAO dao = new CategoryGalleryDAO();
            dao.AddCategory(category);
            return View();
            
        }


        [Authorize]
        public ActionResult AddGallery()
        {
            CategoryGalleryDAO dao = new CategoryGalleryDAO();
            ViewBag.Categories = dao.GetListCategories();
            ViewBag.Count = dao.GetCountCategories();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddGallery(string Category, string Name, string Folder, IEnumerable<HttpPostedFileBase> uploads)
        {
            CategoryGalleryDAO dao = new CategoryGalleryDAO();
            ViewBag.Categories = dao.GetListCategories();
            ViewBag.Count = dao.GetCountCategories();
            var folder = Server.MapPath("~/Content/Gallery/"+Folder);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                foreach (var file in uploads)
                {
                    if (file != null)
                    {
                        string filename = Path.GetFileName(file.FileName);
                        file.SaveAs(Server.MapPath("~/Content/Gallery/" + Folder + "/" + filename));
                    }

                }

                GalleryDAO galleryDAO = new GalleryDAO();
                galleryDAO.AddGallery(Category, Name, Folder, uploads);
            }
            else
            {
                ViewBag.Warning = "Галерея с таким именем уже существует!";
            }                
            return View();
        }


        [Authorize]
        public ActionResult DelCategory(int Id)
        {
            CategoryGalleryDAO dao = new CategoryGalleryDAO();
            dao.DelCategory(Id);
            return Redirect("/Home/Gallery/");

        }

        [Authorize]
        public ActionResult DelGallery(int Id)
        {
            GalleryDAO dao = new GalleryDAO();
            dao.DelGalleryId(Id);
            return Redirect("/Home/Gallery/");
        }

        [Authorize]
        public ActionResult DelComment(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Redirect("/Error/Http404");
            }
            try
            {
                int commentId = Convert.ToInt32(id);
                CommentDAO dao = new CommentDAO();
                dao.DelComment(commentId);
                return Redirect("/Home/AddComment/");
            }
            catch (Exception)
            {
                return Redirect("/Error/Http404");
            }

        }



    }
}
