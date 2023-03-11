using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject RoomPrefab;
    public GameObject BossRoomPrefab;
    public GameObject EnemyPrefab;

    public int RoomCount = 10;
    public Vector2 RoomOffset;

    public float RoomXHalfExtent = 9.6f;
    public float RoomYUpHalfExtent = 4.5f;
    public float RoomYDownHalfExtent = 3.7f;

    public int MaxEnemyCount = 3;
    
    private List<Vector2> _TakenPositions;
    private Room[,] _Rooms;
    private int _GridSizeX = 4, _GridSizeY = 4;
    private const int DOOR_T = 1, DOOR_B = 2, DOOR_L = 3, DOOR_R = 4;
    private Vector2 _BossRoomLocation;
    private int _EnemyCount = 0;
    private GameObject _Grid;
    private GameManager _GameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _Rooms = new Room[_GridSizeX * 2, _GridSizeY * 2];
        _Rooms[_GridSizeX, _GridSizeY] = new Room(Vector3.zero);
        _GameManager.PlayerCellPosition = new Vector2Int(_GridSizeX, _GridSizeY);
        _TakenPositions = new List<Vector2>();
        _TakenPositions.Add(Vector2.zero);
        _Grid = GameObject.Find("Grid");

        GenerateRooms();
        GenerateDoors();
        _BossRoomLocation = GenerateBossRoom();

        DrawRooms();
    }

    private void GenerateRooms() 
    {
        for (int i = 0; i < RoomCount; i++)
        {
            Vector2 position = NewRoomPosition();

            // convert normalized position to world position
            Vector2 scaledPosition = new Vector2(position.x * RoomOffset.x,  position.y * RoomOffset.y);
            
            _Rooms[(int)position.x + _GridSizeX, (int)position.y + _GridSizeY] = new Room(scaledPosition);
            _TakenPositions.Add(position);
        }
    }

    private bool IsRoomPositionValid(Vector2 position) 
    {
        int neighbourCount = 0;

        if(_TakenPositions.Contains(position + Vector2.down)) neighbourCount++;
        if(_TakenPositions.Contains(position + Vector2.left)) neighbourCount++;
        if(_TakenPositions.Contains(position + Vector2.right)) neighbourCount++;
        if(_TakenPositions.Contains(position + Vector2.up)) neighbourCount++;
        
        return !_TakenPositions.Contains(position) && 
                position.x < _GridSizeX && position.x >= -_GridSizeX && 
                position.y < _GridSizeY && position.y >= -_GridSizeY &&
                neighbourCount < 2;
    }

    private Vector2 NewRoomPosition() 
    {
        Vector2 position = Vector2.zero;
        while(!IsRoomPositionValid(position)) 
        {
            int index = Mathf.RoundToInt(Random.value * (_TakenPositions.Count - 1)); // pick a random room
            int x = (int)_TakenPositions[index].x;
            int y = (int)_TakenPositions[index].y;

            bool horizontal = Random.value > 0.5f;
            bool positive = Random.value < 0.5f;

            if(horizontal)
            {
                if(positive) y++;
                else y--;
            }
            else 
            {
                if(positive) x++;
                else x--;
            }

            position = new Vector2(x, y);
        }

        return position;
    }

    private void DrawRooms() 
    {
        for (int x = 0; x < _Rooms.GetLength(0); x++)
        {
            for (int y = 0; y < _Rooms.GetLength(1); y++)
            {
                Room room = _Rooms[x, y];
                if(room == null) continue;
                GameObject instatiatedRoom = Instantiate(RoomPrefab, room.Position, Quaternion.identity, _Grid.transform);
                instatiatedRoom.name = $"Room {x}, {y}";

                for (int i = 0; i < 4; i++)
                {
                    if((room.DoorDirections & 1 << i) != 0)
                        instatiatedRoom.transform.GetChild(DOOR_T + i).gameObject.SetActive(true);
                }

                if(room.Position != new Vector3(_BossRoomLocation.x, _BossRoomLocation.y, 0.0f) &&
                    room.Position != Vector3.zero) 
                {
                    int enemyCount = Random.Range(1, MaxEnemyCount + 1);
                    for (int i = 0; i < enemyCount; i++) 
                    {
                        GameObject enemy = Instantiate(EnemyPrefab, GetRandomEnemyPosition(room.Position), Quaternion.identity, instatiatedRoom.transform.GetChild(6));
                        enemy.name = $"Enemy {_EnemyCount}";
                        _EnemyCount++;
                    }
                }
            }
        }

        Instantiate(BossRoomPrefab, _BossRoomLocation, Quaternion.identity);
    }

    private void GenerateDoors() 
    {
        for (int x = 0; x < _Rooms.GetLength(0); x++)
        {
            for (int y = 0; y < _Rooms.GetLength(1); y++)
            {
                if(_Rooms[x, y] == null) continue;

                if(y - 1 >= 0 && _Rooms[x, y - 1] != null) 
                    _Rooms[x, y].DoorDirections |= (int)DoorDirection.Bottom;

                if(y + 1 < _GridSizeY * 2 && _Rooms[x, y + 1] != null) 
                    _Rooms[x, y].DoorDirections |= (int)DoorDirection.Top;

                if(x - 1 >= 0 && _Rooms[x - 1, y] != null) 
                    _Rooms[x, y].DoorDirections |= (int)DoorDirection.Left;

                if(x + 1 < _GridSizeX * 2 && _Rooms[x + 1, y] != null) 
                    _Rooms[x, y].DoorDirections |= (int)DoorDirection.Right;
            }
        }
    }

    private bool IsBossRoomValid(Vector2 location) 
    {
        // NOTE: add rooms diagonal to the starting room to be invalid?
        return location == Vector2.zero || 
            (location.x - RoomOffset.x == 0 || 
            location.x + RoomOffset.x == 0 && location.y == 0) ||
            (location.y - RoomOffset.y == 0 || 
            location.y + RoomOffset.y == 0 && location.x == 0);
    }

    private Vector2 GenerateBossRoom() 
    {
        List<Vector2> possibleBossLocations = new List<Vector2>();

        foreach (var room in _Rooms)
        {
            if(room == null) continue;

            int doorCount = 0;
            if(room.HasDoor(DoorDirection.Top)) doorCount++;
            if(room.HasDoor(DoorDirection.Bottom)) doorCount++;
            if(room.HasDoor(DoorDirection.Left)) doorCount++;
            if(room.HasDoor(DoorDirection.Right)) doorCount++;

            if(doorCount == 1) possibleBossLocations.Add(room.Position);
        }

        Debug.Log(possibleBossLocations.Count);

        possibleBossLocations.RemoveAll(location => IsBossRoomValid(location));

        if(possibleBossLocations.Count > 0) 
        {
            int index = Random.Range(0, possibleBossLocations.Count);
            return possibleBossLocations[index];
        }

        Debug.LogError("invalid room layout");
        return Vector2.zero;
    }

    private Vector2 GetRandomEnemyPosition(Vector2 roomPosition) 
    {
        float xPosition = Random.Range(roomPosition.x - RoomXHalfExtent, roomPosition.x + RoomXHalfExtent);
        float yPosition = Random.Range(roomPosition.y - RoomYDownHalfExtent, roomPosition.y + RoomYUpHalfExtent);
        return new Vector2(xPosition, yPosition);
    }
}
