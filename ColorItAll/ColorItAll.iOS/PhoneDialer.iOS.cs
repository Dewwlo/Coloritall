using System.Threading.Tasks;
using ColorItAll.iOS;
using ColorItAll.Interface;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]
namespace ColorItAll.iOS
{
    class PhoneDialer : IDialer
    {
        public Task<bool> DialAsync(string number)
        {
            return Task.FromResult(
                UIApplication.SharedApplication.OpenUrl(
                    new NSUrl("tel:" + number))
            );
        }
    }
}