using System;
using System.Windows.Forms;
using System.Linq;
using Test_Constructor.Additional_Classes;

namespace Test_Constructor
{
    public partial class AddQuestion : Form
    {
        public Question question { get; private set; }
        public event EventHandler QuestionReturned;

        public AddQuestion()
        {
            InitializeComponent();
            question = new Question();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AddAnswer addAnswer = new AddAnswer())
            {
                addAnswer.AnswerReturned += AddAnswerForm_AnswerReturned;
                addAnswer.ShowDialog();
            }
        }

        private void AddAnswerForm_AnswerReturned(object sender, EventArgs e)
        {
            AddAnswer addAnswerForm = (AddAnswer)sender;
            Answer returnedAnswer = addAnswerForm.answer;

            if (returnedAnswer != null)
                question.addAnswer(returnedAnswer);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (isTextOfQuestionEmpty(sender, e))
                errLabelTextOfQuestion.Visible = true;
            else
            {
                errLabelTextOfQuestion.Visible = false;
                question.textOfQuestion = textBox1.Text;
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (isTextOfQuestionEmpty(sender,e))
                textBox1_TextChanged(sender, e);
            else if (isListOfAnswersEmpty())
                errLabelAnswers.Visible = true;
            else if (!isAtLeastOneTrue())
                errLabelAnswers.Visible = true;
            else
            {
                errLabelAnswers.Visible = false;
                QuestionReturned?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }
        private bool isTextOfQuestionEmpty(object sender, EventArgs e) => string.IsNullOrEmpty(textBox1.Text);
        private bool isListOfAnswersEmpty() => question.answers.Count == 0;
        private bool isAtLeastOneTrue()=>question.answers.Any(answer => answer.isTrueAnswer);
    }
}
