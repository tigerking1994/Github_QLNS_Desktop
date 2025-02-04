using System;

namespace VTS.QLNS.CTC.Utility.Exceptions
{
    public class WrongReportException : Exception
    {
        public WrongReportException()
        {
        }

        public WrongReportException(string message) : base(message)
        {
        }

    }
}
