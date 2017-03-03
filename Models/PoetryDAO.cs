using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class PoetryDAO
    {
        private Models.listblogEntities7 db = new listblogEntities7();

        public void AddPoetry(Poetry poetry)
        {
            db.Poetries.Add(poetry);
            db.SaveChanges();

        }

        public ICollection<Poetry> ListPoetry()
        {
            var listPoetry = db.Poetries.ToList();
            return listPoetry;
        }

        public Poetry SelectPoem(int Id)
        {
            var poem = db.Poetries.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            return poem;
        }

        public void DeletePoem(int Id)
        {
            var poem = db.Poetries.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            db.Poetries.Remove(poem);
            db.SaveChanges();
        }

        public void ChangePoetry(Poetry poetry)
        {
            var poem = db.Poetries.Where(x => x.Id.Equals(poetry.Id)).FirstOrDefault();
            poem.Title = poetry.Title;
            poem.Body = poetry.Body;
            poem.Date = poetry.Date;
            db.SaveChanges();

        }

    }
}