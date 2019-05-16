using Beyova.JPush;
using Beyova.JPush.V3;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace ZFine.Web.App_Start
{
    public class JPushHelper
    {
        #region fields
        private static JPushClientV3 jpushClient;
        private static JPushInfo jpushInfo = null;
        #endregion
        #region ctor
        static JPushHelper()
        {
            jpushInfo = new JPushInfo
            {
                appKey = "7ff02881ab0d5d86dd17d2ce",
              
                masterSecret = "2d537a9e6b17461479a93351"
            };

        }
        private static void Init()
        {
            jpushClient = new JPushClientV3(jpushInfo.appKey, jpushInfo.masterSecret);
        }
        #endregion

        #region methods
        /// <summary>
        ///     根据多个指定 注册ID推送
        ///          [ "4312kjklfds2", "8914afd2", "45fdsa31" ]
        /// </summary>
        /// <param name="registrationId">极光官网注册ID数组</param>
        /// <param name="notification"></param>
        /// <param name="customizedValues"></param>
        /// <returns></returns>
        public static string DoPushByRegistrationId(
            List<string> registrationIds,
            NotificationInfo notification)
        {
            if (jpushClient == null)
                Init();

            Audience audience = new Audience();
            audience.Add(PushTypeV3.ByRegistrationId, registrationIds);

            var response = jpushClient.SendPushMessage(new PushMessageRequestV3
            {
                Audience = audience,
                Platform = PushPlatform.AndroidAndiOS,
                IsTestEnvironment = true,
                AppMessage = new AppMessage
                {
                    Content = notification.Alert,
                    CustomizedValue = notification.CustomizedValues,
                    Title = notification.Title
                },
                Notification = new Notification
                {
                    AndroidNotification = new AndroidNotificationParameters
                    {
                        Title = notification.Title,
                        Alert = notification.Alert,
                        CustomizedValues = notification.CustomizedValues
                    },
                    iOSNotification = new iOSNotificationParameters
                    {
                        Badge = 1,
                        Alert = notification.Alert,
                        //Sound = "YourSound",//default
                        CustomizedValues = notification.CustomizedValues
                    }
                }
            });
            var res = response.ResponseCode.ToString();
            if (res == "Succeed")
            {
                //发送成功
                List<string> idToCheck = new List<string>();
                idToCheck.Add(response.MessageId);

                var statusList = jpushClient.QueryPushMessageStatus(idToCheck);
                if (statusList != null)
                {
                    //foreach (var one in statusList)
                    //{
                    //    Console.WriteLine(string.Format("Id: {0}, Android: {1}, iOS: {2}", one.MessageId, one.AndroidDeliveredCount, one.ApplePushNotificationDeliveredCount));
                    //}

                    return "推送成功";
                }
                return "推送成功";
            }
            return "推送失败";
        }

        /// <summary>
        ///     根据多个别名推送
        ///         [ "4314", "892", "4531" ]
        /// </summary>
        /// <param name="alias">别名数组</param>
        /// <param name="notification"></param>
        /// <param name="customizedValues"></param>
        /// <returns></returns>
        public static string DoPushByAlias(
            List<string> alias,
            NotificationInfo notification)
        {
            if (jpushClient == null)
                Init();

            Audience audience = new Audience();
            audience.Add(PushTypeV3.ByTagWithinOr, alias);
            audience.Add(PushTypeV3.ByAlias, alias);

            var response = jpushClient.SendPushMessage(new PushMessageRequestV3
            {
                Audience = audience,
                Platform = PushPlatform.All,
                AppMessage = new AppMessage
                {
                    ContentType = notification.Centent_type.ToString(),
                    Content = notification.Centent,
                    CustomizedValue = notification.CustomizedValues,
                    Title = notification.Title
                },
                Notification = new Notification
                {
                  
                    AndroidNotification = new AndroidNotificationParameters
                    {
                        Title = notification.Title,
                        Alert = notification.Alert,
                       
                        CustomizedValues = notification.CustomizedValues
                    }
                },
                LifeTime = 86400,
                IsTestEnvironment = true
            });
            var res = response.ResponseCode.ToString();
            if (res == "Succeed")
            {
                //发送成功
                List<string> idToCheck = new List<string>();
                int parseVal;
                if (Int32.TryParse(response.MessageId, out parseVal))
                {
                    idToCheck.Add(response.MessageId);
                }
                else
                {
                    return "推送成功";
                }

                var statusList = jpushClient.QueryPushMessageStatus(idToCheck);
                if (statusList != null)
                {
                    //foreach (var one in statusList)
                    //{
                    //    Console.WriteLine(string.Format("Id: {0}, Android: {1}, iOS: {2}", one.MessageId, one.AndroidDeliveredCount, one.ApplePushNotificationDeliveredCount));
                    //}
                    return "推送成功";
                }
                return "推送成功";
            }
            return "推送失败";
        }

        #endregion

        public int Send(List<string> list,string Centent_type,string Centent,string Title, Dictionary<string, object> customizedValues)
        {

            var appKey = "7ff02881ab0d5d86dd17d2ce";
            var masterSecret = "2d537a9e6b17461479a93351";

           


            JPushClientV3 client = new JPushClientV3(appKey, masterSecret);
            Audience audience = new Audience();

            //4. 设置别名发送
            audience.Add(PushTypeV3.ByAlias, new List<string>(new string[] { "111111" }));


            Notification notification = new Notification
            {
                AndroidNotification = new AndroidNotificationParameters
                {
                    Title = Title,
                    Alert = "JPush V2 would be retired soon."
                    // CustomizedValues = customizedValues
                }
            };

            try
            {
                var response = client.SendPushMessage(new PushMessageRequestV3
                {
                    Audience = audience,
                    Platform = PushPlatform.AndroidAndiOS,
                    IsTestEnvironment = true,
                    AppMessage = new AppMessage
                    {
                        Content = "Centent",
                        ContentType= Centent_type,
                       CustomizedValue = customizedValues
                    },
                    Notification = notification
                });

                return 1;
            }
            catch (Exception e)
            {

                return 0;
            }



        }
    }


    /// <summary>
    ///     提示信息
    /// </summary>
    public class NotificationInfo
    {
        public string Title { get; set; }
        public string Alert { get; set; }
        public string Centent { get; set; }
        public int Centent_type { get; set; }
        public Dictionary<string, object> CustomizedValues { get; set; }
        //customizedValues.Add("CK1", "CV1");
        //    customizedValues.Add("CK2", "CV2");
        
    }

    /// <summary>
    ///     极光appKey和secret
    /// </summary>
    public class JPushInfo
    {
        public string appKey { get; set; }
        public string masterSecret { get; set; }
    }

}
