using RealEstateBackend.Models;
using System.Web.Http;
using System.Web.Mvc;

namespace RealEstateBackend.Controllers
{
    public class LoginController: Controller
    {

        [System.Web.Http.HttpPost]
        public string Authenticate(string User, string Pass)
        {
            LoginModel login = new LoginModel();
            string user = login.Login(User, Pass);
            return user;
        }

        [System.Web.Http.HttpPost]
        public string SaveUser([FromBody] LoginModel user)
        {
            string saved = user.SaveUser(user);
            return saved;
        }
        /*
        [System.Web.Http.HttpPost]
        public string DeleteUser(LoginModel user)
        {
            string deleted = user.DeleteUser(user);
            return deleted;
        }

        [System.Web.Http.HttpPost]
        public string UpdateUser(LoginModel user)
        {
            string updated = user.UpdateUser(user);
            return updated;
        }*/
    }
}