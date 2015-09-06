using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKS
{
    public class Record
    {
        public Record(string name, string nickname, Gender gender, DateTime dateOfBirth,
            DateTime registrationDate, DateTime? leavingDate, MemberType memberType, Category category)
        {
            Name = name;
            Nickname = nickname;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            RegistrationDate = registrationDate;
            LeavingDate = leavingDate;
            MemberType = memberType;
            Category = category;
        }

        public string Name { get; private set; }

        public string Nickname { get; private set; }

        public Gender Gender { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public DateTime RegistrationDate { get; private set; }

        public DateTime? LeavingDate { get; private set; }

        public MemberType MemberType { get; private set; }

        public Category Category { get; private set; }
    }
}
