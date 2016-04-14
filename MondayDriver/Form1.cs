using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MondayDriver
{
    public partial class Form1 : Form
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        private FileInfo[] files;
        private DirectoryInfo[] directories;

        static int listBoxSelectedItem;

        public Form1()
        {
            InitializeComponent();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            listBox1.Enabled = false;

            foreach (DriveInfo drive in allDrives)
            {
                comboBox.Items.Add(drive.Name + " (" + drive.DriveType + ")");
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox.SelectedIndex;
            textBox1.Text = allDrives[index].Name;

            listBox1.Items.Clear();

            if (index != null)
            {
                listBox1.Enabled = true;
            }

            DirectoryInfo dinfo = new DirectoryInfo(allDrives[index].Name);

            files = dinfo.GetFiles();
            directories = dinfo.GetDirectories();

            Array.Sort(files, delegate (FileInfo f1, FileInfo f2) {
                return f1.Name.CompareTo(f2.Name);
            });

            Array.Sort(directories, delegate (DirectoryInfo f1, DirectoryInfo f2) {
                return f1.Name.CompareTo(f2.Name);
            });

            foreach (DirectoryInfo directory in directories)
            {
                listBox1.Items.Add(directory.Name);
            }

            foreach (FileInfo file in files)
            {
                listBox1.Items.Add(file.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxSelectedItem = listBox1.SelectedIndex;
            if ((listBoxSelectedItem + 1) > directories.Length)
            {
                textBox1.Text = files[listBoxSelectedItem - directories.Length].FullName;
            }
            else
            {
                textBox1.Text = directories[listBoxSelectedItem].FullName;
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(textBox1.Text);

            files = dinfo.GetFiles();
            directories = dinfo.GetDirectories();

            Array.Sort(files, delegate (FileInfo f1, FileInfo f2) {
                return f1.Name.CompareTo(f2.Name);
            });

            Array.Sort(directories, delegate (DirectoryInfo f1, DirectoryInfo f2) {
                return f1.Name.CompareTo(f2.Name);
            });

            foreach (DirectoryInfo directory in directories)
            {
                listBox1.Items.Add(directory.Name);
            }

            foreach (FileInfo file in files)
            {
                listBox1.Items.Add(file.Name);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                File.Delete(textBox1.Text);
            }
            else if (Directory.Exists(textBox1.Text))
            {
                Directory.Delete(textBox1.Text);
            }
        }
    }
}
