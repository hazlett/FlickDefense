using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class WaveSerializer {

    private static WaveSerializer instance = new WaveSerializer();
    internal static WaveSerializer Instance { get { return instance; } }

    public Waves waveSettings;

    private WaveSerializer()
    {
        LoadWaveSettings();
    }

    internal void LoadWaveSettings()
    {
        string path = "WaveSystem.xml";
        XmlSerializer serializer = new XmlSerializer(typeof(Waves));
        if (File.Exists(path))
        {
            Debug.Log(path);
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                waveSettings = serializer.Deserialize(stream) as Waves;
            }
        }
    }

}
