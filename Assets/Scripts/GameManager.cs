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


    //生成Tile
    public GameObject SpawnTile()
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
    public void RecycleTile(GameObject oTile) {
        oTile.SetActive(false);
        if (!tilePool.Contains(oTile)) {
            tilePool.Enqueue(oTile);
        }
    }
}
