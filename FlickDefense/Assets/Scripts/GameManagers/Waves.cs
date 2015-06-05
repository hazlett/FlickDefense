using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot]
public class Waves  {
    [XmlElement]
    public Dictionary<int, EnemySpawner.SpawnParameters> WaveSettings;
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

    public EnemySpawner.SpawnParameters GetWaveData(int waveNum)
    {
        if (WaveSettings.ContainsKey(waveNum)) return WaveSettings[waveNum];
        //else
        return WaveSettings[0];
    }

}
