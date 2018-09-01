using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //tile缓存池
    private Queue<GameObject> tilePool = new Queue<GameObject>();
    private int _tileRecordID = 0;
    public GameObject tilePrefab;
    public TileSetting titleSettings;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start() {
        //初始化Tile权重
        titleSettings.InitCalWeight();
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
    private void RecycleTile(GameObject oTile) {
        oTile.SetActive(false);
        if (!tilePool.Contains(oTile)) {
            tilePool.Enqueue(oTile);
        }
    }

    //生成对象
    public GameObject Spawn(SpawnType spawnType){
        GameObject go = null;
        switch (spawnType) { 
            case SpawnType.Tile:
                go = SpawnTile();
                break;
        }

        return go;
    }

    //回收对象
    public void Recycle(GameObject go, SpawnType spawnType) { 
        switch(spawnType){
            case SpawnType.Tile:
                RecycleTile(go);
                break;
        }
    }

    public enum SpawnType{
        Tile,
    }
}
