using System.Collections.Generic;
using Game.Items;
using UnityEngine;

namespace Game.UI
{
    public class ItemsView : MonoBehaviour, IItemsView
    {
        [SerializeField] private Transform contentObj;
        [SerializeField] private GameObject itemPanelPrefab;

        private readonly Dictionary<string, ItemPanelUIElement> _uiItems = new();

        public void AddUI(string name, float value)
        {
            GameObject itemPanel = Instantiate(itemPanelPrefab, contentObj);
            var uiElement = itemPanel.GetComponent<ItemPanelUIElement>();

            uiElement.SetName(name);
            uiElement.SetValue(value);
            itemPanel.name = name;

            _uiItems[name] = uiElement;
        }

        public void ChangeUI(string name, float newValue)
        {
            if(_uiItems.TryGetValue(name, out var ui))
                ui.SetValue(newValue);
        }

        public void RemoveUI(string name)
        {
            if(!_uiItems.TryGetValue(name, out var ui))
                return;
            
            Destroy(ui.gameObject);
            _uiItems.Remove(name);
        }
    }
}