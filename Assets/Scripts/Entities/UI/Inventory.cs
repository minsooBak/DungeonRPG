using TMPro;

public class Inventory : UIBase
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    private void Awake()
    {
        if (prefab != null)
            return;
        text1.text = "NAME";
        text2.text = "DIE";
        //Disable();
    }
}
