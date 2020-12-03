/**
 * Copyright by Nocksoft
 * https://www.nocksoft.de
 * https://nocksoft.de/tutorials/visual-c-sharp-arbeiten-mit-ini-dateien/
 * -----------------------------------
 * Erstellt von:	Rafael Nockmann
 * Letzte Änderung:	23.08.2017
 * Version:         1.0.3
 *
 * Sprache: Visual C#
 *
 * Lizenz: https://nocksoft.de/downloads/_lizenzen/nocksoft-lizenz-quellcode.txt
 * 
 * Beschreibung:
 * Stellt grundlegende Funktionen bereit, um mit INI-Dateien zu arbeiten.
 *
 * ##############################################################################################
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DesktopManager
{
    public class INIFile
    {
        private readonly string _File;

        public object FileStreamGetter { get; private set; }

        /// <summary>
        /// Aufruf des Konstruktors initialisiert ein Objekt der Klasse INIFile.
        /// </summary>
        /// <param name="file">INI-Datei, auf der zugegriffen werden soll.</param>
        /// <param name="createFile">Gibt an, ob die Datei erstellt werden soll, wenn diese nicht vorhanden ist.</param>
        public INIFile(string file, bool createFile = false)
        {
            if (createFile == true && File.Exists(file) == false)
            {
                FileInfo fileInfo = new FileInfo(file);
                FileStream fileStream = fileInfo.Create();
                fileStream.Close();
            }
            _File = file;
        }

        #region Öffentliche Methoden

        /// <summary>
        /// Entfernt alle Kommentare und leeren Zeilen aus einer kompletten Section und gibt diese zurück.
        /// Die Methode ist nicht Case-sensitivity und ignoriert daher Groß- und Kleinschreibung.
        /// Der Rückgabewert enthält keine Leerzeichen.
        /// </summary>
        /// <param name="section">Name der angeforderten Section.</param>
        /// <param name="getComments">Gibt an, ob Kommentare berücksichtigt werden sollen.</param>
        /// <returns>Gibt die komplette Section zurück.</returns>
        /// 



        public List<string> GetSection(string section, bool getComments = false)
        {

            Start:

            // Stellt sicher, dass eine Section immer im folgenden Format vorliegt: [section]
            section = CheckSection(section);

            List<string> completeSection = new List<string>();
            bool sectionStart = false;


        try 
          { 
           // using (Stream stream = new FileStream(_File, FileMode.Open))  //overflow exeprion??
            {
                // Liest die Zieldatei ein
                string[] fileArray = File.ReadAllLines(_File);

                //wenn nicht eingelesen werden kann =string0----------------
                string result = String.Concat(fileArray);
                if (string.IsNullOrEmpty(result)) { goto Start; }
                //------------------------------------------------------------


                foreach (var item in fileArray)
                {
                    if (item.Length <= 0) continue;

                    // Wenn die gewünschte Section erreicht ist
                    if (item.Replace(" ", "").ToLower() == section)
                    {
                        sectionStart = true;
                    }
                    // Wenn auf eine neue Section getroffen wird, wird die Schleife beendet
                    if (sectionStart == true && item.Replace(" ", "").ToLower() != section && item.Replace(" ", "").Substring(0, 1) == "[" && item.Replace(" ", "").Substring(item.Length - 1, 1) == "]")
                    {
                        break;
                    }
                    if (sectionStart == true)
                    {
                        // Wenn der Eintrag kein Kommentar und kein leerer Eintrag ist, wird er der List<string> completeSection hinzugefügt
                        if (getComments == false
                            && item.Replace(" ", "").Substring(0, 1) != ";" && !string.IsNullOrWhiteSpace(item))
                        {
                            completeSection.Add(ReplaceScpacesAtStartAndEnd(item));
                        }
                        if (getComments == true && !string.IsNullOrWhiteSpace(item))
                        {
                            completeSection.Add(ReplaceScpacesAtStartAndEnd(item));
                        }
                    }



                }

            }
           }
            catch (OverflowException sofEx)
            {
                //put error in result box, or log it, or something
                //textBoxHasil.Text = "Error: result too large"
                //SmallLogAllTogether.Logger("--");
                //SmallLogAllTogether.Logger("-->>> Ini-file-helper - GetSection - File in use goto startpoint \r\n      --  Error: result too large \r\n      --  " + sofEx.Message);
                //SmallLogSystem.Logger("--");
                //SmallLogSystem.Logger("-->>> Ini-file-helper - GetSection - File in use goto startpoint \r\n      --  Error: result too large \r\n      --  " + sofEx.Message);
                //return;

                // include time in Timespan
                TimeSpan interval1 = new TimeSpan(0, 0, 2); //hh-mm-ss                                                                  //include timespan in wait.Task
                Task.Delay(interval1);
                goto Start;
            }
            catch (Exception ex)
            {
                //'some other exception
                // textBoxHasil.Text = ex.Message
               // SmallLogAllTogether.Logger("--");
                //SmallLogAllTogether.Logger("-->>> Ini-file-helper - GetSection - File in use goto startpoint \r\n      --  " + ex.Message);
               // SmallLogSystem.Logger("--");
               // SmallLogSystem.Logger("-->>> Ini-file-helper - GetSection - File in use goto startpoint \r\n      --  " + ex.Message);
                // break;

                // include time in Timespan
                TimeSpan interval1 = new TimeSpan(0, 0, 2); //hh-mm-ss                                                                  //include timespan in wait.Task
                Task.Delay(interval1);
                goto Start;
            }
         

            return completeSection;
        }

        private T TimeoutFileAction<T>(Func<T> func)
        {
            var started = DateTime.UtcNow;
            while ((DateTime.UtcNow - started).TotalMilliseconds < 2000)
            {
                try
                {
                    return func();
                }
                catch (System.IO.IOException exception)
                {
                    //ignore, or log somewhere if you want to
                     //SmallLogAllTogether.Logger("-->>> Ini-file-helper - TimeoutFileAction File-in-use");
                    // SmallLogSystem.Logger("-->>> Ini-file-helper - TimeoutFileAction File-in-use");
                }
            }
            return default(T);
        }


        /// <summary>
        /// Die Methode gibt einen Wert zum dazugehörigen Key zurück.
        /// Die Methode ist nicht Case-sensitivity und ignoriert daher Groß- und Kleinschreibung.
        /// </summary>
        /// <param name="section">Name der angeforderten Section.</param>
        /// <param name="key">Name des angeforderten Keys.</param>
        /// <param name="convertKeyToLower">Wenn "true" übergeben wird, wird der Rückgabewert in Kleinbuchstaben zurückgegeben.</param>
        /// <returns>Gibt, wenn vorhanden, den Wert zu dem angegebenen Key in der angegeben Section zurück.</returns>
        public string GetValue(string section, string key, bool convertValueToLower = false)
        {
            // Stellt sicher, dass eine Section immer im folgenden Format vorliegt: [section]
            section = CheckSection(section);
            key = key.ToLower();

            List<string> completeSection = GetSection(section);

            foreach (var item in completeSection)
            {
                // In Schleife fortfahren, wenn kein Key
                if (!item.Contains("=") && item.Contains("[") && item.Contains("]")) continue;

                string[] keyAndValue = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                if (keyAndValue[0].ToLower() == key && keyAndValue.Count() > 1)
                {
                    if (convertValueToLower == true)
                    {
                        keyAndValue[1] = keyAndValue[1].ToLower();
                    }
                    return keyAndValue[1];
                }
            }
            return null;
        }

        /// <summary>
        /// Ändert einen Wert des dazugehörigen Schlüssels in der angegebenen Section.
        /// </summary>
        /// <param name="section">Name der Section, in dem sich der Schlüssel befindet.</param>
        /// <param name="key">Name des Schlüssels, dessen Wert geändert werden soll.</param>
        /// <param name="value">Neuer Wert.</param>
        /// <param name="convertValueToLower">Wenn "true" übergeben wird, wird der Wert in Kleinbuchstaben gespeichert.</param>
        public async System.Threading.Tasks.Task SetValueAsyncAsync(string section, string key, string value, bool convertValueToLower = false)
        {
            // Stellt sicher, dass eine Section immer im folgenden Format vorliegt: [section]
            section = CheckSection(section);
            string keyToLower = key.ToLower();

            // Prüft, ob die gesuchte Section gefunden wurde
            bool sectionFound = false;

            List<string> newFileContent = new List<string>();

            // Liest die Zieldatei ein
            string[] fileLines = File.ReadAllLines(_File);

            // Wenn die Zieldatei leer ist...
            if (fileLines.Length <= 0)
            {
                newFileContent = CreateSection(newFileContent, section, value, key, convertValueToLower);
                await WriteFile(newFileContent);
                return;
            }

            // ...sonst wird jede Zeile durchsucht
            for (int i = 0; i < fileLines.Length; i++)
            {
                // Option 1 -> Gewünschte Section wurde (noch) nicht gefunden
                if (fileLines[i].Replace(" ", "").ToLower() != section)
                {
                    newFileContent.Add(fileLines[i]);
                    // Wenn Section nicht vorhanden ist, wird diese erzeugt
                    if (i == fileLines.Length - 1 && fileLines[i].Replace(" ", "").ToLower() != section && sectionFound == false)
                    {
                        newFileContent.Add(null);
                        newFileContent = CreateSection(newFileContent, section, value, key, convertValueToLower);
                        break;
                    }
                    continue;
                }


                // Option 2 -> Gewünschte Section wurde gefunden
                sectionFound = true;

                // Enthält die komplette Section, in der sich der Zielschlüssel befindet
                List<string> targetSection = GetSection(section, true);

                // Jeden Eintrag in der Section, in der sich der Zielschlüssel befindet, durchgehen
                for (int x = 0; x < targetSection.Count; x++)
                {
                    string[] targetKey = targetSection[x].Split(new string[] { "=" }, StringSplitOptions.None);
                    // Wenn der Zielschlüssel gefunden ist
                    if (targetKey[0].ToLower() == keyToLower)
                    {
                        // Prüft, in welcher Schreibweise die Werte abgespeichert werden sollen
                        if (convertValueToLower == true)
                        {
                            newFileContent.Add(keyToLower + "=" + value.ToLower());
                        }
                        else
                        {
                            newFileContent.Add(key + "=" + value);
                        }
                        i = i + x;
                        break;
                    }
                    else
                    {
                        newFileContent.Add(targetSection[x]);
                        // Wenn Key nicht vorhanden ist, wird dieser erzeugt
                        if (x == targetSection.Count - 1 && targetKey[0].ToLower() != keyToLower)
                        {
                            // Prüft, in welcher Schreibweise die Werte abgespeichert werden sollen
                            if (convertValueToLower == true)
                            {
                                newFileContent.Add(keyToLower + "=" + value.ToLower());
                            }
                            else
                            {
                                newFileContent.Add(key + "=" + value);
                            }
                            i = i + x;
                            break;
                        }
                    }
                }

            }

            await WriteFile(newFileContent);
        }

        #endregion

        #region Private Methoden

        /// <summary>
        /// Stellt sicher, dass eine Section immer im folgenden Format vorliegt: [section]
        /// </summary>
        /// <param name="section">Section, die auf korrektes Format geprüft werden soll.</param>
        /// <returns>Gibt Section in dieser Form zurück: [section]</returns>
        private string CheckSection(string section)
        {
            section = section.ToLower();
            if (section.Substring(0, 1) != "[" && section.Substring(section.Length - 1, 1) != "]")
            {
                section = "[" + section + "]";
            }
            return section;
        }

        /// <summary>
        /// Entfernt voranstehende und hintenstehende Leerzeichen bei Sections, Keys und Values.
        /// </summary>
        /// <param name="item">String, der gekürzt werden soll.</param>
        /// <returns>Gibt einen gekürzten String zurück.</returns>
        private string ReplaceScpacesAtStartAndEnd(string item)
        {
            // Wenn der Eintrag einen Schlüssel und einen Wert hat
            if (item.Contains("=") && !item.Contains("[") && !item.Contains("]"))
            {
                string[] keyAndValue = item.Split(new string[] { "=" }, StringSplitOptions.None);
                return keyAndValue[0].Trim() + "=" + keyAndValue[1].Trim();
            }

            return item.Trim();
        }

        /// <summary>
        /// Legt eine neue Section an.
        /// </summary>
        /// <param name="newSettings">Liste newSettings aus SetValueAsync.</param>
        /// <param name="section">section die angelegt werden soll.</param>
        /// <param name="value">Wert der hinzugefügt werden soll.</param>
        /// <param name="key">Schlüssel der hinzugefügt werden soll.</param>
        /// <param name="convertValueToLower">Gibt an, ob Schlüssel und Wert in Kleinbuchstaben abgespeichert werden sollen.</param>
        /// <returns></returns>
        private List<string> CreateSection(List<string> newSettings, string section, string value, string key, bool convertValueToLower)
        {
            string keyToLower = key.ToLower();

            newSettings.Add(section);
            // Prüft, in welcher Schreibweise die Werte abgespeichert werden sollen
            if (convertValueToLower == true)
            {
                newSettings.Add(keyToLower + "=" + value.ToLower());
            }
            else
            {
                newSettings.Add(key + "=" + value);
            }
            return newSettings;
        }

        private async Task WriteFile(List<string> content)
        {

            try
            {


                 StreamWriter writer = new StreamWriter(_File);
                foreach (var item in content)
                {
                    writer.WriteLine(item);
                }
                writer.Close();
            }

            catch (Exception ex)
            {
                //MessageBox.Show("Button_Sync_AppPath_to_Backup_Templates Error-Handling =\r\n" + ex.Message);
               // await SmallLogError.Logger("Button_Sync_AppPath_to_Backup_Templates Error-Handling = \r\n" + ex.Message);
               // await SmallLogAllTogether.Logger("Button_Sync_AppPath_to_Backup_Templates Error-Handling = \r\n" + ex.Message);
                return;
            }


        }

        #endregion
    }
}
