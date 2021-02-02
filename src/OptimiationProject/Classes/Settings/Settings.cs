using Newtonsoft.Json;
using OptimiationProject.Classes.Dialogs;
using OptimiationProject.Models;
using OptimiationProject.Windows.Graph;
using OptimiationProject.Windows.Graph.Model;
using System;
using System.IO;

namespace OptimiationProject.Classes.Settings
{
    public class Settings
    {
        private static readonly Settings MySettings = new Settings();
        public static Settings GetInstanse()
        {
            return MySettings;
        }

        public static MainWindow MainWindow { get; set; }
        public static GraphWindow GraphWindow { get; set; }
        

        //=================================================================

        public void Save(string settingsFile = "Settings.json")
        {
            try
            {
                settingsFile = $"{Environment.CurrentDirectory}\\{settingsFile}";

                if (File.Exists(settingsFile))
                {
                    File.Delete(settingsFile);
                }

                File.WriteAllText(settingsFile, JsonConvert.SerializeObject(MainSettings.Instanse, Formatting.Indented));
            }
            catch (Exception ex)
            {
                MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
            }
        }

        public void Load(string settingsFile = "Settings.json")
        {
            try
            {
                settingsFile = $"{Environment.CurrentDirectory}\\{settingsFile}";

                if (!File.Exists(settingsFile))
                {
                    throw new Exception($"Сохраненные настройки не найдены!");
                }

                MainSettings.Instanse = JsonConvert.DeserializeObject<MainSettings>(File.ReadAllText(settingsFile));
            }
            catch (Exception ex)
            {
                MainSettings.Instanse = new MainSettings();
                MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
            }
            finally
            {
                MainModel.Instanse = MainSettings.Instanse.MyMainModel ?? new MainModel();
                GraphModel.Instanse = MainSettings.Instanse.MyGraphModel ?? new GraphModel();

                SwannModel.Instanse = MainSettings.Instanse.MySwannModel ?? new SwannModel();

                DichotomiesModel.Instanse = MainSettings.Instanse.MyDichotomiesModel ?? new DichotomiesModel();
                GoldenSelectionModel.Instanse = MainSettings.Instanse.MyGoldenSelectionModel ?? new GoldenSelectionModel();
                ParabolasModel.Instanse = MainSettings.Instanse.MyParabolasModel ?? new ParabolasModel();
            }
        }
    }
}
