using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBattleHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Image portrait;
    [SerializeField] private Slider health;
    [SerializeField] private Slider valor;

    public void Initialize(CharacterInstance player)
    {
        // Subscribing to player events
        player.OnHealthChanged += UpdateHealth; 
        player.OnValorChanged += UpdateHealth;

        // Filling name and portrait info
        playerName.text = player.CharacterName;
        portrait.sprite = player.portrait;

        // Setting new max val for the slide
        health.maxValue = player.maxHP;
        health.value = player.currentHP;
        health.GetComponentInChildren<TextMeshProUGUI>().text = $"{player.currentHP}";

        valor.maxValue = player.maxValor;
        valor.value = player.currentValor;
        valor.GetComponentInChildren<TextMeshProUGUI>().text = $"{player.currentValor}";
    }

    private void UpdateHealth(int newHealth)
    {
        health.value = newHealth;
        Debug.Log( health.value);
        health.GetComponentInChildren<TextMeshProUGUI>().text = $"{newHealth}";
    }

    public void UpdateValor(int newValor)
    {
        valor.value = newValor;
        health.GetComponentInChildren<TextMeshProUGUI>().text = $"{newValor}";
    }

    //public void LowHealth() { }

    //public void Dead() { }

}