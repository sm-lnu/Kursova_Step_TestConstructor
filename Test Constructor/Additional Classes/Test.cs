using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Constructor.Additional_Classes
{
    class Test
    {
        public String textOfQuestion { get; set; }
        public long points { get; set; }
        public int countOfAnswers { get; set; }
        public List<Question> questions { get; set; }

        public Test(String textOfQuetion, long points, int countOfAnswers)
        {
            this.textOfQuestion = textOfQuetion;
            this.points = points;
            this.countOfAnswers = countOfAnswers;
        }
    }
}
