using DG.Tweening;
using UnityEngine;

public static class Effects
{
    public static void DoHeartbeatFX(Transform target, float scaleFactor = 1.1f, float popDuration = 0.3f, float restDuration = 2f)
    {
        Vector3 originalScale = target.localScale;
        Sequence seq = DOTween.Sequence();
        
        seq.AppendInterval(restDuration);
        seq.Append(target.DOScale(originalScale * scaleFactor, popDuration).SetEase(Ease.OutBack));
        seq.Append(target.DOScale(originalScale, popDuration).SetEase(Ease.InOutSine));
        seq.SetLoops(-1);
    }

    public static void DoPopupFX(Transform target,float popDuration, out Sequence sequence)
    {
        sequence = DOTween.Sequence();
        target.localScale = Vector3.zero;
        sequence.Join(target.DOScale(Vector3.one, popDuration).SetEase(Ease.OutBack));
    }

    public static void DoPopdownFX(Transform target, float popDuration, out Sequence sequence)
    {
        sequence = DOTween.Sequence();
        //target.localScale = Vector3.one;
        sequence.Join(target.DOScale(Vector3.zero, popDuration).SetEase(Ease.InBack));
    }
    
}
