using System;

namespace DbClasses
{
    abstract class DbClass
    {
        protected static String StringOrNull(object stringToParse){
            if (DBNull.Value.Equals(stringToParse)) return "";
            else return stringToParse.ToString();
        }
    }
}