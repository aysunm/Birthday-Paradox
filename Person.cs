using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_P1
{
    public class Person
    {
        private DateTime birthDate;

        public Person (DateTime b)
        {
            this.birthDate = b;
        }

        public DateTime Birthday { get { return birthDate;  } } 
    }
}
