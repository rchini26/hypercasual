using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using DG.Tweening;
using System.Linq;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<CollectableCoin> items = new List<CollectableCoin>();

    [Header("Animation")] 
    public float scaleDuration = 0.1f;
    public float scaleTimeBetweenPieces = 0.05f;
    public Ease ease = Ease.OutBack;

    public void RegisterCoin(CollectableCoin i)
    {
        if (i != null && !items.Contains(i))
        {
            items.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void StartAnimation()
    {
        StartCoroutine(ScalePiecesByTime());
    }
    
    IEnumerator ScalePiecesByTime()
    {
        Sort();
        // Remove destroyed coins
        items = items.Where(i => i != null).ToList();
        yield return null;
        
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                items[i].transform.DOScale(1, scaleDuration).SetEase(ease);
                yield return new WaitForSeconds(scaleTimeBetweenPieces);   
            }
        }   
    }

    void Sort()
    {
        items = items.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
