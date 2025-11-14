using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    // publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;
    
    [Header("TextMeshPro")]
    public TextMeshPro uiTextPowerUp;
    
    [Header("Coin Setup")]
    public GameObject coinCollector;
    
    [Header("Animation Setup")]
    public AnimatorManager animatorManager;
    
    [Header("Player Setup")]
    public float speed = 1f;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "Finish";
    public GameObject endScreen;
    public bool invencible;
    // privates
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 2;

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
        if (collision.transform.tag == tagToCheckEnemy)
        {
            if (!invencible)
            {
                MoveBack(collision.transform);
                EndGame(AnimatorManager.AnimationType.Death);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheckEndLine)
        {
            if(!invencible) EndGame();
        }
    }

    void MoveBack(Transform t)
    {
        t.DOMoveZ(1f, .3f).SetRelative();
    }
    
    void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.Idle)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }
    
    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.Run, _baseSpeedToAnimation);
    }
    
    #region POWER UPS

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/
        
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(float animationDuration)
    {
        transform.DOMoveY(_startPosition.y, animationDuration);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
