using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_SpaBackend.DataObjects
{
    public enum SocialPlatform
    {
        facebook=0,
        google=1,
        mail=2
    }
    public class PlatformID : EntityData
    {
        [Required, Column(TypeName = "VARCHAR")]
        public string PlatformId { get; set; }
        [Required, Range(0,2, ErrorMessage ="Platform Does not exist")]
        public SocialPlatform SocialPlatform { get; set; }


    }
}