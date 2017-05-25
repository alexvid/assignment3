using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using asm3.Models;
using asm3.Providers;
using asm3.Controllers;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Windows.Forms;
using Admin;
using Secretary;
using Doctor;

namespace Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            passTxt.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string username = userTxt.Text;
            //User test = new User();
            //test.username = "admin";
            //test.firstname = "alex";
            //test.lastname = "vid";
            //test.password = "admin";
            //test.type = 1;

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:56895/");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = client.PostAsJsonAsync("api/addUser", test).Result;
            //MessageBox.Show(response.ToString());
            //if (response.IsSuccessStatusCode)
            //{
            //    MessageBox.Show("Operation succeded");
            //}
            //else
            //{
            //    MessageBox.Show("Operation failed");
            //}

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56895/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/Login?data=" + userTxt.Text + "&data=" + passTxt.Text).Result;
            User user = new User();

            //MessageBox.Show(response.ToString());
            if (response.IsSuccessStatusCode)
            {
                user = response.Content.ReadAsAsync<User>().Result;

                if(user != null)
                    if (user.type == 1)
                    {
                        Admin.Form1 adminForm = new Admin.Form1();
                        adminForm.Show();
                    }
                    else if (user.type == 2)
                    {
                        Doctor.Form1 df = new Doctor.Form1(user);
                        df.Show();
                    }
                    else if (user.type == 3)
                    {
                        Secretary.Form1 sf = new Secretary.Form1();
                        sf.Show();
                    }
            }
            else
            {
                MessageBox.Show("operation failed");
            }

        }

    }
}
