using System;
using Enteties;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;

namespace BLC
{
    public class BLC
    {

        public static void Register(User user)
        {
            user.pass = EncodeBase64(user.pass);
            DALC.DALC.Register(user);
        }
        public static bool IsUserExist(string email, DataTable dt)
        {
            if (dt == null) return false;
            foreach (DataRow r in dt.Rows)
            {
                if (r[0].ToString() == email) return true;
            }
            return false;
        }
        public static response IsValidUser(string email, string password, DataTable dt)
        {

            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r[0].ToString() == email)
                        if (DecodeBase64(r[1].ToString()) == password)
                        {

                            return DALC.DALC.IsValidUser(email);


                        }
                        else break;
                }
                return null;
            }
        }
        public static DataTable filter(string[] arr, DataTable dt)
        {
            bool[] b = new bool[5];
            ArrayList delete = new ArrayList();
            int[] link = { 9, 7, 11, 8, 1 };
            try
            {
                foreach (DataRow r in dt.Rows)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (arr[i] == "All" || arr[i] == "" || arr[i] == "Both") b[i] = true;
                        else b[i] = r[link[i]].ToString().Contains(arr[i]);
                    }
                    if (!(b[0] && b[1] && b[2] && b[3] && b[4]))
                    {

                        delete.Add(dt.Rows.IndexOf(r));
                    };



                }
                foreach (var i in delete)
                {
                    dt.Rows.Remove(dt.Rows[(int)i]);
                }
                return dt;
            }
            catch { return null; }

        }
        public static string EncodeBase64(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}
