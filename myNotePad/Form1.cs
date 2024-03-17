using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace myNotePad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initController();
        }

        private void initController()
        {
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button5.Click += Button5_Click;

            textBox1.KeyDown += TextBox1_KeyDown;
            listBox1.DoubleClick += Listbox1_DoubleClick;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // 사용자가 Ctrl + S 키를 눌렀는지 확인합니다.
            if (e.Control && e.KeyCode == Keys.S)
            {
                // textbox1의 내용을 listbox1에 추가합니다.
                listBox1.Items.Add(textBox1.Text);

                // 추가 후에는 textbox1을 비워줍니다.
                textBox1.Clear();

                // 입력을 처리했으므로 이벤트를 중단합니다.
                e.SuppressKeyPress = true;
            }
        }

        private void Listbox1_DoubleClick(object sender, EventArgs e)
        {
            // 리스트 박스에서 선택된 항목을 가져와서 textbox1에 표시합니다.
            if (listBox1.SelectedItem != null)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // textbox1의 내용을 listbox1에 추가합니다.
            listBox1.Items.Add(textBox1.Text);

            // 추가 후에는 textbox1을 비워줍니다.
            textBox1.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("초기화 할 내용이 없습니다.", "알림");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string logFolder = "log"; // 로그 폴더 이름
            string fileName = Path.Combine(logFolder, DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var item in listBox1.Items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
                MessageBox.Show("파일이 log 폴더에 성공적으로 저장되었습니다.");
                MessageBox.Show("리스트가 초기화 됩니다");
                listBox1.Items.Clear();
            }
            catch (System.ObjectDisposedException)
            {
                MessageBox.Show("The System.IO.TextWriter is closed.");
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("An I/O error occurs.");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show("정말 리스트를 삭제하시겠습니까?", "알림", MessageBoxButtons.YesNo))
                {
                    listBox1.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show("삭제할 리스트가 없습니다.", "알림");
            }
            return;
        }

        private void Button5_Click(object sender , EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            else
            {
                MessageBox.Show("선택된 항목이 없습니다.");
            }
        }
    }
}
