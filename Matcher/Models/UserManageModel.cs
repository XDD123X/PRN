namespace Matcher.Models
{
    public class UserManageModel : User
    {
        public bool IsBanIP { get; set; }

        public UserManageModel(User user) {
            this.Name = user.Name;
            this.UserId = user.UserId;
            this.Ipaddress = user.Ipaddress;
            this.Password = user.Password;
            this.Email = user.Email;    
            this.Gender = user.Gender;
            this.DateOfBirth = user.DateOfBirth;
            this.Description = user.Description;
            this.Location = user.Location;
            this.Status = user.Status;
            this.UserType = user.UserType;
        }
    }
}
