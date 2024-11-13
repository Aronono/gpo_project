using DbClasses;
using Mono.Data.Sqlite;
using System.Collections.Generic;

namespace GenerationClasses
{
    class NameGenerator : ObjectGenerator
    {
        readonly System.Random rnd = new System.Random();
        int nameRowsCount = 0;
        int nounRowsCount = 0;
        int adjRowsCount = 0;
        Name found_name = null;
        Noun found_noun = null;
        Adjective found_adjective = null;
        Gender gender;
        string result = "";
        string temp_word = "";
        List<Name> names = null;
        List<Noun> nouns = null;
        List<Adjective> adjectives = null;

        public NameGenerator(string _dbName)
        {
            this.dbLink = _dbName;
        }

        public string GenerateName()
        {
            result = "";
            if (names == null) GetNamesT();
            found_name = names[rnd.Next(nameRowsCount)];
            gender = found_name.Gender;

            //главный метод класса, вызвающий все остальные
            switch (RandomNameType())
            {
                case 0:
                    return GenerateNameType0();
                case 1:
                    return GenerateNameType1();
                case 2:
                    return GenerateNameType2();
                case 3:
                    return GenerateNameType3();
                case 4:
                    return GenerateNameType4();
                case 5:
                    return GenerateNameType5();
                case 6:
                    return GenerateNameType6();
                case 7:
                    return GenerateNameType7();
                case 8:
                    return GenerateNameType8();
                default:
                    return "Error";
            }
        }

        private void GetNamesT()
        {
            names = GetObjectsByQuery(
                "SELECT * FROM Names",
                Name.ToName
            );
        }

        private void GetNounsT()
        {
            nouns = GetObjectsByQuery(
                "SELECT * FROM Nouns",
                Noun.ToNoun
            );
        }

        private void GetAdjT()
        {
            adjectives = GetObjectsByQuery(
                "SELECT * FROM Adjectives",
                Adjective.ToAdjective
            );
        }

        private int RandomNameType()
        {
            return rnd.Next(9);
        }

        private string GenerateNameType0()
        {
            if (nouns == null) GetNounsT();
            //Название + сущЕд
            found_noun = nouns[rnd.Next(nounRowsCount)];
            result = found_name.SingNom() + " " + found_noun.SingGen();
            return result;
            //return "Пещера Гоблина"
        }

        private string GenerateNameType1()
        {
            if (nouns == null) GetNounsT();
            //Название + сущМн
            found_noun = nouns[rnd.Next(nounRowsCount)];
            result = found_name.SingNom() + " " + found_noun.PluralGen();
            return result;
            //return "Пещера гоблинов"
        }

        private string GenerateNameType2()
        {
            if (nouns == null) GetNounsT();
            if (adjectives == null) GetAdjT();
            //Название + прил + сущЕд
            found_noun = nouns[rnd.Next(nounRowsCount)];
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            gender = found_noun.Gender;

            switch (gender)
            {
                case Gender.Masculine:
                case Gender.Neuter:
                    result = found_name.SingNom() + " " + found_adjective.SingGenOther(found_noun.Title) + " " + found_noun.SingGen();
                    break;
                case Gender.Feminine:
                    result = found_name.SingNom() + " " + found_adjective.SingGenFem(found_noun.Title) + " " + found_noun.SingGen();
                    break;
            }

            return result;
            //return "Пещера Великого гоблина";
        }
        private string GenerateNameType3()
        {
            if (nouns == null) GetNounsT();
            if (adjectives == null) GetAdjT();
            //Название + прил + сущМн
            found_noun = nouns[rnd.Next(nounRowsCount)];
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            result = found_name.SingNom() + " " + found_adjective.PluralGen(found_noun.Title) + " " + found_noun.PluralGen();
            return result;
            //return "Пещера Великих Гоблинов";
        }

        private string GenerateNameType4()
        {
            if (adjectives == null) GetAdjT();
            //Прил + название
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            switch (gender)
            {
                case Gender.Masculine : 
                    result = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Feminine :
                    result = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Neuter:
                    result = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower();
                    break;
            }
            return result;
            //return "Великая пещера";
        }

        private string GenerateNameType5()
        {
            if (nouns == null) GetNounsT();
            if (adjectives == null) GetAdjT();
            //Прил + название + сущМн
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            found_noun = nouns[rnd.Next(nounRowsCount)];
            switch (gender)
            {
                case Gender.Masculine:
                    result = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower() + " " + found_noun.PluralGen();
                    break;
                case Gender.Feminine:
                    result = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower() + " " + found_noun.PluralGen();
                    break;
                case Gender.Neuter:
                    result = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower() + " " + found_noun.PluralGen();
                    break;
            }
            return result;
            //return "Великая пещера гоблинов";
        }

        private string GenerateNameType6()
        {
            if (nouns == null) GetNounsT();
            if (adjectives == null) GetAdjT();
            //Прил + название + сущЕд
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            found_noun = nouns[rnd.Next(nounRowsCount)];
            switch (gender)
            {
                case Gender.Masculine:
                    result = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower() + " " + found_noun.SingGen();
                    break;
                case Gender.Feminine:
                    result = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower() + " " + found_noun.SingGen();
                    break;
                case Gender.Neuter:
                    result = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower() + " " + found_noun.SingGen();
                    break;
            }
            return result;
            //return "Великая пещера гоблина";
        }

        private string GenerateNameType7()
        {
            if (nouns == null) GetNounsT();
            if (adjectives == null) GetAdjT();
            //Прил + название + прил + сущМнож
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            switch (gender)
            {
                case Gender.Masculine:
                    temp_word = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Feminine:
                    temp_word = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Neuter:
                    temp_word = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower();
                    break;
            }
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            found_noun = nouns[rnd.Next(nounRowsCount)];
            result = temp_word + " " + found_adjective.PluralGen(found_noun.Title) + " " + found_noun.PluralGen();

            return result;
            //return "Великая пещера великих гоблинов";
        }

        private string GenerateNameType8()
        {
            if (nouns == null) GetNounsT();
            if (adjectives == null) GetAdjT();
            //Прил + название + прил + сущЕд
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            switch (gender)
            {
                case Gender.Masculine:
                    temp_word = found_adjective.SingNomMasc() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Feminine:
                    temp_word = found_adjective.SingNomFem() + " " + found_name.SingNom().ToLower();
                    break;
                case Gender.Neuter:
                    temp_word = found_adjective.SingNomNeuter() + " " + found_name.SingNom().ToLower();
                    break;
            }
            found_adjective = adjectives[rnd.Next(adjRowsCount)];
            found_noun = nouns[rnd.Next(nounRowsCount)];
            gender = found_noun.Gender;
            switch (gender)
            {
                case Gender.Masculine:
                case Gender.Neuter:
                    result = temp_word + " " + found_adjective.SingGenOther(found_noun.Title) + " " + found_noun.SingGen();
                    break;
                case Gender.Feminine:
                    result = temp_word + " " + found_adjective.SingGenFem(found_noun.Title) + " " + found_noun.SingGen();
                    break;
            }

            return result;
            //return "Великая пещера великого гоблина";
        }
    }
}