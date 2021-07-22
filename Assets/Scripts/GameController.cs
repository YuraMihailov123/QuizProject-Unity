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

    public GameObject grid;
    public GameObject taskLabel;

    public int level = 1;

    public List<GameObject> cellsInScene = new List<GameObject>();

    public Sprite[] spritesSet;

    // Start is called before the first frame update
    void Start()
    {
        grid = transform.Find("Grid").gameObject;
        taskLabel = transform.Find("Canvas").Find("task").gameObject;
        level = 1;
        cellsInScene = new List<GameObject>();


        spritesSet = Resources.LoadAll<Sprite>("Sprites");
    }

    public void IncreaseLevel()
    {
        level++;

    }

    private void RepositionCells()
    {
        
    }

}
