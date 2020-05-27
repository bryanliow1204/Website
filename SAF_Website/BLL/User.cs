using SAF_Website.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using SAF_Website.BLL;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;

namespace SAF_Website.BLL
{

    public class Userlist
    {
        public List<User> UserList { get; set; }
    }

    public class User
    {

        static HttpClient client = new HttpClient();
        static string activationcode;
        static string decrpted;

        //Define Class Properties
        [JsonProperty("user_id")]
        public int user_id { get; set; }
        [JsonProperty("email_addr")]
        public string email_addr { get; set; }
        [JsonProperty("otp")]
        public string otp { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        public static SmtpDeliveryMethod DeliveryMethod { get; private set; }

        // Default constructor
        public User()
        {

        }

      
        public User(string email, string otpcode)
        {

            email_addr = email;
            otp = otpcode;


        }

       

      

        //insert user if not in the database
        public int AddUser()
        {
            UserDAO dao = new UserDAO();
            int result = dao.Insert(this);
            return result;
        }

        static string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        //call api to retrieve user details
        public static User GetUsers(string email, string otp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44384/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                User usr = new User();
                var response = client.GetAsync($"api/User/GetByEmail/" + email + "/").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseresult = response.Content.ReadAsStringAsync().Result;
                    var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(responseresult);
                    foreach (var item in results)
                    {
                        if (item.email_addr == email)
                        {
                            var response1 = client.GetAsync($"api/User/UpdateUserOTP/" + item.user_id + "/").Result;
                            if (response1.IsSuccessStatusCode)
                            {
                              
                            }

                        }
                    }
                }

                var response2 = client.GetAsync($"api/User/GetAllUser").Result;

                if (response2.IsSuccessStatusCode)
                {
                    string responseresult3 = response2.Content.ReadAsStringAsync().Result;
                    var results3 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(responseresult3);

                    foreach (var item2 in results3)
                    {
                        if (item2.email_addr == email)
                        {
                            if (item2.otp == otp)
                            {
                                return item2;
                            }
                        }
                        if (item2.email_addr == email)
                        {
                            string encryptedOTP = Convert.ToString(item2.otp);
                            string decryptOTP = DecryptString(encryptedOTP);
                            string text = "Dear";
                            string texts = text.PadRight(5);
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Credentials = new System.Net.NetworkCredential("saftestingportalteam@gmail.com", "SAF12345678portal");
                            smtp.EnableSsl = true;
                            MailMessage msg = new MailMessage();
                            msg.Subject = "Activation Code to verify Email Address";
                            msg.Body = texts + email + "," + "\n\n" + "Your Activation Code is " + decryptOTP + "." + "\n\n\nThanks & Regards\nSAF Team";
                            string toaddress = email;
                            msg.To.Add(toaddress);

                            string fromaddress = "SAF <saftestingportalteam@gmail.com>";
                            msg.From = new MailAddress(fromaddress);

                            smtp.Send(msg);
                            return item2;
                        }

                    }
                }

                return null;
            }
        }


            
        //Verify the otp 
        public static User GetUsersOTP(string email, string otp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44384/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                User usr = new User();
                var response = client.GetAsync($"api/User/GetByEmail/" + email + "/").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseresult = response.Content.ReadAsStringAsync().Result;
                    var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(responseresult);

                    foreach (var item in results)
                    {
                        if (item.email_addr == email)
                        {
                            if (item.otp == otp)
                            {
                                return item;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            return null;
        }

        //simulate fake json data for login
        public static User Read(string email, string otp)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://localhost:44378/");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //var response = client.GetAsync($"api/Users/{id}").Result;

                string jsonData = "[{\"Id\": 1, \"Email\": \"bryanliow1204@gmail.com\", \"OTP\": 1234, \"Username\": \"bryan\"},{\"Id\": 2, \"Email\": \"ben@gmail.com\", \"OTP\": 1222, \"Username\": \"ben\"},{\"Id\": 3, \"Email\": \"sally@gmail.com\", \"OTP\": 3212, \"Username\": \"sally\"}]";

                var myDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(jsonData);

                foreach (var item in myDetails)
                {
                    if (item.email_addr == email)
                    {
                        return item;
                    }

                    //This will yield the proper index that you are currently on
                    if (item.email_addr == email)
                    {
                        if (item.otp == otp)
                        {
                            return item;
                        }
                    }
                }
                return null;
            }

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        ////select the id like email
        //public static User GetUserById(string email)
        //{
        //    UserDAO dao = new UserDAO();
        //    return dao.SelectById(email);
        //}
    }
}