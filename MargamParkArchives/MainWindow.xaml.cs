using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MargamParkArchives.DatabaseManagement;
using MargamParkArchives.Entities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MargamParkArchives
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private Artefact[] _artefacts = [];

        public MainWindow()
        {
            this.InitializeComponent();
            LoadRandomArtefacts();
        }

        private void LoadRandomArtefacts()
        {
            try
            {
                _artefacts = DatabaseOperationHandler.Get2RandomArtefacts();
            }
            catch (Exception ex)
            {
                // Display Error Message
                Debug.WriteLine(ex.Message);
            }

            // print artefacts
            Debug.WriteLine("2 random artefacts loaded from database:");
            foreach (Artefact artefact in _artefacts)
            {
                Debug.WriteLine(artefact);
            }
        }
    }
}
