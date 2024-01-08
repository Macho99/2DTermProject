using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private Sprite[] itemSprites;
    private void Awake()
    {
        Sprite[] spriteResources = Resources.LoadAll<Sprite>("");
        itemSprites = new Sprite[spriteResources.Length];

        foreach(var sprite in spriteResources)
        {
            //���� �̸��� ItemType enum �̸� ���ƾ���!!
            ItemType type = (ItemType) Enum.Parse(typeof(ItemType), sprite.name);

            //type ������ŭ sprite�� �ִ��� Ȯ��
            itemSprites[(int)type] = sprite;
        }
    }

    public Sprite GetItemSprite(ItemType type)
    {
        return itemSprites[(int) type];
    }
}
