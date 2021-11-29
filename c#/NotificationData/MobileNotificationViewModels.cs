using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialDataAccess.Models.NotificationData
{
    public class AddToken
    {
        [Required]
        public string NewToken { get; set; }

        public AppType appType { get; set; } = AppType.nativeAndroid;

    }

    public class UpdateToken
    {
        [Required]
        public string OldToken { get; set; }
        [Required]
        public string NewToken { get; set; }
        public AppType appType { get; set; } = AppType.nativeAndroid;


    }

    public class RemoveToken
    {
        [Required]
        public string TokenToDelete { get; set; }
        public AppType appType { get; set; } = AppType.nativeAndroid;


    }



    public enum AppType
    {
        nativeAndroid=1,
        Flutter=2
    }

}
