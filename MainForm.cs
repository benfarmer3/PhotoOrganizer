using PhotoOrganiser.DataBase;
using PhotoOrganiser.Traverser;

namespace PhotoOrganiser
{
    public partial class MainForm : Form
    {
        private string destinationFolder = string.Empty;
        private string sourceFolder = string.Empty;
        private FileSystemTraverser traverser = new FileSystemTraverser();

        public MainForm()
        {
            InitializeComponent();
            DataBaseManager.CreateDb();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            destinationFolder = DestFolder.Text;
            sourceFolder = SourceFolder.Text;
            traverser.StartTraversal(sourceFolder);
        }
    }
}