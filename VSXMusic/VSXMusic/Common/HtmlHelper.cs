﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;
using VSXMusic;
using System.ComponentModel.Composition;

namespace VSXMusic.Common
{
    /// <summary>
    /// 网络连接基础类
    /// </summary>
    [Export(typeof(Common.HtmlHelper))]
    public class HtmlHelper
    {
        /// <summary>
        /// Cookie
        /// </summary>
        public static CookieContainer Cookie;
        /// <summary>
        /// 默认HTTP头：UserAgent
        /// </summary>
        public static string DefaultUserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        /// <summary>
        /// 默认HTTP头：accept
        /// </summary>
        public static string DefaultAccept = "text/html, application/xhtml+xml, */*";
        /// <summary>
        /// 默认HTTP头：contentType
        /// </summary>
        public static string DefaultContentType = "application/x-www-form-urlencoded";
        /// <summary>
        /// 默认编码
        /// </summary>
        public static Encoding DefaultEncoding = Encoding.UTF8;
        /// <summary>
        /// 你懂的……
        /// </summary>
        public string UserAgent, Accept, ContentType;
        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding;
        /// <summary>
        /// 是否抛出异常
        /// </summary>
        public bool ThrowException;

        static HtmlHelper()
        {
            if (!LoadCookies()) ClearCookie();
        }

        public HtmlHelper(bool throwException, string userAgent, string accept, string contentType, Encoding encoding)
        {
            ThrowException = throwException;
            UserAgent = userAgent;
            Accept = accept;
            ContentType = contentType;
            Encoding = encoding;
        }

        public HtmlHelper() : this(false)
        {

        }

        public HtmlHelper(bool throwException)
            : this(DefaultEncoding, throwException)
        {
        }

        public HtmlHelper(Encoding encoding, bool throwException)
            : this(throwException, DefaultUserAgent, DefaultAccept, DefaultContentType, encoding)
        {
        }
        /// <summary>
        /// 用Post方法发送请求
        /// </summary>
        /// <param name="postUri">请求的地址</param>
        /// <param name="accept">Accept头</param>
        /// <param name="referer">Referer头</param>
        /// <param name="contentType">ContentType头</param>
        /// <param name="content">请求正文</param>
        /// <returns>
        /// 响应正文
        /// </returns>
        public string Post(string postUri, string accept, string referer, string contentType, byte[] content)
        {
            string file = null;

            try
            {
                HttpWebRequest request = WebRequest.Create(postUri) as HttpWebRequest;
                request.Accept = accept;
                request.AllowAutoRedirect = true;
                request.ContentLength = content.Length;
                request.ContentType = contentType;
                request.CookieContainer = Cookie;
                request.KeepAlive = true;
                request.Method = "POST";
                request.Referer = referer;
                request.UserAgent = UserAgent;
                request.ServicePoint.Expect100Continue = false;
                using (Stream requestStream = request.GetRequestStream())
                    requestStream.Write(content, 0, content.Length);
                using (HttpWebResponse responce = request.GetResponse() as HttpWebResponse)
                using (StreamReader sr = new StreamReader(responce.GetResponseStream(), Encoding))
                    file = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                if (ThrowException)
                    throw;
            }

            return file;
        }
        /// <summary>
        /// 用Post方法发送请求
        /// </summary>
        /// <param name="postUri">请求的地址</param>
        /// <param name="content">请求正文</param>
        /// <returns>响应正文</returns>
        public string Post(string postUri, byte[] content)
        {
            return Post(postUri, null, content);
        }
        /// <summary>
        /// 用Post方法发送请求
        /// </summary>
        /// <param name="postUri">请求的地址</param>
        /// <param name="referer">Referer头</param>
        /// <param name="content">请求正文</param>
        /// <returns>响应正文</returns>
        public string Post(string postUri, string referer, byte[] content)
        {
            return Post(postUri, Accept, referer, ContentType, content);
        }
        /// <summary>
        /// 用Get方法发送请求
        /// </summary>
        /// <param name="getUri">请求的地址</param>
        /// <param name="accept">Accept头</param>
        /// <param name="referer">Referer头</param>
        /// <returns>
        /// 响应正文
        /// </returns>
        public string Get(string getUri, string accept, string referer)
        {
            string file = null;

            try
            {
                HttpWebRequest request = WebRequest.Create(getUri) as HttpWebRequest;
                request.Accept = accept;
                request.AllowAutoRedirect = true;
                request.CookieContainer = Cookie;
                request.KeepAlive = true;
                request.Method = "GET";
                request.Referer = referer;
                request.UserAgent = UserAgent;
                using (HttpWebResponse responce = request.GetResponse() as HttpWebResponse)
                using (StreamReader sr = new StreamReader(responce.GetResponseStream(), Encoding))
                    file = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                if (ThrowException)
                    throw;
            }

            return file;
        }
        /// <summary>
        /// 用Get方法发送请求
        /// </summary>
        /// <param name="getUri">请求的地址</param>
        /// <returns>响应正文</returns>
        public string Get(string getUri)
        {
            return Get(getUri, Accept, null);
        }

        /// <summary>
        /// 用Get方法发送请求
        /// </summary>
        /// <param name="getUri">请求的地址</param>
        /// <param name="referer">Referer头</param>
        /// <returns>响应正文</returns>
        public string Get(string getUri, string referer)
        {
            return Get(getUri, Accept, referer);
        }
        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <returns>成功与否</returns>
        public static bool LoadCookies()
        {
            try
            {
                Cookie = BinarySerializeHelper.Deserialize<CookieContainer>(Paths.CookiesFile);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 保存Cookies
        /// </summary>
        /// <returns>成功与否</returns>
        public static bool SaveCookies()
        {
            try
            {
                BinarySerializeHelper.Serialize(Paths.CookiesFile, Cookie);
            }

            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 根据请求的URL和参数构造一个新的URL
        /// </summary>
        /// <param name="baseUrl">请求URL</param>
        /// <param name="parameters">参数</param>
        /// <returns>新的URL</returns>
        public static string ConstructUrlWithParameters(string baseUrl, Parameters parameters)
        {
            if (parameters == null || parameters.Count() == 0)
                return baseUrl;
            return baseUrl + "?" + parameters;
        }

        /// <summary>
        /// 清除Cookie
        /// </summary>
        public static void ClearCookie()
        {
            Cookie = new CookieContainer(1000, 1000, 100000);
        }

        /// <summary>
        /// 设置代理服务器
        /// </summary>
        /// <param name="host">主机</param>
        /// <param name="port">端口</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public static void SetProxy(string host, int port, string username = null, string password = null)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentException("主机不能为空", "host");
            if (string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                throw new ArgumentException("填写密码后用户名不能为空", "username");
            if (string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(username))
                throw new ArgumentException("填写用户名后密码不能为空", "password");
            WebRequest.DefaultWebProxy = new WebProxy(host, port);
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                WebRequest.DefaultWebProxy.Credentials = new NetworkCredential(username, password);
            }
        }
        /// <summary>
        /// 使用默认代理
        /// </summary>
        public static void UseDefaultProxy()
        {
            WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
        }

        /// <summary>
        /// 不使用任何代理服务器设置
        /// </summary>
        public static void DontUseProxy()
        {
            WebRequest.DefaultWebProxy = null;
        }
    }
}