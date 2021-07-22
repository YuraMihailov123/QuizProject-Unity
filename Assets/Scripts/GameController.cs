using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    public Image fader;
    public Image loader;

    public DataSource dataSource;

    public int level = 1;

    public int currentTaskIndex = 0;

    public string currentTask = "";

    public List<string> currentDataSet;
    public List<string> previousTasks;

    public List<Sprite> currentSpriteSet = new List<Sprite>();

    public List<GameObject> cellsInScene = new List<GameObject>();

    public Sprite[] spritesSet;

    public bool isPlaying = false;
    

    // Start is called before the first frame update
    void Start()
    {
        grid = transform.Find("Grid").gameObject;
        taskLabel = transform.Find("Canvas").Find("task").gameObject;
        fader = transform.Find("Canvas").Find("fader").GetComponent<Image>();
        loader = transform.Find("Canvas").Find("loader").GetComponent<Image>();
        level = 1;
        cellsInScene = new List<GameObject>();
        previousTasks = new List<string>();
        isPlaying = true;
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
        currentTaskIndex = currentDataSet.IndexOf(currentTask);

        if (previousTasks.Count >= 8)
        {
            previousTasks.Clear();
        }

        while (previousTasks.Contains(currentTask))
        {
            currentTask = currentDataSet[Random.Range(0, currentDataSet.Count)];
            currentTaskIndex = currentDataSet.IndexOf(currentTask);
        }
        previousTasks.Add(currentTask);
        currentDataSet.Remove(currentTask);

        currentSpriteSet = GetCurrentSpriteSet(dataSet);

        taskLabel.GetComponent<Text>().text = "Find " + currentTask;

        FillCellsWithObjects();
    }

    public void FillCellsWithObjects()
    {
        int indexCellWithCorrectAnswer = Random.Range(0, level * 3 - 1);
        Debug.Log(indexCellWithCorrectAnswer);
        cellsInScene[indexCellWithCorrectAnswer].transform.Find("object").GetComponent<SpriteRenderer>().sprite = currentSpriteSet[currentTaskIndex];
        cellsInScene[indexCellWithCorrectAnswer].GetComponent<OnSpriteClick>().myObjectValue = currentTask;
        Debug.Log(indexCellWithCorrectAnswer);
        currentSpriteSet.RemoveAt(currentTaskIndex);

        for(int i = 0; i < level * 3; i++)
        {
            if (i == indexCellWithCorrectAnswer)
                continue;
            int rndIndx = Random.Range(0, currentSpriteSet.Count);
            Debug.Log(i + " - test");
            cellsInScene[i].transform.Find("object").GetComponent<SpriteRenderer>().sprite = currentSpriteSet[rndIndx];
            cellsInScene[i].GetComponent<OnSpriteClick>().myObjectValue = "";
            currentSpriteSet.RemoveAt(rndIndx);
        }
    }

    private List<Sprite> GetCurrentSpriteSet(int dataSet) {
        List<Sprite> list = new List<Sprite>();

        int multiplier = dataSet - 1 < 0 ? 0 : dataSource.dataSetsSizes[dataSet - 1];
        int setCount = dataSource.dataSetsSizes[dataSet];
        int startIndex = dataSet * multiplier;
        Debug.Log(startIndex + ":" + setCount);
        for (int i = startIndex; i < startIndex + setCount; i++)
        {
            list.Add(spritesSet[i]);
        }
        Debug.Log("YEP: " + list.Count);
        return list;
    }


    public void IncreaseLevel()
    {
        if(level == 3)
        {
            isPlaying = false;
            StartCoroutine("ShowEndScreen");
            return;
        }
        level++;
        grid.transform.GetChild(level-1).gameObject.SetActive(true);
        CreateTask();
    }

    IEnumerator ShowEndScreen()
    {
        fader.gameObject.SetActive(true);
        fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 0);
        fader.DOFade(0.5f, 0.5f);
        yield return null;
    }

    IEnumerator CloseEndScreen()
    {

        fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 0.5f);
        fader.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        fader.gameObject.SetActive(false);

        loader.gameObject.SetActive(true);
        loader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 0);
        loader.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);

        level = 1;
        isPlaying = true;
        grid.transform.GetChild(1).gameObject.SetActive(false);
        grid.transform.GetChild(2).gameObject.SetActive(false);
        CreateTask();

        yield return new WaitForSeconds(0.1f);

        loader.color = new Color(fader.color.r, fader.color.g, fader.color.b, 1);
        loader.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        loader.gameObject.SetActive(false);
        
    }


    public void RestartGame()
    {
        StartCoroutine("CloseEndScreen");
        
    }

}
