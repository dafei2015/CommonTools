using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrip.CSVSpilt
{
    public class Example : MonoBehaviour
    {
        static string content = (Resources.Load("TEXT") as TextAsset).text;
        List<ConfigExample> configTest = CsvParser.DoParseByFileContent<ConfigExample>(
            typeof(ConfigExample), new string[] { "id", "text" }, content);
        
     
    }
}
