using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FlyerLocationsManager : MonoBehaviour {

    private FlyerLocation[] locations;
	void Start () {
        locations = GetComponentsInChildren<FlyerLocation>();
	}
	
    public FlyerLocation AssignLocation(GameObject go)
    {
        List<FlyerLocation> availableLocations = new List<FlyerLocation>();
        foreach(FlyerLocation location in locations)
        {
            if (!location.Taken)
            {
                availableLocations.Add(location);
            }
        }
        if (availableLocations.Count > 0)
        {
            int index = Random.Range(0, availableLocations.Count - 1);
            if (availableLocations[index].Assign(go))
            {
                return availableLocations[index];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
