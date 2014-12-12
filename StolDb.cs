using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using BarObjekti;

namespace BarDataAccessLayer
{
    public class StolDb
    {
        public Stol SelectStol(int stolId)
        {
            var s = new Stol();
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    //STORED PROCEDURE
                    var command = new SqlCommand("SELECTStol", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StolId", stolId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        s.BrojStola = Int32.Parse(reader["BrojStola"].ToString());
                    }

                    con.Close();
                }
                catch (SqlException ex)
                {
                    var err = new ErrorHandling();
                    err.ErrorLog(HttpContext.Current.Server.MapPath("~/Errors/ErrorLog.txt"), err.GetLogMessage() + ex.Message);
                    return null;
                }
            }
            return s;
        }

        public List<Stol> SelectAllStol()
        {
            var stolList = new List<Stol>();
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    //STORED PROCEDURE
                    var command = new SqlCommand("SELECTAllStol", con);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var s = new Stol();
                        s.StolId = Int32.Parse(reader["StolId"].ToString());
                        s.BrojStola = Int32.Parse(reader["BrojStola"].ToString());
                        stolList.Add(s);
                    }

                    con.Close();
                }
                catch (SqlException ex)
                {
                    var err = new ErrorHandling();
                    err.ErrorLog(HttpContext.Current.Server.MapPath("~/Errors/ErrorLog.txt"), err.GetLogMessage() + ex.Message);
                    return null;
                }
            }
            return stolList;
        }

        public int StolSlobodanZauzet(int slobodanZauzet, int stolId)
        {
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    var command = new SqlCommand("UPDATEStolSlobodanZauzet", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SlobodanZauzet", slobodanZauzet);
                    command.Parameters.AddWithValue("@StolId", stolId);
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
            return 0;
        }
    }
}
