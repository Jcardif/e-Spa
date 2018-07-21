using Xamarin.Facebook;
namespace e_SpaMobileApp.Helpers
{
    public class OnProfileChangedEventArgs
    {
        public Profile mProfile;
        public OnProfileChangedEventArgs(Profile profile)
        {
            mProfile = profile;
        }
    }
}