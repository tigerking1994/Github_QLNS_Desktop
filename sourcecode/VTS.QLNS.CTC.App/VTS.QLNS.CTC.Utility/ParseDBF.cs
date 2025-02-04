using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace VTS.QLNS.CTC.Utility
{
    public class ParseDBF
    {
        public static DataTable ReadDBF(string dbfFile)
        {
            long ticks1 = DateTime.Now.Ticks;
            DataTable dataTable = new DataTable();
            if (!File.Exists(dbfFile))
                return dataTable;
            BinaryReader binaryReader1 = (BinaryReader)null;
            try
            {
                binaryReader1 = new BinaryReader((Stream)File.OpenRead(dbfFile));
                GCHandle gcHandle1 = GCHandle.Alloc((object)binaryReader1.ReadBytes(Marshal.SizeOf(typeof(ParseDBF.DBFHeader))), GCHandleType.Pinned);
                ParseDBF.DBFHeader structure = (ParseDBF.DBFHeader)Marshal.PtrToStructure(gcHandle1.AddrOfPinnedObject(), typeof(ParseDBF.DBFHeader));
                gcHandle1.Free();
                ArrayList arrayList = new ArrayList();
                while (13 != binaryReader1.PeekChar())
                {
                    GCHandle gcHandle2 = GCHandle.Alloc((object)binaryReader1.ReadBytes(Marshal.SizeOf(typeof(ParseDBF.FieldDescriptor))), GCHandleType.Pinned);
                    arrayList.Add((object)(ParseDBF.FieldDescriptor)Marshal.PtrToStructure(gcHandle2.AddrOfPinnedObject(), typeof(ParseDBF.FieldDescriptor)));
                    gcHandle2.Free();
                }
                binaryReader1.BaseStream.Seek((long)((int)structure.headerLen + 1), SeekOrigin.Begin);
                BinaryReader binaryReader2 = new BinaryReader((Stream)new MemoryStream(binaryReader1.ReadBytes((int)structure.recordLen)));
                DataColumn column = (DataColumn)null;
                foreach (ParseDBF.FieldDescriptor fieldDescriptor in arrayList)
                {
                    Encoding.ASCII.GetString(binaryReader2.ReadBytes((int)fieldDescriptor.fieldLen));
                    switch (fieldDescriptor.fieldType)
                    {
                        case 'C':
                            column = new DataColumn(fieldDescriptor.fieldName, typeof(string));
                            break;
                        case 'D':
                            column = new DataColumn(fieldDescriptor.fieldName, typeof(DateTime));
                            break;
                        case 'F':
                            column = new DataColumn(fieldDescriptor.fieldName, typeof(double));
                            break;
                        case 'L':
                            column = new DataColumn(fieldDescriptor.fieldName, typeof(bool));
                            break;
                        case 'N':
                            column = fieldDescriptor.count <= (byte)0 ? new DataColumn(fieldDescriptor.fieldName, typeof(long)) : new DataColumn(fieldDescriptor.fieldName, typeof(Decimal));
                            break;
                        case 'T':
                            column = new DataColumn(fieldDescriptor.fieldName, typeof(DateTime));
                            break;
                    }
                    dataTable.Columns.Add(column);
                }
                binaryReader1.BaseStream.Seek((long)structure.headerLen, SeekOrigin.Begin);
                for (int index = 0; index <= structure.numRecords - 1; ++index)
                {
                    BinaryReader binaryReader3 = new BinaryReader((Stream)new MemoryStream(binaryReader1.ReadBytes((int)structure.recordLen)));
                    if (binaryReader3.ReadChar() != '*')
                    {
                        int columnIndex = 0;
                        DataRow row = dataTable.NewRow();
                        foreach (ParseDBF.FieldDescriptor fieldDescriptor in arrayList)
                        {
                            switch (fieldDescriptor.fieldType)
                            {
                                case 'C':
                                    byte[] byteSource = binaryReader3.ReadBytes((int)fieldDescriptor.fieldLen);
                                    row[columnIndex] = (object)ParseDBF.TCVNtoUnicode(byteSource);
                                    break;
                                case 'D':
                                    string str1 = Encoding.ASCII.GetString(binaryReader3.ReadBytes(4));
                                    string str2 = Encoding.ASCII.GetString(binaryReader3.ReadBytes(2));
                                    string str3 = Encoding.ASCII.GetString(binaryReader3.ReadBytes(2));
                                    row[columnIndex] = (object)DBNull.Value;
                                    try
                                    {
                                        if (ParseDBF.IsNumber(str1))
                                        {
                                            if (ParseDBF.IsNumber(str2))
                                            {
                                                if (ParseDBF.IsNumber(str3))
                                                {
                                                    if (int.Parse(str1) > 1900)
                                                    {
                                                        row[columnIndex] = (object)new DateTime(int.Parse(str1), int.Parse(str2), int.Parse(str3));
                                                        break;
                                                    }
                                                    break;
                                                }
                                                break;
                                            }
                                            break;
                                        }
                                        break;
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                case 'F':
                                    string str4 = Encoding.ASCII.GetString(binaryReader3.ReadBytes((int)fieldDescriptor.fieldLen));
                                    row[columnIndex] = !ParseDBF.IsNumber(str4) ? (object)0.0f : (object)double.Parse(str4, (IFormatProvider)CultureInfo.InvariantCulture);
                                    break;
                                case 'L':
                                    row[columnIndex] = (byte)89 != binaryReader3.ReadByte() ? (object)false : (object)true;
                                    break;
                                case 'N':
                                    string str5 = Encoding.ASCII.GetString(binaryReader3.ReadBytes((int)fieldDescriptor.fieldLen));
                                    row[columnIndex] = !ParseDBF.IsNumber(str5) ? (object)0 : (str5.IndexOf(".") <= -1 ? (object)long.Parse(str5, (IFormatProvider)CultureInfo.InvariantCulture) : (object)Decimal.Parse(str5, (IFormatProvider)CultureInfo.InvariantCulture));
                                    break;
                                case 'T':
                                    long lJDN = (long)binaryReader3.ReadInt32();
                                    long num = (long)binaryReader3.ReadInt32() * 10000L;
                                    row[columnIndex] = (object)ParseDBF.JulianToDateTime(lJDN).AddTicks(num);
                                    break;
                            }
                            ++columnIndex;
                        }
                        binaryReader3.Close();
                        dataTable.Rows.Add(row);
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                binaryReader1?.Close();
            }
            long ticks2 = DateTime.Now.Ticks;
            dataTable.AcceptChanges();
            return dataTable;
        }

        public static bool IsNumber(string numberString)
        {
            char[] charArray = numberString.ToCharArray();
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            foreach (char ch in charArray)
            {
                if (ch >= '0' && ch <= '9')
                {
                    ++num1;
                }
                else
                {
                    switch (ch)
                    {
                        case ' ':
                            ++num3;
                            continue;
                        case '.':
                            ++num2;
                            continue;
                        default:
                            return false;
                    }
                }
            }
            return num1 > 0 && num2 < 2;
        }

        private static DateTime JulianToDateTime(long lJDN)
        {
            double num1 = Convert.ToDouble(lJDN) + 68569.0;
            double num2 = Math.Floor(4.0 * num1 / 146097.0);
            double num3 = num1 - Math.Floor((146097.0 * num2 + 3.0) / 4.0);
            double num4 = Math.Floor(4000.0 * (num3 + 1.0) / 1461001.0);
            double num5 = num3 - Math.Floor(1461.0 * num4 / 4.0) + 31.0;
            double num6 = Math.Floor(80.0 * num5 / 2447.0);
            double num7 = num5 - Math.Floor(2447.0 * num6 / 80.0);
            double num8 = Math.Floor(num6 / 11.0);
            double num9 = num6 + 2.0 - 12.0 * num8;
            return new DateTime(Convert.ToInt32(100.0 * (num2 - 49.0) + num4 + num8), Convert.ToInt32(num9), Convert.ToInt32(num7));
        }

        public static string TCVNtoUnicode(byte[] byteSource)
        {
            string str1 = "áạàảã ằẳẵắặă ấậâầẩẫ ốộờôỡớợóọồổởỗòỏõơẽéẹèẻeềểễếệê ỳỷỹýỵ ùúụủũ ừửữứựư ìỉĩíị ĂÂÊÔƠĐđƯ";
            string str2 = "";
            char[] charArray1 = "¸\u00B9µ¶· »\u00BC\u00BD\u00BEÆ¨ ÊË©ÇÈÉ èéê«ìíîãäåæëçßáâ¬ÏÐÑÌÎeÒÓÔÕÖª úûüýþ ïóôñò õö÷øù\u00AD ×ØÜÝÞ ¡¢£¤¥§®¦".ToCharArray();
            char[] charArray2 = str1.ToCharArray();
            foreach (byte num in byteSource)
            {
                bool flag = false;
                for (int index = 0; index < charArray1.Length; ++index)
                {
                    if ((int)Convert.ToByte(charArray1[index]) == (int)num && num != (byte)32)
                    {
                        str2 += charArray2[index].ToString();
                        flag = true;
                    }
                }
                if (!flag)
                {
                    byte[] bytes = new byte[1] { num };
                    str2 += Encoding.ASCII.GetString(bytes);
                }
            }
            return str2;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct DBFHeader
        {
            public byte version;
            public byte updateYear;
            public byte updateMonth;
            public byte updateDay;
            public int numRecords;
            public short headerLen;
            public short recordLen;
            public short reserved1;
            public byte incompleteTrans;
            public byte encryptionFlag;
            public int reserved2;
            public long reserved3;
            public byte MDX;
            public byte language;
            public short reserved4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct FieldDescriptor
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string fieldName;
            public char fieldType;
            public int address;
            public byte fieldLen;
            public byte count;
            public short reserved1;
            public byte workArea;
            public short reserved2;
            public byte flag;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] reserved3;
            public byte indexFlag;
        }
    }
}
