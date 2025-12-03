using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MirkovaHra_ResourceUIHandler : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countText;

    private Sprite _sprite;
    public Sprite sprite { get { return _sprite; } set { _sprite = value; _image.sprite = value; } }
    private int _count;
    public int count { get { return _count; } set { _count = value; _countText.text = value.ToString(); } }
}
