using Additel.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Additel.BLE
{
    public static partial class BLEExtensions
    {
        #region 字段

        private static readonly Lazy<IDictionary<Guid, string>> _lazyServiceNames
            = new Lazy<IDictionary<Guid, string>>(CreateServiceNames, true);

        private static readonly Lazy<IDictionary<Guid, string>> _lazyCharacteristicNames
            = new Lazy<IDictionary<Guid, string>>(CreateCharacteristicNames, true);

        private static readonly Lazy<IDictionary<Guid, string>> _lazyDescriptorNames
            = new Lazy<IDictionary<Guid, string>>(CreateDescriptorNames, true);
        #endregion

        #region 方法

        private static IDictionary<Guid, string> CreateServiceNames()
            => new List<(Guid UUID, string Name)>
            {
                (Guid.Parse(BLEConstants.ALERT_NOTIFICATION_SERVICE), "Alert Notification Service"),
                (Guid.Parse(BLEConstants.BATTERY_SERVICE), "Battery Service"),
                (Guid.Parse(BLEConstants.BLOOD_PRESSURE), "Blood Pressure"),
                (Guid.Parse(BLEConstants.CURRENT_TIME_SERVICE), "Current Time Service"),
                (Guid.Parse(BLEConstants.CYCLING_POWER), "Cycling Power"),
                (Guid.Parse(BLEConstants.CYCLING_SPEED_AND_CADENCE), "Cycling Speed and Cadence"),
                (Guid.Parse(BLEConstants.DEVICE_INFORMATION), "Device Information"),
                (Guid.Parse(BLEConstants.GENERIC_ACCESS), "Generic Access"),
                (Guid.Parse(BLEConstants.GENERIC_ATTRIBUTE), "Generic Attribute"),
                (Guid.Parse(BLEConstants.GLUCOSE), "Glucose"),
                (Guid.Parse(BLEConstants.HEALTH_THERMOMETER), "Health Thermometer"),
                (Guid.Parse(BLEConstants.HEART_RATE), "Heart Rate"),
                (Guid.Parse(BLEConstants.HUMAN_INTERFACE_DEVICE), "Human Interface Device"),
                (Guid.Parse(BLEConstants.IMMEDIATE_ALERT), "Immediate Alert"),
                (Guid.Parse(BLEConstants.LINK_LOSS), "Link Loss"),
                (Guid.Parse(BLEConstants.LOCATION_AND_NAVIGATION), "Location and Navigation"),
                (Guid.Parse(BLEConstants.NEXT_DST_CHANGE_SERVICE), "Next DST Change Service"),
                (Guid.Parse(BLEConstants.PHONE_ALERT_STATUS_SERVICE), "Phone Alert Status Service"),
                (Guid.Parse(BLEConstants.REFERENCE_TIME_UPDATE_SERVICE), "Reference Time Update Service"),
                (Guid.Parse(BLEConstants.RUNNING_SPEED_AND_CADENCE), "Running Speed and Cadence"),
                (Guid.Parse(BLEConstants.SCAN_PARAMETERS), "Scan Parameters"),
                (Guid.Parse(BLEConstants.TX_POWER), "TX Power"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_ACCELEROMETER), "TI SensorTag Accelerometer"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_BAROMETER), "TI SensorTag Barometer"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_CONNECTION_CONTROL), "TI SensorTag Connection Control"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_GYROSCOPE), "TI SensorTag Gyroscope"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_HUMIDITY), "TI SensorTag Humidity"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_INFRARED_THERMOMETER), "TI SensorTag Infrared Thermometer"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_MAGNOMETER), "TI SensorTag Magnometer"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_OVERTHEAIR_DOWNLOAD), "TI SensorTag OvertheAir Download"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_SMART_KEYS), "TI SensorTag Smart Keys"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_TEST), "TI SensorTag Test"),
                (Guid.Parse(BLEConstants.TXRX_SERV_UUID_REDBEARLABS_BISCUIT_SERVICE), "TXRX_SERV_UUID RedBearLabs Biscuit Service"),
            }.ToLookup(n => n.UUID, n => n.Name).ToDictionary(n => n.Key, n => n.FirstOrDefault());

        private static IDictionary<Guid, string> CreateCharacteristicNames()
            => new List<(Guid UUID, string Name)>
            {
                (Guid.Parse(BLEConstants.AEROBIC_HEART_RATE_LOWER_LIMIT), "Aerobic Heart Rate Lower Limit"),
                (Guid.Parse(BLEConstants.AEROBIC_HEART_RATE_UPPOER_LIMIT), "Aerobic Heart Rate Uppoer Limit"),
                (Guid.Parse(BLEConstants.AGE), "Age"),
                (Guid.Parse(BLEConstants.AGGREGATE_INPUT), "Aggregate Input"),
                (Guid.Parse(BLEConstants.ALERT_CATEGORY_ID), "Alert Category ID"),
                (Guid.Parse(BLEConstants.ALERT_CATEGORY_ID_BIT_MASK), "Alert Category ID Bit Mask"),
                (Guid.Parse(BLEConstants.ALERT_LEVEL), "Alert Level"),
                (Guid.Parse(BLEConstants.ALERT_NOTIFICATION_CONTROL_POINT), "Alert Notification Control Point"),
                (Guid.Parse(BLEConstants.ALERT_STATUS), "Alert Status"),
                (Guid.Parse(BLEConstants.ANAEROBIC_HEART_RATE_LOWER_LIMIT), "Anaerobic Heart Rate Lower Limit"),
                (Guid.Parse(BLEConstants.ANAEROBIC_HEART_RATE_UPPER_LIMIT), "Anaerobic Heart Rate Upper Limit"),
                (Guid.Parse(BLEConstants.ANAEROBIC_THRESHOLD), "Anaerobic Threshold"),
                (Guid.Parse(BLEConstants.ANALOG_INPUT), "Analog Input"),
                (Guid.Parse(BLEConstants.APPARENT_WIND_DIRECTION), "Apparent Wind Direction"),
                (Guid.Parse(BLEConstants.APPARENT_WIND_SPEED), "Apparent Wind Speed"),
                (Guid.Parse(BLEConstants.APPEARANCE), "Appearance"),
                (Guid.Parse(BLEConstants.BATTERY_LEVEL), "Battery Level"),
                (Guid.Parse(BLEConstants.BATTERY_LEVEL_STATE), "Battery Level State"),
                (Guid.Parse(BLEConstants.BATTERY_POWER_STATE), "Battery Power State"),
                (Guid.Parse(BLEConstants.BLOOD_PRESSURE_FEATURE), "Blood Pressure Feature"),
                (Guid.Parse(BLEConstants.BLOOD_PRESSURE_MEASUREMENT), "Blood Pressure Measurement"),
                (Guid.Parse(BLEConstants.BODY_COMPOSITION_FEATURE), "Body Composition Feature"),
                (Guid.Parse(BLEConstants.BODY_COMPOSITION_MEASUREMENT), "Body Composition Measurement"),
                (Guid.Parse(BLEConstants.BODY_SENSOR_LOCATION), "Body Sensor Location"),
                (Guid.Parse(BLEConstants.BOOT_KEYBOARD_INPUT_REPORT), "Boot Keyboard Input Report"),
                (Guid.Parse(BLEConstants.BOOT_KEYBOARD_OUTPUT_REPORT), "Boot Keyboard Output Report"),
                (Guid.Parse(BLEConstants.BOOT_MOUSE_INPUT_REPORT), "Boot Mouse Input Report"),
                (Guid.Parse(BLEConstants.CSC_FEATURE), "CSC Feature"),
                (Guid.Parse(BLEConstants.CSC_MEASUREMENT), "CSC Measurement"),
                (Guid.Parse(BLEConstants.CURRENT_TIME), "Current Time"),
                (Guid.Parse(BLEConstants.CYCLING_POWER_CONTROL_POINT), "Cycling Power Control Point"),
                (Guid.Parse(BLEConstants.CYCLING_POWER_FEATURE), "Cycling Power Feature"),
                (Guid.Parse(BLEConstants.CYCLING_POWER_MEASUREMENT), "Cycling Power Measurement"),
                (Guid.Parse(BLEConstants.CYCLING_POWER_VECTOR), "Cycling Power Vector"),
                (Guid.Parse(BLEConstants.DATABASE_CHANGE_INCREMENT), "Database Change Increment"),
                (Guid.Parse(BLEConstants.DATE_OF_BIRTH), "Date of Birth"),
                (Guid.Parse(BLEConstants.DATE_OF_THRESHOLD_ASSESSMENT), "Date of Threshold Assessment"),
                (Guid.Parse(BLEConstants.DATE_TIME), "Date Time"),
                (Guid.Parse(BLEConstants.DAY_DATE_TIME), "Day Date Time"),
                (Guid.Parse(BLEConstants.DAY_OF_WEEK), "Day of Week"),
                (Guid.Parse(BLEConstants.DESCRIPTOR_VALUE_CHANGED), "Descriptor Value Changed"),
                (Guid.Parse(BLEConstants.DEVICE_NAME), "Device Name"),
                (Guid.Parse(BLEConstants.DEW_POINT), "Dew Point"),
                (Guid.Parse(BLEConstants.DIGITAL_INPUT), "Digital Input"),
                (Guid.Parse(BLEConstants.DIGITAL_OUTPUT), "Digital Output"),
                (Guid.Parse(BLEConstants.DST_OFFSET), "DST Offset"),
                (Guid.Parse(BLEConstants.ELEVATION), "Elevation"),
                (Guid.Parse(BLEConstants.EMAIL_ADDRESS), "Email Address"),
                (Guid.Parse(BLEConstants.EXACT_TIME_100), "Exact Time 100"),
                (Guid.Parse(BLEConstants.EXACT_TIME_256), "Exact Time 256"),
                (Guid.Parse(BLEConstants.FAT_BURN_HEART_RATE_LOWER_LIMIT), "Fat Burn Heart Rate Lower Limit"),
                (Guid.Parse(BLEConstants.FAT_BURN_HEART_RATE_UPPER_LIMIT), "Fat Burn Heart Rate Upper Limit"),
                (Guid.Parse(BLEConstants.FIRMWARE_REVISION_STRING), "Firmware Revision String"),
                (Guid.Parse(BLEConstants.FIRST_NAME), "First Name"),
                (Guid.Parse(BLEConstants.FIVE_ZONE_HEART_RATE_LIMITS), "Five Zone Heart Rate Limits"),
                (Guid.Parse(BLEConstants.GENDER), "Gender"),
                (Guid.Parse(BLEConstants.GLUCOSE_FEATURE), "Glucose Feature"),
                (Guid.Parse(BLEConstants.GLUCOSE_MEASUREMENT), "Glucose Measurement"),
                (Guid.Parse(BLEConstants.GLUCOSE_MEASURE_CONTEXT), "Glucose Measure Context"),
                (Guid.Parse(BLEConstants.GUST_FACTOR), "Gust Factor"),
                (Guid.Parse(BLEConstants.HARDWARE_REVISION_STRING), "Hardware Revision String"),
                (Guid.Parse(BLEConstants.HEART_RATE_CONTROL_POINT), "Heart Rate Control Point"),
                (Guid.Parse(BLEConstants.HEART_RATE_MAX), "Heart Rate Max"),
                (Guid.Parse(BLEConstants.HEART_RATE_MEASUREMENT), "Heart Rate Measurement"),
                (Guid.Parse(BLEConstants.HEAT_INDEX), "Heat Index"),
                (Guid.Parse(BLEConstants.HEIGHT), "Height"),
                (Guid.Parse(BLEConstants.HID_CONTROL_POINT), "HID Control Point"),
                (Guid.Parse(BLEConstants.HID_INFORMATION), "HID Information"),
                (Guid.Parse(BLEConstants.HIP_CIRCUMFERENCE), "Hip Circumference"),
                (Guid.Parse(BLEConstants.HUMIDITY), "Humidity"),
                (Guid.Parse(BLEConstants.IEEE_11073_20601_REGULATORY_CERTIFICATION_DATA_LIST), "IEEE 11073-20601 Regulatory Certification Data List"),
                (Guid.Parse(BLEConstants.INTERMEDIATE_CUFF_PRESSURE), "Intermediate Cuff Pressure"),
                (Guid.Parse(BLEConstants.INTERMEDIATE_TEMPERATURE), "Intermediate Temperature"),
                (Guid.Parse(BLEConstants.IRRADIANCE), "Irradiance"),
                (Guid.Parse(BLEConstants.LAST_NAME), "Last Name"),
                (Guid.Parse(BLEConstants.LATITUDE), "Latitude"),
                (Guid.Parse(BLEConstants.LN_CONTROL_POINT), "LN Control Point"),
                (Guid.Parse(BLEConstants.LN_FEATURE), "LN Feature"),
                (Guid.Parse(BLEConstants.LOCAL_TIME_INFORMATION), "Local Time Information"),
                (Guid.Parse(BLEConstants.LOCATION_AND_SPEED), "Location And Speed"),
                (Guid.Parse(BLEConstants.LONGITUDE), "Longitude"),
                (Guid.Parse(BLEConstants.MAXIMUM_RECOMMENDED_HEART_RATE), "Maximum Recommended Heart Rate"),
                (Guid.Parse(BLEConstants.MANUFACTURER_NAME_STRING), "Manufacturer Name String"),
                (Guid.Parse(BLEConstants.MEASUREMENT_INTERVAL), "Measurement Interval"),
                (Guid.Parse(BLEConstants.MODEL_NUMBER_STRING), "Model Number String"),
                (Guid.Parse(BLEConstants.NAVIGATION), "Navigation"),
                (Guid.Parse(BLEConstants.NETWORK_AVAILABILITY), "Network Availability"),
                (Guid.Parse(BLEConstants.NEW_ALERT), "New Alert"),
                (Guid.Parse(BLEConstants.PERIPHERAL_PREFERRED_CONNECTION_PARAMETERS), "Peripheral Preferred Connection Parameters"),
                (Guid.Parse(BLEConstants.PERIPHERAL_PRIVACY_FLAG), "Peripheral Privacy Flag"),
                (Guid.Parse(BLEConstants.PNP_ID), "PnP ID"),
                (Guid.Parse(BLEConstants.POLLEN_CONCENTRATION), "Pollen Concentration"),
                (Guid.Parse(BLEConstants.POSITION_2D), "Position 2D"),
                (Guid.Parse(BLEConstants.POSITION_3D), "Position 3D"),
                (Guid.Parse(BLEConstants.POSITION_QUALITY), "Position Quality"),
                (Guid.Parse(BLEConstants.PRESSURE), "Pressure"),
                (Guid.Parse(BLEConstants.PROTOCOL_MODE), "Protocol Mode"),
                (Guid.Parse(BLEConstants.PULSE_OXIMETRY_CONTINUOUS_MEASUREMENT), "Pulse Oximetry Continuous Measurement"),
                (Guid.Parse(BLEConstants.PULSE_OXIMETRY_CONTROL_POINT), "Pulse Oximetry Control Point"),
                (Guid.Parse(BLEConstants.PULSE_OXIMETRY_FEATURES), "Pulse Oximetry Features"),
                (Guid.Parse(BLEConstants.PULSE_OXIMETRY_PULSATILE_EVENT), "Pulse Oximetry Pulsatile Event"),
                (Guid.Parse(BLEConstants.PULSE_OXIMETRY_SPOT_CHECK_MEASUREMENT), "Pulse Oximetry Spot-Check Measurement"),
                (Guid.Parse(BLEConstants.RAINFALL), "Rainfall"),
                (Guid.Parse(BLEConstants.RECONNECTION_ADDRESS), "Reconnection Address"),
                (Guid.Parse(BLEConstants.RECORD_ACCESS_CONTROL_POINT_TEST_VERSION), "Record Access Control Point (Test Version)"),
                (Guid.Parse(BLEConstants.REDBEARLABS_BISCUIT_BAUDRATE_CHAR_UUID_WRITEWITHOUTRESPONSE), "RedBearLabs Biscuit BAUDRATE_CHAR_UUID WriteWithoutResponse"),
                (Guid.Parse(BLEConstants.REDBEARLABS_BISCUIT_DEV_NAME_CHAR_UUID_READ), "RedBearLabs Biscuit DEV_NAME_CHAR_UUID Read"),
                (Guid.Parse(BLEConstants.REDBEARLABS_BISCUIT_READ), "RedBearLabs Biscuit Read"),
                (Guid.Parse(BLEConstants.REDBEARLABS_BISCUIT_RX_DATA_CHAR_UUID_WRITEWITHOUTRESPONSE), "RedBearLabs Biscuit RX_DATA_CHAR_UUID WriteWithoutResponse"),
                (Guid.Parse(BLEConstants.REDBEARLABS_BISCUIT_TX_DATA_CHAR_UUID_NOTIFY), "RedBearLabs Biscuit TX_DATA_CHAR_UUID Notify"),
                (Guid.Parse(BLEConstants.REDBEARLABS_BISCUIT_TX_POWER_CHAR_UUID_UNKNOWN), "RedBearLabs Biscuit TX_POWER_CHAR_UUID unknown"),
                (Guid.Parse(BLEConstants.REDBEARLABS_BISCUIT_VERSION_CHAR_UUID_UNKNOWN), "RedBearLabs Biscuit VERSION_CHAR_UUID unknown"),
                (Guid.Parse(BLEConstants.REFERENCE_TIME_INFORMATION), "Reference Time Information"),
                (Guid.Parse(BLEConstants.REMOVABLE), "Removable"),
                (Guid.Parse(BLEConstants.REPORT), "Report"),
                (Guid.Parse(BLEConstants.REPORT_MAP), "Report Map"),
                (Guid.Parse(BLEConstants.RESTING_HEART_RATE), "Resting Heart Rate"),
                (Guid.Parse(BLEConstants.RINGER_CONTROL_POINT), "Ringer Control Point"),
                (Guid.Parse(BLEConstants.RINGER_SETTING), "Ringer Setting"),
                (Guid.Parse(BLEConstants.RSC_FEATURE), "RSC Feature"),
                (Guid.Parse(BLEConstants.RSC_MEASUREMENT), "RSC Measurement"),
                (Guid.Parse(BLEConstants.SC_CONTROL_POINT), "SC Control Point"),
                (Guid.Parse(BLEConstants.SCAN_INTERVAL_WINDOW), "Scan Interval Window"),
                (Guid.Parse(BLEConstants.SCAN_REFRESH), "Scan Refresh"),
                (Guid.Parse(BLEConstants.SCIENTIFIC_TEMPERATURE_IN_CELSIUS), "Scientific Temperature in Celsius"),
                (Guid.Parse(BLEConstants.SECONDARY_TIME_ZONE), "Secondary Time Zone"),
                (Guid.Parse(BLEConstants.SENSOR_LOCATION), "Sensor Location"),
                (Guid.Parse(BLEConstants.SERIAL_NUMBER_STRING), "Serial Number String"),
                (Guid.Parse(BLEConstants.SERVICE_CHANGED), "Service Changed"),
                (Guid.Parse(BLEConstants.SERVICE_REQUIRED), "Service Required"),
                (Guid.Parse(BLEConstants.SOFTWARE_REVISION_STRING), "Software Revision String"),
                (Guid.Parse(BLEConstants.SPORT_TYPE_FOR_AEROBIC_AND_ANAEROBIC_THRESHOLDS), "Sport Type for Aerobic and Anaerobic Thresholds"),
                (Guid.Parse(BLEConstants.STRING), "String"),
                (Guid.Parse(BLEConstants.SUPPORTED_NEW_ALERT_CATEGORY), "Supported New Alert Category"),
                (Guid.Parse(BLEConstants.SUPPORTED_UNREAD_ALERT_CATEGORY), "Supported Unread Alert Category"),
                (Guid.Parse(BLEConstants.SYSTEM_ID), "System ID"),
                (Guid.Parse(BLEConstants.TEMPERATURE), "Temperature"),
                (Guid.Parse(BLEConstants.TEMPERATURE_IN_CELSIUS), "Temperature in Celsius"),
                (Guid.Parse(BLEConstants.TEMPERATURE_IN_FAHRENHEIT), "Temperature in Fahrenheit"),
                (Guid.Parse(BLEConstants.TEMPERATURE_MEASUREMENT), "Temperature Measurement"),
                (Guid.Parse(BLEConstants.TEMPERATURE_TYPE), "Temperature Type"),
                (Guid.Parse(BLEConstants.THREE_ZONE_HEART_RATE_LIMITS), "Three Zone Heart Rate Limits"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_ACCELEROMETER_DATA), "TI SensorTag Accelerometer Data"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_ACCELEROMETER_ON_OFF), "TI SensorTag Accelerometer On/Off"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_ACCELEROMETER_SAMPLE_RATE), "TI SensorTag Accelerometer Sample Rate"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_BAROMETER_CALIBRATION), "TI SensorTag Barometer Calibration"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_BAROMETER_DATA), "TI SensorTag Barometer Data"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_BAROMETER_ON_OFF), "TI SensorTag Barometer On/Off"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_BAROMETER_SAMPLE_RATE), "TI SensorTag Barometer Sample Rate"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_CONNECTION_PARAMETERS), "TI SensorTag Connection Parameters"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_CONNECTION_REQUEST_DISCONNECT), "TI SensorTag Connection Request Disconnect"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_CONNECTION_REQUEST_PARAMETERS), "TI SensorTag Connection Request Parameters"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_GYROSCOPE_DATA), "TI SensorTag Gyroscope Data"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_GYROSCOPE_ON_OFF), "TI SensorTag Gyroscope On/Off"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_GYROSCOPE_SAMPLE_RATE), "TI SensorTag Gyroscope Sample Rate"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_HUMIDITY_DATA), "TI SensorTag Humidity Data"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_HUMIDITY_ON_OFF), "TI SensorTag Humidity On/Off"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_HUMIDITY_SAMPLE_RATE), "TI SensorTag Humidity Sample Rate"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_INFRARED_TEMPERATURE_DATA), "TI SensorTag Infrared Temperature Data"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_INFRARED_TEMPERATURE_ON_OFF), "TI SensorTag Infrared Temperature On/Off"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_INFRARED_TEMPERATURE_SAMPLE_RATE), "TI SensorTag Infrared Temperature Sample Rate"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_KEYS_DATA), "TI SensorTag Keys Data"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_MAGNOMETER_DATA), "TI SensorTag Magnometer Data"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_MAGNOMETER_ON_OFF), "TI SensorTag Magnometer On/Off"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_MAGNOMETER_SAMPLE_RATE), "TI SensorTag Magnometer Sample Rate"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_OAD_IMAGE_BLOCK), "TI SensorTag OAD Image Block"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_OAD_IMAGE_IDENTIFY), "TI SensorTag OAD Image Identify"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_TEST_CONFIGURATION), "TI SensorTag Test Configuration"),
                (Guid.Parse(BLEConstants.TI_SENSORTAG_TEST_DATA), "TI SensorTag Test Data"),
                (Guid.Parse(BLEConstants.TIME_ACCURACY), "Time Accuracy"),
                (Guid.Parse(BLEConstants.TIME_BROADCAST), "Time Broadcast"),
                (Guid.Parse(BLEConstants.TIME_SOURCE), "Time Source"),
                (Guid.Parse(BLEConstants.TIME_UPDATE_CONTROL_POINT), "Time Update Control Point"),
                (Guid.Parse(BLEConstants.TIME_UPDATE_STATE), "Time Update State"),
                (Guid.Parse(BLEConstants.TIME_WITH_DST), "Time with DST"),
                (Guid.Parse(BLEConstants.TIME_ZONE), "Time Zone"),
                (Guid.Parse(BLEConstants.TREND), "Trend"),
                (Guid.Parse(BLEConstants.TRUE_WIND_DIRECTION), "True Wind Direction"),
                (Guid.Parse(BLEConstants.TRUE_WIND_SPEED), "True Wind Speed"),
                (Guid.Parse(BLEConstants.TWO_ZONE_HEART_RATE_LIMIT), "Two Zone Heart Rate Limit"),
                (Guid.Parse(BLEConstants.TX_POWER_LEVEL), "Tx Power Level"),
                (Guid.Parse(BLEConstants.UNREAD_ALERT_STATUS), "Unread Alert Status"),
                (Guid.Parse(BLEConstants.USER_CONTROL_POINT), "User Control Point"),
                (Guid.Parse(BLEConstants.USER_INDEX), "User Index"),
                (Guid.Parse(BLEConstants.UV_INDEX), "UV Index"),
                (Guid.Parse(BLEConstants.VO2_MAX), "VO2 Max"),
                (Guid.Parse(BLEConstants.WAIST_CIRCUMFERENCE), "Waist Circumference"),
                (Guid.Parse(BLEConstants.WEIGHT), "Weight"),
                (Guid.Parse(BLEConstants.WEIGHT_MEASUREMENT), "Weight Measurement"),
                (Guid.Parse(BLEConstants.WEIGHT_SCALE_FEATURE), "Weight Scale Feature"),
                (Guid.Parse(BLEConstants.WIND_CHILL), "Wind Chill"),
            }.ToLookup(n => n.UUID, n => n.Name).ToDictionary(n => n.Key, n => n.FirstOrDefault());

        private static IDictionary<Guid, string> CreateDescriptorNames()
            => new List<(Guid UUID, string Name)>
            {
                (Guid.Parse(BLEConstants.CHARACTERISTIC_EXTENDED_PROPERTIES), "Characteristic Extended Properties"),
                (Guid.Parse(BLEConstants.CHARACTERISTIC_USER_DESCRIPTION), "Characteristic User Description"),
                (Guid.Parse(BLEConstants.CLIENT_CHARACTERISTIC_CONFIGURATION), "Client Characteristic Configuration"),
                (Guid.Parse(BLEConstants.SERVER_CHARACTERISTIC_CONFIGURATION), "Server Characteristic Configuration"),
                (Guid.Parse(BLEConstants.CHARACTERISTIC_PRESENTATION_FORMAT), "Characteristic Presentation Format"),
                (Guid.Parse(BLEConstants.CHARACTERISTIC_AGGREGATE_FORMAT), "Characteristic Aggregate Format"),
                (Guid.Parse(BLEConstants.VALID_RANGE), "Valid Range"),
                (Guid.Parse(BLEConstants.EXTERNAL_REPORT_REFERENCE), "External Report Reference"),
                (Guid.Parse(BLEConstants.EXPORT_REFERENCE), "Export Reference")
            }.ToLookup(n => n.UUID, n => n.Name).ToDictionary(n => n.Key, n => n.FirstOrDefault());

        internal static string GetName(this BLEService service)
            => _lazyDescriptorNames.Value.TryGetValue(service.UUID, out var name) ? name : "Custom Service";

        internal static string GetName(this BLECharacteristic characteristic)
            => _lazyDescriptorNames.Value.TryGetValue(characteristic.UUID, out var name) ? name : "Custom Characteristic";

        internal static string GetName(this BLEDescriptor descriptor)
            => _lazyDescriptorNames.Value.TryGetValue(descriptor.UUID, out var name) ? name : "Custom Descriptor";

        private static BLEException ToException(this BLEErrorEventArgs e)
            => new BLEException(e.ErrorCode);

        public static Task StartScanAsync(this BLEAdapter adapter, params Guid[] uuids)
            => TaskUtils.FromEvent<bool, BLEErrorEventArgs>(
                execute: () => adapter.StartScan(uuids),
                getCompleteHandler: complete => (s, e) => complete(true),
                subscribeComplete: handler => adapter.ScanStateChanged += handler,
                unsubscribeComplete: handler => adapter.ScanStateChanged -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => adapter.ScanFailed += handler,
                unsubscribeReject: handler => adapter.ScanFailed -= handler);

        public static async Task ScanAsync(this BLEAdapter adapter, int milliseconds, params Guid[] serviceUuids)
        {
            await adapter.StartScanAsync(serviceUuids);
            await Task.Delay(milliseconds);

            if (!adapter.IsScanning)
                return;

            adapter.StopScan();
        }

        public static Task ConnectAsync(this BLEDevice device)
            => TaskUtils.FromEvent<bool, BLEErrorEventArgs>(
                execute: () => device.Connect(),
                getCompleteHandler: complete => (s, e) => complete(true),
                subscribeComplete: handler => device.Connected += handler,
                unsubscribeComplete: handler => device.Connected -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => device.ConnectFailed += handler,
                unsubscribeReject: handler => device.ConnectFailed -= handler);

        public static Task DisconnectAsync(this BLEDevice device)
            => TaskUtils.FromEvent<bool>(
                execute: () => device.Disconnect(),
                getCompleteHandler: complete => (s, e) => complete(true),
                subscribeComplete: handler => device.Disconnected += handler,
                unsubscribeComplete: handler => device.Disconnected -= handler);

        public static Task<int> ReadRSSIAsync(this BLEDevice device)
            => TaskUtils.FromEvent<int, BLERSSIEventArgs, BLEErrorEventArgs>(
                execute: () => device.ReadRSSI(),
                getCompleteHandler: complete => (s, e) => complete(e.RSSI),
                subscribeComplete: handler => device.RSSIRead += handler,
                unsubscribeComplete: handler => device.RSSIRead -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => device.ReadRSSIFailed += handler,
                unsubscribeReject: handler => device.ReadRSSIFailed -= handler);

        public static Task<IList<BLEService>> DiscoverServicesAsync(this BLEDevice device)
            => TaskUtils.FromEvent<IList<BLEService>, BLEServicesEventArgs, BLEErrorEventArgs>(
                execute: () => device.DiscoverServices(),
                getCompleteHandler: complete => (s, e) => complete(e.Services),
                subscribeComplete: handler => device.ServicesDiscovered += handler,
                unsubscribeComplete: handler => device.ServicesDiscovered -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => device.DiscoverServicesFailed += handler,
                unsubscribeReject: handler => device.DiscoverServicesFailed -= handler);

        public static Task<IList<BLECharacteristic>> DiscoverCharacteristicsAsync(this BLEService service)
            => TaskUtils.FromEvent<IList<BLECharacteristic>, BLECharacteristicsEventArgs, BLEErrorEventArgs>(
                execute: () => service.DiscoverCharacteristics(),
                getCompleteHandler: complete => (s, e) => complete(e.Characteristics),
                subscribeComplete: handler => service.CharacteristicsDiscovered += handler,
                unsubscribeComplete: handler => service.CharacteristicsDiscovered -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => service.DiscoverCharacteristicsFailed += handler,
                unsubscribeReject: handler => service.DiscoverCharacteristicsFailed -= handler);

        public static Task<byte[]> ReadAsync(this BLECharacteristic characteristic)
            => TaskUtils.FromEvent<byte[], BLEErrorEventArgs>(
                execute: () => characteristic.Read(),
                getCompleteHandler: complete => (s, e) => complete(characteristic.Value),
                subscribeComplete: handler => characteristic.ValueChanged += handler,
                unsubscribeComplete: handler => characteristic.ValueChanged -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => characteristic.ReadFailed += handler,
                unsubscribeReject: handler => characteristic.ReadFailed -= handler);

        public static Task WriteAsync(this BLECharacteristic characteristic, byte[] data)
            => TaskUtils.FromEvent<bool, BLEErrorEventArgs>(
                execute: () => characteristic.Write(data),
                getCompleteHandler: complete => (s, e) => complete(true),
                subscribeComplete: handler => characteristic.ValueWritten += handler,
                unsubscribeComplete: handler => characteristic.ValueWritten -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => characteristic.WriteFailed += handler,
                unsubscribeReject: handler => characteristic.WriteFailed -= handler);

        public static Task SetNotificationAsync(this BLECharacteristic characteristic, bool enabled)
            => TaskUtils.FromEvent<bool, BLEErrorEventArgs>(
                execute: () => characteristic.SetNotification(enabled),
                getCompleteHandler: complete => (s, e) => complete(true),
                subscribeComplete: handler => characteristic.NotificationSet += handler,
                unsubscribeComplete: handler => characteristic.NotificationSet -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => characteristic.SetNotificationFailed += handler,
                unsubscribeReject: handler => characteristic.SetNotificationFailed -= handler);

        public static Task<IList<BLEDescriptor>> DiscoverDescriptorsAsync(this BLECharacteristic characteristic)
            => TaskUtils.FromEvent<IList<BLEDescriptor>, BLEDescriptorsEventArgs, BLEErrorEventArgs>(
                execute: () => characteristic.DiscoverDescriptors(),
                getCompleteHandler: complete => (s, e) => complete(e.Descriptors),
                subscribeComplete: handler => characteristic.DescriptorsDiscovered += handler,
                unsubscribeComplete: handler => characteristic.DescriptorsDiscovered -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => characteristic.DiscoverDescriptorsFailed += handler,
                unsubscribeReject: handler => characteristic.DiscoverDescriptorsFailed -= handler);

        public static Task<byte[]> ReadAsync(this BLEDescriptor descriptor)
            => TaskUtils.FromEvent<byte[], BLEErrorEventArgs>(
                execute: () => descriptor.Read(),
                getCompleteHandler: complete => (s, e) => complete(descriptor.Value),
                subscribeComplete: handler => descriptor.ValueChanged += handler,
                unsubscribeComplete: handler => descriptor.ValueChanged -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => descriptor.ReadFailed += handler,
                unsubscribeReject: handler => descriptor.ReadFailed -= handler);

        public static Task WriteAsync(this BLEDescriptor descriptor, byte[] data)
            => TaskUtils.FromEvent<bool, BLEErrorEventArgs>(
                execute: () => descriptor.Write(data),
                getCompleteHandler: complete => (s, e) => complete(true),
                subscribeComplete: handler => descriptor.ValueWritten += handler,
                unsubscribeComplete: handler => descriptor.ValueWritten -= handler,
                getRejectHandler: reject => (s, e) => reject(e.ToException()),
                subscribeReject: handler => descriptor.WriteFailed += handler,
                unsubscribeReject: handler => descriptor.WriteFailed -= handler);
        #endregion
    }
}
