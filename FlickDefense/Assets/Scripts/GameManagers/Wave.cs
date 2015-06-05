using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Wave {
    //[XmlAttribute]
    //public int WaveNumber = 0;
    [XmlElement]
    public List<Spawner> Spawners;
   
    public Wave() { }

}
