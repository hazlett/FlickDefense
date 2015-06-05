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
        if (instance == null)
        instance = this;
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
            Debug.Log("Waves: " + waveSettings.WaveSettings.ToString());
            Debug.Log("Spawners[1]: " + waveSettings.WaveSettings[1].Spawners.Count);
            Debug.Log(waveSettings.WaveSettings[0].Spawners.ToString());
            Debug.Log("Spawner: " + waveSettings.WaveSettings[0].Spawners[0].type);
        }
    }

}
