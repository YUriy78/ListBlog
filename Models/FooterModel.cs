using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class FooterModel
    {
        public FooterModel()
        {
            ArticleDAO articleDAO = new ArticleDAO();
            Articles = articleDAO.GetArticles(4);
            JournalDAO journalDAO = new JournalDAO();
            Journals = journalDAO.GetJournals(4);

        }


        public ICollection<Article> Articles { get; set; }
        public ICollection<Journal> Journals { get; set; }
    }
}