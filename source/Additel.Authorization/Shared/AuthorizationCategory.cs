namespace Additel.Authorization
{
    public enum AuthorizationCategory
    {
        Camera,
#if __ANDROID__
        Location,
        Bluetooth,
        ExternalStorage,
#elif __IOS__
        LocationWhenUse,
        LocationAlways,
        PhotoLibrary,
#endif
    }
}
