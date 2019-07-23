using System;

namespace Additel.Core
{
    /// <summary>
    /// <see cref="NotImplementedInReferenceAssemblyException"/> 类
    /// </summary>
    public class NotImplementedInReferenceAssemblyException : NotImplementedException
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public NotImplementedInReferenceAssemblyException()
            : base("目标平台中引用的部分程序集未实现此功能，需要在目标平台中引用本程序集在此平台下的特定实现部分程序集")
        {
        }
    }
}
