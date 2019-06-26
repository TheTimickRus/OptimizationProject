using System;
using System.IO;
using Microsoft.Win32;

namespace OptimiationProject.Classes.Ocher
{
    public static class SaveInFile
    {
        public static void Save(string result, string titleAlg, string filename)
        {
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                CheckPathExists = true,
                InitialDirectory = Environment.CurrentDirectory,
                OverwritePrompt = true,
                Title = $"Сохранение результата ({ titleAlg })",
                ValidateNames = true,
                FileName = $"{ filename }.txt",
                DefaultExt = ".txt",
                Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*"
            };

            if (!(sfd.ShowDialog() ?? false))
            {
                return;
            }

            File.WriteAllText(sfd.FileName, result);
        }
    }
}
