using System;
using System.Data;
using System.Text.RegularExpressions;

namespace NurseryServices.Services.Common
{
    public static class DataUtils
    {
        /// <summary>
        /// Checks whether DataSet contains Rows 
        /// </summary>
        /// <param name="data">Data Set</param>
        /// <returns>true or false</returns>
        public static bool RowsPresent(DataSet data)
        {
            if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataRow GetFirstOrDefaultRow(DataSet data)
        {
            if (data != null && data.Tables != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
            {
                return data.Tables[0].Rows[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string DBString(Object col)
        {
            string retVal = string.Empty;

            try
            {
                if (col != null && col != DBNull.Value)
                {
                    retVal = col.ToString();
                }
            }
            catch (Exception)
            {
                // Ignore errors
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static int DBInteger(Object col)
        {
            int retVal = 0;

            try
            {
                if (col != null && col != DBNull.Value)
                {
                    if (IsInteger(col.ToString()))
                    {
                        retVal = Convert.ToInt32(col.ToString());
                    }
                }
            }
            catch (Exception)
            {
                // Ignore errors
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static bool DBBool(Object col)
        {
            bool retVal = false;

            try
            {
                if (col != null && col != DBNull.Value)
                {
                    if (col.ToString().ToLower() == "true")
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception)
            {
                // Ignore errors
            }

            return retVal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static DateTime DBDateTime(Object col)
        {
            DateTime retVal = new DateTime(1900, 1, 1);

            try
            {
                if (col != null && col != DBNull.Value)
                {
                    DateTime dateVal = Convert.ToDateTime(col.ToString());

                    retVal = dateVal;
                }
            }
            catch (Exception)
            {
                // Ignore errors
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static decimal DBDecimal(Object col)
        {
            decimal retVal = 0;

            try
            {
                if (col != null && col != DBNull.Value)
                {
                    // if (IsInteger(col.ToString()))
                    {
                        retVal = Convert.ToDecimal(col.ToString());
                    }
                }
            }
            catch (Exception)
            {
                // Ignore errors
            }

            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strvalue"></param>
        /// <returns></returns>
        public static bool IsInteger(string strvalue)
        {
            bool validInt = false;

            Regex objValid = new Regex("0*[0-9][0-9]*");
            Regex objInValid = new Regex("[^0-9]");

            validInt = objValid.IsMatch(strvalue) && !objInValid.IsMatch(strvalue);

            return validInt;
        }
    }
}