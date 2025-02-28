﻿using Lljxww.ConsoleTool.Utils;
using McMaster.Extensions.CommandLineUtils;

namespace Lljxww.ConsoleTool.Commands.Config;

[Command("cleanup", Description = "删除未使用的配置文件")]
internal class CleanupSubCommand
{
    private void OnExecute(IConsole console)
    {
        if (DbModelUtil.Instance.CallerConfigInfos!.Count != 0)
        {
            DbModelUtil.UpdateDbModel(instance =>
            {
                CallerConfigInfo info = instance.CallerConfigInfos.Single(i => i.Active);
                instance.CallerConfigInfos = [
                        info
                ];
                return instance;
            });
        }

        SystemManager.Cleanup();

        console.Success("清理完成");
    }
}
