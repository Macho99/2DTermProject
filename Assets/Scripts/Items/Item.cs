using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ItemType
{
    Size
}

public abstract class Item {
    private ItemType type;
    public ItemType Type { get { return type;} }
    public string Name { get { return GameManager.Data.dictItemName[type]; } }
    public string Desc { get { return GameManager.Data.dictItemDesc[type]; } }
}