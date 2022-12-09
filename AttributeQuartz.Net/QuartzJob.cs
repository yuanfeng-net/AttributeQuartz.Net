using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AttributeQuartz
{
    /// <summary>
    /// QuartzJob
    /// </summary>
    public class QuartzJob : IJob
    {
        /// <summary>
        /// 执行定时任务内容
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            var data= context.JobDetail.JobDataMap.Get("obj") as QuartzData;

            var scope= data.ServiceProvider.CreateScope();

            var controller= scope.ServiceProvider.GetService(data.ControllerType);

            if (controller == null)
            {
                Console.WriteLine($"AttributeQuartz：未正确配置控制器{data.ControllerType.Name}的依赖注入！");
                Debug.WriteLine($"AttributeQuartz：未正确配置控制器{data.ControllerType.Name}的依赖注入！");
                return Task.CompletedTask;
            }

            return Task.Run(() =>
            {
                data.Method.Invoke(controller,null);
            });
        }
    }
}
