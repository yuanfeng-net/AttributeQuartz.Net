using System;
using System.Collections.Generic;
using System.Text;

namespace AttributeQuartz
{
    /// <summary>
    /// 定时任务特性
    /// </summary>
    public class QuartzTaskAttribute:Attribute
    {
        /// <summary>
        /// 定时任务参数
        /// </summary>
        public string CronExpression { get; set; }

        /// <summary>
        /// 定时任务执行路由
        /// </summary>
        public string Router { get; set; }

        /// <summary>
        /// 是否重复
        /// </summary>
        public bool IsRepeatForever { get; set; }

        /// <summary>
        /// 执行间隔
        /// </summary>
        public int IntervalInSeconds { get; set; }

        private int _RepeatCount;

        /// <summary>
        /// 重复次数
        /// </summary>
        public int RepeatCount
        {
            get { return _RepeatCount; }
            set {
                if (value <= 0)
                {
                    //最少需要执行一次
                    _RepeatCount = 1;
                }
                else
                {
                    _RepeatCount = value;
                }
            }
        }

    }

}
