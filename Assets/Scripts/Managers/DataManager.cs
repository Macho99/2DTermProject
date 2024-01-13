using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Item[] items;
    private string[] itemNames;
    private string[] itemSummary;
    private string[] itemDetailDesc;

    private void Awake()
    {
        items = new Item[(int) ItemID.Size];
        itemNames = new string[(int) ItemID.Size];
        itemSummary = new string[(int) ItemID.Size];
        itemDetailDesc = new string[(int) ItemID.Size];

        int idx = (int)ItemID.RoosterMeat;
        items[idx] = null;
        itemNames[idx] = "닭고기";
        itemSummary[idx] = "요리 재료";
        itemDetailDesc[idx] = "이걸로 스튜, 구이 등을 만들 수 있을 것 같다";

        idx = (int)ItemID.DuckEgg;
        items[idx] = null;
        itemNames[idx] = "오리 알";
        itemSummary[idx] = "요리 재료";
        itemDetailDesc[idx] = "프라이팬에 구워서 소금만 뿌려 먹어도 맛있을까? 직접 한번 해보자.";

        idx = (int)ItemID.Sword;
        items[idx] = new SwordItem();
        itemNames[idx] = "강철 칼";
        itemSummary[idx] = "공격력 : 10\n공격 속도: 보통\n사거리 : 1";
        itemDetailDesc[idx] = "A -> S -> A(차지): 다수의 적들에게 강력한 데미지를 주고, 최대 5초까지 기절시킨다.";

        idx = (int)ItemID.Bident;
        items[idx] = new BidentItem();
        itemNames[idx] = "바이던트";
        itemSummary[idx] = "공격력 : 20\n공격 속도: 느림\n사거리 : 1.5";
        itemDetailDesc[idx] = "S -> A -> S(차지): 전방으로 돌진하며 적들에게 데미지와 기절 피해를 준다.";

        idx = (int)ItemID.Bow;
        items[idx] = new BowItem();
        itemNames[idx] = "활";
        itemSummary[idx] = "공격력 : 10\n공격 속도: 빠름\n사거리 : 10";
        itemDetailDesc[idx] = "S -> S: 뒤로 후퇴하며 화살 두발을 날린다.\nS -> A(연타) -> S: 무수한 화살을 쏜 뒤 적에게 박힌 화살을 뽑아내 강력한 피해를 준다.";
    }

    public Item GetItem(ItemID id, int amount = 1)
    {
        //기타 아이템
        if (null == items[(int)id]){
            return new IngredientItem(id, amount);
        }


        return items[(int)id];
    }

    public string GetItemName(ItemID id)
    {
        return itemNames[(int) id];
    }
    public string GetItemSummary(ItemID id)
    {
        return itemSummary[(int) id];
    }

    public string GetItemDetailDesc(ItemID id)
    {
        return itemDetailDesc[(int) id];
    }
}