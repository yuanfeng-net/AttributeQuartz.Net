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
    //如果不手动指定路由参数，则需要使用如下路由格式
    [Route("/[controller]/[action]")]
    public class TestController : Controller
    {
        /// <summary>
        /// 指定路由和表达式
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/TestRouter")]
        [QuartzTask(CronExpression = "/10 * * ? * *", Router = "/api/TestRouter")]
        public ActionResult RepeatWith10Second()
        {
            Console.WriteLine("RepeatOn10Second");
            return Ok();
        }

        /// <summary>
        /// 指定表达式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
        [QuartzTask(RepeatCount = 3, IntervalInSeconds = 5)]
        public ActionResult RepeatWith5SecondOn3Time()
        {
            Console.WriteLine("RepeatWith5SecondOn3Time");
            return Ok();
        }

    }
}
