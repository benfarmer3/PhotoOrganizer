using PhotoOrganiser.DataBase;
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
            traverser = new FileSystemTraverser(this);
            InitializeComponent();
            DataBaseManager.CreateDb();
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            destinationFolder = DestFolder.Text;
            sourceFolder = SourceFolder.Text;

            await Task.Run(() => traverser.StartTraversal(sourceFolder));
            
        }

        private delegate void NameCallBack(string varText);
        public void UpdateTextBox(string input)
        {
            if (InvokeRequired)
            {
                TotalPhotos.BeginInvoke(new NameCallBack(UpdateTextBox), new object[] { input });
            }
            else
            {
                TotalPhotos.Text = input;
            }
        }
    }
}