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
            //파일 이름과 ItemType enum 이름 같아야함!!
            ItemType type = (ItemType) Enum.Parse(typeof(ItemType), sprite.name);

            //type 개수만큼 sprite가 있는지 확인
            itemSprites[(int)type] = sprite;
        }
    }

    public Sprite GetItemSprite(ItemType type)
    {
        return itemSprites[(int) type];
    }
}
