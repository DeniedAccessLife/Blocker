using System;
using System.IO;
using Microsoft.Win32;

namespace Blocker
{
    class Host
    {
        public static string file;

        public static void Initialization()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\");
            string value = key.GetValue("DataBasePath").ToString();
            key.Close();

            file = Environment.ExpandEnvironmentVariables(value + @"\hosts").ToLower();

            File.Copy(file, file + ".back", true);
            File.SetAttributes(file, File.GetAttributes(file) & ~FileAttributes.ReadOnly);
        }

        public static void Add(string data)
        {
            string text = string.Empty;
            using (StreamReader reader = new StreamReader(file))
            {
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    text = reader.ReadToEnd();

                    if (!text.Contains(data))
                    {
                        writer.WriteLine("127.0.0.1 " + data);
                    }

                    writer.Close();
                    reader.Close();
                }
            }
        }

        public static void Rem(string data)
        {
            string line = string.Empty;
            using (StreamReader reader = new StreamReader(file))
            {
                using (StreamWriter writer = new StreamWriter(file + ".temp"))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && line != "127.0.0.1 " + data)
                        {
                            writer.WriteLine(line);
                        }
                    }

                    writer.Close();
                    reader.Close();

                    File.Delete(file);
                    File.Move(file + ".temp", file);
                }
            }
        }
    }
}
