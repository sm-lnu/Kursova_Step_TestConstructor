using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Test_Constructor.Additional_Classes;

namespace Test_Constructor
{
    public partial class AddQuestion : Form
    {
        public Question question { get; private set; }

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

            if (addAnswerForm != null)
                question.addAnswer(returnedAnswer);
        }
    }
}
