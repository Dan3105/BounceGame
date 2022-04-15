using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
public static class SaveSystem
{
    public const string KEY_SYSTEM = "SaveDataGame";
    public static void SavePlayer()
    {
        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "saveFile.txt";
        //FileStream stream = new FileStream(path, FileMode.Create);
        //formatter.Serialize(stream, data);
        //stream.Close();
        GameStatusData data = new GameStatusData(1);
        string formatter = JsonUtility.ToJson(data);
        Debug.Log(formatter);
        PlayerPrefs.SetString(KEY_SYSTEM, formatter);
    }

    public static GameStatusData LoadFile()
    {
        //string path = Application.persistentDataPath + "saveFile.txt";

        //if(!File.Exists(path))
        //{
        //    Debug.LogError("file is null");
        //    return null;
        //}    
        //else
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    FileStream stream = new FileStream(path, FileMode.Open);
        //    GameStatusData data = formatter.Deserialize(stream) as GameStatusData;
        //    stream.Close();
        //    return data;
        //}

        if (!PlayerPrefs.HasKey(KEY_SYSTEM))
            return null;
        string formatter = PlayerPrefs.GetString(KEY_SYSTEM);
        GameStatusData statusData = JsonUtility.FromJson<GameStatusData>(formatter);
        
        return statusData;
    }

    
}
