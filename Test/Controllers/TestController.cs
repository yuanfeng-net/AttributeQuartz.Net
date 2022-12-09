using AttributeQuartz;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    [ApiController]
    public class TestController : Controller
    {
        /// <summary>
        /// 指定表达式10秒一次
        /// </summary>
        /// <returns></returns>
        [QuartzTask(CronExpression = "/10 * * ? * *")]
        public ActionResult RepeatWith10Second()
        {
            Console.WriteLine("RepeatOn10Second");
            return Ok();
        }

        /// <summary>
        /// 指定表达式
        /// </summary>
        /// <returns></returns>
        [QuartzTask(CronExpression = "00 00 00 * * ?")]
        public ActionResult RepeatWithEveryDay()
        {
            Console.WriteLine("ReSetDayData");
            return Ok();
        }

        /// <summary>
        /// 每隔5秒执行一次
        /// </summary>
        /// <returns></returns>
        [QuartzTask(IsRepeatForever = true, IntervalInSeconds = 5)]
        public ActionResult RepeatWith5Second()
        {
            Console.WriteLine("RepeatWith5Second");
            return Ok();
        }

        /// <summary>
        /// 每隔5秒执行一次,一共只执行3次
        /// </summary>
        /// <returns></returns>
        [QuartzTask(RepeatCount = 3, IntervalInSeconds = 5)]
        public ActionResult RepeatWith5SecondOn3Time()
        {
            Console.WriteLine("RepeatWith5SecondOn3Time");
            return Ok();
        }

    }
}
