using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Security.Cryptography;

namespace ZFine.Web.Areas.Weixin.Controllers
{
    public class WeixinController : Controller
    {
        //
        // GET: /IOTongWeixin/Weixin/
       public static string appid = ConfigurationManager.AppSettings["Appid"];
       public static string appsecret = ConfigurationManager.AppSettings["Appsecret"];
       public static string token = ConfigurationManager.AppSettings["Token"];
       public static string access_token= "13_SCsBhphh7Vu1Miq2pe0Qs9lL00CeWHIC80lcGm-4HoXbva-37LMzO1PLVi31jOp0aOnYD6tnJVzjlNO83vCpqC4o-eUh7MOUcxz6b7opdcERMC8fgih-ovZ-RC7RmzieDxYa1JpQdGCpz11uHEUeAJAWTA";
       string postStr = "";
        public ActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            //  WXOpera();
            //string httpMethod = Request.HttpMethod.ToLower();
            //if (httpMethod == "post")
            //{
            //    //第一次验证的时候开启
            //    FirstValid();
            //}
            //else
            //{
            //    Valid();  //如果不是post请求就去做开发者验证
            //}



             return  View();

           // return RedirectToAction("Index", "weixinController", new { area = "Weixin" });
           //   return RedirectToAction("index", "weixinController");
        }
        /// <summary>
        /// 验证成为开发者
        /// </summary>
        private void Valid()
        {
            string echoStr = Request.QueryString["echoStr"].ToString();
            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    Response.Write(echoStr);
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 第一次验证配置
        /// </summary>
        private void FirstValid()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            postStr = System.Text.Encoding.UTF8.GetString(b);
            if (!string.IsNullOrEmpty(postStr))
            {
                ResponseMsg(postStr);
            }
        }

        public ActionResult scanQRCode()
        {

            //获取ACCESS_TOKEN
            string _url = Request.Url.ToString();
            //获取Ticket
            string _ticket = Requestjsapi_ticket(Request_Url());
            //获取ticket
            string _finalticket = _ticket;
            //获取noncestr
            string _noncestr = CreatenNonce_str();
            //获取timestamp
            long _timestamp = CreatenTimestamp();
            //获取sinature
            string _sinature = GetSignature(_finalticket, _noncestr, _timestamp, _url).ToLower();

            ViewBag.appid = appid;
            ViewBag.timestamp = _timestamp;
            ViewBag.noncestr = _noncestr;
            ViewBag.sinature = _sinature;
            return View();

        }

        //获取AccessToken
        public static string Request_Url()
        {
            // 设置参数
          //  string _appid = appid ;
          //  string _appsecret = "这里写入你的APPSecrect";
            string _url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + appsecret;
            string method = "GET";
            HttpWebRequest request = WebRequest.Create(_url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            //由于微信服务器返回的JSON串中包含了很多信息，我们只需要将AccessToken获取就可以了，需要将JSON拆分
            String[] str = content.Split('"');
            content = str[3];
            return content;
        }


        //根据AccessToken来获取jsapi_ticket
        public static string Requestjsapi_ticket(string accesstoken)
        {

            string _accesstoken = accesstoken;
            string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + _accesstoken + "&type=jsapi";
            string method = "GET";
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            //同样，返回的JSON中只要取出ticket的信息即可
            String[] str = content.Split('"');
            content = str[9];
            return content;
        }

        //接下来就是辅助工具类，生成随机字符串
        #region 字符串随机 CreatenNonce_str()
        private static string[] strs = new string[]
                                    {
                                    "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                    "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                                    };
        public static string CreatenNonce_str()
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = strs.Length;
            for (int i = 0; i < 15; i++)
            {
                sb.Append(strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }
        #endregion

        //生成时间戳，备用
        #region 时间戳生成 CreatenTimestamp()
        public static long CreatenTimestamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
        #endregion

        //获取签名，这里的三个参数分别为前面生成的ticket，随机字符串以及时间戳
        #region 获取签名 GetSignature()
        public static string GetSignature(string jsapi_ticket, string noncestr, long timestamp, string url)
        {

            var string1Builder = new StringBuilder();
            string1Builder.Append("jsapi_ticket=").Append(jsapi_ticket).Append("&")
                          .Append("noncestr=").Append(noncestr).Append("&")
                          .Append("timestamp=").Append(timestamp).Append("&")
                          .Append("url=").Append(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url);
            return SHA1(string1Builder.ToString());
        }
        #endregion


        //最后就是SHA1的加密算法工具
        #region 加密签名算法 SHA1(content)
        //加密签名算法
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);

        }
        //加密签名
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
        #endregion

        //public ActionResult GetToken()
        //{
        //    Response.Write(GetToken(appid,appsecret));

        //    return View() ;
        //}

