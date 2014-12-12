using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using BarObjekti;

namespace BarDataAccessLayer
{
    public class ArtiklDb
    {
        public static List<Artikl> SviArtikli()
        {
            var artiklList = new List<Artikl>();
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    var command = new SqlCommand("SELECTAllArtikl", con);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var a = new Artikl();
                        a.ArtiklId = Int32.Parse(reader["ArtiklId"].ToString());
                        a.Naziv = reader["Naziv"].ToString();
                        a.ImagePath = String.Concat(reader["ImagePath"]).Replace('\\', '/');
                        artiklList.Add(a);
                    }
                    reader.Close();

                    con.Close();
                }
                catch (SqlException ex)
                {
                    var err = new ErrorHandling();
                    err.ErrorLog(HttpContext.Current.Server.MapPath("~/Errors/ErrorLog.txt"), err.GetLogMessage() + ex.Message);
                    return null;
                }
            }
            return artiklList;
        
        } 
    }
}
