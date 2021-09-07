using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace AttributeQuartz
{
    /// <summary>
    /// 特性定时任务帮助类
    /// </summary>
    public static class QuartzExtentions
    {
        /// <summary>
        /// 添加特性定时任务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAttributeQuartz(this IServiceCollection services)
        {
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            return services;
        }

        /// <summary>
        /// 启用特性定时任务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="webRoot">站点根目录</param>
        /// <returns></returns>
        public static IApplicationBuilder StartAttributeQuartz(this IApplicationBuilder app, string webRoot)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler ischeduler = schedulerFactory.GetScheduler().Result;

            var assembly = Assembly.GetEntryAssembly().GetTypes().AsEnumerable()
         .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToList();

            var quartzJobs = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();

            assembly.ForEach(r =>
            {
                foreach (var methodInfo in r.GetMethods())
                {
                    foreach (Attribute attribute in methodInfo.GetCustomAttributes())
                    {
                        if (attribute is QuartzTaskAttribute taskAttribute)
                        {
                            var (job, trigger) = CreateActionQuartz(methodInfo, taskAttribute, webRoot);

                            quartzJobs.Add(job, new List<ITrigger>() { trigger });
                        }
                    }
                }
            });

            ischeduler.ScheduleJobs(quartzJobs, true);
            ischeduler.Start();

            return app;
        }

        /// <summary>
        /// 生成定时任务
        /// </summary>
        /// <param name="method">加了特性的方法</param>
        /// <param name="attribute">特性</param>
        /// <param name="webRoot">网站根目录</param>
        /// <returns></returns>
        private static (IJobDetail job, ITrigger trigger) CreateActionQuartz(MethodInfo method, QuartzTaskAttribute attribute, string webRoot)
        {
            IDictionary<string, object> dic = new Dictionary<string, object>() {
                { "obj",new QuartzData{ WebRoot=webRoot, Method=method,Attribute=attribute } }
            };

            IJobDetail job = JobBuilder.Create<QuartzJob>().WithIdentity(method.Name, method.DeclaringType.Name).SetJobData(new JobDataMap(dic)).Build();

            ITrigger trigger = null;

            var builder = TriggerBuilder.Create();

            //判断是否使用Cron表达式
            if (!string.IsNullOrEmpty(attribute.CronExpression))
            {
                builder.WithCronSchedule(attribute.CronExpression);
            }
            else
            {
                builder.WithSimpleSchedule(x =>
                {
                    if (attribute.IsRepeatForever)
                    {
                        x.RepeatForever().WithIntervalInSeconds(attribute.IntervalInSeconds);
                    }
                    else
                    {
                        x.WithRepeatCount(attribute.RepeatCount - 1).WithIntervalInSeconds(attribute.IntervalInSeconds);
                    }
                });
            }


            trigger = builder.Build();

            return (job, trigger);
        }
    }
}
