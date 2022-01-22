﻿using PassGuardianWS.Models;

namespace PassGuardianWS.Interfaces
{
    public interface IUser :ICRUD<User>
    {
        User Login(User entity);
    }
}
