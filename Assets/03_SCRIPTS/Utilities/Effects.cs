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
        sequence.Join(target.DOScale(Vector3.zero, popDuration).SetEase(Ease.InBack));
    }

    public static void DoMiniatureFX(Transform target, float duration, out Sequence sequence)
    {
        sequence = DOTween.Sequence();
        sequence.Join(target.DOScale(Vector3.zero, duration).SetEase(Ease.InBack));
    }

    public static void DoJumpMoveFX(Transform target, Vector3 targetPosition, float duration, out Sequence sequence)
    {
        sequence = DOTween.Sequence();
        sequence.Join(target.DOLocalJump(targetPosition, 1f, 1, duration).SetEase(Ease.Linear));
    }

    public static void DoNotificationFX(RectTransform target, float duration, out Sequence sequence)
    {
        sequence = DOTween.Sequence();
        Vector2 finalPos = target.anchoredPosition;
        float height = target.rect.height;
        Vector2 startPos = finalPos + new Vector2(0, height * 2); 
        target.anchoredPosition = startPos;
        sequence.Join(target.DOAnchorPos(finalPos, duration));
    }

    public static void DoInverseNotificationFX(RectTransform target, float duration, out Sequence sequence)
    {
        sequence = DOTween.Sequence();
        float height = target.rect.height;
        Vector2 finalPos = target.anchoredPosition +  new Vector2(0, -height);
        sequence.Join(target.DOAnchorPos(finalPos, duration));
    }
    
}
