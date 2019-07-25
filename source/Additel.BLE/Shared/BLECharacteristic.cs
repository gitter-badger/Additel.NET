using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    public partial class BLECharacteristic : IDisposable
    {
        #region 事件

        public event EventHandler ValueChanged;
        public event EventHandler<BLEErrorEventArgs> ReadFailed;
        public event EventHandler ValueWritten;
        public event EventHandler<BLEErrorEventArgs> WriteFailed;
        public event EventHandler NotificationSet;
        public event EventHandler<BLEErrorEventArgs> SetNotificationFailed;
        public event EventHandler<BLEDescriptorsEventArgs> DescriptorsDiscovered;
        public event EventHandler<BLEErrorEventArgs> DiscoverDescriptorsFailed;
        #endregion

        #region 属性

        public Guid UUID
            => PlatformUUID;

        public string Name
            => this.GetName();

        public byte[] Value
            => PlatformValue;

        public BLECharacteristicProperty Properties
            => PlatformProperties;

        public bool CanRead
            => Properties.HasFlag(BLECharacteristicProperty.Read);

        public bool CanWrite
            => Properties.HasFlag(BLECharacteristicProperty.Write |
                                  BLECharacteristicProperty.WriteWithoutResponse);

        public bool CanNotify
            => Properties.HasFlag(BLECharacteristicProperty.Notify |
                                  BLECharacteristicProperty.Indicate);

        public bool IsNotifying
            => PlatformIsNotifying;
        #endregion

        #region 构造

        private BLECharacteristic() { }
        #endregion

        #region 方法

        public void Read()
            => PlatformRead();

        /// <summary>
        /// 发送数据, 大于20字节建议分包发送
        /// </summary>
        /// <param name="data">数据包</param>
        public void Write(byte[] data)
            => PlatformWrite(data);

        public void SetNotification(bool enabled)
            => PlatformSetNotification(enabled);

        public void DiscoverDescriptors()
            => PlatformDiscoverDescriptors();

        private void RaiseValueChanged()
            => ValueChanged?.Invoke(this, EventArgs.Empty);

        private void RaiseReadFailed(BLEErrorCode code)
            => ReadFailed?.Invoke(this, new BLEErrorEventArgs(code));

        private void RaiseValueWritten()
            => ValueWritten?.Invoke(this, EventArgs.Empty);

        private void RaiseWriteFailed(BLEErrorCode code)
            => WriteFailed?.Invoke(this, new BLEErrorEventArgs(code));

        private void RaiseNotificationSet()
            => NotificationSet?.Invoke(this, EventArgs.Empty);

        private void RaiseSetNotificationFailed(BLEErrorCode code)
            => SetNotificationFailed?.Invoke(this, new BLEErrorEventArgs(code));

        private void RaiseDescriptorsDiscovered(IList<BLEDescriptor> descriptors)
            => DescriptorsDiscovered?.Invoke(this, new BLEDescriptorsEventArgs(descriptors));

        private void RaiseDiscoverDescriptorsFailed(BLEErrorCode code)
            => DiscoverDescriptorsFailed?.Invoke(this, new BLEErrorEventArgs(code));
        #endregion

        #region IDisposable

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                PlatformDispose(disposing);

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~BLECharacteristic() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
