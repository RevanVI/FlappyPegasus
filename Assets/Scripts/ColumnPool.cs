using UnityEngine;
using System.Collections;

public class ColumnPool : MonoBehaviour
{
    public static ColumnPool instanceC;
    public GameObject[] bonusPrefab = new GameObject[3];
    public GameObject columnPrefab;                                 //The column game object.
    public int columnPoolSize = 8;                                  //How many columns to keep on standby.
    public float spawnRate = 4f;                                    //How quickly columns spawn.
    public float columnMin = -10f;                                   //Minimum y value of the column position.
    public float columnMax = -20f;                                  //Maximum y value of the column position.

    private GameObject[] columns;                                   //Collection of pooled columns.
    private int currentColumn = 0;                                  //Index of the current column in the collection.

    private Vector2 objectPoolPosition = new Vector2(-15, -25);     //A holding position for our unused columns offscreen.
    private float spawnXPosition = 15f;

    private float timeSinceLastSpawned;

    void Awake()
    {
        if (instanceC == null)
        {
            instanceC = this;
        }
        else if (instanceC != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timeSinceLastSpawned = spawnRate;

        //Initialize the columns collection.
        columns = new GameObject[columnPoolSize];
        //Loop through the collection... 
        for (int i = 0; i < columnPoolSize; i++)
        {
            //...and create the individual columns.
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
    }


    //This spawns columns as long as the game is not over.
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
            columns[currentColumn].gameObject.transform.Find("ColumnSpriteTop").gameObject.SetActive(true);
            columns[currentColumn].gameObject.transform.Find("ColumnSpriteDown").gameObject.SetActive(true);
            timeSinceLastSpawned = 0f;

            //Set a random y position for the column
            float spawnYPosition = Random.Range(columnMin, columnMax);

            //...then set the current column to that position.
            columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);

            int probability = Random.Range(1, 101); //вероятность создания бонуса
            if (probability>=80)
            CreateBonus(new Vector2(spawnXPosition, spawnYPosition));

            //Increase the value of currentColumn. If the new size is too big, set it back to zero
            currentColumn++;

            if (currentColumn >= columnPoolSize)
            {
                currentColumn = 0;
            }
        }
    }

    void CreateBonus(Vector2 positionColums)
    {
        // создание бонуса 
        Debug.Log("создание бонуса");
        Instantiate(bonusPrefab[Random.Range(0, 3)], new Vector2(positionColums.x, positionColums.y + 2f), Quaternion.identity);
    }
}