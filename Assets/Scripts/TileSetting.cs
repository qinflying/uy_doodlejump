using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Tile设置类基类
/// </summary>
public abstract class SerializeTileBase
{
    public float minHeight; //随机最小高度
    public float maxHeight; //随机最大高度
    public float weight;    //随机权重 
    public TileMode mode;   //tile枚举类型

    //根据权重随机到自身
    public bool IsRandMeWithAllWeight(ref float allWeight)
    {
        allWeight -= this.weight;
        return allWeight <= 0;
    }

    //随机高度
    public float RandHeight() {
        return UnityEngine.Random.Range(minHeight, maxHeight);
    }
}

/// <summary>
/// Tile设置类，可序列化
/// </summary>
[Serializable]
public class TileSetting
{
    [Serializable]
    public class TileNormal : SerializeTileBase
    {
    }

    [Serializable]
    public class TileBroken : SerializeTileBase
    {
        public float gravityScale; //下落重力
    }

    [Serializable]
    public class TileOneTime : SerializeTileBase
    {
        public float gravityScale; //下落重力
    }

    [Serializable]
    public class TileSpring : SerializeTileBase
    {
        public float jumpFactor;  //施加弹跳系数
    }

    [Serializable]
    public class TileHorzontalMove : SerializeTileBase
    {
        public float distance;  //移动距离
        public float speed;     //移动速度
    }

    [Serializable]
    public class TileVerticalMove : SerializeTileBase
    {
        public float distance;  //移动距离
        public float speed;     //移动速度
    }

    public TileNormal tileNormal;
    public TileBroken tileBroken;
    public TileOneTime tileOneTime;
    public TileSpring tileSpring;
    public TileHorzontalMove tileHorzontalMove;
    public TileVerticalMove tileVerticalMove;

    private float _weight;
    public void InitCalWeight()
    {
        _weight = 0;
        _weight += tileNormal.weight;
        _weight += tileBroken.weight;
        _weight += tileOneTime.weight;
        _weight += tileSpring.weight;
        _weight += tileHorzontalMove.weight;
        _weight += tileVerticalMove.weight;
    }

    public TileMode GetTileModeByRandWeight()
    {
        float randWeiget = UnityEngine.Random.Range(0, _weight);
        SerializeTileBase[] tiles = { 
            tileNormal, tileBroken, 
            tileOneTime, tileSpring, 
            tileHorzontalMove, tileVerticalMove
        };

        TileMode mode = TileMode.Normal;
        foreach (SerializeTileBase oSerializeTile in tiles) {
            if (oSerializeTile.IsRandMeWithAllWeight(ref randWeiget)) {
                mode = oSerializeTile.mode;
                break;
            }
        }
        return mode;
    }
}
