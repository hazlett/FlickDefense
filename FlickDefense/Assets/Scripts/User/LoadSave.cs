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
    public UserData[] Users = new UserData[3];

    public void Save(string path)
    {
        Debug.Log("Saving");
        XmlSerializer serializer = new XmlSerializer(typeof(LoadSave));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public void Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LoadSave));
        if (File.Exists(path))
        {
            Debug.Log(path);
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as LoadSave;
            }
        }
    }
}
