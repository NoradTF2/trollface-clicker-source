using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[System.Serializable]
public static class Save
{
    public static void SaveData(Clicker clicker)
    {
        string path = Path.Combine(Application.persistentDataPath, "savedata.dat");
        Debug.Log("Save file location = " + path);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        ClickerData data = new ClickerData(clicker);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static ClickerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "savedata.dat");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ClickerData data = formatter.Deserialize(stream) as ClickerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found. Path = " + path);
            return null;
        }
    }
}
