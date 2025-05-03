using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace WpfFileCopy
{
    public partial class MainWindow : Window
    {
        private string selectedFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                selectedFilePath = dialog.FileName;
                FilePathText.Text = "เลือกไฟล์: " + selectedFilePath;
            }
        }

        private void CopyFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath) || !File.Exists(selectedFilePath))
            {
                MessageBox.Show("กรุณาเลือกไฟล์ก่อน");
                return;
            }

            if (!int.TryParse(RepeatCountTextBox.Text, out int repeatCount) || repeatCount <= 0)
            {
                MessageBox.Show("กรุณาใส่จำนวนรอบที่ถูกต้อง");
                return;
            }

            string directory = Path.GetDirectoryName(selectedFilePath);
            string fileName = Path.GetFileNameWithoutExtension(selectedFilePath);
            string extension = Path.GetExtension(selectedFilePath);

            for (int i = 1; i <= repeatCount; i++)
            {
                string newFilePath = Path.Combine(directory, $"{fileName}_{i}{extension}");
                File.Copy(selectedFilePath, newFilePath, overwrite: true);
            }

            ResultText.Text = $"คัดลอกไฟล์ {repeatCount} ไฟล์เรียบร้อยแล้ว";
        }
    }
}
