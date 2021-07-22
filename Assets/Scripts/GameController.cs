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

    public List<Sprite> currentSpriteSet = new List<Sprite>();

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


        for(int i = 0; i < grid.transform.childCount; i++)
        {
            cellsInScene.Add(grid.transform.GetChild(i).transform.GetChild(0).gameObject);
            cellsInScene.Add(grid.transform.GetChild(i).transform.GetChild(1).gameObject);
            cellsInScene.Add(grid.transform.GetChild(i).transform.GetChild(2).gameObject);
        }

        CreateTask();
    }


    public void CreateTask()
    {
        int dataSet = Random.Range(0, dataSource.dataSets.Length);

        currentDataSet = new List<string>(dataSource.dataSets[dataSet].Split(','));
        
        currentTask = currentDataSet[Random.Range(0,currentDataSet.Count)];
        currentDataSet.Remove(currentTask);

        currentSpriteSet = GetCurrentSpriteSet(dataSet);

        taskLabel.GetComponent<Text>().text = "Find " + currentTask;
    }

    private List<Sprite> GetCurrentSpriteSet(int dataSet) {
        List<Sprite> list = new List<Sprite>();

        int multiplier = dataSet - 1 < 0 ? 0 : dataSource.dataSetsSizes[dataSet - 1];
        int setCount = dataSource.dataSetsSizes[dataSet];
        int startIndex = dataSet * multiplier;
        for (int i = startIndex; i <  + setCount; i++)
        {
            list.Add(spritesSet[i]);
        }

        return list;
    }


    public void IncreaseLevel()
    {
        level++;

    }

    private void RepositionCells()
    {
        
    }

}
