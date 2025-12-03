using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MirkovaHra_ItemUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Image _image;

    private string _name;
    public string name { get { return _name; } set { _name = value; _nameText.text = value; } }
    private string _description;
    public string description { get { return _description; } set { _description = value; _descriptionText.text = value; } }
    private int _count;
    public int count { get { return _count; } set { _count = value; _countText.text = value.ToString(); } }
    private Sprite _sprite;
    public Sprite sprite { get { return _sprite; } set { _sprite = value; _image.sprite = value; } }
}
