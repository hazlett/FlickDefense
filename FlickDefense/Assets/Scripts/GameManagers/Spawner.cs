using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Spawner {
    [XmlAttribute]
    public string type = "";
    //[XmlAttribute]
    //public int Num;
    //[XmlAttribute]
    //public float Freq;
    //[XmlAttribute]
    //public float Delay;

    public Spawner() { }
}
