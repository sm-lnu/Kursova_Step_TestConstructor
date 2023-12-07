using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Windows.Forms;
using Test_Constructor.Additional_Classes;
using Test_Constructor.SerializationDeserializationQuestion;

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
            toolStripMenuItem3.Enabled = true;
            toolStripMenuItem4.Enabled = true;
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

                dataGridView1.Refresh();
                dataGridView2.Refresh();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView1.Rows.Count && test.questions.Count-1 >= selectedRowIndex) {
                test.questions
                        .RemoveAt(selectedRowIndex);
                dataGridView1.Rows.RemoveAt(selectedRowIndex);

                dataGridView1.Refresh();
                dataGridView2.Refresh();
            }
            else
                MessageBox.Show("No row selected.");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView1.Rows.Count && test.questions.Count - 1 >= selectedRowIndex)
            {
                using (AddQuestion addQuestion = new AddQuestion(test.questions[selectedRowIndex]))
                {
                    addQuestion.ShowDialog();

                    Question returnedQuestion = addQuestion.question;

                    if (returnedQuestion != null)
                    {
                        test.questions[selectedRowIndex] = returnedQuestion;
                        editRowInDataGridView(returnedQuestion);
                    }
                }
            }
            else
                MessageBox.Show("No row selected.");
        }

        private void editRowInDataGridView(Question question)
        {
            dataGridView1.Rows[selectedRowIndex].Cells[0].Value = question.textOfQuestion;
            dataGridView1.Rows[selectedRowIndex].Cells[1].Value = question.points;
            dataGridView1.Rows[selectedRowIndex].Cells[2].Value = question.answers.Count;

            dataGridView1.Refresh();
            dataGridView2.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            test.author = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            test.title = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            test.description = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            test.infoForTestTaker = textBox4.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                test.maximumPointsForTest = int.Parse(textBox5.Text);
                errLabelMaximumPointsForTest.Visible = false;
            }
            catch (FormatException)
            {
                textBox5.Text = "0";
                errLabelMaximumPointsForTest.Visible = true;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
            {
                test.minimumPassingPercent = numericUpDown1.Value;
                errLabelMinimumPassingPercent.Visible = false;
            }
            else
                errLabelMinimumPassingPercent.Visible = true;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML Files|*.xml|All Files|*.*";
                saveFileDialog.Title = "Save XML File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = saveFileDialog.FileName;

                    XmlSerializerHelper<Test> xmlSerializerHelper = new XmlSerializerHelper<Test>();
                    string serializedXml = xmlSerializerHelper.SerializeToXml(test);

                    File.WriteAllText(selectedFilePath, serializedXml);

                    MessageBox.Show("XML file saved successfully!");
                }
            }
        }

        private void openTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedFilePath = OpenFileExplorer();

            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                string xml = File.ReadAllText(selectedFilePath);
                if (xml != null)
                {
                    XmlSerializerHelper<Test> xmlSerializerHelper = new XmlSerializerHelper<Test>();
                    Test deserializedTest = xmlSerializerHelper.DeserializeFromXml(xml);
                    test = deserializedTest;
                    fillTestFields();
                }
                else
                {
                    MessageBox.Show("Failed to deserialize the object.");
                }
            }
            else
                MessageBox.Show("No file selected.");
        }
        private string OpenFileExplorer()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
               openFileDialog.Filter = "XML Files|*.xml|All Files|*.*";
               openFileDialog.Title = "Select an XML File";

               if (openFileDialog.ShowDialog() == DialogResult.OK)
                  return openFileDialog.FileName;

               return string.Empty;
            }
        }
    }
}
