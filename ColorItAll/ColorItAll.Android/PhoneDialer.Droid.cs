﻿using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Telephony;
using ColorItAll.Droid;
using ColorItAll.Interface;
using Xamarin.Forms;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(PhoneDialer))]
namespace ColorItAll.Droid
{
    public class PhoneDialer : IDialer
    {
        public Task<bool> DialAsync(string number)
        {
            var context = Android.App.Application.Context;
            if (context != null)
            {
                var intent = new Intent(Intent.ActionCall);
                intent.SetData(Uri.Parse("tel:" + number));

                if (IsIntentAvailable(context, intent))
                {
                    context.StartActivity(intent);
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }

        public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));
            if (list.Any())
                return true;

            TelephonyManager mgr = TelephonyManager.FromContext(context);
            return mgr.PhoneType != PhoneType.None;
        }
    }
}