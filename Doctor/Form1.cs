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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doctor
{
    public partial class Form1 : Form
    {
        public User user;
        private Boolean first = false;
        private IList<int> consultations = new List<int>();
        public Form1(User user)
        {
            this.user = user;
            InitializeComponent();
            Thread thread = new Thread(new ThreadStart(CheckPatient));
            thread.Start();
        }
        public void CheckPatient()
        {
            while (true)
            {
                IList<int> consultations2 = new List<int>();


                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:56895/");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/getConsultationsId?id=" + this.user.ID.ToString()).Result;
         
                //MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    if (!first)
                    {
                        consultations = response.Content.ReadAsAsync<IList<int>>().Result;
                        first = true;
                    }
                    else
                    {
                        consultations2 = response.Content.ReadAsAsync<IList<int>>().Result;
                        foreach (int c in consultations2)
                        {
                            if (!consultations.Contains(c))
                            {
                                MessageBox.Show("You have a new consultation!☺☻ id= "+c.ToString());
                                consultations.Add(c);
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(10000);
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            Consultatie cons = new Consultatie();
            cons.IDPatient = Convert.ToInt32(textBox1.Text);
            cons.IDDoctor = Convert.ToInt32(textBox2.Text);
            cons.schedule = dateTimePicker1.Value;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:56895/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/addConsult", cons).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Operation succeded");
            }
            else
            {
                MessageBox.Show("Operation failed");
            }
        }

        private void viewbtn_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56895/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/getConsultations?id=" + textBox2.Text).Result;
            //MessageBox.Show(response.ToString());

            if (response.IsSuccessStatusCode)
            {
                dataGridView1.DataSource = response.Content.ReadAsAsync<IList<Consultatie>>().Result;
            }
        }
    }
}
