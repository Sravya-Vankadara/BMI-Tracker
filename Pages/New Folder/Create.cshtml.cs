using BMI_Tracker.Pages.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BMI_Tracker.Pages.New_Folder
{
    public class CreateModel : PageModel
    {
        public PatientInfo cf = new PatientInfo();
        public String errorMessage="";
        public String succMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            cf.name = Request.Form["name"];
            cf.email = Request.Form["email"];
            cf.phone = Request.Form["phone"];
            if(cf.name.Length==0 || cf.email.Length == 0|| cf.phone.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //db
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=BMI_Patients;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String query = "INSERT INTO PATIENTS" +
                        "(pname,email,phone) VALUES" +
                        "(@name,@email,@phone);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", cf.name);
                        command.Parameters.AddWithValue("@email", cf.email);
                        command.Parameters.AddWithValue("@phone", cf.phone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            cf.name = ""; cf.email = ""; cf.phone = "";
            succMessage = "Data saved successfully!";

            Response.Redirect("/New Folder/Index");
        }
    }
}
