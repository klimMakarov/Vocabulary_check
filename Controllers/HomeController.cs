using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EnglishVocabularyTest.Models;
using System.Data.Common;
using EnglishVocabularyTest.ViewModels;

namespace EnglishVocabularyTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public double FinalResult(QuestionVM result)
        //{
        //    double TotalScore = 0;
        //    double[] points = { result.PTS_4, result.PTS_8, result.PTS_12, result.PTS_16 };
        //    for(int i = 0; i< points.Length; i++)
        //    {
        //        points[i] = points[i] / 10 * 4000;
        //        TotalScore += points[i];
        //    }
        //    return TotalScore;
        //}
    }
}
