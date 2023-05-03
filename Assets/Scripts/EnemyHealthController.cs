using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int _maxHealthCount;
    [SerializeField] private Image _indicator;

    private Tweener _indicatorTween;
    private int _currentHealthCount;

    private void Start()
    {
        _currentHealthCount = _maxHealthCount;
        UpdateIndicator();
    }

    public void DamageToHealth(int damageCount = 1)
    {
        _currentHealthCount -= damageCount;

        UpdateIndicator();
        IndicatorMove();

        if (_currentHealthCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateIndicator()
    {
        var percent = (float)_currentHealthCount / (float)_maxHealthCount;
        _indicator.fillAmount = percent;
    }

    private void IndicatorMove()
    {
        _indicatorTween?.Kill();
        _indicatorTween = _indicator.transform.parent.DOPunchScale(
            new Vector3(.06f, .06f, .06f), 1f, 2, 0.2f);
    }
}
