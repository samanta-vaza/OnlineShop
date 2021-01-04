using System;
using System.Collections.Generic;
using System.Text;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using OnlineShop.DataAccessLayer.Context;
using OnlineShop.DataAccessLayer.Entities;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Services;

namespace OnlineShop.Core.Classes
{

    public class RoleAttribute : AuthorizeAttribute, IAuthorizationFilter
    {

        int _permissionID;
        IUser _iuser;

        public RoleAttribute(int permissionID)
        {
            _permissionID = permissionID;

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string username = context.HttpContext.User.Identity.Name;
                _iuser = (IUser)context.HttpContext.RequestServices.GetService(typeof(IUser));
                int roleID = _iuser.GetUserRole(username);

                if (_iuser.ExistsPermisssion(_permissionID, roleID))
                {
                    
                    context.Result = new RedirectResult("/Accounts/Login");
                }

            }
            else
            {
                context.Result = new RedirectResult("/Accounts/Login");
            }
        }
    }
}
