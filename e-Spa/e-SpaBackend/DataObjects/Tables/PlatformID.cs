using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_SpaBackend.DataObjects
{
    public enum SocialPlatform { facebook, google }
    public class PlatformID : EntityData
    {
        [Required, Column(TypeName = "VARCHAR")]
        public string PlatformId { get; set; }
        [Required]
        public SocialPlatform SocialPlatform { get; set; }

        public virtual Client Client { get; set; }

    }
}