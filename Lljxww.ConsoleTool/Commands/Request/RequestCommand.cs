﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Lljxww.ApiCaller;
using McMaster.Extensions.CommandLineUtils;

namespace Lljxww.ConsoleTool.Commands.Request;

[Command("request", Description = "发送请求操作")]
public class RequestCommand
{
    [Argument(0, Description = "请求的目标，使用\"服务名\".\"方法名\"的形式, 如: weibo.hot")]
    [Required(ErrorMessage = "请指定要调用的目标")]
    public string Target { get; set; }

    [Option("-p|--param", Description = "调用服务时的参数, 使用Json文本形式")]
    public string Param { get; set; }

    private async Task<int> OnExecuteAsync(CommandLineApplication app, IConsole console)
    {
        var path = SystemManager.GetCallerConfigPath();
        if (string.IsNullOrWhiteSpace(path))
        {
            console.Error("请先添加Caller使用的配置文件");
        }

        var caller = new Caller(path!);

        object? paramObj = null;

        if (!string.IsNullOrWhiteSpace(Param))
        {
            try
            {
                paramObj = JsonSerializer.Deserialize<object>(Param);
            }
            catch (Exception ex)
            {
                console.ForegroundColor = ConsoleColor.Red;
                console.WriteLine($"参数错误: {ex.Message}");
                console.ResetColor();
            }
        }

        try
        {
            var result = await caller.InvokeAsync(Target, paramObj);
            console.WriteLine(result.RawStr);

            return 1;
        }
        catch (Exception ex)
        {
            console.Error($"请求失败: {ex.Message}");
            return -1;
        }
    }
}
