using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;

namespace UWPMultipleInstancesBug
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ApiInformation.IsTypePresent("Windows.ApplicationModel.AppInstance"))
            {
                IActivatedEventArgs activatedArgs = AppInstance.GetActivatedEventArgs();

                if (AppInstance.RecommendedInstance != null)
                {
                    AppInstance.RecommendedInstance.RedirectActivationTo();
                }
                else
                {

                    var instance = AppInstance.GetInstances().LastOrDefault();
                    if (instance == null || activatedArgs is Windows.ApplicationModel.Activation.LaunchActivatedEventArgs)
                    {
                        // If we successfully registered this instance, we can now just
                        // go ahead and do normal XAML initialization.
                        AppInstance.FindOrRegisterInstanceForKey(Guid.NewGuid().ToString());
                        global::Windows.UI.Xaml.Application.Start((p) => new App());
                    }
                    else
                    {
                        instance.RedirectActivationTo();
                    }
                }
            }
            else
            {
                global::Windows.UI.Xaml.Application.Start((p) => new App());
            }
        }
    }
}
