using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ii.InfinityEngine.Files;
using System;
using System.Collections.Generic;

namespace ItmView.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ItmFile file;

    [ObservableProperty]
    private List<ItmExtendedHeader2> ext;

    [ObservableProperty]
    private List<ItmFeatureBlock2> featureBlocks;

    [RelayCommand]
    private void Close()
    {
        Environment.Exit(0);
    }
}