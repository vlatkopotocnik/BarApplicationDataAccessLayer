using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;


namespace BarDataAccessLayer
{
    public class NarudzbaDb
    {
        public string Naziv;
        public int Cijena;
        public int Kolicina;

        public List<NarudzbaDb> SelectAllNarudzbaDb(int racunId)
        {
            var listnDb = new List<NarudzbaDb>();
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    var command = new SqlCommand("SELECTNarudzba", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RacunId", racunId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var nDb = new NarudzbaDb();
                        nDb.Naziv = reader["Naziv"].ToString();
                        nDb.Cijena = Int32.Parse(reader["Cjena"].ToString());
                        nDb.Kolicina = Int32.Parse(reader["Kolicina"].ToString());
                        listnDb.Add(nDb);
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
            return listnDb;
        }
    }
}
