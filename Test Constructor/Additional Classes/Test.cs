using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Constructor.Additional_Classes
{
    public class Test
    {
        public String author { get; set; }
        public String title { get; set; }
        public String description { get; set; }
        public String infoForTestTaker { get; set; }
        public double maximumPointsForTest { get; set; }
        public double minimumPassingPercent { get; set; }
        public List<Question> questions { get; set; }

        public Test(string author, string title, string description, string infoForTestTaker, double maximumPointsForTest, double minimumPassingPercent, int countOfQuestions)
        {
            this.author = author;
            this.title = title;
            this.description = description;
            this.infoForTestTaker = infoForTestTaker;
            this.maximumPointsForTest = maximumPointsForTest;
            this.minimumPassingPercent = minimumPassingPercent;
            this.questions = new List<Question>(countOfQuestions);
        }
    }
}
