using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

[XmlRoot]
public class Waves  {
    [XmlElement]
    public List<Wave> WaveSettings;

    public Waves() { }

}
