using iniParser.exeptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace iniParser
{
    public class iniparser
    {
        private Hashtable pairsParameters = new Hashtable();
        private string pathToFile;
        private struct dataPairs
        {
            public string section;
            public string key;
        }

        public iniparser(string pathfile)
        {
            TextReader file = null;
            string strLine;
            string currentMain = null;
            string[] keyPair;

            pathToFile = pathfile;

            if (File.Exists(pathToFile))
            {
                try
                {
                    file = new StreamReader(pathToFile);
                    var extension = Path.GetExtension(pathToFile); // получаем расширени файла
                    if (extension != ".ini")
                    {
                        throw new myExeptions.InvalidFormat("Error format"); // ошибка неверного расширение файла
                    }
                    else
                    {
                        strLine = file.ReadLine();
                        while (strLine != null)
                        {
                            strLine = strLine.Trim(); // удаляет все начальные и конечные символы пробела из текущей строки

                            if (strLine != "")
                            {
                                if (strLine.StartsWith("[") && strLine.EndsWith("]")) // проверка на название [Секции]
                                {
                                    currentMain = strLine.Substring(1, strLine.Length - 2); // считываем название
                                }
                                else
                                {
                                    if (strLine.StartsWith(";"))
                                    {   // строка = коммент   
                                    }
                                    else
                                    {
                                        strLine = strLine.Replace(" ", string.Empty); // удаляем пробелы 
                                        keyPair = strLine.Split(new char[] { '=' }, 2); // разделяем строку на 2 подстроки разделенные знаком равно

                                        dataPairs datapairs;
                                        string value = null;

                                        if (currentMain == null)
                                        {  }
                                        else
                                        {
                                            datapairs.section = currentMain;
                                            datapairs.key = keyPair[0];

                                            if (keyPair.Length > 1)
                                            {
                                                keyPair[1] = Regex.Replace(keyPair[1], @";.+$", string.Empty); // удаляем комментарий, если он находится в строке
                                                value = keyPair[1];
                                            }

                                            pairsParameters.Add(datapairs, value);
                                        }
                                    }
                                }
                            }
                            strLine = file.ReadLine();
                        }
                    }
                }
                catch (myExeptions ex)
                {
                    throw ex;
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }
            }
            else
                throw new FileNotFoundException("Path not found " + pathfile);
            }

        public string readData(string section, string setting)
        {
            dataPairs datapairs;
            datapairs.section = section;
            datapairs.key = setting;
            if (pairsParameters[datapairs] == null)
            {
                throw new myExeptions.InvalidParametr("Section or parametr not found");
            }
            else
                return (string)pairsParameters[datapairs];
        }

        public void changeParameter(string section, string setting, string value)
        {
            dataPairs datapairs;
            datapairs.section = section;
            datapairs.key = setting;

            if (pairsParameters.ContainsKey(datapairs))
            {
                pairsParameters.Remove(datapairs);
            }
        }

        public int TryGetInt(string section, string setting)
        {
            dataPairs datapairs;
            datapairs.section = section;
            datapairs.key = setting;
            
            if(pairsParameters[datapairs] == null )
            {
                throw new myExeptions.InvalidParametr("Section or parametr not found");
            }
            else
            {
                string s = pairsParameters[datapairs].ToString();
                int number;
                bool valid = int.TryParse(s, out number);
                if (!valid)
                    throw new myExeptions.InvalidParametr("Invalid format to Parse");
                else
                    return number;
            }
            
        }

        public double TryGetDouble(string section, string setting)
        {
            dataPairs datapairs;
            datapairs.section = section;
            datapairs.key = setting;
            if (pairsParameters[datapairs] == null)
            {
                throw new myExeptions.InvalidParametr("Section or parametr not found");
            }
            else
            {
                string s = pairsParameters[datapairs].ToString();
                double number;
                bool valid = double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out number);
                if(!valid)
                    throw new myExeptions.InvalidParametr("Invalid format to Parse");
                else
                    return number;
            }
        }






    }
}
