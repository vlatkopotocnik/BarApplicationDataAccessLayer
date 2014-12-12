using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace BarDataAccessLayer
{
    public class RacunArtiklCjenikDb
    {
        public void InsertRacunArtiklCjenik(int racunId,int kolicina, int atriklId)
        {
            
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    var command1 = new SqlCommand("SELECTCjenikArtiklId", con);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.AddWithValue("@ArtiklId", atriklId);
                    command1.Parameters.Add("@CjenikArtiklId", SqlDbType.Int).Direction = ParameterDirection.Output;

                    command1.ExecuteNonQuery();
                    int cjenikArtiklId = Convert.ToInt32(command1.Parameters["@CjenikArtiklId"].Value);

                    var command2 = new SqlCommand("INSERTRacunArtiklCjenik", con);
                    command2.CommandType = CommandType.StoredProcedure;
             
                    command2.Parameters.AddWithValue("@RacunId", racunId);
                    command2.Parameters.AddWithValue("@CjenikArtiklId", cjenikArtiklId);
                    command2.Parameters.AddWithValue("@Kolicina", kolicina);
                    command2.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex)
                {
                    var err = new ErrorHandling();
                    err.ErrorLog(HttpContext.Current.Server.MapPath("~/Errors/ErrorLog.txt"), err.GetLogMessage() + ex.Message);
                }
            }
        }
    }
}
