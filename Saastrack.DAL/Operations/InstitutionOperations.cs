using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saastrack.DAL.Operations
{
    public class InstitutionOperations : BaseOperations
    {
        public static string GetInstitutionName(string instType)
        {
            string returnDbName = string.Empty;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT Institution FROM Institution where Institution_type = '" + instType + "'", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        returnDbName = reader.GetString(0);
                    }
                }
            }
            return returnDbName;
        }
    }
}
