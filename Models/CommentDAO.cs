using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class CommentDAO
    {
        private listblogEntities7 db = new listblogEntities7();

        public void AddComment(Comment comment)
        {
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        public void DelComment(int id)
        {
            var comment = db.Comments.Where(x => x.Id.Equals(id)).FirstOrDefault();
            db.Comments.Remove(comment);
            db.SaveChanges();
        }

        public ICollection<Comment> GetListComment()
        {
            return db.Comments.ToList();
        }

    }
}