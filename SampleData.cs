using EnglishVocabularyTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishVocabularyTest
{
    public class SampleData
    {
        private static string _path = @"C:\322\Vanya's app\Новая папка\Vocabulary-check-master\wwwroot\fList\20200.json";
        private static FrequencyContainer GetWordsFromJson()
        {
            using(var reader = File.OpenText(_path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<FrequencyContainer>(fileText);
            }
        }
        public static void Initialize(DataContext context)
        {
            if (!context.Words.Any())
            {
                FrequencyContainer container = GetWordsFromJson();
                foreach(var question in container.Words)
                {
                    context.Add(question);
                }
                context.SaveChanges();
            }
        }
    }
}
