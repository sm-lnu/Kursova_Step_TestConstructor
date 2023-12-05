using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Constructor.Additional_Classes
{
    public class Answer
    {
        public String textOfAnswer { get; set; }
        public bool isTrueAnswer { get; set; }

        public Answer(string textOfAnswer, bool isTrueAnswer)
        {
            this.textOfAnswer = textOfAnswer;
            this.isTrueAnswer = isTrueAnswer;
        }
    }
}
