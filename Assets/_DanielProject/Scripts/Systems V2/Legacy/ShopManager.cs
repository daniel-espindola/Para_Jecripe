using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject[] cosmeticItems; // Array de itens cosm�ticos dispon�veis
    private bool[] itemPurchased; // Array para rastrear os itens comprados

    private void Start()
    {
        // Inicializa o array de itens comprados com base nos itens dispon�veis
        itemPurchased = new bool[cosmeticItems.Length];
    }

    public void BuyItem(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= cosmeticItems.Length)
        {
            Debug.LogWarning("�ndice de item inv�lido: " + itemIndex);
            return;
        }

        if (itemPurchased[itemIndex])
        {
            Debug.LogWarning("Item j� comprado: " + cosmeticItems[itemIndex].name);
            return;
        }

        int itemCost = GetItemCost(itemIndex);
        int playerCoins = CoinManager.instance.GetCoins();

        if (playerCoins >= itemCost)
        {
            CoinManager.instance.RemoveCoins(itemCost);
            ActivateItem(itemIndex);
            itemPurchased[itemIndex] = true;
            Debug.Log("Item comprado: " + cosmeticItems[itemIndex].name);
        }
        else
        {
            Debug.LogWarning("Moedas insuficientes para comprar o item: " + cosmeticItems[itemIndex].name);
        }
    }

    private int GetItemCost(int itemIndex)
    {
        // Implemente sua l�gica para definir o custo de cada item cosm�tico
        // Neste exemplo, retornaremos um valor fixo de 50 moedas para todos os itens
        return 50;
    }

    private void ActivateItem(int itemIndex)
    {
        // Ativa o item cosm�tico correspondente
        cosmeticItems[itemIndex].SetActive(true);
    }
}

