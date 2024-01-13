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
            //파일 이름과 ItemType enum 이름 같아야함!!
            ItemID type = (ItemID) Enum.Parse(typeof(ItemID), sprite.name);

            //type 개수만큼 sprite가 있는지 확인
            itemSprites[(int)type] = sprite;
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
