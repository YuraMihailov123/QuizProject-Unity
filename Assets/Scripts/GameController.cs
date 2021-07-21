using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    #region Singleton
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            try
            {
                if (_instance == null) _instance = GameObject.Find("Root").transform.Find("GameController").GetComponent<GameController>();
            }
            catch { }
            return _instance;
        }
    }
    #endregion


    public GameObject cellPrefab;

    public int level = 1;

    public List<GameObject> cellsInScene = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //cellPrefab = Resources.Load<GameObject>("Prefabs/cell");
        level = 1;
        cellsInScene = new List<GameObject>();
    }

    public void GenerateObjectsInCells()
    {
        
    }

}
