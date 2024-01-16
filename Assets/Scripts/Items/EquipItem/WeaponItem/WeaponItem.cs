using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class WeaponItem : EquipItem
{
    Weapon weaponPrefab;
    public Weapon weaponObj { get; private set; }
    FieldPlayer player;
    public WeaponItem(ItemID id) : base(id)
    {
        weaponPrefab = GameManager.Resource.prefabDict[id.ToString()].GetComponent<Weapon>();
        if(weaponPrefab == null) { Debug.Log($"{id.ToString()} 프리팹에 Weapon 컴포넌트가 없습니다!!"); }
    }

    public void AddWeapon()
    {
        player = FieldSceneFlowController.Player;
        if(player == null) return;

        weaponObj = player.AddWeapon(weaponPrefab);
    }

    public override void DeleteWeapon()
    {
        base.DeleteWeapon();
        player.DeleteWeapon(weaponObj);
    }

    //public override void Use()
    //{
    //    base.Use();
    //    if (weaponObj == null)
    //    {
    //        Debug.LogError("WeaponObj가 Instantiate 되지 않음");
    //        return;
    //    }
    //    weaponObj.gameObject.SetActive(true);
    //}
}