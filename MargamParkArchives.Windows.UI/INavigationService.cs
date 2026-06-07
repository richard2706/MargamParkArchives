using Microsoft.UI.Xaml.Controls;
using System;

namespace MargamParkArchives.Windows.UI;

public interface INavigationService
{
    /// <summary>
    /// Sets up the navigation service including setting the frame
    /// </summary>
    /// <param name="frame">Frame for which the service will operate</param>
    void Initialise(Frame frame);

    /// <summary>
    /// Navigates the frame to the specified page.
    /// </summary>
    /// <param name="pageType">The type of the page to navigate to.</param>
    /// <exception cref="InvalidOperationException">Thrown when the navigation service has not been initialised.</exception>
    void NavigateTo(Type page);

    /// <summary>
    /// Returns true if the frame has a previous page to navigate to.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Thrown when the navigation service has not been initialised.</exception>
    bool CanGoBack();

    /// <summary>
    /// Navigates the frame back to the previous page.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the navigation service has not been initialised.</exception>
    void NavigateBack();
}