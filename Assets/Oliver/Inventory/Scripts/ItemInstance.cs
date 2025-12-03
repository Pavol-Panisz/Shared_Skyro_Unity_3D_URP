using System;

[Serializable]
public class ItemInstanceClass
{
    public ItemDataSOScript itemData;
    public int count;

    public void AddCount(int addCount)
    {
        count += addCount;
    }

    public void SetCount(int newCount)
    {
        count = newCount;
    }
}