# .net core特性定时任务

#### 介绍
这是一款只需要在控制器中的Action中加上特性即可实现定时任务的类库，极大的简化了Quartz的使用流程

#### 安装教程

1、将类库引用到项目中

2、在 appsettings.json 中配置网站根地址 WebRootUrl 

3、在 Startup.cs 中的 ConfigureServices 方法里调用 services.AddAttributeQuartz(); 添加特性定时服务

4、在 Startup.cs 中的 Configure 方法里调用 app.StartAttributeQuartz(Configuration["WebRootUrl"]); 来启动特性定时任务


#### 使用说明

1、完成安装操作之后，只需要在需要执行定时任务的Action上加入特性 QuartzTask 就可以实现定时任务了

#### 注意事项

1、如果不指定Router参数，则需要Controller路由配置为  **[Route("/[controller]/[action]")]** 

2、如果指定CronExpression参数，则不需要指定IntervalInSeconds、IsRepeatForever、RepeatCount参数

3、此版本暂不支持调用需要权限验证的接口，如果需要调用权限验证，请自行下载源码，在请求处添加权限请求头即可，或者加上AllowAnonymous特效

#### 参与贡献

QQ:279202647
作者：Core园主

#### 转载请注明出处，以后会开源更多大家喜欢的项目。
