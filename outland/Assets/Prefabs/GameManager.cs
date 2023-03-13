using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int PlayerCellPosition;

    private void Start()
    {
        PlayerCellPosition = new Vector2Int(0, 0);
    }
}
