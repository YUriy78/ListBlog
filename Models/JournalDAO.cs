using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class JournalDAO
    {
       private listblogEntities7 db = new listblogEntities7();

       public ICollection<Journal> GetJournals(int take)
       {
           var journals = db.Journals.Take(take).OrderByDescending(x => x.Id).ToList();
           return journals;

       }

       public ICollection<Journal> GetJournalsAll()
       {
           var journals = db.Journals.OrderByDescending(x => x.Id).ToList();
           return journals;
       }

       public void DeleteJournal(int id)
       {
           var article = db.Journals.Where(x => x.Id.Equals(id)).FirstOrDefault();
           db.Journals.Remove(article);
           db.SaveChanges();

       }

       public void AddJournal(Journal journal)
       {
           db.Journals.Add(journal);
           db.SaveChanges();

       }


    }
}