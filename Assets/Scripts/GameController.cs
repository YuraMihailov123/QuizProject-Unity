using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public DataSource dataSource;

    public int level = 1;

    public string currentTask = "";
    public List<string> currentDataSet;

    public List<GameObject> cellsInScene = new List<GameObject>();

    public Sprite[] spritesSet;

    // Start is called before the first frame update
    void Start()
    {
        grid = transform.Find("Grid").gameObject;
        taskLabel = transform.Find("Canvas").Find("task").gameObject;
        level = 1;
        cellsInScene = new List<GameObject>();

        dataSource = Resources.Load<DataSource>("ScriptableObjects/DataSource");
        spritesSet = Resources.LoadAll<Sprite>("Sprites");

        CreateTask();
    }


    public void CreateTask()
    {
        int dataSet = Random.Range(0, dataSource.dataSets.Length);

        currentDataSet = new List<string>(dataSource.dataSets[dataSet].Split(','));
        
        currentTask = currentDataSet[Random.Range(0,currentDataSet.Count)];
        currentDataSet.Remove(currentTask);

        taskLabel.GetComponent<Text>().text = "Find " + currentTask;
    }

    public void IncreaseLevel()
    {
        level++;

    }

    private void RepositionCells()
    {
        
    }

}
