using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Constructor.Additional_Classes
{
    public class Question
    {
        public String textOfQuestion { get; set; }
        public double points { get; set; }
        byte[] image;
        public List<Answer> answers { get; set; }
        public Question() => createListOfAnswers();
        public Question(double points)
        {
            this.points = points;
            createListOfAnswers();
        }
        private void createListOfAnswers()=>answers = new List<Answer>();
        public void addAnswer(Answer answer) => answers.Add(answer);
    }
}
