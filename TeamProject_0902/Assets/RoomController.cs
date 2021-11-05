using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomController : MonoBehaviour
{
    UISlot uiSlot;
    [SerializeField]
    private List<GameObject> spellSlots;
    [SerializeField]
    private List<Image> spellSlotsImg;
    [SerializeField]
    private List<GameObject> spellSelectSlots;

    public void OnClickCharacterPick(int whichCharacter)
    {
        if (PlayerInfo.PI != null)
        {
            //Normal Selected mode
            PlayerInfo.PI.mySelectedChampion = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
            PlayerPrefs.SetInt("MyLobbyCharacter", whichCharacter);//Lobby Test
        }
    }

    public void OnClickSpellPick(int whichSpell)
    {
        if(PlayerInfo.PI!=null)
        {
            if(PhotonRoom.room.currentSpellBtn==0)
            {
                PlayerInfo.PI.mySelectedSpell1 = whichSpell;
                PlayerPrefs.SetInt("MySpell1", whichSpell);
                
                var obj = spellSlotsImg[whichSpell].GetComponent<Image>().sprite;
                Debug.Log(obj.name);
                spellSlotsImg[0].sprite = obj;

                PhotonRoom.room.OnSpellSetBoxClose();
                Debug.Log("Spell1 Setting = " + PlayerInfo.PI.mySelectedSpell1);
            }
            if (PhotonRoom.room.currentSpellBtn == 1)
            {
                PlayerInfo.PI.mySelectedSpell2 = whichSpell;
                PlayerPrefs.SetInt("MySpell2", whichSpell);

                var obj = spellSlotsImg[whichSpell].GetComponent<Image>().sprite;
                Debug.Log(obj.name);
                spellSlotsImg[1].sprite = obj;

                PhotonRoom.room.OnSpellSetBoxClose();
                Debug.Log("Spell2 Setting = " + PlayerInfo.PI.mySelectedSpell2);
            }
            else
            {
                PhotonRoom.room.spellSelectBox.SetActive(false);
                return;
            }
        }
    }
}
