using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject HeartPrefab;
    public Transform HeartStartPosition;
    
    private Stats _PlayerStats;
    private GameObject[] _Hearts;
    private GameObject _Canvas;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerStats = GetComponent<Stats>();
        _Hearts = new GameObject[_PlayerStats.Health];
        _Canvas = GameObject.Find("Canvas");
        for (int i = 0; i < _PlayerStats.Health; i++) 
        {
            Vector3 heartPosition = HeartStartPosition.position;
            heartPosition.x = (HeartStartPosition.position.x + (HeartPrefab.transform.localScale.x * 0.5f) + 1.0f) * (i + 1);
            _Hearts[i] = Instantiate(HeartPrefab, heartPosition, 
            Quaternion.identity, _Canvas.transform);
        }

        _PlayerStats.OnDamageTaken = (x) => 
        {
            _Hearts[_PlayerStats.Health].SetActive(false);
        };

        _PlayerStats.OnDied = () => 
        {
            SceneManager.LoadSceneAsync(2);
            //Destroy(gameObject);
        };
    }
}
