using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Constructor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lockElements();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            unlockElements();
            button1.Enabled = true;
        }

        private void unlockElements()
        {
            button1.Enabled = true;

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            numericUpDown1.Enabled = true;
        }
        private void lockElements()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            numericUpDown1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddQuestion addQuestion = new AddQuestion();
            addQuestion.ShowDialog();
        }
    }
}
