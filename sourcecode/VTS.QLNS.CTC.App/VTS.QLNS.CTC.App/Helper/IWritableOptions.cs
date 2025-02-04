using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Helper
{
    public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }
}
