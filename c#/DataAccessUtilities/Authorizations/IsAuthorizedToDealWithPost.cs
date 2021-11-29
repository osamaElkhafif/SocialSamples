using Microsoft.AspNetCore.Authentication;
using SocialDataAccess.Enums;
using SocialDataAccess.Models;
using SocialDataAccess.Models.KeyLessModels;
using SocialDataAccess.Models.ModelsRepository;
using SocialDataAccess.Models.PostsModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialDataAccess.DataAccessUtilities.Authorizations
{
    public static partial class CustomAuthorizations
    {


        public static async Task<Boolean> IsAutorizedToDealWithResource(UserDataPrivacy privacy,
            ApplicationUser appUser,ApplicationUser resourceOwner ,IUsersRelationsRepository usersRelationsRepository)
        {

            if (privacy.id == (short)PostPrivacyEnum.Public)
            {
                return true;
            }


            else if (privacy.id == (short)PostPrivacyEnum.Friends)
            {

               if(resourceOwner.Id == appUser.Id)
               {
                    return true;
               }

                return await usersRelationsRepository.AreFriends(resourceOwner, appUser);

            }

            else if (privacy.id == (short)PostPrivacyEnum.OnlyMe)
            {

                return resourceOwner.Id == appUser.Id;

            }

            else
            {
                throw new Exception("invalid data passed to IsAutorizedToDealWithPost");
            }



        }
    }
}
