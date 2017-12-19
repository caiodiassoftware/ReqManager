using System.ComponentModel.DataAnnotations;

namespace ReqManager.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
                
        }


        public string login { get; set; }
        public string password { get; set; }
    }
}