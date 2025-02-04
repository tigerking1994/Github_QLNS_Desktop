using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain.Query.Shared;

namespace VTS.QLNS.CTC.App.Extensions
{
    public static class SortingHierarchicalNumber<TKey>
    {
        /// <summary>
        ///     Trả về 1 list số thứ tự phân cấp đã sắp xếp.
        /// </summary>
        /// <param name="values">
        ///     List số thứ tự
        /// </param>
        /// <param name="charSplit">
        ///     Kí tự phân cấp trong số thứ tự VD: 1.1.1 => '.'; 01-01-02 => '-'
        /// </param>
        /// <param name="orderType">
        ///     type = 1 => ASC, other => DESC
        /// </param>
        /// <returns></returns>
        public static List<SortingListQuery<TKey>> GetSortingLists(List<SortingListQuery<TKey>> values, char charSplit = '.', int orderType = 1)
        {
            foreach (var item in values)
            {
                if (item == null)
                {
                    continue;
                }
                var lstStt = item.SortKey.Split(charSplit);
                var orderStt = string.Join("", lstStt.Select(x => ConvertIntToAlpha(int.TryParse(x, out int n) ? n : -1)).ToList());
                item.SortKeyConverter = orderStt;
            }

            if (orderType == 1)
            {
                return values.OrderBy(x => x.SortKeyConverter).ToList();
            }
            else
            {
                return values.OrderByDescending(x => x.SortKeyConverter).ToList();
            }
        }

        /// <summary>
        ///     Convert number to alpha VD: 1 => A, 2 => B,...
        /// </summary>
        /// <param name="i"></param>
        private static string ConvertIntToAlpha(int i)
        {
            string result = "";
            if (i == -1)
            {
                return result;
            }

            do
            {
                result = (char)((i - 1) % 26 + 'A') + result;
                i = (i - 1) / 26;
            } while (i > 0);
            return result;
        }
    }
}
