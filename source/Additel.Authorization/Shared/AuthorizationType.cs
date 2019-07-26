namespace Additel.Authorization
{
    public enum AuthorizationType
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
