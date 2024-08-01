
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class ItemRankData
{
    public GameObject Model;
    public float Scale;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData_SO : SerializedScriptableObject
{
    public Dictionary<int, ItemRankData> ItemRankDataList;

    public ItemRankData GetItemRankData(int currentRank)
    {
        if (currentRank > ItemRankDataList.Count)
        {
            Debug.LogError("Could not get ItemRankData in list. Out of range");
            return null;
        }

        return ItemRankDataList[currentRank];
    }
}
