using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


//written by Andy @rmfz
//lmk on discord if u have any questions on this
public class rmfz_UTGameManager : MonoBehaviour
{
    [Header("Player Attributes")]
    public int playerHP;
    public int playerMaxHP;
    public int playerAttack;

    public Image healthBar;
    public TextMeshProUGUI healthText;

    public Image damageIndicatorBar;
    Coroutine damageIndicatorCoroutine = null; //for the bar

    [Header("Boss Attributes")]
    public int bossHP;

    [Header("UI Stuff")]
    public Image healthChangeVignette;

    public GameObject healthChangeTextPrefab;
    public Transform healthChangeGridLayout;
    List<rmfz_HPChangeIndicator> healthChangeIndicatorList = new List<rmfz_HPChangeIndicator>();
    int maxIndicators = 4;
    float indicatorLifetime = 3f; //seconds each indicator remains visible


    [Header("Others")]
    public rmfz_AudioManager audioManager;


    Coroutine healthChangeVignetteCoroutine = null;


    private void Start()
    {
        playerHP = playerMaxHP;
        healthText.text = $"{playerMaxHP} / {playerMaxHP}";
        healthBar.fillAmount = 1;
        damageIndicatorBar.fillAmount = 1;
    }

    /// <summary>
    /// Handles the spawning and destroying of the health change indicators.
    /// </summary>
    /// <param name="changeAmount"></param>
    void AddHealthIndicator(int changeAmount)
    {
        //remove oldest indicator
        if(healthChangeIndicatorList.Count >= maxIndicators)
        {
            Destroy(healthChangeIndicatorList[healthChangeIndicatorList.Count - 1].indicatorObject);
            healthChangeIndicatorList.RemoveAt(healthChangeIndicatorList.Count - 1);
        }

        //copy (instantiate) the motherfuker
        GameObject newIndicator = Instantiate(healthChangeTextPrefab, healthChangeGridLayout);
        TextMeshProUGUI newIndicatorText = newIndicator.GetComponent<TextMeshProUGUI>();
        
        if(changeAmount > 0)
        {
            newIndicatorText.text = $"+{changeAmount}";
            newIndicatorText.color = new Color(0.95f, 0.95f, 0.05f);
        }
        else
        {
            newIndicatorText.text = $"{changeAmount}";
            newIndicatorText.color = new Color(1, 0, 0);
        }

        //play funny animation
        newIndicator.transform.localScale = Vector3.zero;
        LeanTween.scale(newIndicator, Vector3.one, 0.1f).setEaseOutBack();

        //put the newest damage indicator at the first spot
        newIndicator.transform.SetAsFirstSibling();
        
        //start indicator coroutine and add to list
        Coroutine fadeCoroutine = StartCoroutine(HandleDamageIndicatorLife(newIndicator, newIndicatorText));
        healthChangeIndicatorList.Insert(0, new rmfz_HPChangeIndicator(newIndicator, newIndicatorText, fadeCoroutine)); //insert @ beginning of list
    }

    IEnumerator HandleDamageIndicatorLife(GameObject indicator, TextMeshProUGUI text)
    {
        float duration = 3f; //time visible before starting to fade
        float fadeDuration = 1f; //time it takes to fade out

        yield return new WaitForSeconds(duration);

        //fade out
        float startTime = Time.time;
        while(Time.time < startTime + fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - t);
            yield return null;
        }

        //remove from list and game
        healthChangeIndicatorList.Remove(healthChangeIndicatorList.FirstOrDefault(i  => i.indicatorObject == indicator));
        Destroy(indicator);
    }

    /// <summary>
    /// Makes the player either take damage or heal. Input a negative number to damage, input positive to heal.
    /// Also handles basically everything 
    /// </summary>
    /// <param name="baseChange"></param>
    public void ChangePlayerHealth(int baseChange)
    {
        float valueVariation = Random.Range(-0.15f, 0.15f); //variation of 15% when taking damage
        int finalValue = Mathf.RoundToInt(baseChange * (1 + valueVariation));
        float healthChangeRatio = Mathf.Abs((float)finalValue / playerMaxHP);
        playerHP += finalValue;

        //caps the player health to the confines
        if(playerHP < 0)
        {
            playerHP = 0;
            //death thing here
        }
        if(playerHP > playerMaxHP)
        {
            playerHP = playerMaxHP;
        }

        //do things based on if hurt/heal
        if(baseChange < 0)
        {
            audioManager.StopAudio("hurt");
            audioManager.PlayAudio("hurt");
            HandleVignetteColor(new Color(1, 0, 0), healthChangeRatio);
        }
        if(baseChange > 0)
        {
            audioManager.StopAudio("heal");
            audioManager.PlayAudio("heal");
            HandleVignetteColor(new Color(0.5f, 1f, 0f), healthChangeRatio);
        }

        //update bar and HP text
        healthBar.fillAmount = (float)playerHP / (float)playerMaxHP;
        if (playerHP >= 0 && playerHP <= 9)
        {
            healthText.text = $"000{playerHP} / {playerMaxHP}";
        }
        else if(playerHP >= 10 && playerHP <= 99)
        {
            healthText.text = $"00{playerHP} / {playerMaxHP}";
        }
        else if(playerHP >= 100 && playerHP <= 999)
        {
            healthText.text = $"0{playerHP} / {playerMaxHP}";
        }
        else
        {
            healthText.text = $"{playerHP} / {playerMaxHP}";
        }

        //stop damageIndicator Coroutine if running, and runs it again
        if (damageIndicatorCoroutine != null)
        {
            StopCoroutine(damageIndicatorCoroutine);
        }
        damageIndicatorCoroutine = StartCoroutine(UpdateIndicatorBar());

        //do the health indicator thing
        AddHealthIndicator(finalValue);
    }

    void HandleVignetteColor(Color color, float changeRatio)
    {
        changeRatio = Mathf.Clamp(changeRatio, 0f, 1f);

        color.a = changeRatio;
        healthChangeVignette.color = color;

        if(healthChangeVignetteCoroutine != null)
        {
            StopCoroutine(healthChangeVignetteCoroutine);
        }
        healthChangeVignetteCoroutine = StartCoroutine(FadeVignette(Color.clear));
    }

    IEnumerator UpdateIndicatorBar()
    {
        yield return new WaitForSeconds(0.5f); //delay before indicator starts decreasing

        float startFillAmount = damageIndicatorBar.fillAmount;
        float elapsedTime = 0f;
        float duration = 1f; //time it takes for indicator bar to decrease to yellow bar's value

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            float easeOutProgress = 1 - Mathf.Pow(1 - progress, 3); //cubic ease out

            damageIndicatorBar.fillAmount = Mathf.Lerp(startFillAmount, healthBar.fillAmount, easeOutProgress);
            yield return null;
        }

        damageIndicatorBar.fillAmount = healthBar.fillAmount;
    }

    IEnumerator FadeVignette(Color targetColor)
    {
        float duration = 1.5f;
        float time = 0;

        Color startColor = healthChangeVignette.color;

        while(time < duration)
        {
            healthChangeVignette.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        healthChangeVignette.color = targetColor; //ensure final color
    }
}
