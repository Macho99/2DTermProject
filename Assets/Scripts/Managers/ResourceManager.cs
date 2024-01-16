using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private Sprite[] itemSprites;
    public Dictionary<string, Sprite> spriteDict;
    public Dictionary<string, GameObject> prefabDict;
    private void Awake()
    {
        Sprite[] spriteResources = Resources.LoadAll<Sprite>("ItemSprites");
        itemSprites = new Sprite[spriteResources.Length];

        foreach(var sprite in spriteResources)
        {
            //���� �̸��� ItemID enum �̸� ���ƾ���!!
            ItemID id = (ItemID) Enum.Parse(typeof(ItemID), sprite.name);

            //id ������ŭ sprite�� �ִ��� Ȯ��
            itemSprites[(int)id] = sprite;
        }

        spriteDict = new Dictionary<string, Sprite>();
        Sprite[] otherSprites = Resources.LoadAll<Sprite>("");
        foreach(var otherSprite in otherSprites)
        {
            spriteDict.Add(otherSprite.name, otherSprite);
        }

        prefabDict = new Dictionary<string, GameObject>();
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");
        foreach(var prefab in prefabs)
        {
            prefabDict.Add(prefab.name, prefab);
        }
    }

    public Sprite GetItemSprite(ItemID type)
    {
        return itemSprites[(int) type];
    }
}
