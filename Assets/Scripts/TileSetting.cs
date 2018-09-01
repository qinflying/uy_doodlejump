using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Tile设置类，可序列化
/// </summary>
[Serializable]
public class TileSetting
{
    [Serializable]
    public class TileNormal
    {
        public float minHeight;
        public float maxHeight;
    }

    [Serializable]
    public class TileBroken
    {
        public float minHeight;
        public float maxHeight;
        public float dropSpeed;
    }

    [Serializable]
    public class TileOneTime
    {
        public float minHeight;
        public float maxHeight;
        public float dropSpeed;
    }

    [Serializable]
    public class TileSpring
    {
        public float minHeight;
        public float maxHeight;
    }

    [Serializable]
    public class TileHorzontalMove
    {
        public float minHeight;
        public float maxHeight;
        public float distance;
        public float speed;
    }

    [Serializable]
    public class TileVerticalMove
    {
        public float minHeight;
        public float maxHeight;
        public float distance;
        public float speed;
    }

    public TileNormal tileNormal;
    public TileBroken tileBroken;
    public TileOneTime tileOneTime;
    public TileSpring tileSpring;
    public TileHorzontalMove tileHorzontalMove;
    public TileVerticalMove tileVerticalMove;
}
