namespace Additel.BLE
{
    /// <summary>
    /// 扫描模式，仅在 Android 5.0 版本后可用
    /// </summary>
    public enum BLEScanMode
    {
        Opportunistic = -1,
        LowPower = 0,
        Balanced = 1,
        LowLatency = 2
    }
}