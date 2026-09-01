using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace MargamParkArchives.Windows.UI.Pages;

/// <summary>
/// View model for import page
/// </summary>
public partial class ImportViewModel : ObservableObject
{
    internal const string FilePickerConfirmButtonText = "Confirm";
    internal const string ArtefactFilePickerDialogTitle = "Select Artefacts File";
    internal const string ArtefactFilePickerValidFileType = ".csv";
    internal const string ArtefactFilePickerInitialMessage = "No file selected.";
    internal const string ArtefactFilePickerCancelledMessage = "No file selected. Please try again.";
    internal const string ArtefactFilePickerPromptText = "Choose the CSV file containing artefacts to be imported";

    /// <summary>
    /// 
    /// </summary>
    internal event EventHandler? ArtefactFilePickerRequested;

    /// <summary>
    /// If the artefact file picker button is enabled or not
    /// </summary>
    /// <remarks>
    /// Uses OneWay bind in XAML so changes to this property will be reflected in the UI, but changes in the UI will not be reflected
    /// back to this property (not needed in this case anyway)
    /// </remarks>
    [ObservableProperty]
    private bool isPickArtefactFileButtonEnabled = true;

    /// <summary>
    /// 
    /// </summary>
    [ObservableProperty]
    private bool isImportButtonEnabled = true;

    /// <summary>
    /// 
    /// </summary>
    [ObservableProperty]
    private string artefactFilePickerMessage = ImportViewModel.ArtefactFilePickerInitialMessage;

    /// <summary>
    /// 
    /// </summary>
    public string ArtefactFilePickerPromptMessage => ImportViewModel.ArtefactFilePickerPromptText;

    /// <summary>
    /// The user has picked an artefact file from the file picker
    /// </summary>
    /// <param name="filePath"></param>
    internal void OnArtefactFilePicked(string? filePath)
    {
        this.ArtefactFilePickerMessage = filePath ?? ImportViewModel.ArtefactFilePickerCancelledMessage;
        this.IsPickArtefactFileButtonEnabled = true;
    }

    /// <summary>
    /// The user has cancelled the artefact file picker without picking a file
    /// </summary>
    internal void OnArtefactFilePickerCancelled()
    {
        this.ArtefactFilePickerMessage = ImportViewModel.ArtefactFilePickerCancelledMessage;
        this.IsPickArtefactFileButtonEnabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    [RelayCommand]
    private void OpenArtefactFilePicker()
    {
        this.IsPickArtefactFileButtonEnabled = false;

        // Request the view to open the file picker
        this.ArtefactFilePickerRequested?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 
    /// </summary>
    [RelayCommand]
    private void ImportArtefacts()
    {
        this.IsImportButtonEnabled = false;
    }
}
