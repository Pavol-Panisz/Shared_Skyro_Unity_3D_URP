using System.Collections.Generic;
using UnityEngine;
using Game.UI;

namespace Game.Core
{
    public class ItemsManager : MonoBehaviour
    {
        [SerializeField] private Transform contentObj;
        [SerializeField] private GameObject itemPanelPrefab;

        private readonly Dictionary<string, ItemPanelUIElement> items = new();

        public void AddItem(string name, string value)
        {
            if (items.ContainsKey(name)) return;

            GameObject itemPanel = Instantiate(itemPanelPrefab, contentObj);
            ItemPanelUIElement ui = itemPanel.GetComponent<ItemPanelUIElement>();
            ui.SetName(name);
            ui.SetValue(value);
            itemPanel.name = name;

            items[name] = ui;
        }

        public bool ExistsItem(string name) => items.ContainsKey(name);

        public string GetItem(string name)
        {
            return items.TryGetValue(name, out var ui) ? ui.GetValue() : null;
        }

        public void ChangeItem(string name, float newValue)
        {
            if (items.TryGetValue(name, out var ui))
                ui.SetValue(newValue.ToString());
        }

        public void RemoveItem(string name)
        {
            if (!items.TryGetValue(name, out var ui)) return;
            Destroy(ui.gameObject);
            items.Remove(name);
        }
    }
}
