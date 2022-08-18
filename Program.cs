// See https://aka.ms/new-console-template for more information
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;


namespace XCOM_Enemy_Speak_English
{
    internal class Program
    {
        static Dictionary<string, string> lng = new();
        [STAThread]
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ChoiceLngThis();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t" + lng["NoWar"]);
            Console.WriteLine();
            Console.WriteLine();
            System.Threading.Thread.Sleep(3000);

            string? radPach = null;
            string gamePach = string.Empty;


        retry:
            Console.Clear();
            Console.WriteLine(lng["Title"]);
            Console.WriteLine();

            if (string.IsNullOrEmpty(gamePach))
                gamePach = FindGameFolder();
            Console.WriteLine(lng["GameFoundDir"].Replace("%DirName%", gamePach));

            if (string.IsNullOrEmpty(radPach))
                radPach = FindRadToolsFolder();

            if (string.IsNullOrEmpty(radPach))
                Console.WriteLine(lng["BinkNotFond"]);
            else
                Console.WriteLine(lng["BinkFondInDir"].Replace("%DirName%", radPach));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine(lng["ProgramManagement"]);
            Console.WriteLine();
            Console.WriteLine("1 - " + lng["ChangeVoiceToEng"]);
            Console.WriteLine("2 - " + lng["DirGameChange"]);
            Console.WriteLine("3 - " + lng["BinkSelectDir"]);
            Console.WriteLine();
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(lng["RemoveDirVoice"]);
                    EngSound(gamePach);

                    if (!string.IsNullOrEmpty(radPach))
                    {
                        Console.Write(lng["ChangeLngVideo"] + " ");
                        EngVideo(gamePach, radPach);
                        Console.WriteLine(lng["ChangeLngVideoEnd"]);
                    }
                    break;
                case "2":
                    FindGameFolderDLG();
                    goto retry;
                case "3":
                    string? radTmp = FindRadToolsFolderDLG();
                    if (radTmp != null)
                        radPach = radTmp;
                    goto retry;
                default:
                    goto retry;
            }

