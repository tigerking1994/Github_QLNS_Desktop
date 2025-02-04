using System;
using System.Collections.Generic;
using System.Text;
using FlexCel.Core;
using FlexCel.Report;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Service.UserFunction
{
    public class CurrencyToText : TFlexCelUserFunction
    {

        public CurrencyToText()
        {

        }
        public override object Evaluate(object[] parameters)
        {
            if (parameters == null)
                throw new ArgumentException("Bad parameter count in call to CurrencyToText() user-defined function");
            if (parameters.Length == 2)
            {
                return StringUtils.NumberToText(Convert.ToInt64(parameters[0]), true, parameters[1].ToString());
            }
            return StringUtils.NumberToText(Convert.ToInt64(parameters[0]));
        }
    }
}
