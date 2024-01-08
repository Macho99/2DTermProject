using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private Sprite[] itemSprites;
    public Dictionary<string, Sprite> spriteDict;
    private void Awake()
    {
        Sprite[] spriteResources = Resources.LoadAll<Sprite>("ItemSprites");
        itemSprites = new Sprite[spriteResources.Length];

        foreach(var sprite in spriteResources)
        {
            //파일 이름과 ItemType enum 이름 같아야함!!
            ItemType type = (ItemType) Enum.Parse(typeof(ItemType), sprite.name);

            //type 개수만큼 sprite가 있는지 확인
            itemSprites[(int)type] = sprite;
        }

        spriteDict = new Dictionary<string, Sprite>();
        Sprite[] otherSprites = Resources.LoadAll<Sprite>("");
        foreach(var otherSprite in otherSprites)
        {
            spriteDict.Add(otherSprite.name, otherSprite);
        }
    }

    public Sprite GetItemSprite(ItemType type)
    {
        return itemSprites[(int) type];
    }
}
