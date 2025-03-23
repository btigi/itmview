using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ii.InfinityEngine.Readers;
using ItmView.ViewModels;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ItmView.Views;

public partial class MainWindow : Window
{
    private readonly MainViewModel _mainViewModel = new();

    public void InitializeComponent()
    {
        var overridePath = "override.axaml";
        if (File.Exists(overridePath))
        {
            var stream = File.ReadAllText(overridePath);
            var xaml = AvaloniaRuntimeXamlLoader.Parse<Control>(stream);
            this.Content = xaml;
        }
        else
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

    public MainWindow(IConfiguration _configuration, string filePath)
    {
        InitializeComponent();
        this.DataContext = _mainViewModel;

        var tlkPath = _configuration["DialogTlkPath"];
        var tlkReader = new TlkFileBinaryReader();
        var tlk = tlkReader.Read(tlkPath);

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