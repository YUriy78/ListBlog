//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ListBlog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Gallery
    {
        public Gallery()
        {
            this.Photos = new HashSet<Photo>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public string Folder { get; set; }
    
        public virtual CategoryGallery CategoryGallery { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
