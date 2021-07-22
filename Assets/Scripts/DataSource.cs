using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSource", menuName = "Data Source", order = 51)]
public class DataSource : ScriptableObject
{
    
    public string[] dataSets;

    public int[] dataSetsSizes;
}
