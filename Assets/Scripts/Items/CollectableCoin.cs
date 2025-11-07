using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : CollectableItemBase
{
   protected override void OnCollect()
   {
      base.OnCollect();
      ItemManager.Instance.AddCoins();
   }
}
