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
        playerName.text = player.CharacterName;
        portrait.sprite = player.portrait;
        health.value = player.currentHP;
        health.GetComponentInChildren<TextMeshProUGUI>().text = $"{player.currentHP}";
        valor.value = player.currentValor;
        valor.GetComponentInChildren<TextMeshProUGUI>().text = $"{player.currentValor}";
        
    }
}