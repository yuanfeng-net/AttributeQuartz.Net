using Quartz;
using System;
using System.Collections.Generic;
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

            return Task.Run(() =>
            {
                using (HttpClient client = new HttpClient())
                {
                    string path = $"/{data.Method.DeclaringType.Name.Replace("Controller", "")}/{data.Method.Name}";
                    //判断是否有指定接口路由地址
                    if (!string.IsNullOrEmpty(data.Attribute.Router))
                    {
                        path = data.Attribute.Router;
                    }
                    //调用接口
                    client.GetAsync($"{data.WebRoot}{path}").Wait();
                }
            });
        }
    }
}
