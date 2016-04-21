using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
public class Local : MonoBehaviour
{   
    /// <summary>
    /// 游戏中的程序的 key value 字典
    /// </summary>
    public Dictionary<string,string> mInGameStr = new Dictionary<string,string>();
    //public override void Init()
    //{
    //    base.Init();
    //}
    public void LoadInGameStr()
    {
        string loadpath = Application.dataPath + "/../../../" + PathConfig.mLocalInGameStrPath;
 
        List<string> templestrlist = new List<string>();
        FileStream filestream = new FileStream(loadpath, FileMode.Open);
        try
        {


            using (StreamReader streamread = new StreamReader(filestream))
            {
                string line;
                // 必须为null 
                // 调用一次移动一行
                while ((line =streamread.ReadLine())!= null)
                {
                    Debug.Log(line);
                    templestrlist.Add(line);
                }

            }
            // 疑问----
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            filestream.Close();
        }
        char[] spliechar = new char[] {' ','\r','\t'};
        for (int i = 0; i < templestrlist.Count ; i++)
        {
            string  templestr = templestrlist[i];
            string[] strArray = templestr.Split(spliechar,StringSplitOptions.RemoveEmptyEntries);
            Debug.Log(strArray[0]);
            Debug.Log(strArray[1]);
            mInGameStr.Add(strArray[0],strArray[1]);
        }
       
    }
    void Start()
    {   
       //  路径 末尾 /../../../ 上三级目录 末尾 无 / ../../ 上级上一级 目录 （还是没搞懂）
        string loadpath = Application.dataPath + "/../../../" + PathConfig.mLocalInGameStrPath;
        if (File.Exists(loadpath))
        {
            Debug.Log("文件存在！！");
        }
        else
        {
            Debug.Log(loadpath);
            Debug.Log("文件不存在！！");
        }
        Debug.Log(Application.dataPath);

       LoadInGameStr();
    }

  
}
