using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Coursach.DAL
{
    public class PodrazdelenieRepository : IRepository<Podrazdelenie>
    {
        private string connectionString;
        private SqlConnection connection;
        public PodrazdelenieRepository()
        {
            connectionString = "Data Source=DESKTOP-4TH92UH;Initial Catalog='Kid Garden';Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            connection = new SqlConnection(connectionString);
        }

        public void Create(Podrazdelenie data)
        {
            SqlCommand checkUnits = new SqlCommand("CreateUnit", connection);
            checkUnits.CommandType = CommandType.StoredProcedure;
            checkUnits.Parameters.Add(new SqlParameter("@Unit_Name", SqlDbType.VarChar, 100));
            checkUnits.Parameters["@Unit_Name"].Value = data.Podrazdelenie_Name;

            using (connection)
            {
                connection.Open();
                checkUnits.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            string connectionString = "Data Source=DESKTOP-4TH92UH;Initial Catalog='Kid Garden';Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            var connection = new SqlConnection(connectionString);
            SqlCommand checkUnits = new SqlCommand("DeleteUnit", connection);
            checkUnits.CommandType = CommandType.StoredProcedure;
            checkUnits.Parameters.Add(new SqlParameter("@Unit_Id",SqlDbType.Int));
            checkUnits.Parameters["@Unit_Id"].Value = id;

            using (connection)
            {
                connection.Open();
                checkUnits.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Podrazdelenie> Read()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ReadUnit", connection);
                command.CommandType = CommandType.StoredProcedure;
                List<Podrazdelenie> UnitList = new List<Podrazdelenie>();
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    var CurrentUnit = new Podrazdelenie()
                    {
                        Podrazdelenie_Code = int.Parse(reader["Podrazdelenie_Code"].ToString()),
                        Podrazdelenie_Name = reader["Podrazdelenie_Name"].ToString()
                    };
                    UnitList.Add(CurrentUnit);
                }
                connection.Close();
                return UnitList;
            }
        }

        public Podrazdelenie Read(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ReadUnitsID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Unit_Id", SqlDbType.Int));
                command.Parameters["@Unit_Id"].Value = id;
                SqlDataReader reader = command.ExecuteReader();
                Podrazdelenie CurrentUnit = null;

                if (reader.Read())
                {
                    CurrentUnit = new Podrazdelenie()
                    {
                        Podrazdelenie_Code = int.Parse(reader["Podrazdelenie_Code"].ToString()),
                        Podrazdelenie_Name=reader["Podrazdelenie_Name"].ToString()                        
                    };
                }

                return CurrentUnit;
            }
        }


        public void Update(Podrazdelenie data)
        {
            SqlCommand checkUnits = new SqlCommand("UpdateUnitList", connection);
            checkUnits.CommandType = CommandType.StoredProcedure;
            checkUnits.Parameters.Add(new SqlParameter("@Unit_Id",SqlDbType.Int));
            checkUnits.Parameters["@Unit_Id"].Value = data.Podrazdelenie_Code;

            checkUnits.Parameters.Add(new SqlParameter("@Unit_Name", SqlDbType.VarChar,100));
            checkUnits.Parameters["@Unit_Name"].Value = data.Podrazdelenie_Name;

            using (connection)
            {
                connection.Open();
                checkUnits.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}