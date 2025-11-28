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

    [SerializeField] BounceHelper _bounceHelper;
    
    [Header("Player Setup")]
    public float speed = 1f;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "Finish";
    public GameObject endScreen;
    public bool invencible;

    [Header("Limits")] 
    public float limit = 3;
    // privates
    private bool _canRun;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 2;

    void Start()
    {
        _startPosition = transform.position;
        // Start player with scale 0
        transform.DOScale(0, 0.3f).SetLoops(2, LoopType.Yoyo);
        ResetSpeed();
    }
  
    void Update()
    {
        if (!_canRun) return;
        
        var pos = target.position;
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        pos.x = Mathf.Clamp(pos.x, -limit, limit);
            
        transform.position = Vector3.Lerp(transform.position, pos, lerpSpeed * Time.deltaTime);
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

    public void Bounce()
    {
        _bounceHelper.Bounce();
    }
    
    #region POWER UPS

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        Bounce();
        _currentSpeed = f;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b = true)
    {
        Bounce();
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        Bounce();
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(float animationDuration)
    {
        transform.DOMoveY(_startPosition.y, animationDuration);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        Bounce();
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
