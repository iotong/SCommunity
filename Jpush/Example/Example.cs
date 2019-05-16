using System;
using Jiguang.JPush;
using Jiguang.JPush.Model;
using System.Collections.Generic;

namespace Example
{
    class Example
    {
  
        private static JPushClient client = new JPushClient("AppKey7ff02881ab0d5d86dd17d2ce", "2d537a9e6b17461479a93351");

        public static void Main(string[] args)
        {
            ExecutePushExample("1","数据更新");
            ExecuteDeviceEample();
            ExecuteReportExample();
            ExecuteScheduleExample();

            Console.ReadLine();
        }

        public static void ExecutePushExample(string F_Id,string Centent)
        {
            PushPayload pushPayload = new PushPayload()
            {
                Platform = new List<string> { "android", "ios" },
                Audience = "all",
                Notification = new Notification
                {
                    Alert = "hello jpush",
                    Android = new Android
                    {
                        Alert = "android alert",
                        Title = "title"
                    },
                    IOS = new IOS
                    {
                        Alert = "ios alert",
                        Badge = "+1"
                    }
                },
                Message = new Message
                {
                    Title = "message title",
                    msg_content = "message content",
                    Extras = new Dictionary<string, string>
                    {
                        ["key1"] = "value1"
                    }
                },
                Options = new Options
                {
                    IsApnsProduction = true // 设置 iOS 推送生产环境。不设置默认为开发环境。
                }
            };
            var response = client.SendPush(pushPayload);
            Console.WriteLine(response.Content);
        }

        private static void ExecuteDeviceEample()
        {
            var registrationId = "12145125123151";
            var devicePayload = new DevicePayload
            {
                Alias = "alias1",
                Mobile = "12300000000",
                Tags = new Dictionary<string, object>
                {
                    { "add", new List<string>() { "tag1", "tag2" } },
                    { "remove", new List<string>() { "tag3", "tag4" } }
                }
            };
            var response = client.Device.UpdateDeviceInfo(registrationId, devicePayload);
            Console.WriteLine(response.Content);
        }

        private static void ExecuteReportExample()
        {
            var response = client.Report.GetMessageReport(new List<string> { "1251231231" });
            Console.WriteLine(response.Content);
        }

        private static void ExecuteScheduleExample()
        {
            var pushPayload = new PushPayload
            {
                Platform = "all",
                Notification = new Notification
                {
                    Alert = "Hello JPush"
                }
            };
            var trigger = new Trigger
            {
                StartDate = "2017-08-03 12:00:00",
                EndDate = "2017-12-30 12:00:00",
                TriggerTime = "12:00:00",
                TimeUnit = "week",
                Frequency = 2,
                TimeList = new List<string>
                {
                    "wed", "fri"
                }
            };
            var response = client.Schedule.CreatePeriodicalScheduleTask("task1", pushPayload, trigger);
            Console.WriteLine(response.Content);
        }
    }
}