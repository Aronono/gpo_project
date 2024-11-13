using System;
using NUnit.Framework;
using UnityEngine;

namespace DbClasses
{
    class Name : DbClass
    {
        //public int Id { get; set; }
        public string Base { get; set; }
        public string SingularNominative { get; set; }
        public string SingularGenitive { get; set; }
        public string PluralNominative { get; set; }
        public string PluralGenitive { get; set; }
        public Gender Gender { get; set; }

        public Name(string Base, string SingularNominative, string SingularGenitive, string PluralNominative, string PluralGenitive, Gender gender)
        {
            this.Base = Base;
            this.SingularNominative = SingularNominative;
            this.SingularGenitive = SingularGenitive;
            this.PluralNominative = PluralNominative;
            this.PluralGenitive = PluralGenitive;
            this.Gender = gender;
        }

        public static Name ToName(object[] values)
        {
            string Base = values[1].ToString();
            string SingularNominative = DbClass.StringOrNull(values[2]);
            string SingularGenitive = DbClass.StringOrNull(values[3]);
            string PluralNominative = DbClass.StringOrNull(values[4]);
            string PluralGenitive = DbClass.StringOrNull(values[5]);
            Gender gender = ToGender(Convert.ToInt32(values[6]));
            return new Name(Base, SingularNominative,  SingularGenitive,  PluralNominative, PluralGenitive, gender);
        }

        private static Gender ToGender(int genderString)
        {
            switch (genderString)
            {
                case 1:
                    return Gender.Masculine;
                case 2:
                    return Gender.Feminine;
                case 3:
                    return Gender.Neuter;
                default: 
                    return Gender.None;
            }
        }

        public string SingNom()
        {
            return this.Base + this.SingularNominative;
        }

        public string SingGen()
        {
            return this.Base + this.SingularGenitive;
        }
        public string PluralNom()
        {
            return this.Base + this.PluralNominative;
        }
        public string PluralGen()
        {
            return this.Base + this.PluralGenitive;
        }
    }
}
