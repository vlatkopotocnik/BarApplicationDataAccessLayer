using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using BarObjekti;

namespace BarDataAccessLayer
{
    public class HitsDb
    {
        private int hits;
        public int HitsCount()
        {
         
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    var command = new SqlCommand("SELECTHits", con);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        hits = Int32.Parse(reader["Hits"].ToString());
                    }
                    hits++;
                    reader.Close();
                    command = new SqlCommand("UPDATEHits", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Hits", hits);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    var err = new ErrorHandling();
                    err.ErrorLog(HttpContext.Current.Server.MapPath("~/Errors/ErrorLog.txt"), err.GetLogMessage() + ex.Message);
                    return -1;
                }
            }
            return hits;
        }
    }
}
