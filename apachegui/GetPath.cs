using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace apachegui
{
    class GetPath
    {
        public static string onecv832;
        public static string onecv864;
        public static string IBfilepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\1C\\1CEStart\\ibases.v8i";
        public static string system = Environment.GetFolderPath(Environment.SpecialFolder.System);
        public static string ApachePath = $"{Path.GetPathRoot(system)}Apache24";
        public static string ApacheConfP = $@"{ApachePath}\1C";
        internal static void GetPathInstall1c()
        {
            string ProgramFiles64;
            string ProgramFiles32;
            if (Environment.Is64BitOperatingSystem)
            {
                ProgramFiles64 = Environment.Is64BitProcess
                    ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                    : Environment.GetEnvironmentVariable("ProgramW6432");
                ProgramFiles32 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                onecv832 = ProgramFiles32 + "\\1cv8";
                onecv864 = ProgramFiles64 + "\\1cv8";
                if (Directory.Exists(onecv832))
                {
                    Form1.InstallPlatforms32 = Directory.GetDirectories(onecv832, "8.3*");
                    Form1.x32 = true;
                }
                else
                {
                    Form1.x32 = false;
                }
                //
                if (Directory.Exists(onecv864))
                {
                    Form1.InstallPlatforms64 = Directory.GetDirectories(onecv864, "8.3*");
                    Form1.x64 = true;
                }
                else
                {
                    Form1.x64 = false;
                }
            }
            else
            {
                ProgramFiles32 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                onecv832 = ProgramFiles32 + "\\1cv8";
                Form1.InstallPlatforms32 = Directory.GetDirectories(onecv832, "8.3*");
                Form1.x32 = true;
                Form1.x64 = false;
            }
        }
        internal static void GetApachePath()
        {
            if (Directory.Exists(ApachePath))
            {
                if (File.Exists(ApachePath + @"\bin\httpd.exe"))
                {
                    Form1.ApacheInstall = true;
                }
                else
                {
                    Form1.ApacheInstall = false;
                }
            }
            else
            {
                Form1.ApacheInstall = false;
            }
        }
        internal static void GetIbName()//список бд
        {
            INIManager manager = new INIManager(IBfilepath); // объект для работы с ini
            INIManager v8i = new INIManager(IBfilepath); //список баз, поиск реализован на лунксе.
            string[] v8is = v8i.SectionNames(); //получаем список баз
            Form1.IB = v8is.Where(n => !string.IsNullOrEmpty(n)).ToArray(); //удалем пустые значения(?)
        }
        internal static void CheckDBPath(string ibname, ref bool Result, ref int Type, ref string IbPath)
        {
            INIManager v8i = new INIManager(IBfilepath);
            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(ibname);
            var res = Encoding.GetEncoding("windows-1251").GetString(bytes);
            IbPath = v8i.GetPrivateString(res, "Connect");
            var bytes1 = Encoding.GetEncoding("windows-1251").GetBytes(IbPath);
            IbPath = Encoding.GetEncoding("UTF-8").GetString(bytes1);
            bool b = IbPath.Contains("File");
            if (IbPath.Contains("File"))
            {
                Type = 0;
                IbPath = IbPath.Substring(6);
                var re = new Regex('"' + ";");
                IbPath = re.Replace(IbPath, "");
                IbPath = IbPath + "\\1Cv8.1CD";
                if (File.Exists(IbPath))
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
            }
            else if (IbPath.Contains("Srvr"))
            {
                Type = 1;
            }
            else if (IbPath.Contains("ws"))
            {
                Type = 2;
                IbPath = IbPath.Substring(4);
            }
        }
        internal static bool ApacheConfPath()
        {
            if (Directory.Exists(ApacheConfP))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
