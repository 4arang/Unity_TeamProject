using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
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
                switch (whichSpell)
                {
                    case 0:
                        {
                            PlayerInfo.PI.mySelectedSpell1 = whichSpell;
                            PlayerPrefs.SetInt("MySpell1", whichSpell);

                            PhotonRoom.room.OnSpellSetBoxClose();
                            Debug.Log("Spell1 Setting = " + PlayerInfo.PI.mySelectedSpell1);
                            break;
                        }
                    case 1:
                        {
                            PlayerInfo.PI.mySelectedSpell2 = whichSpell;
                            PlayerPrefs.SetInt("MySpell2", whichSpell);

                            PhotonRoom.room.OnSpellSetBoxClose();
                            Debug.Log("Spell2 Setting = " + PlayerInfo.PI.mySelectedSpell2);
                            break;
                        }
                }
            }
            if (PhotonRoom.room.currentSpellBtn == 1)
            {
                switch (whichSpell)
                {
                    case 0:
                        {
                            PlayerInfo.PI.mySelectedSpell1 = whichSpell;
                            PlayerPrefs.SetInt("MySpell1", whichSpell);

                            PhotonRoom.room.OnSpellSetBoxClose();
                            Debug.Log("Spell1 Setting = " + PlayerInfo.PI.mySelectedSpell1);
                            break;
                        }
                    case 1:
                        {
                            PlayerInfo.PI.mySelectedSpell2 = whichSpell;
                            PlayerPrefs.SetInt("MySpell2", whichSpell);

                            PhotonRoom.room.OnSpellSetBoxClose();
                            Debug.Log("Spell2 Setting = " + PlayerInfo.PI.mySelectedSpell2);
                            break;
                        }
                }
            }
        }
    }
}
