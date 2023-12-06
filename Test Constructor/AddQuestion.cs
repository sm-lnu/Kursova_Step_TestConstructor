using System;
using System.Windows.Forms;
using System.Linq;
using Test_Constructor.Additional_Classes;
using System.Collections.Generic;

namespace Test_Constructor
{
    public partial class AddQuestion : Form
    {
        private int selectedRowIndex = -1;
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
            {
                question.addAnswer(returnedAnswer);
                dataGridView1.Rows.Add(returnedAnswer.textOfAnswer,returnedAnswer.isTrueAnswer);
            }
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            SelectAndManipulateRow();
        }

        private void SelectAndManipulateRow()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string name = selectedRow.Cells["Name"].Value.ToString();
                bool isTrueAnswer = (bool)selectedRow.Cells[""].Value;
            }
            else
                MessageBox.Show("No row selected.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectedRowIndex = e.RowIndex;
            else
                selectedRowIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView1.Rows.Count)
            {
                (string textOfAnswer, bool isTrueAnswer) = getDataFromDataGridView();
                question.answers
                    .RemoveAll(a => a.textOfAnswer == textOfAnswer && a.isTrueAnswer == isTrueAnswer);
                dataGridView1.Rows.RemoveAt(selectedRowIndex);
            }
            else
                MessageBox.Show("No row selected.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //realize
            //if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView1.Rows.Count)
            //{
            //    DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

            //    string textOfAnswer = selectedRow.Cells["textOfAnswer"].Value.ToString();
            //    bool isTrueAnswer = (bool)selectedRow.Cells["isTrueAnswer"].Value;
            //}
        }
        private (string,bool) getDataFromDataGridView()
        {
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

            string textOfAnswer = selectedRow.Cells[0].Value.ToString();
            bool isTrueAnswer = (bool)selectedRow.Cells[1].Value;
            return (textOfAnswer,isTrueAnswer);
        }
    }
}
