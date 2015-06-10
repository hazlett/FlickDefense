using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot]
public class Waves  {
    [XmlElement]
    List<List<EnemySpawner.SpawnParameters>> Settings = new List<List<EnemySpawner.SpawnParameters>>();

    [XmlIgnore]
    public Dictionary<int, List<EnemySpawner.SpawnParameters>> WaveSettings;
    private static Waves instance;
    public static Waves Instance
    {
        get
        {
            if (instance == null) instance = new Waves();
            return instance;
        }
        private set { }
    }

    public Waves() { }

    public List<EnemySpawner.SpawnParameters> GetWaveData(int waveNum)
    {
        if (WaveSettings.ContainsKey(waveNum)) return WaveSettings[waveNum];
        //else
        return WaveSettings[0];
    }

    internal void LoadWaves()
    {
        string path = "WaveSystem.xml";
        XmlSerializer serializer = new XmlSerializer(typeof(Waves));
        if (File.Exists(path))
        {
            Debug.Log(path);
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as Waves;
            }
            Debug.Log("Settings: " + Settings.Count);
        }
    }

    internal void SaveWaves()
    {
        Debug.Log("Saving Waves");
        instance.Settings = new List<List<EnemySpawner.SpawnParameters>>()
        {
            new List<EnemySpawner.SpawnParameters>()
        {
            new EnemySpawner.SpawnParameters(Enemies.Grunt, 10, 1.0f, 1.0f),
            new EnemySpawner.SpawnParameters(Enemies.Archer, 5, 1.0f, 1.0f)

        },
        new List<EnemySpawner.SpawnParameters>()
        {
            new EnemySpawner.SpawnParameters(Enemies.Grunt, 12, 1.0f, 1.0f),
            new EnemySpawner.SpawnParameters(Enemies.Archer, 7, 1.0f, 1.0f),
            new EnemySpawner.SpawnParameters(Enemies.Bomber, 1, 1.0f, 1.0f)
        }
    };
        XmlSerializer xmls = new XmlSerializer(typeof(Waves));
        using (FileStream stream = new FileStream("XMLTest.xml", FileMode.Create))
        {
            xmls.Serialize(stream, instance);
        }
    }

}
