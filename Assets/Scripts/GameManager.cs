using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //tile缓存池
    private Queue<GameObject> tilePool = new Queue<GameObject>();
    private int _tileRecordID = 0;
    public GameObject tilePrefab;           //tile预制体
    public TileSetting tileSettings;       //tile配置
    public int initTileCount = 50;          //初始Tile数量
    private float currentTileHeight = 0;    //当前生成tile的总高度

    public GameState gameState = GameState.Running;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        //初始化Tile权重
        tileSettings.InitCalWeight();
        StartGenerateTiles();
    }

    void StartGenerateTiles()
    {
        for (int i = 0; i < initTileCount; i++)
        {
            GenerateOneTileInMap();
        }
    }

    void GenerateOneTileInMap() {
        SerializeTileBase oSerializeTileConfig = tileSettings.GetTileConfigByRandWeight();
        currentTileHeight += oSerializeTileConfig.RandHeight();
        GameObject oTile = Spawn(SpawnType.Tile);
        oTile.GetComponent<Tile>().Mode = oSerializeTileConfig.mode;
        float x = Random.Range(-4.5f, 4.5f);
        oTile.transform.position = new Vector3(x, currentTileHeight);
        oTile.SetActive(true);
    }

    //生成Tile
    private GameObject SpawnTile()
    {
        GameObject oTile;
        if (tilePool.Count > 0)
        {

            oTile = tilePool.Dequeue();
        }
        else
        {
            oTile = Instantiate(tilePrefab, transform);
            _tileRecordID++;
            oTile.name = _tileRecordID.ToString();
            oTile.SetActive(false);
        }
        return oTile;
    }

    //回收Tile
    private void RecycleTile(GameObject oTile)
    {
        oTile.SetActive(false);
        oTile.GetComponent<Rigidbody2D>().gravityScale = 0;
        if (!tilePool.Contains(oTile))
        {
            tilePool.Enqueue(oTile);
        }

        GenerateOneTileInMap();
    }

    //生成对象
    public GameObject Spawn(SpawnType spawnType)
    {
        GameObject go = null;
        switch (spawnType)
        {
            case SpawnType.Tile:
                go = SpawnTile();
                break;
        }

        return go;
    }

    //回收对象
    public void Recycle(GameObject go, SpawnType spawnType)
    {
        switch (spawnType)
        {
            case SpawnType.Tile:
                RecycleTile(go);
                break;
        }
    }

    public bool IsRunning() {
        return gameState == GameState.Running;
    }

    public void GameOver() {
        gameState = GameState.GameOver;
    }

    public enum SpawnType
    {
        Tile,
    }

    public enum GameState { 
        Paused,
        Running,
        GameOver,
    }
}
