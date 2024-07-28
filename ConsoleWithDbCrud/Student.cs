using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWithDbCrud
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public DateOnly DoB { get; set; }
        public bool Gender { get; set; }

    }
}
