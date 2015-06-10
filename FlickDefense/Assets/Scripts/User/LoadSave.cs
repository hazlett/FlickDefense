using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("UserContainer")]
public class LoadSave
{

    private static LoadSave instance = new LoadSave();
    public static LoadSave Instance
    {
        get
        {
            return instance;
        }
    }

    [XmlArray("Users"), XmlArrayItem("User")]
    public List<UserData> Users = new List<UserData>();

    private UserData blank1 = new UserData(), blank2 = new UserData(), blank3 = new UserData();

    public void Save(string path)
    {
        Debug.Log("LoadSave. Saving to " + path);
        XmlSerializer serializer = new XmlSerializer(typeof(LoadSave));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public void Load(string path)
    {
        Debug.Log("LoadSave. Loading: " + path);
        XmlSerializer serializer = new XmlSerializer(typeof(LoadSave));
        if (File.Exists(path))
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as LoadSave;
            }
        }
    }

    public void BlankList()
    {
        Users.Add(blank1);
        Users.Add(blank2);
        Users.Add(blank3);
    }
}
