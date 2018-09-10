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
    private float moveSpeed;
    private float moveDistance;
    private Vector3 startPosition;
    private int direction;          //  移动方向0.left/up, 1.right/down

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

    // Active激活初始化
    void OnEnable()
    {
        startPosition = transform.position;
        InitTile();

    }

    void InitTile()
    {
        switch (Mode)
        {
            case TileMode.Broken:
                dropGravity = GameManager.Instance.tileSettings.tileBroken.gravityScale;
                break;
            case TileMode.OneTime:
                dropGravity = GameManager.Instance.tileSettings.tileOneTime.gravityScale;
                break;
            case TileMode.HorizontalMove:
                moveSpeed = GameManager.Instance.tileSettings.tileHorzontalMove.speed;
                moveDistance = GameManager.Instance.tileSettings.tileHorzontalMove.distance;
                break;
            case TileMode.VerticalMove:
                moveSpeed = GameManager.Instance.tileSettings.tileVerticalMove.speed;
                moveDistance = GameManager.Instance.tileSettings.tileVerticalMove.distance;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        HorizontalAndVerticalAutoMove();
    }

    void HorizontalAndVerticalAutoMove()
    {
        switch (Mode)
        {
            case TileMode.HorizontalMove:
                OnHorizontalAutoMove();
                break;
            case TileMode.VerticalMove:
                OnVerticalAutoMove();
                break;
        }
    }

    private void OnHorizontalAutoMove()
    {
        if (direction == 0)
        {
            transform.Translate(new Vector2(-moveSpeed * Time.deltaTime, 0));
            if (Vector3.Distance(transform.position, startPosition) >= moveDistance)
            {
                direction = 1;
            }
        }
        else if (direction == 1)
        {
            transform.Translate(new Vector2(moveSpeed * Time.deltaTime, 0));
            if (transform.position.x >= startPosition.x)
            {
                direction = 0;
            }
        }
    }

    private void OnVerticalAutoMove()
    {
        if (direction == 0)
        {
            transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
            if (Vector3.Distance(transform.position, startPosition) >= moveDistance)
            {
                direction = 1;
            }
        }
        else if (direction == 1)
        {
            transform.Translate(new Vector2(0, -moveSpeed * Time.deltaTime));
            if (transform.position.y <= startPosition.y)
            {
                direction = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Common.Tag.player)
        {
            GameObject oPlayer = collision.gameObject;
            if (oPlayer.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                JumpPlayer(oPlayer);
            }
        }

        if (collision.tag == Common.Tag.bottomborder)
        {
            ClearTile();
            GameManager.Instance.Recycle(gameObject, GameManager.SpawnType.Tile);
        }
    }

    private void JumpPlayer(GameObject oPlayer)
    {
        switch (Mode)
        {
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

    public void ClearTile()
    {
        dropGravity = 0;
        moveSpeed = 0;
        moveDistance = 0;
    }
}
