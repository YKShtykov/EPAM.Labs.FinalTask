﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Interfaces
{
    public interface IUserService
    {
        void Logout(int userId);

        bool Login(int userId);

        int CreateUser(string userName);

        int GetCurrentUser();
        //safdhgjdsfsdsdsgfdsgfg

    }
}