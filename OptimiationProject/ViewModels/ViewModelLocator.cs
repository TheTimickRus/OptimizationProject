/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MvvmExample"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmExample.WindowService;

namespace MvvmExample.ViewModel
{
    //Это тот самый Composition Root (гугли)

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            #region Пример, можешь удалить.

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            #endregion

            
            SimpleIoc.Default.Register<IBrowserWindowService, BrowserWindowService>();
            SimpleIoc.Default.Register<IIntegratorWindowsService, IntegratorWindowService>();
            SimpleIoc.Default.Register<IntegratorSettingsViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        //Не стоит помещать все возможные вью модели сюда таким способом. Остальные, к примеру, 
        //может создавать эта вью модель в своих методах, когда нужно.
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}