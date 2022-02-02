using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanObjects : MonoBehaviour
{
    public GameObject[] objectsSpawn;
    public GameObject[] holeSpawn;
    private GameManager gameManager;

    //Tiempo de spawneo de cubos
    private float timeToSpawnCubesMin = 2f;
    public float _timeToSpawnCubesMax = 10.0f;
    private float TimeToSpawnCubesMax
    {
        get { return _timeToSpawnCubesMax; }
        set { _timeToSpawnCubesMax = Mathf.Clamp(value, timeToSpawnCubesMin, _timeToSpawnCubesMax); }
    }
    float timeToSpawnCubes; //tiempo que debe transcurrir actualmente antes de spawnear otro cubo
    float timeToSpanwCubesDouble; //tiempo que debe transcurrir actualmente antes de spawnear otro cubo doble


    //Tiempo de spawneo de agujeros
    private float timeToSpawnHolesMin = 2f;
    public float _timeToSpawnHolesMax = 6.0f;
    private float TimeToSpawnHolesMax
    {
        get { return _timeToSpawnHolesMax; }
        set { _timeToSpawnHolesMax = Mathf.Clamp(value, timeToSpawnHolesMin, _timeToSpawnHolesMax); }
    }
    float timeToSpawnHoles; //tiempo que debe transcurrir actualmente antes de spawnear otro agujero

    //Tiempo de spawneo de objetos en mov
    private float timeToSpawnMoveObjectsMin = 3.5f;
    public float _timeToSpawnMoveObjectsMax = 9f;
    private float TimeToSpawnMoveObjectsMax
    {
        get { return _timeToSpawnMoveObjectsMax; }
        set { _timeToSpawnMoveObjectsMax = Mathf.Clamp(value, timeToSpawnMoveObjectsMin, _timeToSpawnMoveObjectsMax); }
    }
    float timeToSpawnMoveObjects; //tiempo que debe transcurrir actualmente antes de spawnear otro agujero


    private float rangeSpawnX = 3f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToSpawnCubes <= 0)
        {
            StartCoroutine(ObjectsSpawner());
        }
        if (timeToSpawnHoles <= 0 && gameManager.Score > 1)
        {
            StartCoroutine(HoleSpawner());
        }
        if (gameManager.Score >= 25 && timeToSpawnMoveObjects <= 0)
        {
            StartCoroutine(BoxSpawnerMove());
        }
        if (gameManager.Score >= 50 && timeToSpanwCubesDouble <= 0)
        {
            StartCoroutine(SpawnerDoubleBox());
        }
    }



    public Queue<GameObject> objectStatic = new Queue<GameObject>();
    public Queue<GameObject> doubleBox = new Queue<GameObject>();
    public Queue<GameObject> moveObj = new Queue<GameObject>();
    public Queue<GameObject> holeStatic = new Queue<GameObject>();

    /// <summary>
    /// Temporizador e instanciador de spawner de objetos estaticos
    /// </summary>
    /// <returns timeToSpawnCubes>tiempo random entre el min y max en el que deberan spawnear </returns>
    IEnumerator ObjectsSpawner()
    {
        int maxObjects = 0;


        while (gameManager.states == GameStates.inGame && maxObjects < 5)
        {
            GameObject spawnStaticBox;

            Vector3 posInicial = new Vector3(Random.Range(-rangeSpawnX, rangeSpawnX), this.transform.position.y, this.transform.position.z);

            spawnStaticBox = Instantiate(objectsSpawn[0], posInicial, transform.rotation);

            objectStatic.Enqueue(spawnStaticBox);
            spawnStaticBox.SetActive(false);

            maxObjects++;
        }

        while (gameManager.states == GameStates.inGame && maxObjects >= 5)
        {
            GameObject spawnStaticBox;

            Vector3 posInicial = new Vector3(Random.Range(-rangeSpawnX, rangeSpawnX), this.transform.position.y, this.transform.position.z);

            timeToSpawnCubes = Random.Range(timeToSpawnCubesMin, TimeToSpawnCubesMax);

            spawnStaticBox = objectStatic.Dequeue();

            spawnStaticBox.SetActive(true);
            spawnStaticBox.transform.position = posInicial;

            yield return new WaitForSecondsRealtime(timeToSpawnCubes);
            TimeToSpawnCubesMax -= 0.01f;
        }

    }

    /// <summary>
    /// Temporizador e instanciador de spawner de objetos estaticos
    /// </summary>
    /// <returns timeToSpawnCubes>tiempo random entre el min y max en el que deberan spawnear </returns>
    IEnumerator SpawnerDoubleBox()
    {
        int maxObjects = 0;

        while (gameManager.states == GameStates.inGame && maxObjects < 5)
        {
            GameObject spawnDoubleBox;

            Vector3 posInicial = new Vector3(3f, this.transform.position.y, this.transform.position.z);

            spawnDoubleBox = Instantiate(objectsSpawn[1], posInicial, transform.rotation);

            doubleBox.Enqueue(spawnDoubleBox);
            spawnDoubleBox.SetActive(false);

            maxObjects++;
        }

        while (gameManager.states == GameStates.inGame && maxObjects >= 5)
        {
            GameObject spawnDoubleBox;

            Vector3 posInicial = new Vector3(3f, this.transform.position.y, this.transform.position.z);

            timeToSpanwCubesDouble = Random.Range(timeToSpawnCubesMin, TimeToSpawnCubesMax);

            spawnDoubleBox = doubleBox.Dequeue();

            spawnDoubleBox.SetActive(true);
            spawnDoubleBox.transform.position = posInicial;

            yield return new WaitForSecondsRealtime(timeToSpanwCubesDouble * 2);
        }
    }

    /// <summary>
    ///  Temporizador e instanciador de Spawner de objetos en movimiento
    /// </summary>
    /// <returns> 5 seg fijos en tiempo real </returns>
    IEnumerator BoxSpawnerMove()
    {
        int maxObjects = 0;

        while (gameManager.states == GameStates.inGame && maxObjects < 4)
        {
            GameObject spawnMoveObj;
            GameObject spawnMoveObj2;
            Vector3 posInicial = new Vector3(Random.Range(-rangeSpawnX, rangeSpawnX), this.transform.position.y, this.transform.position.z);
            Vector3 posInicial2 = new Vector3(Random.Range(-rangeSpawnX, rangeSpawnX), -2f, this.transform.position.z);

            spawnMoveObj = Instantiate(objectsSpawn[2], posInicial, transform.rotation);
            spawnMoveObj2 = Instantiate(objectsSpawn[3], posInicial2, transform.rotation);

            moveObj.Enqueue(spawnMoveObj);
            moveObj.Enqueue(spawnMoveObj2);
            spawnMoveObj.SetActive(false);
            spawnMoveObj2.SetActive(false);

            maxObjects++;
        }

        while (gameManager.states == GameStates.inGame && maxObjects >= 4)
        {
            GameObject spawnMoveObj;

            timeToSpawnMoveObjects = Random.Range(timeToSpawnMoveObjectsMin, TimeToSpawnMoveObjectsMax);

            spawnMoveObj = moveObj.Dequeue();

            spawnMoveObj.SetActive(true);

            yield return new WaitForSecondsRealtime(timeToSpawnMoveObjects);
            TimeToSpawnMoveObjectsMax -= 0.05f;
        }
    }

    /// <summary>
    /// Temporizador e instanciador de agujeros
    /// </summary>
    /// <returns timeToSpawnHoles>Tiempo random entre el min y el max en el que instanciara obj de tipo agujeros</returns>
    IEnumerator HoleSpawner()
    {
        int maxObjects = 0;

        while (gameManager.states == GameStates.inGame && maxObjects < 5)
        {
            GameObject spawnHole;
            Vector3 posInicial = new Vector3(this.transform.position.x, -3.94f, this.transform.position.z);

            spawnHole = Instantiate(holeSpawn[0], posInicial, transform.rotation);
            holeStatic.Enqueue(spawnHole);
            spawnHole.SetActive(false);

            maxObjects++;
        }

        while (gameManager.states == GameStates.inGame && maxObjects >= 5)
        {
            GameObject spawnHole;
            Vector3 posInicial = new Vector3(this.transform.position.x, -3.94f, this.transform.position.z);
            timeToSpawnHoles = Random.Range(timeToSpawnHolesMin, TimeToSpawnHolesMax);

            spawnHole = holeStatic.Dequeue();

            spawnHole.SetActive(true);
            spawnHole.transform.position = posInicial;

            yield return new WaitForSecondsRealtime(timeToSpawnHoles*0.6f);
            TimeToSpawnHolesMax -= 0.01f;
        }
    }
}