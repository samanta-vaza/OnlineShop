using System;
using System.Collections.Generic;
using System.Text;

using OnlineShop.DataAccessLayer.Entities;

namespace OnlineShop.Core.Interfaces
{
    public interface IUser
    {
        bool ExistsPermisssion(int permissionID, int roleID);
        int GetUserRole(string username);

        string GetUserRoleName(string username);

        Store GetUserStore(string username);

        bool ExistsMailActivate(string username, string code);

        bool ExistsMobileActivate(string username, string code);

        void ActiveMailAddress(string mailAddress);

        void ActiveMobileNumber(string mobileNumber);




    }
}
