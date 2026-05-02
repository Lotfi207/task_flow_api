using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TaskFlowAPI.Models

{
        public class User
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string PasswordHash { get; set; }

            public Role UserRole { get; set; }

            public enum Role
            {
                Admin,
                User
            }
        public ICollection<Project> Projects { get; set; }
            = new List<Project>();
    }
}