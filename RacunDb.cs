using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using BarObjekti;



namespace BarDataAccessLayer
{
    public class RacunDb
    {
        public int InsertRacun(Racun r)
        {
            int racunId;

            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    var command = new SqlCommand("INSERTRacun", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@RacunId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.Parameters.AddWithValue("@KonobarId", r.KonobarId);
                    command.Parameters.AddWithValue("@StolId", r.StolId);
                    command.Parameters.AddWithValue("@Vrijeme", r.Vrijeme);
                    command.ExecuteNonQuery();
                    racunId = Convert.ToInt32(command.Parameters["@RacunId"].Value);

                    con.Close();
                }
                catch (Exception ex)
                {
                    var err = new ErrorHandling();
                    err.ErrorLog(HttpContext.Current.Server.MapPath("~/Errors/ErrorLog.txt"), err.GetLogMessage() + ex.Message);
                    return -1;
                }
            }
            return racunId;
        }

        public List<Racun> SviRacuni()
        {

            var racunList = new List<Racun>();
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    var command = new SqlCommand("SELECTAllRacun02", con);
                    command.CommandType = CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var r = new Racun(Int32.Parse(reader["KonobarId"].ToString()),Int32.Parse(reader["StolId"].ToString()));
                        r.RacunId = Int32.Parse(reader["RacunId"].ToString());
                        r.Vrijeme = Convert.ToDateTime(reader["Vrijeme"].ToString());
                        racunList.Add(r);
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
            return racunList;
        }

        public Racun SelectRacun(int racunId)
        {
            var r = new Racun(0,0);
            using (var con = new SqlConnection(DbHelper.ConnString))
            {
                try
                {
                    con.Open();

                    var command = new SqlCommand("SELECTRacun", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RacunId", racunId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        r.KonobarId = Int32.Parse(reader["KonobarId"].ToString());
                        r.StolId = Int32.Parse(reader["StolId"].ToString());
                        r.Vrijeme = Convert.ToDateTime(reader["Vrijeme"].ToString());
                    }
                    reader.Close();

                    con.Close();
                }
                catch (SqlException ex)
                {
                    var err = new ErrorHandling();
                    err.ErrorLog(HttpContext.Current.Server.MapPath("~/Errors/ErrorLog.txt"), err.GetLogMessage() + ex.Message);
                }
                
            }
            return r;
        }
    }
}
