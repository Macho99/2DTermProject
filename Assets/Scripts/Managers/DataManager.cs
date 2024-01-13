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
        itemNames[idx] = "�߰��";
        itemSummary[idx] = "�丮 ���";
        itemDetailDesc[idx] = "�̰ɷ� ��Ʃ, ���� ���� ���� �� ���� �� ����";

        idx = (int)ItemID.DuckEgg;
        items[idx] = null;
        itemNames[idx] = "���� ��";
        itemSummary[idx] = "�丮 ���";
        itemDetailDesc[idx] = "�������ҿ� ������ �ұݸ� �ѷ� �Ծ ��������? ���� �ѹ� �غ���.";

        idx = (int)ItemID.Sword;
        items[idx] = new SwordItem();
        itemNames[idx] = "��ö Į";
        itemSummary[idx] = "���ݷ� : 10\n���� �ӵ�: ����\n��Ÿ� : 1";
        itemDetailDesc[idx] = "A -> S -> A(����): �ټ��� ���鿡�� ������ �������� �ְ�, �ִ� 5�ʱ��� ������Ų��.";

        idx = (int)ItemID.Bident;
        items[idx] = new BidentItem();
        itemNames[idx] = "���̴�Ʈ";
        itemSummary[idx] = "���ݷ� : 20\n���� �ӵ�: ����\n��Ÿ� : 1.5";
        itemDetailDesc[idx] = "S -> A -> S(����): �������� �����ϸ� ���鿡�� �������� ���� ���ظ� �ش�.";

        idx = (int)ItemID.Bow;
        items[idx] = new BowItem();
        itemNames[idx] = "Ȱ";
        itemSummary[idx] = "���ݷ� : 10\n���� �ӵ�: ����\n��Ÿ� : 10";
        itemDetailDesc[idx] = "S -> S: �ڷ� �����ϸ� ȭ�� �ι��� ������.\nS -> A(��Ÿ) -> S: ������ ȭ���� �� �� ������ ���� ȭ���� �̾Ƴ� ������ ���ظ� �ش�.";
    }

    public Item GetItem(ItemID id, int amount = 1)
    {
        //��Ÿ ������
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