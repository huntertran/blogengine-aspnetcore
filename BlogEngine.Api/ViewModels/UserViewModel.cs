namespace BlogEngine.Api.ViewModels
{
    using Models;
    using System.Collections.Generic;

    public class UserRoles
    {
        public string RoleName { get; set; }
        public bool IsInRole { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<UserRoles> Roles { get; set; }

        public UserViewModel()
        {
        }

        public UserViewModel(ApplicationUser applicationUser)
        {
            Id = applicationUser.Id;
            UserName = applicationUser.UserName;
            Email = applicationUser.Email;
            PhoneNumber = applicationUser.PhoneNumber;
        }

        public UserViewModel(
            ApplicationUser applicationUser,
            ICollection<string> currentRoles,
            IEnumerable<string> availableRoles) : this(applicationUser)
        {
            Roles = new List<UserRoles>();

            foreach (var role in availableRoles)
            {
                var userRole = new UserRoles
                {
                    RoleName = role
                };

                if (currentRoles.Contains(role))
                {
                    userRole.IsInRole = true;
                }

                Roles.Add(userRole);
            }
        }
    }
}
