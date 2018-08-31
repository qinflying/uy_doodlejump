using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileMode : byte
{
    Normal = 0,
    Broken,
    OneTime,
    Spring,
    HorizontalMove,
    VerticalMove,
}
public class Tile : MonoBehaviour
{
    public Sprite[] tileSprites;        //精灵集合
    private float dropGravity;           //下落重力，非必须
    public TileMode mode;             //模式

    public TileMode Mode
    {
        get { return mode; }
        set
        {
            mode = value;
            int index = (int)mode;
            if (index >= tileSprites.Length)
            {
                index = tileSprites.Length - 1;
            }
            if (index >= 0)
            {
                GetComponent<SpriteRenderer>().sprite = tileSprites[index];
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        InitTile(mode);
    }

    void InitTile(TileMode tileMode) {
        Mode = tileMode;
        switch (Mode) { 
            case TileMode.Broken:
                dropGravity = 1.5f;
                break;
            case TileMode.OneTime:
                dropGravity = 1.5f;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            GameObject oPlayer = collision.gameObject;
            if (oPlayer.GetComponent<Rigidbody2D>().velocity.y <= 0) {
                JumpPlayer(oPlayer);
            }
        }
    }

    private void JumpPlayer(GameObject oPlayer) {
        Debug.Log(Mode);
        switch (Mode) { 
            case TileMode.Normal:
                oPlayer.GetComponent<Player>().ToJumpAction();
                break;
            case TileMode.Broken:
                GetComponent<Rigidbody2D>().gravityScale = dropGravity;
                break;
            case TileMode.OneTime:
                GetComponent<Rigidbody2D>().gravityScale = dropGravity;
                oPlayer.GetComponent<Player>().ToJumpAction();
                break;
            case TileMode.Spring:
                oPlayer.GetComponent<Player>().ToJumpAction(1.3f);
                break;
            case TileMode.HorizontalMove:
                oPlayer.GetComponent<Player>().ToJumpAction();
                break;
            case TileMode.VerticalMove:
                oPlayer.GetComponent<Player>().ToJumpAction();
                break;
            default:
                break;
        }
    }
}
