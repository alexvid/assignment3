using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using asm3.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using asm3.Providers;

namespace Secretary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.idCode = txtIDCode.Text;
            p.firstName = txtFirstName.Text;
            p.lastName = txtLastName.Text;
            p.birthDate = dateBirthDate.Value;
            p.address = txtAddress.Text;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:56895/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/addPatient", p).Result;

            //MessageBox.Show(response.ToString());

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Operation succeded");
            }
            else
            {
                MessageBox.Show("Operation failed");
            }

        }

        private void btnAUpdatePatient_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            if (txtID.Text != "")
            {
                p.id = Convert.ToInt32(txtID.Text);
                p.idCode = txtIDCode.Text;
                p.firstName = txtFirstName.Text;
                p.lastName = txtLastName.Text;
                p.birthDate = dateBirthDate.Value;
                p.address = txtAddress.Text;
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("http://localhost:56895/");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/updatePatient", p).Result;

                //MessageBox.Show(response.ToString());

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Operation succeded");
                }
                else
                {
                    MessageBox.Show("Operation failed");
                }
            }
            else
                MessageBox.Show("No id ☹");
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            Consultatie cons = new Consultatie();
            cons.IDPatient = Convert.ToInt32(txtIDPatient.Text);
            cons.IDDoctor = Convert.ToInt32(txtIDDoctor.Text);
            cons.schedule = dateSchedule.Value;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:56895/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/addConsult", cons).Result;
            //MessageBox.Show(response.ToString());
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
            HttpResponseMessage response = client.GetAsync("api/getConsultations?id=" + txtIDC.Text).Result;

            //MessageBox.Show(response.ToString());

            if (response.IsSuccessStatusCode)
            {
                dataGridView1.DataSource = response.Content.ReadAsAsync<IList<Consultatie>>().Result;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Consultatie cons = new Consultatie();
            cons.ID = Convert.ToInt32(txtIDC.Text);
            cons.IDPatient = Convert.ToInt32(txtIDPatient.Text);
            cons.IDDoctor = Convert.ToInt32(txtIDDoctor.Text);
            cons.schedule = dateSchedule.Value;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:56895/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/updateConsult", cons).Result;
            //MessageBox.Show(response.ToString());
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Operation succeded");
            }
            else
            {
                MessageBox.Show("Operation failed");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56895/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/deleteConsultations?id=" + txtIDC.Text).Result;
            MessageBox.Show(response.ToString());

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Operation succeded");
            }
            else
            {
                MessageBox.Show("Operation failed");
            }
        }
    }
}
