using System.ComponentModel.DataAnnotations;

namespace ReqManager.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
                
        }

        [Required]
        public string login { get; set; }
        [Required]
        [DataType (DataType.Password)]
        public string password { get; set; }
    }
}