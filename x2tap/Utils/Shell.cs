using System.Diagnostics;

namespace x2tap.Utils
{
    public static class Shell
    {
        /// <summary>
        ///     执行
        /// </summary>
        /// <param name="content">内容</param>
        public static Objects.ShellExitCode Execute(params string[] content)
        {
            var process = new Process();
            process.StartInfo.FileName = content[0];
            process.StartInfo.Arguments = "";
            for (var i = 1; i < content.Length; i++)
            {
                process.StartInfo.Arguments += " \"" + content[i] + "\"";
            }

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();

            process.WaitForExit();
            return new Objects.ShellExitCode
            {
                ExitCode = process.ExitCode,

                Ok = process.ExitCode == 0
            };
        }

        /// <summary>
        ///     执行命令
        /// </summary>
        /// <param name="content">内容</param>
        public static Objects.ShellExitCode ExecuteCommand(params string[] content)
        {
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c \"";
            for (var i = 0; i < content.Length; i++)
            {
                process.StartInfo.Arguments += " " + content[i];
            }

            process.StartInfo.Arguments += "\"";

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();

            process.WaitForExit();
            return new Objects.ShellExitCode
            {
                ExitCode = process.ExitCode,

                Ok = process.ExitCode == 0
            };
        }

        /// <summary>
        ///     执行不等待
        /// </summary>
        /// <param name="content">内容</param>
        public static void ExecuteNoWait(params string[] content)
        {
            var process = new Process();
            process.StartInfo.FileName = content[0];
            process.StartInfo.Arguments = "";
            for (var i = 1; i < content.Length; i++)
            {
                process.StartInfo.Arguments += " \"" + content[i] + "\"";
            }

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
        }

        /// <summary>
        ///     执行命令不等待
        /// </summary>
        /// <param name="content">内容</param>
        public static void ExecuteCommandNoWait(params string[] content)
        {
            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c \"";
            for (var i = 0; i < content.Length; i++)
            {
                process.StartInfo.Arguments += " " + content[i];
            }

            process.StartInfo.Arguments += "\"";

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
        }
    }
}