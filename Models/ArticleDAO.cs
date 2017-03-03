using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class ArticleDAO
    {
        private Models.listblogEntities7 db = new listblogEntities7();

        public ICollection<Article> GetArticles(int take)
        {
            var articles = db.Articles.Take(take).OrderByDescending(x => x.Id).ToList();
            return articles;
        }

        public ICollection<Article> GetArticles()
        {
            var articles = db.Articles.ToList();
            return articles;
        }


        public Article GetArticle(int id)
        {
            var article = db.Articles.Where(x => x.Id.Equals(id)).FirstOrDefault();
            return article;
        }

        public void AddArticle(Article article)
        {
            db.Articles.Add(article);
            db.SaveChanges();
        }

        public void ChangeArticle(string section, Article article)
        {
            SectionDAO sectionDAO = new SectionDAO();
            var getSection = sectionDAO.GetSection(section);
            var articleChange = db.Articles.Where(x => x.Id.Equals(article.Id)).FirstOrDefault();
            if (!string.IsNullOrEmpty(section))
                articleChange.Section_articlesID = getSection.Id;
            articleChange.Title = article.Title;
            articleChange.Description = article.Description;
            articleChange.Body = article.Body;
            articleChange.Author = article.Author;
            articleChange.Date = article.Date;
            db.SaveChanges();
         }

        public void DeleteArticle(int id)
        {
            var article = db.Articles.Where(x => x.Id.Equals(id)).FirstOrDefault();
            db.Articles.Remove(article);
            db.SaveChanges();
            
        }


    }
}