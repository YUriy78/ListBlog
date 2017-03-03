using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class GalleryDAO
    {
        private listblogEntities7 db = new listblogEntities7();

        public void AddGallery(string Category, string Name, string Folder, IEnumerable<HttpPostedFileBase> uploads)
        {
            CategoryGalleryDAO categoryDAO = new CategoryGalleryDAO();
            var cath = categoryDAO.GetCategoryName(Category);
            Gallery newGallery = new Gallery()
            {
                Name = Name,
                CategoryID = cath.Id,
                Folder = Folder
            };
            db.Galleries.Add(newGallery);
            db.SaveChanges();

            PhotoDAO photoDAO = new PhotoDAO();
            photoDAO.AddPhoto(newGallery, uploads);
            
        }

        public Gallery GetGallery(int Id)
        {
            var gallery = db.Galleries.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            return gallery;
        }

        public void DelGalleryCategoryId(int CategoryId)
        {
            
            var galleries = db.Galleries.Where(x => x.CategoryID.Equals(CategoryId));
            foreach (var item in galleries)
            {
                PhotoDAO dao = new PhotoDAO();
                dao.DelPhotos(item.Id);
                db.Galleries.Remove(item);

            }
            db.SaveChanges();
        }

        public void DelGalleryId(int GalleryId)
        {

            var gallery = db.Galleries.Where(x => x.Id.Equals(GalleryId)).FirstOrDefault();
            PhotoDAO dao = new PhotoDAO();
            dao.DelPhotos(gallery.Id);
            db.Galleries.Remove(gallery);
            db.SaveChanges();
            
        }


    }
}