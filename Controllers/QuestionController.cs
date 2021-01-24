using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishVocabularyTest.Models;
using EnglishVocabularyTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace EnglishVocabularyTest.Controllers
{
    public class QuestionController : Controller
    {
        DataContext db;
        Random rnd = new Random();

        public QuestionController(DataContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult QuestionPage(QuestionVM qvm)
        {
            if(qvm.Counter == 40)
            {
                return RedirectToAction("ResultPage", "Question", new { result = qvm.Rating });
            }
                
            Word _question = _wordExtractor(GetMeAScope(qvm), qvm);
            qvm.Answers = _answersCreator(_question);
            qvm.Question = _question.Eng;
            qvm.RightAnswer = _question.Rus;
            qvm.Rating = _question.Rating;
            qvm.PreviousWords.Add(_question.Eng);
            return View(qvm);
        }

        [HttpPost]
        public IActionResult QuestionPage(QuestionVM respond, string Answer)
        {
            respond = respond.Counter < 30 ? 
                RatingChanger(respond, Answer, 1000) : RatingChanger(respond, Answer, 100);

            respond.Counter++;
            return QuestionPage(respond);
        }

        public IActionResult ResultPage(int result)
        {
            ViewBag.Result = result / 100 * 100;
            return View();
        }
        private int GetMeAScope(QuestionVM vm)
        {
            if (vm.Counter < 30)
            {
                return vm.Counter == 0 ? 5000 : vm.Rating / 1000 * 1000;
            }
            else if (vm.Counter == 30)
            {
                return vm.Rating / 1000 * 1000 + 500;
            }
            else
            {
                return vm.Rating / 100 * 100;
            }
        }
        private Word _wordExtractor(int scope, QuestionVM qvm)
        {
            Word _word = null;
            int midScope = scope;
            int a;
            while(_word == null)
            {
                try
                {
                    if (qvm.Counter < 30)
                    {
                        a = rnd.Next(scope, scope + 1000);
                        _word = db.Words.Where(x => x.Rating == a).First();
                    }
                    else if (qvm.Counter == 30)
                        _word = db.Words.Where(x => x.Rating == midScope).First();
                    else
                    {
                        a = rnd.Next(scope, scope + 100);
                        _word = db.Words.Where(x => x.Rating == a).First();
                    }
                    //Проверка на повторяемость слов
                    foreach(string s in qvm.PreviousWords)
                    {
                        if (s == _word.Eng)
                            throw new Exception();
                    }
                }
                catch
                {
                    _word = null;
                    midScope++;
                }
            }
            return _word;
        }
        private Word[] _answersCreator(Word _question)
        {
            Word[] _answers = new Word[5];
            for (int i = 0; i < _answers.Length; i++)
            {
                _answers[i] = db.Words.Find(rnd.Next(3000, 10000));
                if (_answers[i] == _question)
                {
                    i = 0;
                }
            }
            _answers[rnd.Next(0, 4)] = _question;
            return _answers;
        }
        private QuestionVM RatingChanger(QuestionVM respond, string Answer, int points)
        {
            if (Answer == respond.RightAnswer)
            {
                respond.Scale++;
                if (respond.Scale == 2)
                {
                    respond.Scale = 0;
                    respond.Rating += points;
                }
            }
            else
            {
                respond.Scale--;
                if (respond.Scale == -2)
                {
                    respond.Scale = 0;
                    respond.Rating -= points;
                }
            }
            return respond;
        }
    }
}
