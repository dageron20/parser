using iniParser.exeptions;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using static iniParser.exeptions.iniExeptions;

namespace iniParser
{
    public class iniparser
    {
        private Hashtable pairsParameters = new Hashtable();
        private string pathToFile;
        private struct DataPairs
        {
            public string section;
            public string key;
        }

        public iniparser(string pathfile)
        {
            string strLine;
            string currentMain = null;
            string[] keyPair;

            pathToFile = pathfile;

            if (File.Exists(pathToFile))
            {
                try
                {
                    TextReader file = new StreamReader(pathToFile);
                    var extension = Path.GetExtension(pathToFile); // получаем расширени файла
                    if (extension != ".ini")
                    {
                        throw new InvalidFormat("Error format"); // ошибка неверного расширение файла
                    }
                    
                    else
                    {
                        strLine = file.ReadLine();
                        while (strLine != null)
                        {
                            strLine = strLine.Trim(); // удаляет все начальные и конечные символы пробела из текущей строки
                            if (strLine == "")
                            {
                                strLine = file.ReadLine();
                                continue;                               
                            }
                            if (strLine.StartsWith("[") && strLine.EndsWith("]")) // проверка на название [Секции]
                            {
                                currentMain = strLine.Substring(1, strLine.Length - 2); // считываем название
                            }
                            else
                            {
                                if (strLine.StartsWith(";"))
                                {
                                    // строка = коммент   
                                    strLine = file.ReadLine();
                                    continue;
                                }
                                strLine = strLine.Replace(" ", string.Empty); // удаляем пробелы 
                                keyPair = strLine.Split(new char[] { '=' }, 2); // разделяем строку на 2 подстроки разделенные знаком равно
                                DataPairs datapairs;
                                string value = null;
                                if (currentMain == null)
                                {
                                    continue;                                        
                                }
                                datapairs.section = currentMain;
                                datapairs.key = keyPair[0];
                                if (keyPair.Length > 1)
                                {
                                    keyPair[1] = Regex.Replace(keyPair[1], @";.+$", string.Empty); // удаляем комментарий, если он находится в строке
                                    value = keyPair[1];
                                }
                                if (pairsParameters[datapairs] != null)
                                    ChangeParameter(datapairs.section, datapairs.key, value);
                                else
                                    pairsParameters.Add(datapairs, value);
                            }
                            strLine = file.ReadLine();
                        }
                    }
                }
                catch (iniExeptions ex)
                {
                    throw ex;
                }
            }
            else
                throw new FileNotFoundException("Path not found " + pathfile);
        }

        public string ReadData(string section, string setting)
        {
            DataPairs datapairs;
            datapairs.section = section;
            datapairs.key = setting;
            if (pairsParameters[datapairs] == null)
            {
                throw new InvalidParametr("Section or parametr not found");
            }
            else
            {
                return (string)pairsParameters[datapairs];
            }
        }

        public void ChangeParameter(string section, string setting, string value)
        {
            DataPairs datapairs;
            datapairs.section = section;
            datapairs.key = setting;

            if (pairsParameters.ContainsKey(datapairs))
            {
                pairsParameters.Remove(datapairs);
                pairsParameters.Add(datapairs, value);
            }
        }

        public int TakeInt(string section, string setting)
        {
            DataPairs datapairs;
            datapairs.section = section;
            datapairs.key = setting;

            if (pairsParameters[datapairs] == null)
            {
                throw new InvalidParametr("Section or parametr not found");
            }
            else
            {
                string s = pairsParameters[datapairs].ToString();
                bool valid = int.TryParse(s, out int number);
                if (!valid)
                    throw new InvalidParametr("Invalid format to Parse");
                else
                    return number;
            }

        }

        public double TakeDouble(string section, string setting)
        {
            DataPairs datapairs;
            datapairs.section = section;
            datapairs.key = setting;
            if (pairsParameters[datapairs] == null)
            {
                throw new InvalidParametr("Section or parametr not found");
            }
            else
            {
                string s = pairsParameters[datapairs].ToString();
                bool valid = double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double number);
                if (!valid)
                    throw new InvalidParametr("Invalid format to Parse");
                else
                    return number;
            }
        }
    }
}