            Console.WriteLine(lng["End"]);
        }

        static void ChoiceLngThis()
        {
            Console.WriteLine("\tSelect language.");
            Console.WriteLine();
            Console.WriteLine("\tОберіть мову. Введіть її дволітерний символ і натисніть клавішу Enter.");
            Console.WriteLine();
            Console.WriteLine("\tWählen Sie die Sprache. Geben Sie zwei Buchstaben der Sprache ein und drücken Sie die Eingabetaste.");
            Console.WriteLine();
            Console.WriteLine("\tВыберите язык. Введите его двухбуквенный символ и нажмите клавишу Enter.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("UK - Українська.");
            Console.WriteLine("DE - Deutsch");
            Console.WriteLine("RU - Русский");

            string lngFileName = string.Empty;

        retryLNG:
            switch (Console.ReadLine()?.ToLower())
            {
                case "1":
                case "ua":
                case "uk":
                case "ukr":
                    lngFileName = "uk.json";
                    break;

                case "2":
                case "de":
                case "deu":
                case "ger":
                    lngFileName = "de.json";
                    break;

                case "3":
                case "ru":
                case "rus":
                    lngFileName = "ru.json";
                    break;

                default:
                    goto retryLNG;
            }


            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(lngFileName));

            using Stream? stream = assembly.GetManifestResourceStream(resourceName);
            lng = JsonSerializer.Deserialize<Dictionary<string, string>>(stream, new System.Text.Json.JsonSerializerOptions { ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip, WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            Console.Clear();
        }


        static void EngSound(string gameDir)
        {
            string[] localizNames = new string[] { "DEU", "ESN", "FRA", "ITA", "POL", "RUS" };
            foreach (string localizName in localizNames)
            {
                string dir = Path.Combine(gameDir, "XComGame", "CookedPCConsole", localizName);
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);

                string dirX = Path.Combine(gameDir, "XEW", "XComGame", "CookedPCConsole", localizName);
                if (Directory.Exists(dirX))
                    Directory.Delete(dirX, true);
            }
        }

        static void EngVideo(string gameDir, string radVideoDir)
        {
            string[] movieListClassic = { "1080_BattleOver_LOC.bik", "1080_CIN_Intro_Movie_LOC.bik", "1080_CIN_TP01A_ArcThrower_LOC.bik", "1080_CIN_TP01B_PostInterrogation_LOC.bik", "1080_CIN_TP01_1stUFOShotDown_LOC.bik", "1080_CIN_TP02_Terror_LOC.bik", "1080_CIN_TP03_CodeRevealed_LOC.bik", "1080_CIN_TP04_AlienPact_LOC.bik", "1080_CIN_TP05_AlienBaseDetected_LOC.bik", "1080_CIN_TP06A_Victory_LOC.bik", "1080_CIN_TP06_HyperwaveRetrieved_LOC.bik", "1080_CIN_TP07_HyperwaveUplink_LOC.bik", "1080_CIN_TP08_Firestorm_LOC.bik", "1080_CIN_TP10_DeviceRetrieved_LOC.bik", "1080_CIN_TP11_Part02_LOC.bik", "1080_CIN_TP13_Part03_LOC.bik", "1080_CIN_TP13_Part05_LOC.bik", "1080_CIN_TP_Autopsy_Chryssalid_LOC.bik", "1080_CIN_TP_Autopsy_Cyberdisc_LOC.bik", "1080_CIN_TP_Autopsy_Drone_LOC.bik", "1080_CIN_TP_Autopsy_Ethereal_LOC.bik", "1080_CIN_TP_Autopsy_Floater_LOC.bik", "1080_CIN_TP_Autopsy_HeavyFloater_LOC.bik", "1080_CIN_TP_Autopsy_MutonBerserker_LOC.bik", "1080_CIN_TP_Autopsy_MutonElite_LOC.bik", "1080_CIN_TP_Autopsy_Muton_LOC.bik", "1080_CIN_TP_Autopsy_SectoidComm_LOC.bik", "1080_CIN_TP_Autopsy_Sectoid_LOC.bik", "1080_CIN_TP_Autopsy_Sectopod_LOC.bik", "1080_CIN_TP_Autopsy_ThinMan_LOC.bik", "1080_CIN_TP_WelcometoEngineering_LOC.bik", "1080_CIN_TP_WelcometoScienceLabs_LOC.bik", "1080_CIN_TUT_Act2ScienceLabExplain_LOC.bik", "1080_CIN_TUT_Act2ScienceLabThankYou_LOC.bik", "1080_CIN_TUT_Act2SituationRoom_LOC.bik", "1080_CIN_TUT_EngineeringNano_LOC.bik", "1080_CIN_TUT_EngineeringScope_LOC.bik", "1080_CIN_TUT_FacConstructionBegin_LOC.bik", "1080_CIN_TUT_FacConstructionScienceLab_LOC.bik", "1080_CIN_TUT_FacConstructionThankYou_LOC.bik", "1080_CIN_TUT_HangarBegin_LOC.bik", "1080_CIN_TUT_MissionControlSelected_LOC.bik", "1080_CIN_TUT_MissionControlStart_LOC.bik", "1080_CIN_TUT_Movingtimeforward_LOC.bik", "1080_CIN_TUT_MultipleObjectives_LOC.bik", "1080_CIN_TUT_PanicMechanicA_LOC.bik", "1080_CIN_TUT_PanicmechanicB_LOC.bik", "1080_CIN_TUT_ResearchTakesTime_LOC.bik", "1080_CIN_TUT_SatelliteLaunchBegin_LOC.bik", "1080_CIN_TUT_SatelliteLaunchEnd_LOC.bik", "1080_CIN_TUT_ScienceLabThankYou_LOC.bik", "1080_CIN_TUT_SurvivorReturns_LOC.bik", "1080_CIN_TUT_UFOInterception_LOC.bik", "1080_SR_hover_02_LOC.bik", "1080_TUT_MissionPrep_LOC.bik", "1080_TUT_TP_UnloadScreen_LOC.bik", "SR_hover_02_LOC.bik" };
            string[] movieListExp = { "1080_XEW_CIN_TUT_Act2SituationRoom_LOC.bk2", "1080_XEW_TP_BaseAssault-Intro_LOC.bk2", "1080_XEW_TP_BaseAssault-Win_LOC.bk2", "1080_XEW_TP_Exalt-Intro_LOC.bk2", "1080_XEW_TP_Exalt-Win_LOC.bk2", "1080_XEW_TP_Meld_LOC.bk2", "1080_XEW_Autopsy_Mechtoid_LOC.bk2", "1080_XEW_Autopsy_Seeker_LOC.bk2", "1080_XEW_CIN_Intro_LOC.bk2" };
            Dictionary<string, long> movieDictAll = new();
            double movieSizeAll = 0;
            string tempDir = Path.GetTempPath();
            string binkconvPath = Path.Combine(radVideoDir, "binkconv.exe");
            string binkmixPath = Path.Combine(radVideoDir, "binkmix.exe");

            foreach (string movieName in movieListClassic)
            {
                string file = Path.Combine(gameDir, "XComGame", "Movies", movieName);
                if (File.Exists(file))
                {
                    long size = new FileInfo(file).Length;
                    movieSizeAll += size;
                    movieDictAll.Add(file, size);
                }
            }

            foreach (string movieName in movieListExp)
            {
                string file = Path.Combine(gameDir, "XEW", "XComGame", "Movies", movieName);
                if (File.Exists(file))
                {
                    long size = new FileInfo(file).Length;
                    movieSizeAll += size;
                    movieDictAll.Add(file, size);
                }
            }

            double progr = 0;
            using (ProgressBar progress = new())
            {
                foreach (var movieFile in movieDictAll)
                {
                    string fNameWAV = Path.ChangeExtension(Path.GetFileName(movieFile.Key), ".wav");
                    fNameWAV = Path.Combine(tempDir, fNameWAV);
                    File.Delete(fNameWAV);

                    // Извлекаем английскую озвучку.
                    Process.Start(binkconvPath, "\"" + movieFile.Key + "\" \"" + fNameWAV + "\"" + @" /V /N5 /#").WaitForExit();

                    double oneSixth = (movieFile.Value / 6) / movieSizeAll;
                    // Встраиваем английскую озвучку вместо остальных.
                    StartProcess(binkmixPath, "\"" + movieFile.Key + "\" \"" + fNameWAV + "\" \"" + movieFile.Key + "\"" + @" /O /L0 /T11 /#");
                    ProgReport(oneSixth);
                    StartProcess(binkmixPath, "\"" + movieFile.Key + "\" \"" + fNameWAV + "\" \"" + movieFile.Key + "\"" + @" /O /L0 /T8 /#");
                    ProgReport(oneSixth);
                    StartProcess(binkmixPath, "\"" + movieFile.Key + "\" \"" + fNameWAV + "\" \"" + movieFile.Key + "\"" + @" /O /L0 /T14 /#");
                    ProgReport(oneSixth);
                    StartProcess(binkmixPath, "\"" + movieFile.Key + "\" \"" + fNameWAV + "\" \"" + movieFile.Key + "\"" + @" /O /L0 /T17 /#");
                    ProgReport(oneSixth);
                    StartProcess(binkmixPath, "\"" + movieFile.Key + "\" \"" + fNameWAV + "\" \"" + movieFile.Key + "\"" + @" /O /L0 /T32 /#");
                    ProgReport(oneSixth);
                    StartProcess(binkmixPath, "\"" + movieFile.Key + "\" \"" + fNameWAV + "\" \"" + movieFile.Key + "\"" + @" /O /L0 /T29 /#");
                    ProgReport(oneSixth);

                    // Удаляем использованный wav
                    File.Delete(fNameWAV);

                    // Рапартуем о прогрессе
                    //progr += movieFile.Value / movieSizeAll;
                    //progress.Report(progr);
                    //progress.Report(Math.Round((double)(100 * movieFile.Value) / movieSizeAll));
                }
                void ProgReport(double oneSixth)
                {
                    progr += oneSixth;
                    progress.Report(progr);
                }
            }

            void StartProcess(string pathExe, string param, string? workDir = null, bool hidden = true, bool wait = true)
            {
                Process prc = new();
                if (hidden)
                {
                    prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    prc.StartInfo.UseShellExecute = false;
                    prc.StartInfo.CreateNoWindow = true;
                }

                if (!string.IsNullOrEmpty(workDir))
                    prc.StartInfo.WorkingDirectory = workDir;

                //Console.WriteLine($"Start {pathExe} {param}");

                prc.StartInfo.Arguments = param;
                prc.StartInfo.FileName = pathExe;
                prc.Start();
                //prc.CloseMainWindow();

                if (wait)
                    prc.WaitForExit();
            }
        }


        static string? FindRadToolsFolder()
        {
            // Если не находим в папке с программой, то ищем в реестре.
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "binkmix.exe")) && File.Exists(Path.Combine(Environment.CurrentDirectory, "binkconv.exe")))
                return Environment.CurrentDirectory;

            RegistryKey? hklm32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            if (hklm32 != null)
            {
                RegistryKey? regRADVideo = hklm32.OpenSubKey(@"SOFTWARE\RAD Game Tools\RADVideo", false);
                if(regRADVideo != null)
                {
                    string? pathRADVideo = (string?)regRADVideo.GetValue("InstallDir");
                    if (!string.IsNullOrEmpty(pathRADVideo))
                        if (File.Exists(Path.Combine(Environment.CurrentDirectory, "binkmix.exe")) && File.Exists(Path.Combine(Environment.CurrentDirectory, "binkconv.exe")))
                            return pathRADVideo;
                }
            }
            return null;
        }

        static string? FindRadToolsFolderDLG()
        {
            FolderBrowserDialog folderBrowserDlg = new()
            {
                ShowNewFolderButton = false,
                UseDescriptionForTitle = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                Description = lng["BinkSelectDirDlgTitle"]
            };
            if (folderBrowserDlg.ShowDialog() == DialogResult.Cancel)
                return null;
            else
                if (File.Exists(Path.Combine(folderBrowserDlg.SelectedPath, "binkmix.exe")) && File.Exists(Path.Combine(folderBrowserDlg.SelectedPath, "binkconv.exe")))
                return folderBrowserDlg.SelectedPath;
            else
                MessageBox.Show(lng["BinkNotFoundMsgContent"], lng["BinkNotFoundMsgTitle"], MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        static string? FindGameFolderDLG()
        {
            FolderBrowserDialog gameFolderBrowserDlg = new()
            {
                ShowNewFolderButton = false,
                UseDescriptionForTitle = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                Description = lng["DirGameSelectDlgTitle"]
            };
            if (gameFolderBrowserDlg.ShowDialog() == DialogResult.Cancel)
                return null;
            else
            if (File.Exists(Path.Combine(gameFolderBrowserDlg.SelectedPath, "Binaries", "Win32", "XComGame.exe")))
                return gameFolderBrowserDlg.SelectedPath;
            else return "";
        }
        static string FindGameFolder()
        {
            string gamePach = Environment.CurrentDirectory;
            if (!File.Exists(Path.Combine(gamePach, "Binaries", "Win32", "XComGame.exe")))
            {
                // Узнаём через файлы Стима где находится игра.
                gamePach = FoundSteamGamePath("200510");

                // Если путь не нашли, то просим его указать пользователя.
                if (!File.Exists(Path.Combine(gamePach, "Binaries", "Win32", "XComGame.exe")))
                    do
                    {
                        string? findDlgResult = FindGameFolderDLG();
                        if (findDlgResult == null)
                            Environment.Exit(0);
                        else
                        {
                            if ((findDlgResult != "") && (File.Exists(Path.Combine(findDlgResult, "Binaries", "Win32", "XComGame.exe"))))
                                gamePach = findDlgResult;
                            else
                           if (MessageBox.Show(lng["DirGameSelectNotFoundMsgContent"], lng["DirGameSelectNotFoundMsgTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                                Environment.Exit(0);
                        }
                    } while (!File.Exists(Path.Combine(gamePach, "Binaries", "Win32", "XComGame.exe")));
            }

            return gamePach;

            string FoundSteamGamePath(string GemeID)
            {
                // Узнаём где установлен Steam.
                string? pathLibraryF = "";
                RegistryKey view32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                using (RegistryKey? steamRegistry = view32.OpenSubKey(@"SOFTWARE\Valve\Steam", false))
                {
                    if (steamRegistry != null)
                        pathLibraryF = (string?)steamRegistry.GetValue("InstallPath");
                    else
                        return "";
                }

                // На случай если pathLibraryF пуст - делаем обработку ошибки, чтобы программа не вылетала.
                try
                {
                    pathLibraryF = Path.Combine(pathLibraryF, @"steamapps\libraryfolders.vdf");
                }
                catch (ArgumentNullException)
                {
                    return "";
                }

                // Если файл не найден - выходим.
                if (!File.Exists(pathLibraryF))
                    return "";

                // Загружаем файл в массив.
                string[] allText = File.ReadAllLines(pathLibraryF);
                List<string> PathLibrary = new();

                // Ищем в массиве признаки пути.
                for (int i = 0; i < allText.Length; i++)
                {
                    allText[i] = allText[i].Replace(@"\\", @"\");

                    // Определяем начало и конец нужного участка строки.
                    int qp1 = allText[i].IndexOf(@":\") - 1; // Отступаем один символ чтобы было видно букву диска.
                    int qp2 = allText[i].Length - qp1 - 1; // Удаляем последнюю кавычку.

                    // Если найдено, вырезаем нужный участок строки и добавляем её в список.
                    if (qp1 > 0)
                        PathLibrary.Add(allText[i].Substring(qp1, qp2) + @"\steamapps");
                }

                // Добываем полный путь к файлу .acf
                string fullPathAcf = "";
                string strPathLibrary = "";
                for (int i = 0; i < PathLibrary.Count; i++)
                {
                    string strTmp = Path.Combine(PathLibrary[i], "appmanifest_" + GemeID + ".acf");
                    if (File.Exists(strTmp))
                    {
                        fullPathAcf = strTmp;
                        strPathLibrary = PathLibrary[i];
                        break;
                    }
                }

                if (!File.Exists(fullPathAcf))
                    return "";


                // Ковыряем .acf
                string installdir = string.Empty;
                foreach (string line in File.ReadLines(fullPathAcf))
                {
                    int ql0 = line.IndexOf("\"installdir\"");
                    if (ql0 > -1)
                    {
                        int ql1 = GetNthIndex(line, '\"', 3) + 1;
                        int ql2 = line.Length - ql1 - 1;

                        installdir = line.Substring(ql1, ql2);
                        break;
                    }
                }

                // Возвращаем путь к папке с игрой.
                return Path.Combine(strPathLibrary, "common", installdir);
            }

            int GetNthIndex(string s, char t, int n)
            {
                int count = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == t)
                    {
                        count++;
                        if (count == n)
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }
        }

    }
}


            



