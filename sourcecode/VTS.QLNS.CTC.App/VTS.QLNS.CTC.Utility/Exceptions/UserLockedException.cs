using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Exceptions
{
    public class UserLockedException : Exception
    {
        public string Username { get; set; }

        public UserLockedException(string username)
        {
            Username = username;
        }

        public UserLockedException(string message, string username) : base(message)
        {
            Username = username;
        }

        public UserLockedException(string message, Exception innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}
