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

#### 参与贡献

QQ:279202647
作者：Core园主

#### 转载请注明出处，以后会开源更多大家喜欢的项目。
