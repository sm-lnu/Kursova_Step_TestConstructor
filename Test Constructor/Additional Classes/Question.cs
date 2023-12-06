using System;
using System.Collections.Generic;

namespace Test_Constructor.Additional_Classes
{
    public class Question
    {
        public String textOfQuestion { get; set; }
        public decimal points { get; set; }
        public string image { get; set; }
        public List<Answer> answers { get; set; }
        public Question() => createListOfAnswers();
        public Question(decimal points)
        {
            this.points = points;
            createListOfAnswers();
        }
        private void createListOfAnswers()=>answers = new List<Answer>();
        public void addAnswer(Answer answer) => answers.Add(answer);
    }
}
