using System.Globalization;
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

        public void SetValue(float value)
        {
            rightTextObj.text = value.ToString(CultureInfo.InvariantCulture);
        }

        public string GetValue()
        {
            return rightTextObj.text;
        }

    }
}
