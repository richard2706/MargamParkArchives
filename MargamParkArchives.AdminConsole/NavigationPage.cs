namespace MargamParkArchives.AdminConsole;

// <summary>
/// Represents the different pages available in the MainPage navigation view.
/// </summary>
internal enum NavigationPage
{
    ArtefactSearch,
    Import,
}

// TODO consider changing this for a dictionary of page types to page info (containing page type, viewmodel type, page title etc.)
// this should make navigation service implementation more robust and flexible
// Can also implement NavigateTo using enum instead of page type
