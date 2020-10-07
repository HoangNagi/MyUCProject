using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDocApp.Models
{
    public class College
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Student> Students { get; set; }

        internal void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
