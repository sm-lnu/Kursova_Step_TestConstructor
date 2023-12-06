using System;
using System.Windows.Forms;
using Test_Constructor.Additional_Classes;

namespace Test_Constructor
{
    public partial class AddAnswer : Form
    {
        public Answer answer { get; private set; }
        public event EventHandler AnswerReturned;

        public AddAnswer(Answer rAnswer)
        {
            InitializeComponent();
            if (rAnswer == null)
                answer = new Answer();
            else
            {
                answer = rAnswer;
                setDataFromReceivedAnswer();
            }
        }

        private void button2_Click(object sender, EventArgs e) => invokeAnswerReturned();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length !=0 )
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!=null) 
                answer = new Answer(textBox1.Text, checkBox1.Checked);
            invokeAnswerReturned();
        }
        private void invokeAnswerReturned()
        {
            AnswerReturned?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
        private void setDataFromReceivedAnswer()
        {
            textBox1.Text = answer.textOfAnswer;
            checkBox1.Checked = answer.isTrueAnswer;
        }
    }
}
