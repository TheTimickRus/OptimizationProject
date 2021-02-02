using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace OptimiationProject.ViewModels.Ocher
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainVm => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}