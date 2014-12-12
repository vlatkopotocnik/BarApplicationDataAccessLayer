using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using BarObjekti;



namespace BarDataAccessLayer
{
    public class KonobarDb
    {
        public Konobar SelectKonobar(int konobarId)
        {
            var k = new Konobar();
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    //STORED PROCEDURE
                    var command = new SqlCommand("SELECTKonobar", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@KonobarId", SqlDbType.Int).Value = konobarId;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        k.Ime = reader["Ime"].ToString();
                        k.Prezime = reader["Prezime"].ToString();
                    }

                    con.Close();
                }
                catch (SqlException ex)
                {
                    var err = new ErrorHandling();
                    err.ErrorLog(HttpContext.Current.Server.MapPath("~/Errors"), err.GetLogMessage() + "===>" + ex.Message);
                    return null;
                }
            }
            return k;
        }

        public List<Konobar> SelectAllKonobar()
        {
            var konobarList = new List<Konobar>();
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    //STORED PROCEDURE
                    var command = new SqlCommand("SELECTAllKonobar", con);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var k = new Konobar();
                        k.KonobarId = Int32.Parse(reader["KonobarId"].ToString());
                        k.Ime = reader["Ime"].ToString();
                        k.Prezime = reader["Prezime"].ToString();
                        konobarList.Add(k);
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
            return konobarList;
        }
    }
}
