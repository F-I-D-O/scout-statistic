using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKS
{
    class DataTransformer
    {
        private const int NAME_PART_FIRST_NAME = 0;
        private const int NAME_PART_NICK = 1;

        private const int BIRTH_NUMBER_GENDER_DIGIT_INDEX = 2;

        public ObservableCollection<Record> GetTransformedData(ObservableCollection<Row> rows)
        {
            ObservableCollection<Record> records = new ObservableCollection<Record>();

            int index = 0;
            while (index < rows.Count)
            {
                Row row = rows[index];
                DateTime oldestRegistrationDate = Convert.ToDateTime(row.FieldStartDate);

                // zvláštní to věc, kompilátor porovnává typy navzájem
                DateTime? newestLeavingDate = row.FieldEndDate == "" ? null : (DateTime?)Convert.ToDateTime(row.FieldEndDate);
                string birthNumber = row.FieldBirthNumber;

                int oldestRecordIndex = index;
                int newestRecordIndex = index;

                while (index + 1 < rows.Count && rows[index + 1].FieldBirthNumber == birthNumber)
                {
                    index++;
                    row = rows[index];
                    DateTime registrationDateNext = Convert.ToDateTime(row.FieldStartDate);

                    // zvláštní to věc, kompilátor porovnává typy navzájem
                    DateTime? leavingDateNext = row.FieldEndDate == "" ? null : (DateTime?)Convert.ToDateTime(row.FieldEndDate);

                    if (registrationDateNext < oldestRegistrationDate)
                    {
                        oldestRecordIndex = index;
                        oldestRegistrationDate = registrationDateNext;

                    }

                    if (newestLeavingDate != null && (leavingDateNext == null || leavingDateNext > newestLeavingDate))
                    {
                        newestRecordIndex = index;
                        newestLeavingDate = leavingDateNext;
                    }
                }

                row = rows[newestRecordIndex];

                string[] nameArray = GetNameParts(row.FieldName);
                string name = nameArray[NAME_PART_FIRST_NAME];
                string nick = nameArray[NAME_PART_NICK];
                Gender gender = GetGender(row.FieldBirthNumber);
                DateTime birthDate = Convert.ToDateTime(row.FieldBirthDate);
                MemberType membership = GetMembership(row.FieldMemberType);
                Category category = GetCategory(row.FieldCategory);

                Record record = new Record(name, nick, gender, birthDate, oldestRegistrationDate, newestLeavingDate, 
                    membership, category);
                records.Add(record);

                index++;
            }

            return records;
        }

        private Category GetCategory(string categoryString)
        {
            Category category = Category.Benjaminek;
            switch (categoryString)
            {
                case "Benjamínek":
                    category = Category.Benjaminek;
                    break;
                case "Vlče":
                    category = Category.Vlce;
                    break;
                case "Světluška":
                    category = Category.Svetluska;
                    break;
                case "Skaut":
                    category = Category.Skaut;
                    break;
                case "Skautka":
                    category = Category.Skautka;
                    break;
                case "Rover":
                    category = Category.Rover;
                    break;
                case "Ranger":
                    category = Category.Ranger;
                    break;
                case "Ostatní":
                    category = Category.Other;
                    break;
                case "Člen kmene dospělých":
                    category = Category.Oldskaut;
                    break;
            }
            return category;
        }

        private MemberType GetMembership(string membershipString)
        {
            MemberType membership = MemberType.Regular;
            switch (membershipString)
            {
                case "Řádné":
                    membership = MemberType.Regular;
                    break;
                case "Nečlen":
                    membership = MemberType.Orher;
                    break;
            }
            return membership;
        }

        private Gender GetGender(string birthNumber)
        {
            int genderDigit = birthNumber[BIRTH_NUMBER_GENDER_DIGIT_INDEX];
            if(genderDigit < 2){
                return Gender.Man;
            }
 	        else{
                return Gender.Woman;
            }
        }

        private string[] GetNameParts(string fieldName)
        {
            string[] nameArray = fieldName.Split('(');
            string name = nameArray[0];
            string nick = null;

            if(name.Length == 2){
                nick = nameArray[1].Remove(nameArray[1].Length - 1);
            }

            string[] names = {name, nick};

            return names;
        }
    }
}
