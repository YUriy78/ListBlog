using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class CategoryGalleryDAO
    {
        private Models.listblogEntities7 db = new listblogEntities7();

        public void AddCategory(CategoryGallery category)
        {
            db.CategoryGalleries.Add(category);
            db.SaveChanges();
        }

        public ICollection<CategoryGallery> GetListCategories()
        {
            return db.CategoryGalleries.ToList();
        }

        public int GetCountCategories()
        {
            return db.CategoryGalleries.Count();
        }

        public CategoryGallery GetCategoryName(string name)
        {
            var category = db.CategoryGalleries.Where(x => x.Name.Equals(name)).FirstOrDefault();
            return category;

        }

        public CategoryGallery GetCategoryId(int Id)
        {
            var category = db.CategoryGalleries.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            return category;
        }

        public void DelCategory(int CategoryId)
        {
            GalleryDAO dao = new GalleryDAO();
            dao.DelGalleryCategoryId(CategoryId);
            var category = db.CategoryGalleries.Where(x => x.Id.Equals(CategoryId)).FirstOrDefault();
            db.CategoryGalleries.Remove(category);
            db.SaveChanges();
        }



    }
}