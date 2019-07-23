using System;
using System.Text;

namespace Additel.Core
{
    /// <summary>
    /// <see cref="ArrayExtensions"/> 扩展类
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 使用特定编码方式将 Byte 数组转换为字符串
        /// </summary>
        /// <param name="bytes">需要转换的 Byte 数组</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>转换后的字符串结果</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DecoderFallbackException"></exception>
        public static string GetString(this byte[] bytes, Encoding encoding)
            => encoding.GetString(bytes);

        /// <summary>
        /// 使用 UTF8 编码方式将 Byte 数组转换为字符串
        /// </summary>
        /// <param name="bytes">需要转换的 Byte 数组</param>
        /// <returns>转换后的字符串结果</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DecoderFallbackException"></exception>
        public static string GetString(this byte[] bytes)
            => bytes.GetString(Encoding.UTF8);

        /// <summary>
        /// 将 Byte 数组转换为十六进制字符串
        /// </summary>
        /// <param name="value">需要转换的 Byte 数组</param>
        /// <returns>转换后的十六进制字符串结果</returns>
        public static string GetHexString(this byte[] value)
            => value != null ? BitConverter.ToString(value) : null;
    }
}
