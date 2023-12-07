using System;
using System.Windows.Forms;
using Test_Constructor.Additional_Classes;

namespace Test_Constructor
{
    public partial class Form1 : Form
    {
        private Test test;
        private int selectedRowIndex = -1;

        public Form1()
        {
            InitializeComponent();
            lockOrElements(false);
            test = new Test();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lockOrElements(true);
        }

        private void lockOrElements(bool value)
        {
            button1.Enabled = value;
            button2.Enabled = value;
            button3.Enabled = value;

            dataGridView1.Enabled = value;
            dataGridView2.Enabled = value;

            textBox1.Enabled = value;
            textBox2.Enabled = value;
            textBox3.Enabled = value;
            textBox4.Enabled = value;
            textBox5.Enabled = value;
            textBox6.Enabled = value;

            numericUpDown1.Enabled = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AddQuestion addQuestion = new AddQuestion(new Question()))
            {
                addQuestion.QuestionReturned += AddQuestionForm_QuestionReturned;
                addQuestion.ShowDialog();
            }
        }

        private void AddQuestionForm_QuestionReturned(object sender, EventArgs e)
        {
            AddQuestion addQuestionForm = (AddQuestion)sender;
            Question returnedQuestion = addQuestionForm.question;

            if (returnedQuestion != null)
            {
                test.questions.Add(returnedQuestion);
                dataGridView1.Rows.Add(returnedQuestion.textOfQuestion, returnedQuestion.points,returnedQuestion.answers.Count);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectedRowIndex = e.RowIndex;
            else
                selectedRowIndex = -1;

            if (selectedRowIndex!=-1)
            {

                if (selectedRowIndex <= test.questions.Count-1) {
                    dataGridView2.Rows.Clear();
                    Question question = test.questions[selectedRowIndex];
                    foreach (var a in question.answers)
                        dataGridView2.Rows.Add(a.textOfAnswer, a.isTrueAnswer);

                    pictureBox1.Image = question.image;
                    dataGridView2.Refresh();
                }
            }
        }
    }
}
