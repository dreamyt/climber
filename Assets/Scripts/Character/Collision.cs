using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    private bool onRightTopWall;
    private bool onLeftTopWall;
    private bool onRightBottomWall;
    private bool onLeftBottomWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public float groundcollisionRadius = 0.25f;
    public Vector2 bottomOffset, rightTopOffset, rightBottomOffset, leftTopOffset, leftBottomOffset; 
    private Color debugCollisionColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, groundcollisionRadius, groundLayer);
        
        onRightTopWall = Physics2D.OverlapCircle((Vector2)transform.position + rightTopOffset, collisionRadius, wallLayer);
        onRightBottomWall = Physics2D.OverlapCircle((Vector2)transform.position + rightBottomOffset, collisionRadius, wallLayer);
        onRightWall = onRightTopWall || onRightBottomWall;
       
        onLeftTopWall = Physics2D.OverlapCircle((Vector2)transform.position + leftTopOffset, collisionRadius, wallLayer);
        onLeftBottomWall = Physics2D.OverlapCircle((Vector2)transform.position + leftBottomOffset, collisionRadius, wallLayer);
        onLeftWall = onLeftTopWall || onLeftBottomWall;
        
        onWall = onLeftWall || onRightWall;
        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        var positions = new Vector2[] { bottomOffset, rightTopOffset, rightBottomOffset, leftTopOffset, leftBottomOffset};

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, groundcollisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightTopOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftTopOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position +rightBottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftBottomOffset, collisionRadius);
    }
}
