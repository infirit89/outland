using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorDirection
{
    Top = 1 << 0,
    Bottom = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3
}

public class Room
{
    public Vector3 Position { get; private set; }
    public byte DoorDirections;

    public Room(Vector3 position) 
    {
        Position = position;
    }

    public bool HasDoor(DoorDirection doorDirection) => (DoorDirections & (int)doorDirection) != 0;  
}
