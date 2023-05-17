using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private Image _indicatorImage;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DamagePlayer(int damageCount)
    {
        _currentHealth -= damageCount;

        UpdateIndicator();

        if (_currentHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    private void UpdateIndicator()
    {
        float percent = _currentHealth / _maxHealth;
        _indicatorImage.fillAmount = percent;
    }

}
