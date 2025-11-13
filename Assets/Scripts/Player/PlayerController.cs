using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    // publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;
    
    public float speed = 1f;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "Finish";
    public GameObject endScreen;
    // privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }
  
    void Update()
    {
        if (!_canRun) return;
        
        var _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;
        
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == tagToCheckEnemy)
        {
            EndGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheckEndLine)
        {
            EndGame();
        }
    }
    
    void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }
    
    public void StartToRun()
    {
        _canRun = true;
    }
    
    #region POWER UPS

    public void SetPowerUpText(string s)
    {
        //uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }
    #endregion
}
