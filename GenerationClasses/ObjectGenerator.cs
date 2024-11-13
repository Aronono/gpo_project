using System.Collections.Generic;
using Mono.Data.Sqlite;

namespace GenerationClasses
{
    abstract class ObjectGenerator
    {
        protected string dbLink;
        protected delegate T CreateObjectDelegate<T>(object[] values);

        protected List<T> GetObjectsByQuery<T>(string query, CreateObjectDelegate<T> createObject)
        {
            List<T> resultList = new List<T>();

            using (SqliteConnection connection = new SqliteConnection(dbLink)) 
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] values = new object[reader.FieldCount];
                            reader.GetValues(values); // Получаем все значения в массив
                            T obj = createObject(values); // Вызываем делегат для создания объекта
                            resultList.Add(obj);
                        }
                    }
                }
            }
            return resultList;
        }
    }
}