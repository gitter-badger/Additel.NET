using System;
using System.Collections.Generic;
using System.Text;

namespace Additel.Forms
{
    public enum Stretch
    {
        /// <summary>
        /// 原始比例
        /// </summary>
        None,
        /// <summary>
        /// 缩放图像以完全填充视图。 X 和 Y 方向的缩放程度可以不统一。
        /// </summary>
        Fill,
        /// <summary>
        /// 缩放图像以适应视图。 某些部分可能会留白（宽屏）。
        /// </summary>
        Uniform,
        /// <summary>
        /// 缩放图像以填充视图。 为填充视图，可能会剪裁某些部分。
        /// </summary>
        UniformToFill
    }
}
