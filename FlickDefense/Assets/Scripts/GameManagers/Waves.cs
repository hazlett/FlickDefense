using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot]
public class Waves  {
    [XmlElement]
    public List<List<EnemySpawner.SpawnParameters>> Settings = new List<List<EnemySpawner.SpawnParameters>>();

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
        return WaveSettings[0];
    }

    internal void LoadWaves()
    {
        Debug.Log("LoadWaves");
        string path = Path.Combine(Application.persistentDataPath, "WaveSettings.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(Waves));
        if (File.Exists(path))
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                instance = serializer.Deserialize(stream) as Waves;
            }
            instance.WaveSettings = new Dictionary<int, List<EnemySpawner.SpawnParameters>>();

            for (int i = 0; i < instance.Settings.Count; i++)
            {
                instance.WaveSettings.Add(i, instance.Settings[i]);
            }
        }
    }

    internal void SaveWaves()
    {
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

        string path = Path.Combine(Application.persistentDataPath, "XMLTest.xml");
        XmlSerializer xmls = new XmlSerializer(typeof(Waves));
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            xmls.Serialize(stream, instance);
        }
    }

}
