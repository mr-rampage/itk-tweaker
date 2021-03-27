using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Itk.Tweaker.Ui.Components
{
    public partial class DicomSourceStage
    {
        public DicomSourceStage()
        {
            InitializeComponent();
        }

        private void OnDicomImageSelected(object sender, RoutedEventArgs e)
        {
            using var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != DialogResult.OK) return;
            var dicomFolder = new DirectoryInfo(folderDialog.SelectedPath);
            if (!dicomFolder.Exists) return;
            RaiseEvent(new LoadDicomImageEvent(dicomFolder));
        }
    }
}