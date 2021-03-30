using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNunitTestExample.Test.TestCase
{
    public class SearchTestCase
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("Видео").SetName("FindVideoByTextTest");
                yield return new TestCaseData("Картинки").SetName("FindImageByTextTest"); ;
            }
        }
    }
}
