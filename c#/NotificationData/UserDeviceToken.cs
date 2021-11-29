using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialDataAccess.Models.NotificationData
{
    public class UserDeviceToken
    {

        public Guid Id { get; set; }

        [ForeignKey("appUser")]
        [Required]
        public string UserId { get; set; }

        [Required]
        public string appType { get; set; }

        [Required]
        public string DeviceToken { get; set; }

        public ApplicationUser appUser { get; set; }

    }
}
