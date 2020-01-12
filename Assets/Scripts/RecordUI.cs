using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : MonoBehaviour
{ 
    public Text[] arrNames;
    public Text[] arrScores;
    void Start()
    {
        List<string[]> data = DataHandler.readData();
        for (int i = 0; i < data.Count; ++i)
        {
            arrNames[i].text = data[i][0];
            arrScores[i].text = data[i][1];
        }

    }
}