        private void ResponseMsg(string weixinXML)
        {
            //回复消息的部分:你的代码写在这里
        }

        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature()
        {
            string signature = Request.QueryString["signature"].ToString();
            string timestamp = Request.QueryString["timestamp"].ToString();
            string nonce = Request.QueryString["nonce"].ToString();
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 获取Token
        /// <summary>
        /// 获取Token
        /// </summary>
        public static string GetToken(string appid, string secret)
        {
            if (TokenExpired(access_token))
            {
                string strJson = RequestUrl(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret));
                access_token = Tools.GetJsonValue(strJson, "access_token");
              //  return Tools.GetJsonValue(strJson, "access_token");
            }
   
                return access_token;
 
        }
        #endregion

        #region 验证Token是否过期
        /// <summary>
        /// 验证Token是否过期
        /// </summary>
        public static bool TokenExpired(string access_token)
        {
            string jsonStr = RequestUrl(string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", access_token));
            if ((Tools.GetJsonValue(jsonStr, "errcode") == "42001") || (Tools.GetJsonValue(jsonStr, "errcode")=="40001"))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 请求Url，不发送数据
        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public static string RequestUrl(string url)
        {
            return RequestUrl(url, "POST");
        }
        #endregion

        #region 请求Url，不发送数据
        /// <summary>
        /// 请求Url，不发送数据
        /// </summary>
        public static string RequestUrl(string url, string method)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = method;
            request.ContentType = "text/html";
            request.Headers.Add("charset", "utf-8");

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
        #endregion

        public class Tools
        {
            #region 获取Json字符串某节点的值
            /// <summary>
            /// 获取Json字符串某节点的值
            /// </summary>
            public static string GetJsonValue(string jsonStr, string key)
            {
                string result = string.Empty;
                if (!string.IsNullOrEmpty(jsonStr))
                {
                    key = "\"" + key.Trim('"') + "\"";
                    int index = jsonStr.IndexOf(key) + key.Length + 1;
                    if (index > key.Length + 1)
                    {
                        //先截逗号，若是最后一个，截“｝”号，取最小值
                        int end = jsonStr.IndexOf(',', index);
                        if (end == -1)
                        {
                            end = jsonStr.IndexOf('}', index);
                        }

                        result = jsonStr.Substring(index, end - index);
                        result = result.Trim(new char[] { '"', ' ', '\'' }); //过滤引号或空格
                    }
                }
                return result;
            }
            #endregion

        }
        public void createMenu()
        {

            FileStream fs1 = new FileStream(Server.MapPath(".") + "\\menu.txt", FileMode.Open);

            StreamReader sr = new StreamReader(fs1, Encoding.GetEncoding("GBK"));
            string menu = sr.ReadToEnd();
            sr.Close();
            fs1.Close();
            GetPage("https://api.weixin.qq.com/cgi-bin/menu/create?access_token="+GetToken(appid, appsecret), menu);


        }

        public string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                Response.Write(content);
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }


        /// <summary>
        /// 微信操作
        /// </summary>
        private void WXOpera()
        {
            wxmessage wx = GetWxMessage();
            string res = "";
            if (!string.IsNullOrEmpty(wx.EventName) && wx.EventName.Trim() == "subscribe")
            {
                //刚关注时的时间，用于欢迎词
                string content = "";
                content = "您好，欢迎关注";
                res = sendTextMessage(wx, content);

                Response.Write(res);
                Response.End();
            }
        }

        /// <summary>
        /// 获取和设置微信类中的信息
        /// </summary>
        /// <returns></returns>
        private wxmessage GetWxMessage()
        {
            wxmessage wx = new wxmessage();
            StreamReader str = new StreamReader(Request.InputStream, Encoding.UTF8);
            XmlDocument xml = new XmlDocument();
            xml.Load(str);
            str.Close();
            str.Dispose();
            wx.ToUserName = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
            wx.FromUserName = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
            wx.MsgType = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;
            if (wx.MsgType.Trim() == "text")
            {
                wx.Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText;
            }
            if (wx.MsgType.Trim() == "event")
            {
                wx.EventName = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
                wx.EventKey = xml.SelectSingleNode("xml").SelectSingleNode("EventKey").InnerText;
            }
            return wx;
        }


        /// 发送文字消息  
        /// </summary>  
        /// <param name="wx" />获取的收发者信息  
        /// <param name="content" />内容  
        /// <returns></returns>  
        private string sendTextMessage(wxmessage wx, string content)
        {
            string res = string.Format(Message_Text,
                wx.FromUserName, wx.ToUserName, DateTime.Now.Ticks, content);
            return res;
        }
        /// <summary>
        /// 普通文本消息
        /// </summary>
        private static string Message_Text
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[text]]></MsgType>
                            <Content><![CDATA[{3}]]></Content>
                            </xml>";
            }
        }

    }
}
