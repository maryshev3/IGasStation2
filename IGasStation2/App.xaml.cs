using IGasStation2.ViewModels;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace IGasStation2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModelLocator.Init();

            base.OnStartup(e);
        }
    }

}
