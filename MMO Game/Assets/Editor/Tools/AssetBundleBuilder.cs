using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
public class AssetBundleBuilder : MonoBehaviour
{
    private static Dictionary<string, string> mMd5Dic = new Dictionary<string, string>();
    private const string mAssetBundleOutpath = Config.ASSETBUNDLEOUTPATH;
    private const string BUNDLE_EXTENSION = Config.BUNDLE_EXTENSION;
    // 主游戏物体以自己的名字命名 所需依赖由于会自动计算依赖所以一GUID命名 选择游戏物体进行打包
    [MenuItem("Tools/Build AssetBundle By Selected")]
    public static void BuildAssetBundleBySelected()
    {
        GameObject selectedgo = Selection.activeGameObject;
        if (selectedgo != null)
        {
            string mainassetname = selectedgo.name;
            string selectedgopath = AssetDatabase.GetAssetPath(selectedgo);
            GetFileDependcies(mainassetname, selectedgopath);
            string path = GetAssetBundleOutPath();
            //每次打包删除以前的
            DeleteOutPutFiles(path);


            //BuildPipeline.BuildAssetBundles(GetAssetBundleOutPath(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        else
        {
            throw new Exception("请选择需要打包的游戏物体！！！");
        }

    }
    public static void DeleteOutPutFiles(string path)
    {
        DirectoryInfo fileinfo = new DirectoryInfo(path);
        FileInfo[] files = fileinfo.GetFiles();
        if (files.Length > 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                files[i].Delete();
            }
        }
    }
    // 选中一个文件夹 对文件夹中的游戏物体进行打包
    [MenuItem("Tools/Build AssetBundle By Seleced Folder")]
    public static void BulidAssetBundleByFolder()
    {
        int selectid = Selection.activeInstanceID;
        string selectpath = AssetDatabase.GetAssetPath(selectid);
        int index = selectpath.LastIndexOf("/");
        string filterselectpath = selectpath.Substring(index, selectpath.Length - index);
        string folderpath = GetAppDataPath() + filterselectpath;
        string[] files = Directory.GetFiles(folderpath, "*.prefab", SearchOption.AllDirectories);
        if (files.Length > 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                string path = files[i].Replace("\\", "/");
                string selectedpath = path.Replace(Application.dataPath.Replace("Assets", ""), "");
                string mainassetname = Path.GetFileNameWithoutExtension(path);
                GetFileDependcies(mainassetname, selectedpath);
            }
            string outpath = GetAssetBundleOutPath();
            //每次打包删除以前的
            DeleteOutPutFiles(outpath);
            BuildPipeline.BuildAssetBundles(GetAssetBundleOutPath(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            GeneratorMD5Files();
        }
        else
        {
            throw new Exception("文件夹中不包含预制体！！！");
        }
    }
    public static string GetAssetBundleOutPath()
    {
        string outputpath = mAssetBundleOutpath + Getplatform();
        DirectoryInfo info = new DirectoryInfo(outputpath);
        if (!info.Exists)
        {
            Directory.CreateDirectory(outputpath);
        }
        return outputpath;
    }
    public static string GetAppDataPath()
    {
        return Application.dataPath;
    }
    public static void GetFileDependcies(string mainassetname, string selectedpath)
    {
        string selectedgopath = selectedpath;
        string[] paths = new string[] { selectedgopath };
        string[] dependcies = AssetDatabase.GetDependencies(paths);
        if (dependcies.Length > 0)
        {
            for (int i = 0; i < dependcies.Length; i++)
            {
                string assetbunldename = AssetDatabase.AssetPathToGUID(dependcies[i]);
                AssetImporter ai = AssetImporter.GetAtPath(dependcies[i]);
                if (selectedgopath == dependcies[i])
                {
                    ai.assetBundleName = mainassetname + BUNDLE_EXTENSION;
                    continue;
                }
                ai.assetBundleName = assetbunldename + BUNDLE_EXTENSION;
            }
        }
    }
    [MenuItem("Tools/Generate MD5 File")]
    public static void GeneratorMD5Files()
    {
        string filepath = GetAssetBundleOutPath();
        string[] files = Directory.GetFiles(filepath, "*.unity3d", SearchOption.AllDirectories);
        mMd5Dic.Clear();
        if (files.Length > 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                string filename = Path.GetFileName(files[i]);
                string md5str = GenerateMD5(files[i]);
                mMd5Dic.Add(filename, md5str);
            }
            if (mMd5Dic != null)
            {
                string localmd5path = GetAssetBundleOutPath() + "/" + "md5.txt";
                FileInfo file = new FileInfo(localmd5path);

                if (file.Exists)
                {
                    file.Delete();
                }
                FileStream filestream = new FileStream(localmd5path, FileMode.CreateNew);
                StreamWriter streamwriter = new StreamWriter(filestream, Encoding.UTF8);
                try
                {
                    foreach (KeyValuePair<string, string> kv in mMd5Dic)
                    {
                        streamwriter.Flush();
                        string line = kv.Key + " " + kv.Value;
                        streamwriter.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
                finally
                {
                    streamwriter.Close();
                    filestream.Close();
                }
            }
        }
        else
        {
            throw new Exception("文件不存在！！！");
        }
    }
    public static string GenerateMD5(string path)
    {
        StringBuilder hash = new StringBuilder();
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        FileStream filestream = File.OpenRead(path);
        byte[] hashbytes = md5.ComputeHash(filestream);
        for (int i = 0; i < hashbytes.Length; i++)
        {
            hash.Append(hashbytes[i].ToString("x2"));
        }
        filestream.Close();
        return hash.ToString();
    }
    [MenuItem("Tools/Test BuildAssetBundle")]
    public static void BuildAssetBundleTest()
    {
        GameObject seletgo = Selection.activeGameObject;
        if (seletgo == null) return;
        string seletpath = AssetDatabase.GetAssetPath(seletgo);
        string[] paths = new string[] { seletpath };
        string[] dependcies = AssetDatabase.GetDependencies(paths);
        AssetBundleBuild[] assetbundlebuilder = new AssetBundleBuild[1];
        assetbundlebuilder[0].assetBundleName = seletgo.name + ".unity3d";
        string[] assetname = new string[dependcies.Length];
        for (int i = 0; i < dependcies.Length; i++)
        {
            assetname[i] = dependcies[i];
        }
        for (int i = 0; i < assetname.Length; i++)
        {
            Debug.Log(assetname[i]);
        }
        assetbundlebuilder[0].assetNames = assetname;
        BuildPipeline.BuildAssetBundles(GetAssetBundleOutPath(), assetbundlebuilder);
    }
    [MenuItem("Tools/ Copy AssetBundle to StreamingAsset")]
    public static void CopyAssetBundleToStreamingAssets()
    {
        string sourcepath = mAssetBundleOutpath + Getplatform();
        string despath = Application.streamingAssetsPath;
        CopyDirectionFiles(sourcepath, despath);

    }
    public static string Getplatform()
    {
        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
        string platform = "";
        switch (target)
        {
            case BuildTarget.Android:
                platform = "Android";
                break;
            case BuildTarget.StandaloneOSXIntel:
                break;
            case BuildTarget.StandaloneOSXIntel64:
                break;
            case BuildTarget.StandaloneOSXUniversal:
            case BuildTarget.iOS:
                platform = "IOS";
                break;
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                platform = "Windows";
                break;
            default:
                break;
        }
        return platform;
    }
    public static void CopyDirectionFiles(string sourcepath, string despath)
    {
        if (!Directory.Exists(sourcepath))
        {
            throw new Exception("源文件路径不存在");
        }
        if (!Directory.Exists(despath))
        {
            Directory.CreateDirectory(despath);
        }
        CopyFile(sourcepath, despath);
        string[] directories = Directory.GetDirectories(sourcepath);
        for (int i = 0; i < directories.Length; i++)
        {
            string templedespath = despath + directories[i].Replace(sourcepath, "");
            CopyDirectionFiles(directories[i], templedespath);
        }
    }
    public static void CopyFile(string sourcepath, string despath)
    {
        string[] files = Directory.GetFiles(sourcepath);
        for (int i = 0; i < files.Length; i++)
        {
            string templedespath = despath + files[i].Replace(sourcepath, "");
            Debug.Log(templedespath);
            try
            {
                if (File.Exists(templedespath))
                {
                    File.Delete(templedespath);
                }
                File.Copy(files[i], templedespath);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
}


