using System;
using System.Windows.Forms;
using System.Linq;
using Test_Constructor.Additional_Classes;
using System.Drawing;
using System.Text;
using System.Xml;
using System.IO;

namespace Test_Constructor
{
    public partial class AddQuestion : Form
    {
        private int selectedRowIndex = -1;
        public Question question { get; private set; }
        public event EventHandler QuestionReturned;

        public AddQuestion(Question question)
        {
            InitializeComponent();
            this.question = question;
            if (question.answers.Count != 0)
                fillFieldsFromExistingQuestion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AddAnswer addAnswer = new AddAnswer(null))
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectedRowIndex = e.RowIndex;
            else
                selectedRowIndex = -1;
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            (string textOfAnswer, bool isTrueAnswer) = getDataFromDataGridView();
            int indexOfEditingAnswer = question.answers
                   .FindIndex(a => a.textOfAnswer == textOfAnswer && a.isTrueAnswer == isTrueAnswer);

            if (indexOfEditingAnswer!=-1) {
                AddAnswer addAnswer = new AddAnswer(question.answers[indexOfEditingAnswer]);
                addAnswer.ShowDialog();

                Answer returnedAnswer = addAnswer.answer;

                if (returnedAnswer != null)
                {
                    question.answers[indexOfEditingAnswer] = returnedAnswer;
                    editRowInDataGridView(returnedAnswer);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            (string textOfAnswer, bool isTrueAnswer) = getDataFromDataGridView();
            if (textOfAnswer!=null) {
                question.answers
                        .RemoveAll(a => a.textOfAnswer == textOfAnswer && a.isTrueAnswer == isTrueAnswer);
                dataGridView1.Rows.RemoveAt(selectedRowIndex);
            }
        }
        private (string,bool) getDataFromDataGridView()
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                string textOfAnswer = selectedRow.Cells[0].Value.ToString();
                bool isTrueAnswer = (bool)selectedRow.Cells[1].Value;
                return (textOfAnswer, isTrueAnswer);
            }
            else
                MessageBox.Show("No row selected.");
            return (null, false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    Image selectedImage = LoadAndResizeImage(selectedFilePath);
                    pictureBox1.Image = selectedImage;
                    question.image = selectedImage;
                }
            }
        }
        private Image LoadAndResizeImage(string imagePath)
        {
            Image originalImage = Image.FromFile(imagePath);
            int desiredHeight = 295;
            int proportionalWidth = (int)((float)desiredHeight / originalImage.Height * originalImage.Width);
            Image resizedImage = new Bitmap(originalImage, new Size(proportionalWidth, desiredHeight));
            return resizedImage;
        }
        private void editRowInDataGridView(Answer answer)
        {
            dataGridView1.Rows[selectedRowIndex].Cells[0].Value = answer.textOfAnswer;
            dataGridView1.Rows[selectedRowIndex].Cells[1].Value = answer.isTrueAnswer;

            dataGridView1.Refresh();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            question.image = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void fillFieldsFromExistingQuestion()
        {
            textBox1.Text = question.textOfQuestion;
            numericUpDown1.Value = question.points;
            pictureBox1.Image = question.image;
            foreach(var a in question.answers)
                dataGridView1.Rows.Add(a.textOfAnswer, a.isTrueAnswer);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            question.points = numericUpDown1.Value;
        }
    }
}
