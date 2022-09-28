using Enteties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DALC
{
    public class DALC
    {

        static SqlConnection con = new SqlConnection("your sql connection string");
        public static DataTable dataTable1;

        public static response refreshEmails()
        {
           string query = "[dbo].[refreshEmails]";
            response r = new response();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open(); DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    r.dt = dt;
                    return r;
                }
                catch (Exception ex)
                {
                    r.error = ex.Message;
                    con.Close();return r;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private bool updateUser(User usermain)
        {
            bool IsUserExist = false;
            string query = "[dbo].[updateTutor]";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try { cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", usermain.email);
                    cmd.Parameters.AddWithValue("@fullname", usermain.fullname);
                    cmd.Parameters.AddWithValue("@mat", usermain.mat);
                    cmd.Parameters.AddWithValue("@cat", usermain.cat);
                    cmd.Parameters.AddWithValue("@country", usermain.country);
                    cmd.Parameters.AddWithValue("@img", usermain.img);
                    cmd.Parameters.AddWithValue("@bio", usermain.bio);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        IsUserExist = true;
                    }
                    return IsUserExist;
                }
                catch
                {
                    con.Close();return true;
                }
                finally
                {
                    con.Close();
                }
            }
           
        }
        public static response sendrequest(string userfrom, string userto)
        {
            DataTable dt = new DataTable();
            string query = "sendrequest";response r = new response();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {try
                 {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@from", userfrom);
                cmd.Parameters.AddWithValue("@to", userto);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                    return r;

                }
                catch(Exception ex)
                {
                    r.error = ex.Message;
                    return r;
                }
                finally
                {
                    con.Close();
                }
            }
        }
       


        public static response removerequest(String userfrom, string userto)
        {
            DataTable dt = new DataTable();
            string query = "removerequest";
            response r = new response();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userfromemail", SqlDbType.NVarChar).Value = userfrom;
                    cmd.Parameters.AddWithValue("@usertoemail", SqlDbType.NVarChar).Value = userto;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return r;

                }

                catch(Exception ex)
                {
                    con.Close();
                    r.error = ex.Message;
                    return r;
                }
                finally
                {
                    con.Close();
                }

            }
        }
        public static response accrequest(String userfrom, string userto)
        {
            DataTable dt = new DataTable();
            string query = "[dbo].[accReq]";
            response r = new response();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userfromemail", SqlDbType.NVarChar).Value = userfrom;
                    cmd.Parameters.AddWithValue("@usertoemail", SqlDbType.NVarChar).Value = userto;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return r;
                }


                catch(Exception ex)
                {
                    con.Close();
                    r.error = ex.Message;
                    return r;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //public static response selecttome(string userto)
        //{
        //    DataTable dt = new DataTable();
        //    string query = "[dbo].[getReq]";
        //    response r = new response();
        //    using (SqlCommand cmd1 = new SqlCommand(query, con))
        //    {
        //        try
        //        {
        //            cmd1.CommandType = CommandType.StoredProcedure;
        //            cmd1.Parameters.Add("@usertoemail", SqlDbType.NVarChar).Value = userto;
        //            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
        //            sda.Fill(dt);
        //            con.Open();
        //            int i = cmd1.ExecuteNonQuery();
        //            con.Close();

        //            r.dt = dt;

        //            return r;
        //        }

        //        catch(Exception ex)
        //        {
        //            con.Close();r.error = ex.Message;
        //            return r;
        //        }
        //        finally
        //        {
        //            con.Close();
        //        }
        //    }
        //}

        
        public static response getStudents(string userfrom)
        {
            DataTable dt = new DataTable();
            string query = "[dbo].[getstudent]";
            response r = new response();
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userfromemail", SqlDbType.NVarChar).Value = userfrom;
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    r.dt = dt;
                    return r;
                }
                catch(Exception ex)
                {
                    r.error = ex.Message;
                    con.Close();return r;
                }
                finally
                {
                    con.Close();
                }

            }

        }
        public static response addEvents(schedule meet)
        {
            DataTable dt = new DataTable();
            string query = "[dbo].[addEvents]";
            
            using (var cmd = new SqlCommand(query,con))
            {
                response resp = new response();
                try
                {
                    foreach (string name in meet.emailto.Split(", "))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@start", SqlDbType.NVarChar).Value = meet.start;
                        cmd.Parameters.Add("@emailto", SqlDbType.NVarChar).Value = name;
                        cmd.Parameters.Add("@emailfrom", SqlDbType.NVarChar).Value = meet.emailfrom;
                        cmd.Parameters.Add("@end", SqlDbType.NVarChar).Value = meet.end;

                        cmd.Parameters.AddWithValue("@description", SqlDbType.NVarChar).Value = meet.descp;
                        cmd.Parameters.AddWithValue("@title", SqlDbType.NVarChar).Value = meet.title;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                    return resp;
                }
                catch(Exception ex) {

                    resp.error = ex.Message;
                    con.Close();
                    return resp;
                }
                finally
                {
                    con.Close();
                }



            }
        }
        
       
        


        //public static response selectfromme(string email)
        //{
        //    DataTable dt = new DataTable();
        //    string query = "[dbo].[getMyReq]";
        //    response r = new response();
        //    using (SqlCommand cmd = new SqlCommand(query, con))
        //    {
        //        try
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@userfromemail", SqlDbType.NVarChar).Value = email;
        //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //            sda.Fill(dt);
        //            con.Open();
        //            int i = cmd.ExecuteNonQuery();
        //            con.Close();
        //            r.dt = dt;
        //            return r;
        //        }
        //        catch(Exception ex)

        //        {
        //            con.Close();r.error = ex.Message;  return r;
        //        }
        //        finally
        //        {
        //            con.Close();
        //        }

        //    }

        //}
        public static response[] selectCon(string emailto)
        {
           
            string query = "[dbo].[getCon]";
            string query1 = "[dbo].[getReq]";
            string query2 = "[dbo].[getMyReq]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlCommand cmd2 = new SqlCommand(query2, con);
            {
                response  r = new response();
                response r1 = new response();
                response r2 = new response();
                
                try
                {
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usertoemail", emailto);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    
                    r.dt = dt;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@usertoemail", SqlDbType.NVarChar).Value = emailto;
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    sda1.Fill(dt1);
                   
                    cmd1.ExecuteNonQuery();
                    

                    r1.dt = dt1;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@userfromemail", SqlDbType.NVarChar).Value = emailto;
                    SqlDataAdapter sda3 = new SqlDataAdapter(cmd2);
                    sda3.Fill(dt2);
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    r2.dt = dt2;
                    response[] arr = { r, r1, r2 };
                    return arr;
                }


                catch (Exception ex)
                {
                    con.Close();
                    r.error = ex.Message;
                    r1.error = ex.Message;
                    r2.error = ex.Message;
                    response[] arr = { r, r1, r2 };
                    return arr;

                }
                finally
                {
                    con.Close();
                }
            }

        }


       
        public static response IsValidUser(string email)
        {

            DataTable dt2 = new DataTable();

            string query = "[dbo].[getUser]";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                response r = new response();
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = email 
                        as object ?? (object)DBNull.Value;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    sda.Fill(dt2);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    r.row = dt2.Rows[0].ItemArray;
                    return r;
                }
                catch(Exception ex)
                { con.Close();r.error = ex.Message;
                    return r; }
                finally
                {
                    con.Close();
                }


            }
        }

        public static response refreshData(string email)
        {
            string query = "[dbo].[getTutors]";
            
                
                using (SqlCommand cmd = new SqlCommand(query, con))
            {
                response r = new response();
                try { 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userfrom", SqlDbType.NVarChar).Value = email;
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open(); DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    r.dt = dt;
                    return r;

                }
                catch (Exception ex)
                {
                    con.Close();r.error = ex.Message;
                    return r;
                }
                finally
                {
                    con.Close();
                }
            }
            
        }
       
        public static response getMeetings(string email)
        {
            string query = "[dbo].[getMeetings]";
            response r = new response();
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@user", SqlDbType.NVarChar).Value = email;
                    if (con.State == ConnectionState.Open) con.Close();
                    con.Open(); DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    r.dt = dt;
                    return r;

                }
            }
            catch(Exception ex)
            { con.Close();
                r.error = ex.Message;
              return r; }
            finally
            {
                con.Close();
            }
        }
        public static response Register(User user)
        {
             using (SqlCommand cmd = new SqlCommand(query, con))
            {
                response r = new response();
                try
                {
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.username;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.email;
                    cmd.Parameters.Add("@tutor", SqlDbType.NVarChar).Value = user.tutor;
                    cmd.Parameters.Add("@way", SqlDbType.NVarChar).Value = user.way;
                    cmd.Parameters.Add("@mat", SqlDbType.NVarChar).Value = user.mat;
                    cmd.Parameters.Add("@cat", SqlDbType.NVarChar).Value = user.cat;
                    cmd.Parameters.Add("@country", SqlDbType.NVarChar).Value = user.country;
                    cmd.Parameters.Add("@pass", SqlDbType.NVarChar).Value = user.pass;
                    cmd.Parameters.Add("@fullname", SqlDbType.NVarChar).Value = user.fullname;
                    cmd.Parameters.Add("@img", SqlDbType.NVarChar).Value = user.img;
                    cmd.Parameters.Add("@bio", SqlDbType.NVarChar).Value = user.bio;
                    con.Open();

                    cmd.ExecuteNonQuery();
                    con.Close();
                    return r;
                }
                catch(Exception ex)
                {
                    con.Close();
                    r.error = ex.Message;
                    return r;
                }
                finally
                {
                    con.Close();
                }

            }

        }
        public static response update(User user)
        {
            string query = "[dbo].[updateTutor]";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                response r = new response();
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = user.username;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.email;
                    cmd.Parameters.Add("@mat", SqlDbType.NVarChar).Value = user.mat;
                    cmd.Parameters.Add("@cat", SqlDbType.NVarChar).Value = user.cat;
                    cmd.Parameters.Add("@country", SqlDbType.NVarChar).Value = user.country;
                    cmd.Parameters.Add("@fullname", SqlDbType.NVarChar).Value = user.fullname;
                    cmd.Parameters.Add("@img", SqlDbType.NVarChar).Value = user.img;
                    cmd.Parameters.Add("@bio", SqlDbType.NVarChar).Value = user.bio;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return r;
                }
                catch(Exception ex)
                {
                    con.Close();
                    r.error = ex.Message;
                    return  r;
                }
                finally
                {
                    con.Close();
                }

            }

        }
    }
}
