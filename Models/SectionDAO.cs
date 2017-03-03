using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class SectionDAO
    {
        private listblogEntities7 db = new listblogEntities7();

        public ICollection<string> GetListSection()
        {
            var section = db.Section_articles.Select(x => x.Name).ToList();
            return section;
        }

        public Section_articles GetSection(string name)
        {
            var section = db.Section_articles.Where(x => x.Name.Equals(name)).FirstOrDefault();
            return section;
        }

        public int GetSectionCount()
        {
            var count = db.Section_articles.Count();
            return count;

        }

        public void AddSection(Section_articles section)
        {
            db.Section_articles.Add(section);
            db.SaveChanges();
        }

        public void DeleteSection(string name)
        {
            var section = db.Section_articles.Where(x => x.Name.Equals(name)).FirstOrDefault();
            db.Section_articles.Remove(section);
            db.SaveChanges();

        }

    }
}