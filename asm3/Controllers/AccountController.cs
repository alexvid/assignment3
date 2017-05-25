using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using asm3.Models;
using asm3.Providers;
using asm3.Results;

namespace asm3.Controllers
{

    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("api/addUser")]
        public void Post([FromBody]User user)
        {
            Security secure = new Security();
            user.password = secure.HashSHA1(user.password);
            DataProvide dp = new DataProvide();
            dp.addUser(user);
        }

        [HttpGet]
        [Route("api/Login")]
        public User Login([FromUri]string[] data)
        {
            DataProvide up = new DataProvide();
            User user = up.GetUser(data[0]);
            if (user != null)
            {
                Security secure = new Security();
                if (secure.VerifyHash(data[1], user.password))
                {
                    return user;
                }
            }
            return null;
        }


        [HttpPost]
        [Route("api/deleteUser")]
        public void Delete([FromBody]User user)
        {
            DataProvide up = new DataProvide();
            up.deleteUser(user);
        }


        [HttpPost]
        [Route("api/addConsult")]
        public void addConsult([FromBody]Consultatie cons)
        {
            DataProvide up = new DataProvide();
            up.addConsult(cons);
        }

        [HttpPost]
        [Route("api/updateConsult")]
        public void updateConsult([FromBody]Consultatie cons)
        {
            DataProvide up = new DataProvide();
            up.updateConsult(cons);
        }

        [HttpPost]
        [Route("api/addPatient")]
        public void addPatient([FromBody]Patient p)
        {
            DataProvide up = new DataProvide();
            up.addPatient(p);
        }

        [HttpPost]
        [Route("api/updatePatient")]
        public void updatePatient([FromBody]Patient p)
        {
            DataProvide up = new DataProvide();
            up.updatePatient(p);
        }

        [HttpGet]
        [Route("api/getConsultations")]
        public IList<Consultatie> GetCons([FromUri] string id)
        {
            int id2;
            if (id == "")
                id2 = 0;
            else
                id2 = Convert.ToInt32(id);
            DataProvide up = new DataProvide();
            return up.getConsultatie(id2);
        }

        [HttpGet]
        [Route("api/getConsultationsId")]
        public IList<int> GetConsID([FromUri] string id)
        {

            DataProvide up = new DataProvide();
            return up.getConsultatieID(id);
        }

        [HttpGet]
        [Route("api/deleteConsultations")]
        public void DelCons([FromUri] string id)
        {
            DataProvide up = new DataProvide();
            up.deleteConsultatie(Convert.ToInt32(id));
        }

        [HttpPost]
        [Route("api/updateUser")]
        public void PostUpdate([FromBody]User user)
        {
            Security secure = new Security();
            user.password = secure.HashSHA1(user.password);
            DataProvide up = new DataProvide();
            up.updateUser(user);
        }

        [HttpGet]
        [Route("api/getUsers")]
        public IList<User> Get()
        {
            DataProvide up = new DataProvide();
            return up.getUsers();
        }
    }
}
