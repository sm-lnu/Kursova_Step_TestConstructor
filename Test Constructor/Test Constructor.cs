using System;
using System.Windows.Forms;

namespace Test_Constructor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lockOrElements(false);
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
            AddQuestion addQuestionForm = new AddQuestion();
            addQuestionForm.ShowDialog();
        }
    }
}
