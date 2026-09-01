using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.Storage.Pickers;
using System;

namespace MargamParkArchives.Windows.UI.Pages;

/// <summary>
/// Page for the user to import items (artefacts, potentially other types in future) into the app
/// </summary>
public sealed partial class ImportPage : Page
{
    private ImportViewModel? _viewModel;

    /// <summary>
    /// 
    /// </summary>
    public ImportPage()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <exception cref="ArgumentException"></exception>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is ImportViewModel viewModel)
        {
            _viewModel = viewModel;
            //this.DataContext = _viewModel; // Only needed when using {Binding ...} in XAML

            this._viewModel.ArtefactFilePickerRequested += this.OnOpenArtefactFilePickerRequested;
        }
        else
        {
            string message = string.Format(WindowsUIConstants.ViewModelInvalidMessage, nameof(ImportPage), nameof(ImportViewModel));
            throw new ArgumentException(message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);

        this._viewModel?.ArtefactFilePickerRequested -= this.OnOpenArtefactFilePickerRequested;
    }

    /// <summary>
    /// Opens the file picker for the user to select an CSV file to import artefacts from
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnOpenArtefactFilePickerRequested(object? sender, EventArgs e)
    {
        FileOpenPicker filePicker = new(this.XamlRoot.ContentIslandEnvironment.AppWindowId)
        {
            FileTypeFilter = { ImportViewModel.ArtefactFilePickerValidFileType },
            CommitButtonText = ImportViewModel.FilePickerConfirmButtonText,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            ViewMode = PickerViewMode.List,
            Title = ImportViewModel.ArtefactFilePickerDialogTitle,
        };

        // Wait for user to select file or cancel
        PickFileResult file = await filePicker.PickSingleFileAsync();
        
        if (file is not null)
        {
            this._viewModel?.OnArtefactFilePicked(file.Path);
        }
        else
        {
            this._viewModel?.OnArtefactFilePickerCancelled();
        }
    }
}
