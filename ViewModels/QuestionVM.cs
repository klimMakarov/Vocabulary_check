using EnglishVocabularyTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishVocabularyTest.ViewModels
{
    public class QuestionVM
    {
        public int Counter { get; set; }
        public string Question { get; set; }
        public string RightAnswer { get; set; }
        public int Rating { get; set; }
        public sbyte Scale { get; set; }
        public Word[] Answers { get; set; }
        public List<string> PreviousWords { get; set; } = new List<string>() { "" };
    }
}
