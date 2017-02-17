using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackUndertow.Models
{
    public class ImageUploadViewModel
    {
        [Required]
        public string Caption { get; set; }

        [Required]
        public HttpPostedFile File { get; set; }
    }

    public class ImgUpload
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string File { get; set; }
        public string TypeRef { get; set; }
        
        //If Type ProfilePic, ref == null. If Type QAttach, ref == Question.Id
        public int? RefId { get; set; }

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual string FilePath
        {
            get
            {
                return $"/Uploads/{File}";
            }
        }

    }
}