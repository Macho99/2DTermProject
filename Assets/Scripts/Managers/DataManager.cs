using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Item[] items;
    private string[] itemNames;
    private string[] itemSummary;
    private string[] itemDetailDesc;

    public bool TitlePlayed { get; set; }

    private void Awake()
    {
        items = new Item[(int) ItemID.Size];
        itemNames = new string[(int) ItemID.Size];
        itemSummary = new string[(int) ItemID.Size];
        itemDetailDesc = new string[(int) ItemID.Size];

        int idx = (int)ItemID.Sword;
        items[idx] = new SwordItem();
        itemNames[idx] = "��ö Į";
        itemSummary[idx] = "���ݷ� : 15\n���� �ӵ�: ����\n��Ÿ� : 1";
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

        //-----------------------------------------------------------------------------

        idx = (int)ItemID.RedPotion;
        items[idx] = new RedPotionItem();
        itemNames[idx] = "��������";
        itemSummary[idx] = "���ô� ��� ü���� 50 ȸ���մϴ�";
        itemDetailDesc[idx] = "�ʵ忡�� �ڶ�� ���� ������ ����� ���� �� ����. �̰� �Ծ �Ǵ°ɱ�..?";

        //-----------------------------------------------------------------------------

        idx = (int)ItemID.ChickMeat;
        items[idx] = null;
        itemNames[idx] = "���Ƹ� ���";
        itemSummary[idx] = "�丮 ���";
        itemDetailDesc[idx] = "���� � �������� ������ �ε巯������ ���� ���� �������� ã�� ������̴�.";

        idx = (int)ItemID.RoosterMeat;
        items[idx] = null;
        itemNames[idx] = "�߰��";
        itemSummary[idx] = "�丮 ���";
        itemDetailDesc[idx] = "Ƣ�ܵ� ���ְ�, ������ ���ְ�, ���� ������ ���ִ� �߰���̴�.";

        idx = (int)ItemID.DuckEgg;
        items[idx] = null;
        itemNames[idx] = "���� ��";
        itemSummary[idx] = "�丮 ���";
        itemDetailDesc[idx] = "�������ҿ� ������ �ұݸ� �ѷ� �Ծ ��������? ���� �ѹ� �غ���.";

        idx = (int)ItemID.BuffaloMeat;
        items[idx] = null;
        itemNames[idx] = "���ȷ� �����";
        itemSummary[idx] = "�丮 ���";
        itemDetailDesc[idx] = "��� ���縸ŭ �丮�� ������� ���� ������ ���� ���̴�.";

        //-----------------------------------------------------------------------------

        idx = (int)ItemID.BasicSoup;
        items[idx] = new BasicSoupItem();
        itemNames[idx] = "�⺻ ����";
        itemSummary[idx] = "�丮 �޴�";
        itemDetailDesc[idx] = "��ᰡ �ƹ��͵� ���� �� ������ �⺻ �޴�. �̰ɷ� �踦 ä�� �մ��� �ٽ� �츮 ���Ը� ã�� ���� �� ����.";

        idx = (int)ItemID.FriedEgg;
        items[idx] = new FriedEggItem();
        itemNames[idx] = "��� ������";
        itemSummary[idx] = "�丮 �޴�";
        itemDetailDesc[idx] = "����� ���� �ƴ϶� ���� �������� �մԵ��� ���� ������?";

        idx = (int)ItemID.GrilledWholeChicken;
        items[idx] = new GrilledWholeChickenItem();
        itemNames[idx] = "��� ����";
        itemSummary[idx] = "�丮 �޴�";
        itemDetailDesc[idx] = "�ε巯�� �ߴٸ� ��� ������ �������� ��ȭ";

        idx = (int)ItemID.GrilledSkewers;
        items[idx] = new GrilledSkewersItem();
        itemNames[idx] = "�߲�ġ";
        itemSummary[idx] = "�丮 �޴�";
        itemDetailDesc[idx] = "������ ȣ��ȣ ���� ������ ���� ���̴�.";

        idx = (int)ItemID.ChickenSkewersAndBoiledEggs;
        items[idx] = new ChickenSkewersAndBoiledEggsItem();
        itemNames[idx] = "�߲�ġ�� ���� ��� ����";
        itemSummary[idx] = "�丮 �޴�";
        itemDetailDesc[idx] = "�߲�ġ�� ���� ����� ��ä�� ���� ǳ���� ���̴�.";

        idx = (int)ItemID.BuffaloSteak;
        items[idx] = new BuffaloSteakItem();
        itemNames[idx] = "���ȷ� ������ũ";
        itemSummary[idx] = "�丮 �޴�";
        itemDetailDesc[idx] = "�ְ�� �����, �ְ�� ��, �ְ�� ����";
    }

    public Item GetItem(ItemID id, int amount = 1)
    {
        Item item = items[(int)id];
        //��Ÿ ������
        if (null == item){
            return new IngredientItem(id, amount);
        }
        else if(item is EquipItem eqiup)
        {
            return (Item) eqiup.Clone();
        }
        else if(item is ConsumptionItem consump)
        {
            ConsumptionItem newConsump = (ConsumptionItem) consump.Clone();
            newConsump.SetAmount(amount);
            return (Item) newConsump;
        }
        else if(item is CuisineItem cuisine)
        {
            CuisineItem newCuisine = (CuisineItem) cuisine.Clone();
            newCuisine.SetAmount(amount);
            return (Item) newCuisine;
        }

        return null;
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