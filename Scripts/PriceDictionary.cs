using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceDictionary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, int> koszt = new Dictionary<string, int>(){
            {"Wall", 1},
            {"Spikes", 2},
            {"Arrow", 3}
        };
        
    }
}
