using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AttributeQuartz
{
    /// <summary>
    /// 任务参数
    /// </summary>
    public class QuartzData
    {
        /// <summary>
        /// 方法参数
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// 服务类
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 控制器类型
        /// </summary>
        public Type ControllerType { get; set; }

        /// <summary>
        /// 特性参数
        /// </summary>
        public QuartzTaskAttribute Attribute { get; set; }
    }
}
