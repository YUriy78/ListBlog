using ListBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListBlog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ArticleDAO dao = new ArticleDAO();
            return View(dao.GetArticles(4));
        }


        public ActionResult Article(string id)
        {   
            if(string.IsNullOrEmpty(id))
            {
                return Redirect("/Error/Http404");
            }
            try
            {
                int articleId = Convert.ToInt32(id);
                ArticleDAO dao = new ArticleDAO();
                var article = dao.GetArticle(articleId);
                if (article == null)
                {
                    return Redirect("/Error/Http404");
                }
                return View(article);
            }
            catch(Exception)
            {
                return Redirect("/Error/Http404");
            }
        }



        [ChildActionOnly]
        public ActionResult Footer()
        {
            FooterModel footer = new FooterModel();
            return PartialView(footer);
        }


        public ActionResult Articles()
        {
            ArticleDAO dao = new ArticleDAO();
            return View(dao.GetArticles());
        }

        public ActionResult Journals()
        {
            JournalDAO dao = new JournalDAO();
            return View(dao.GetJournalsAll());
        }

        public ActionResult Poetry()
        {
            PoetryDAO dao = new PoetryDAO();
            var listPoetry = dao.ListPoetry();
            return View(listPoetry);
        }

        public ActionResult Poem(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Redirect("/Error/Http404");
            }
            try
            {
                int poemId = Convert.ToInt32(id);
                PoetryDAO dao = new PoetryDAO();
                var poem = dao.SelectPoem(poemId);
                if (poem == null)
                {
                    return Redirect("/Error/Http404");
                }
                return View(poem);

            }
            catch(Exception)
            {
                return Redirect("/Error/Http404");
            }
         }



        public ActionResult Gallery()
        {
            CategoryGalleryDAO dao = new CategoryGalleryDAO();
            return View(dao.GetListCategories());
        }


        public ActionResult GalleryName(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Redirect("/Error/Http404");
            }
            try
            {
                int cathId = Convert.ToInt32(id);
                CategoryGalleryDAO dao = new CategoryGalleryDAO();
                var category = dao.GetCategoryId(cathId);
                if (category == null)
                {
                    return Redirect("/Error/Http404");
                }
                return View(category);
            }
            catch(Exception)
            {
                return Redirect("/Error/Http404");
            }

            
        }


        public ActionResult GalleryPhoto(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Redirect("/Error/Http404");
            }
            try
            {
                int galleryId = Convert.ToInt32(id);
                GalleryDAO dao = new GalleryDAO();
                var gallery = dao.GetGallery(galleryId);
                if (gallery == null)
                {
                    return Redirect("/Error/Http404");
                }
                return View(gallery);
            }
            catch (Exception)
            {
                return Redirect("/Error/Http404");
            }
                           
          }

        public ActionResult AddComment()
        {
            CommentDAO dao = new CommentDAO();
            var listComment = dao.GetListComment();
            return View(listComment);

        }

        [ChildActionOnly]
        public ActionResult AddCommentForm()
        {
           return PartialView();
        }


        
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            CommentDAO dao = new CommentDAO();
            if(string.IsNullOrEmpty(comment.Name))
            {
                ModelState.AddModelError("Name", "Нет вашего имени!");
            }
            if (string.IsNullOrEmpty(comment.Email))
            {
                ModelState.AddModelError("Email", "Нет вашего email!");
            }
            if (string.IsNullOrEmpty(comment.Body))
            {
                ModelState.AddModelError("Body", "Нет вашего сообщения!");
            }
            if (ModelState.IsValid)
            {
                Comment newComment = new Comment()
                {
                    Name = comment.Name,
                    Body = comment.Body,
                    Email = comment.Email,
                    Date = DateTime.Now.Date
                };
                dao.AddComment(newComment);
                return RedirectToAction("AddComment");
            }
            else
            {
                var listComment = dao.GetListComment();
                return View(listComment);
            }

        }

        
    }
}
