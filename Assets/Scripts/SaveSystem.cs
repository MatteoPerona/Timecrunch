using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SaveUser (UserData user)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, user);
        stream.Close();
    }

    public static UserData LoadUser()
    {
        string path = Application.persistentDataPath + "/user.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserData user = formatter.Deserialize(stream) as UserData;
            stream.Close();

            return user;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
