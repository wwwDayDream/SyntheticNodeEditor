using System.IO;
using System.Windows;
using NodeEditor.Models;

namespace NodeEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
	public static string WinTitle = "SyntheticNodeEditor";
	public static double WinWidth = 1000;
	public static double WinHeight = 600;

    public MainWindow()
    {
		this.Title = WinTitle;
		this.Width = WinWidth;
		this.Height = WinHeight;
		#if DEBUG
		this.Left = 1920; //! Will be off screen if user doesnt have a second screan
		#endif
		this.Show();
        InitializeComponent();
		Console.WriteLine("START");
		((EditorViewModel)NodeEditor.DataContext).OnFileOpened(Path.Combine(Environment.CurrentDirectory, @"..\ExampleCreation\5dfee9de-305b-4886-ad5d-bee897758293.json"));
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