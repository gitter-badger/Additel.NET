﻿using System;

namespace Additel.BLE
{
    [Flags]
    public enum BLECharacteristicProperty
    {
        /// <summary>
        /// Characteristic value can be broadcasted.
        /// </summary>
        Broadcast = 1,
        /// <summary>
        /// Characteristic value can be read.
        /// </summary>
        Read = 2,
        /// <summary>
        /// Characteristic value can be written without response.
        /// </summary>
        WriteWithoutResponse = 4,
        /// <summary>
        /// Characteristic can be written with response.
        /// </summary>
        Write = 8,
        /// <summary>
        /// Characteristic can notify value changes without acknowledgement.
        /// </summary>
        Notify = 16,
        /// <summary>
        ///Characteristic can indicate value changes with acknowledgement.
        /// </summary>
        Indicate = 32,
        /// <summary>
        /// Characteristic value can be written signed.
        /// </summary>
        AuthenticatedSignedWrites = 64,
        /// <summary>
        /// Indicates that more properties are set in the extended properties descriptor.
        /// </summary>
        ExtendedProperties = 128
    }
}
