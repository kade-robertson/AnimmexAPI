﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Collections.Generic;

namespace AnimmexAPI
{
    public class Http
    {
        /// <summary>
        /// Creates and completes a GET request to a website, with various options.
        /// </summary>
        /// <param name="url">The URL to make the request to.</param>
        /// <param name="referer">The referring URL for the request.</param>
        /// <param name="ua">The user-agent to use as a UserAgent object.</param>
        /// <param name="cookies">The CookieContainer to use if required.</param>
        /// <param name="readdata">If only headers or the final URL are required, do not download all data.</param>
        /// <returns>An HttpResult object containing the data and last visited URL, wrapped in a Task.</returns>
        public static async Task<HttpResult> DoGetAsync(string url, string referer, UserAgent ua = default(UserAgent), CookieContainer cookies = default(CookieContainer), bool readdata = true)
        {
            ua = ua == default(UserAgent) ? UserAgent.Chrome : ua;
            using (var handler = new NativeMessageHandler() { /*UseProxy = false, Proxy = null,*/ UseCookies = true })
            {
                handler.CookieContainer = cookies == default(CookieContainer) ? new CookieContainer() : cookies;
                using (var client = new HttpClient(handler))
                {
                    try
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd(ua.ToString());
                        if (!readdata) client.DefaultRequestHeaders.Range = new System.Net.Http.Headers.RangeHeaderValue(0, 1);

                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                        if (referer != string.Empty) requestMessage.Headers.Referrer = new Uri(referer);

                        HttpResponseMessage responseMessage;
                        if (readdata)
                        {
                            responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);
                            responseMessage.EnsureSuccessStatusCode();
                            return new HttpResult(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), responseMessage.RequestMessage.RequestUri.ToString());
                        }
                        else {
                            responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                            responseMessage.EnsureSuccessStatusCode();
                            return new HttpResult(string.Empty, responseMessage.RequestMessage.RequestUri.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Creates and completes a POST request to a website, with various options.
        /// </summary>
        /// <param name="url">The URL to make the request to.</param>
        /// <param name="referer">The referring URL for the request.</param>
        /// <param name="ua">The user-agent to use as a UserAgent object.</param>
        /// <param name="cookies">The CookieContainer to use if required.</param>
        /// <param name="postdata">The FormUrlEncodedContent that should be POSTed to the URL.</param>
        /// <param name="readdata">If only headers or the final URL are required, do not download all data.</param>
        /// <returns>An HttpResult object containing the data and last visited URL, wrapped in a Task.</returns>
        public static async Task<HttpResult> DoPostAsync(string url, string referer, UserAgent ua = default(UserAgent), CookieContainer cookies = default(CookieContainer), FormUrlEncodedContent postdata = default(FormUrlEncodedContent), bool readdata = true)
        {
            ua = ua == default(UserAgent) ? UserAgent.Chrome : ua;
            postdata = postdata == default(FormUrlEncodedContent) ? new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()) : postdata;
            using (var handler = new NativeMessageHandler() { /*UseProxy = false, Proxy = null,*/ UseCookies = true })
            {
                handler.CookieContainer = cookies == default(CookieContainer) ? new CookieContainer() : cookies;
                using (var client = new HttpClient(handler))
                {
                    try
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd(ua.ToString());
                        if (!readdata) client.DefaultRequestHeaders.Range = new System.Net.Http.Headers.RangeHeaderValue(0, 1);

                        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                        if (referer != string.Empty) requestMessage.Headers.Referrer = new Uri(referer);
                        requestMessage.Content = postdata;

                        HttpResponseMessage responseMessage;
                        if (readdata)
                        {
                            responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);
                            responseMessage.EnsureSuccessStatusCode();
                            return new HttpResult(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false), responseMessage.RequestMessage.RequestUri.ToString() + Environment.NewLine + responseMessage.RequestMessage.Headers.ToString());
                        }
                        else {
                            responseMessage = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                            responseMessage.EnsureSuccessStatusCode();
                            return new HttpResult(string.Empty, responseMessage.RequestMessage.RequestUri.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Get data from a large block between two strings.
        /// </summary>
        /// <param name="data">Data to parse.</param>
        /// <param name="before">String that comes before the desired data.</param>
        /// <param name="after">String that comes after the desired data.</param>
        /// <param name="index">Optional index for splitting.</param>
        /// <returns>Data between the before and after strings.</returns>
        public static string GetBetween(string data, string before, string after, int index = 1)
        {
            return data.Split(new string[] { before }, StringSplitOptions.None)[index].Split(new string[] { after }, StringSplitOptions.None)[0];
        }
    }
}