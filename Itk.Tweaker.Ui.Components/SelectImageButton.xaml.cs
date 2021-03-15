using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace Itk.Tweaker.Ui.Components
{
    public partial class SelectImageButton
    {
        public SelectImageButton()
        {
            InitializeComponent();
        }
        
        public static readonly DependencyProperty SelectedFolderProperty =
            DependencyProperty.Register(
                nameof(SelectedFolder), typeof(DirectoryInfo),
                typeof(SelectImageButton)
            );
        public DirectoryInfo SelectedFolder
        {
            get => (DirectoryInfo)GetValue(SelectedFolderProperty);
            private set => SetValue(SelectedFolderProperty, value);
        }
        private void SetSelectedFolder(object sender, RoutedEventArgs e)
        {
            using var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != DialogResult.OK) return;
            SelectedFolder = new DirectoryInfo(folderDialog.SelectedPath);
        }
    }
}