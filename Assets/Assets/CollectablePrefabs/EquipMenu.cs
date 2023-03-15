using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipMenu : MonoBehaviour
{
    public static Collectables current;
    public static EquipMenu instance;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text conditionText;
    [SerializeField] TMP_Text property1;
    [SerializeField] TMP_Text property2;
    [SerializeField] Image icon;
    public Animator anim;
    private Player player;
    private Vector2 offset;
    // Start is called before the first frame update

    public void ChangeMenu(Collectables col)
    {
        current = col;
        icon.sprite = col.icon;
        nameText.text = col.itemName;
        property1.text = "WEIGHT: " + col.itemWeight;
        conditionText.text = (int)col.condition + "%";
        property2.text = col.type switch
        {
            Collectables.Type.Food => "Calories: " + col.calories,
            Collectables.Type.Burn => "Burns For: " + col.burnDuration,
            Collectables.Type.Cloth => "Warmth: " + col.clothDegree,
            Collectables.Type.Medical => "Amount: " + col.itemAmount,
            _ => ""
        };
    }
    public void Collect()
    {
        if (current == null)
            return;

        if(player.Carrying > Player.MaxCarry)
        {
            StartPopUpMessage.Message("You can't carry much more", Color.red);
        } else
        {
            player.Collect(current);
            Destroy(current.gameObject);
            current = null;
        }
    }
    void Start()
    {
        if(instance == null)
        {
            offset = transform.position;
            player = GameManager.i.player;
            instance = this;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + (Vector2)player.transform.position;
    }
}
