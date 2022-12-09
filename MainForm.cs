using PhotoOrganiser.DataBase;
using PhotoOrganiser.Properties;
using PhotoOrganiser.Traverser;

namespace PhotoOrganiser
{
    public partial class MainForm : Form
    {
        private string destinationFolder = string.Empty;
        private string sourceFolder = string.Empty;
        private FileSystemTraverser traverser;

        public MainForm()
        {
            traverser = new FileSystemTraverser(UpdateValues, UpdateFolder, UpdateAverages);
            InitializeComponent();
            DataBaseManager.CreateDb();
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            destinationFolder = DestFolder.Text;
            sourceFolder = SourceFolder.Text;

            await Task.Run(() => traverser.StartTraversal(sourceFolder));

        }

        public void UpdateFolder(string currentDir)
        {
            ChangeTextBox(Searching, currentDir);
        }
        public void UpdateAverages(string averageExif, string averageNoExif)
        {
            ChangeTextBox(AverageScanTimeExif, averageExif);
            ChangeTextBox(AverageScanTimeNoExif, averageNoExif);
        }

        public void UpdateValues(string totalPhotos, string totalDupplicates, string totalExif)
        {
            ChangeTextBox(TotalPhotos, totalPhotos);
            ChangeTextBox(TotalDuplicates, totalDupplicates);
            ChangeTextBox(TotalNoExif, totalExif);
        }

        public void ChangeTextBox(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new MethodInvoker(() => textBox.Text = text));
            }
            else
            {
                textBox.Text = text;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(Settings.Default.SourceFolder != null)
            {
                this.SourceFolder.Text = Settings.Default.SourceFolder;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.SourceFolder = this.SourceFolder.Text;

            Settings.Default.Save();
        }
    }
}