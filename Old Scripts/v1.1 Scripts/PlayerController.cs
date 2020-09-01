using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    //Tilemaps
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap collisionTilemap;
    
    //Input System
    private PlayerMovement controls;

    //Cheeky Vector3
    private Vector3 vec;

    private void Awake(){
        controls = new PlayerMovement();
    }
    
    private void OnEnable(){
        controls.Enable();
    }

    private void OnDisable(){
        controls.Disable();
    }

    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction){
        vec = (Vector3)direction;
        vec = new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));

        if (!CanMove(direction)) return;
        transform.position += vec;
    }

    private bool CanMove(Vector2 direction){
        vec = (Vector3)direction;
        vec = new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));

        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + vec);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
}
