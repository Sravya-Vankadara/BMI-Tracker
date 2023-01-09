using BMI_Tracker.Pages.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace BMI_Tracker.Pages.New_Folder
{
    public class EditModel : PageModel
    {
        public PatientInfo cf = new PatientInfo();
        public String errorMessage = "";
        public String succMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BMI_Patients;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "Select * from Patients where id=@id";
                    using (SqlCommand c = new SqlCommand(query, connection))
                    {
                        c.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = c.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cf.id = "" + reader.GetInt32(0);
                                cf.name = reader.GetString(1);
                                cf.email = reader.GetString(2);
                                cf.phone = reader.GetString(3);
                                //Console.WriteLine(cf.id);
                                //Console.WriteLine("YOOOO");
                                //cf.created_at= reader.GetDateTime(4).ToString;
                                //Pinfo.Add(cf);
                            }
                            //reader.Close();
                        }
                    }
                }

            }
            catch(Exception ex) 
            {
                errorMessage= ex.Message;
            }
        }

        public void OnPost() 
        {
            cf.id = Request.Form["id"];
            cf.name = Request.Form["name"];
            cf.email = Request.Form["email"];
            cf.phone = Request.Form["phone"];
            if (cf.name.Length == 0 || cf.email.Length == 0 || cf.phone.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BMI_Patients;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "UPDATE patients " +
                        "SET pname=@name, email=@email, phone=@phone " +
                        "WHERE id=@id";
                    using (SqlCommand c = new SqlCommand(query, connection))
                    {
                        c.Parameters.AddWithValue("@name", cf.name);
                        c.Parameters.AddWithValue("@email", cf.email);
                        c.Parameters.AddWithValue("@phone", cf.phone);
                        c.Parameters.AddWithValue("@id", cf.id);

                        c.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/New Folder/Index");
        }
    }
}
