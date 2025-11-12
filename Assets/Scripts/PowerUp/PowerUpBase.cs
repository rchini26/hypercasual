using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : CollectableItemBase
{
 [Header("PowerUp")] 
 public float duration;

 protected override void OnCollect()
 {
  base.OnCollect();
  StartPowerUp();
 }

 protected virtual void StartPowerUp()
 {
  Debug.Log("PowerUp Start");
  Invoke(nameof(EndPowerUp), duration);
 }

 protected virtual void EndPowerUp()
 {
  Debug.Log("PowerUp End");
 }
}
