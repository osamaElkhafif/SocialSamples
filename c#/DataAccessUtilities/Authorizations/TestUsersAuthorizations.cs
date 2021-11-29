using SocialDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SocialDataAccess.DataAccessUtilities.Authorizations
{
    public class TestUsersAuthorizations
    {

        public Dictionary<string,bool> testUsersUserNames { get; set; } = new Dictionary<string,bool>()
        {
            {"farouk",true},
            {"esmail",true},
            {"hadi",true},
            {"sameh",true},
            {"gamal",true},
            {"walid",true},
            {"hesham",true},
            {"maged",true},
            {"abdo",true},
            {"karim2",true},
            {"ayman",true},
            {"mazen",true},
            {"emad1",true},
            {"sherif",true},
            {"essam",true},
            {"fayez",true},
            {"fadi",true},
            {"serag",true},
            {"basel",true},
            {"tarek",true},
            {"bahaa",true},
            {"ammar",true},
            {"refaat",true},
            {"fouad",true},
            {"karam",true},
            {"fahd",true},
            {"fahmi",true},
            {"ahmed",true},
            {"mourad",true},
            {"fathi",true},
            {"diaa",true},
            {"emad",true},
            {"yasser",true},
            {"yaseen",true},
            {"saif",true},
            {"karim",true},
            {"qasem",true},
            {"dalia",true},
            {"tamer",true},
        };

        public bool autorize(ClaimsPrincipal User)
        {
            
            return !testUsersUserNames.ContainsKey(User.Identity.Name.ToLower());
        }
    }
}
