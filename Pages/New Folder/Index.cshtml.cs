using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace BMI_Tracker.Pages.NewFolder
{
    public class IndexModel : PageModel
    {
        public List<PatientInfo> Pinfo = new List<PatientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BMI_Patients;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "Select * from Patients";
                    using(SqlCommand c = new SqlCommand(query, connection)) 
                    {
                        using(SqlDataReader reader = c.ExecuteReader()) 
                        { 
                            while(reader.Read()) 
                            {
                                PatientInfo cf = new PatientInfo();
                                cf.id = ""+reader.GetInt32(0);
                                cf.name = reader.GetString(1);
                                cf.email= reader.GetString(2);
                                cf.phone = reader.GetString(3);
                                Console.WriteLine(cf.id);
                                Console.WriteLine("YOOOO");
                                //cf.created_at= reader.GetDateTime(4).ToString;
                                Pinfo.Add(cf);
                            }
                            //reader.Close();
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception : "+ ex.ToString());
            }
        }
    }

    public class PatientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
    }
}
