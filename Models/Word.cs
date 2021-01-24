using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishVocabularyTest.Models
{
    public class Word
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Eng { get; set; }
        public string Rus { get; set; }
    }
}
