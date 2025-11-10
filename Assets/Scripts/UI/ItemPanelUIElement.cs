using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ItemPanelUIElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text leftTextObj;
        [SerializeField] private TMP_Text rightTextObj;

        public void SetName(string name)
        {
            leftTextObj.text = name;
        }

        public void SetValue(string value)
        {
            rightTextObj.text = value;
        }

        public string GetValue()
        {
            return rightTextObj.text;
        }

    }
}
