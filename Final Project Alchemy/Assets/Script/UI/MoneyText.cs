using ReganAlchemy;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ReganAlchemy
{
    public class MoneyText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private void OnEnable()
        {
            GameManager.OnMoneyChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            GameManager.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            _text.text = $"{money}$";
        }
    }
}