using System.Windows;
using NodeEditor.Models;

namespace NodeEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void FileOpened(object sender, RoutedEventArgs e)
    {
        // Configure open file dialog box
        var dialog = new Microsoft.Win32.OpenFileDialog {
            FileName = "Untitled Project",
            DefaultExt = ".json", // Default file extension
            Filter = "Json Documents (.json)|*.json" // Filter files by extension
        };

        // Show open file dialog box
        var result = dialog.ShowDialog();

        // Process open file dialog box results
        if (result == true)
        {
            ((EditorViewModel)NodeEditor.DataContext).OnFileOpened(dialog.FileName);
        }   
    }
}