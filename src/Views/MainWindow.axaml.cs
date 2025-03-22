using Avalonia.Controls;
using ii.InfinityEngine.Readers;
using ItmView.ViewModels;

namespace ItmView.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _mainViewModel = new MainViewModel();

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = _mainViewModel;
    }

    public MainWindow(string filePath)
    {
        InitializeComponent();
        this.DataContext = _mainViewModel;

        var tlkReader = new TlkFileBinaryReader();
        var tlk = tlkReader.Read(@"D:\Games\IE\bg2ee-pristine\dialog.tlk");

        var reader = new ItmFileBinaryReader();
        reader.TlkFile = tlk;
        var file = reader.Read(filePath);

        this.Title = filePath;

        _mainViewModel.File = file;
        _mainViewModel.FeatureBlocks = file.itmFeatureBlocks;
        _mainViewModel.Ext = file.itmExtendedHeaders;

        DataContext = _mainViewModel;
    }
}