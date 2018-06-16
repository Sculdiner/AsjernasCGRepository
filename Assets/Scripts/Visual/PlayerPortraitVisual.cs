using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerPortraitVisual : MonoBehaviour
{

    // TODO : get ID from players when game starts

    public CharacterAsset CharAsset;
    [Header("Text Component References")]
    //public Text NameText;
    public Text HealthText;
    [Header("Image References")]
    public Image HeroPowerIconImage;
    public Image HeroPowerBackgroundImage;
    public Image PortraitImage;
    public Image PortraitBackgroundImage;

    private void Awake()
    {
        if (CharAsset != null)
            ApplyLookFromAsset();

    }

    public void ApplyLookFromAsset()
    {
        HealthText.text = CharAsset.MaxHealth.ToString();
        HeroPowerIconImage.sprite = CharAsset.HeroPowerIconImage;
        HeroPowerBackgroundImage.sprite = CharAsset.HeroPowerBGImage;
        PortraitImage.sprite = CharAsset.AvatarImage;
        PortraitBackgroundImage.sprite = CharAsset.AvatarBGImage;

        HeroPowerBackgroundImage.color = CharAsset.HeroPowerBGTint;
        PortraitBackgroundImage.color = CharAsset.AvatarBGTint;

    }

    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            HealthText.text = healthAfter.ToString();
        }
    }

    public void Explode()
    {
        Instantiate(GlobalSettings.Instance.ExplosionPrefab, transform.position, Quaternion.identity);
        Sequence s = DOTween.Sequence();
        s.PrependInterval(2f);
        s.OnComplete(() => GlobalSettings.Instance.GameOverCanvas.SetActive(true));
    }



}
