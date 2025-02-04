using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Exceptions
{
    public class NoUserException : Exception
    {
        public string Username { get; set; }

        public NoUserException(string username)
        {
            Username = username;
        }

        public NoUserException(string message, string username) : base(message)
        {
            Username = username;
        }

        public NoUserException(string message, Exception innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}
