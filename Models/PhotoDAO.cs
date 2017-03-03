using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListBlog.Models
{
    public class PhotoDAO
    {
        private listblogEntities7 db = new listblogEntities7();

        public void AddPhoto(Gallery Name, IEnumerable<HttpPostedFileBase> uploads)
        {
            foreach(var item in uploads)
            {
                if (item != null)
                {
                    Photo photo = new Photo()
                    {
                        File = item.FileName,
                        GalleryID = Name.Id

                    };
                    db.Photos.Add(photo);
                    db.SaveChanges();
                }

            }

        }

        public void DelPhotos(int GalleryId)
        {
            var photos = db.Photos.Where(x => x.GalleryID.Equals(GalleryId));
            foreach(var item in photos)
            {
                db.Photos.Remove(item);
            }
            db.SaveChanges();
        }

    }
}