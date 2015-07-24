using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

/// <summary>
/// 读取配置文件
/// </summary>
public class ReadIniFile
{
    private Hashtable ht;
    private string[] textData;

   
    public ReadIniFile(string fileName)
    {
        
        string key = string.Empty;
        this.ht = new Hashtable();
        string text = LoadTextFile(fileName, ".txt");
        if(text != null)
        {
            //将数据读取后然后分割处理
            this.textData = text.Split(new char[]{'\n'});

            for (int i = 0; i < this.textData.Length; i++)
            {
                string readLine = this.textData[i].Trim();
                
                //判断是否空行
                if(!readLine.Equals(string.Empty))
                {

                    //判断是否为注释行
                    if(!readLine.Substring(0,2).Equals("//"))
                    {
                        //获得key字段
                        if (readLine.StartsWith("[") && readLine.EndsWith("]"))
                        {
                            key = readLine.Substring(1, readLine.Length - 2);
                        }
                        else
                        {
                            int index = readLine.IndexOf("=");
                            //获得配置文件中的key
                            string subKey = readLine.Substring(0, index);

                            //获得对应的value值
                            string value = readLine.Substring(++index, readLine.Length - index);

                            //如果hashtable中包含key直接添加 subkey value组成新的哈希表，即哈希表嵌套哈希表
                            if (this.ht.ContainsKey(key))
                            {
                                Hashtable hashteble = (Hashtable)this.ht[key];
                                hashteble.Add(subKey, value);
                            }
                            else
                            {
                                Hashtable hashtable = new Hashtable();
                                hashtable.Add(subKey, value);
                                this.ht.Add(key, hashtable);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if(Debug.isDebugBuild)
            {
                Debug.LogError("Inifile:>" + fileName + "object is null !");
            }
        }

       
    }
   
    /// <summary>
    /// 加载配置文件
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="suffix">文件后缀名</param>
    /// <returns></returns>
    private string LoadTextFile(string path, string suffix)
    {
        string text = string.Empty;
        string strPath = path + suffix;
        text = File.ReadAllText(strPath);
        return text;
    }

    //读取字符
    public string ReadString(string section, string ident, string defaultValue)
    {
        string result = defaultValue;
        if(this.ht.ContainsKey(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if(hashtable.ContainsKey(ident))
            {
                result = hashtable[ident].ToString();
            }
        }
        return result;
    }

    public float ReadFloat(string section, string ident, float defaultValue)
    {
        float result = defaultValue;
        if (this.ht.ContainsKey(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if (hashtable.ContainsKey(ident))
            {
                result = Convert.ToSingle(hashtable[ident]);
            }
        }
        return result;
    }

    public double ReadDouble(string section, string ident, double defaultValue)
    {
        double result = defaultValue;
        if (this.ht.ContainsKey(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if (hashtable.ContainsKey(ident))
            {
                result = Convert.ToDouble(hashtable[ident]);
            }
        }
        return result;
    }

    public void WriteString(string section,string ident,string val)
    {
        if(this.SectionExists(section))
        {
            Hashtable hashtable = (Hashtable)this.ht[section];
            if(hashtable.ContainsKey(ident))
            {
                hashtable[ident] = val;
            }
            else
            {
                hashtable.Add(ident, val);
            }
        }
        else
        {
            Hashtable hashtable = new Hashtable();
            this.ht.Add(section, hashtable);
            hashtable.Add(ident, val);
        }
    }

    
    //获取字段
    public Hashtable GetSection(string section)
    {
        if(this.SectionExists(section))
        {
            return this.ht[section] as Hashtable;
        }
        return null;
    }

    private bool SectionExists(string section)
    {
        return this.ht.ContainsKey(section);
    }
    
    //判断值是否存在
    public bool ValueExists(string  section,string ident)
    {
        return this.SectionExists(section) && ((Hashtable)this.ht[section]).ContainsKey(ident);
    }
}
