using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKS
{
    public class Row
    {
        public Row(string fieldName, string fieldBirthNumber, string fieldBirthDate, string fieldStartDate,
            string fieldEndDate, string fieldMemberType, string fieldCategory, string fieldButton)
        {
            FieldName = fieldName;
            FieldBirthNumber = fieldBirthNumber;
            FieldBirthDate = fieldBirthDate;
            FieldStartDate = fieldStartDate;
            FieldEndDate = fieldEndDate;
            FieldMemberType = fieldMemberType;
            FieldCategory = fieldCategory;
            FieldButton = fieldButton;
        }


        public string FieldName { get; private set; }

        public string FieldBirthNumber { get; private set; }

        public string FieldBirthDate { get; private set; }

        public string FieldStartDate { get; private set; }

        public string FieldEndDate { get; private set; }

        public string FieldMemberType { get; private set; }

        public string FieldCategory { get; private set; }

        public string FieldButton { get; private set; }
    }
}
