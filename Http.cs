using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnimmexAPI
{
    /// <summary>
    /// + DoGetAsync(url: string, referer: string, ua: UserAgent = default, cookies: CookieContainer = default, readdata: bool = false): Task<HttpResult>
    ///   -- Asyncronously GETs data from url with referer, useragent, and cookies, and downloads resulting data if necessary.
    /// + DoPostAsync(url: string, referer: string, ua: UserAgent = default, cookies: CookieContainer = default, postdata: FormUrlEncodedContent = default, readdata: bool = true): Task<HttpResult>
    ///   -- Asyncronously POSTs data to url with referer, useragent, and cookies, and downloads resulting data if necessary.
    /// </summary>
    public class Http
    {
        public static async Task<HttpResult> DoGetAsync(string url, string referer, UserAgent ua = default(UserAgent), CookieContainer cookies = default(CookieContainer), bool readdata = true)
        {
            using (var handler = new HttpClientHandler() { UseProxy = false, Proxy = null, CookieContainer = cookies })
            {
                using (var client = new HttpClient(handler))
                {
                    try
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd(ua.ToString());

                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                        requestMessage.Headers.Referrer = new Uri(referer);

                        HttpResponseMessage responseMessage;
                        if (readdata) {
                            responseMessage = await client.GetAsync(url);
                            responseMessage.EnsureSuccessStatusCode();
                            return new HttpResult(string.Empty, responseMessage.RequestMessage.RequestUri.ToString());
                        } else {
                            responseMessage = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                            responseMessage.EnsureSuccessStatusCode();
                            return new HttpResult(await responseMessage.Content.ReadAsStringAsync(), responseMessage.RequestMessage.RequestUri.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public static async Task<HttpResult> DoPostAsync(string url, string referer, UserAgent ua = default(UserAgent), CookieContainer cookies = default(CookieContainer), FormUrlEncodedContent postdata = default(FormUrlEncodedContent), bool readdata = true)
        {
            using (var handler = new HttpClientHandler() { UseProxy = false, Proxy = null, CookieContainer = cookies })
            {
                using (var client = new HttpClient(handler))
                {
                    try
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd(ua.ToString());

                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                        requestMessage.Headers.Referrer = new Uri(referer);
                        requestMessage.Content = postdata;

                        HttpResponseMessage responseMessage;
                        if (readdata)
                        {
                            responseMessage = await client.GetAsync(url);
                            responseMessage.EnsureSuccessStatusCode();
                            return new HttpResult(string.Empty, responseMessage.RequestMessage.RequestUri.ToString());
                        }
                        else {
                            responseMessage = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                            responseMessage.EnsureSuccessStatusCode();
                            return new HttpResult(await responseMessage.Content.ReadAsStringAsync(), responseMessage.RequestMessage.RequestUri.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }

    
}
