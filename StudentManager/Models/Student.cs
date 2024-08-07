﻿namespace StudentManager.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; } // Nullable DateTime for Birthday

    }
}
