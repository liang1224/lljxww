﻿using Lljxww.ApiCaller.Models.Context;

namespace Lljxww.ApiCaller;

internal static class HttpClientInstance
{
    private static HttpClient? client;

    public static HttpClient Get(CallerContext context)
    {
        client ??= new HttpClient();

        client.DefaultRequestHeaders.Add("User-Agent", CallerOption.UserAgent);
        client.DefaultRequestHeaders.Connection.Add("keep-alive");

        return client;
    }
}