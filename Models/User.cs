using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TASKFLOWAPI.Models
{
        public class User
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string PasswordHash { get; set; }

            public Role Role { get; set; }

            public List<Project> Projects { get; set; }

            public enum Role
            {
                Admin,
                User
            }
        }
}