using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Turret_Stats : MonoBehaviour
{
    PhotonView PV;
    public float HP;
    public float MaxHP;
    public float HPregen; 

    public float AD;
    public float MaxAD;
    public float AttackSpeed;
    public float ADperTime;//�ź�����
    public float AttackRange; //775����

    public float AP;
    public float MaxAP;//14�� ���� ap
    public float APp;
    public float MRP;
    public float MRPp;
    public float MaxMRP; //14�� ���� mrp

    public bool TeamColor;
    public int Gold;
    public int Exp;

    public bool isAttack_Minion;
    [SerializeField] private GameObject ExplosionEffect;


    private void Awake()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");

        if (transform.position.x < 0) TeamColor = true;
        else TeamColor = false; 

        if (transform.position.x>=-25 && transform.position.x<25)
        {

            MaxHP = float.Parse(data[8]["statshp"].ToString());
            HP = MaxHP;

            MaxAD = 278;
            AD = float.Parse(data[8]["statsattackdamage"].ToString()); 
            ADperTime = float.Parse(data[8]["statsattackdamageperlevel"].ToString()); //1�к���
            AttackSpeed = float.Parse(data[8]["statsattackspeed"].ToString()); 
            AttackRange = float.Parse(data[8]["statsattackrange"].ToString());

            MaxAP = 40; 
            AP = float.Parse(data[8]["statsarmor"].ToString());

            MaxMRP = 40;
            MRP = float.Parse(data[8]["statsspellblock"].ToString());
            Gold = int.Parse(data[8]["gold"].ToString());
            Exp = int.Parse(data[8]["exp"].ToString());

            if (TeamColor)
                Turret_Manager.Instance.Red_TargetBuilding1 = transform;
            else
                Turret_Manager.Instance.Blue_TargetBuilding1 = transform;
        }
       else if((transform.position.x>=-65 && transform.position.x<-75)
            ||(transform.position.x>=65 && transform.position.x<75))
         {
            MaxHP = float.Parse(data[9]["statshp"].ToString());
            HP = MaxHP;

            MaxAD = 278;
            AD = float.Parse(data[9]["statsattackdamage"].ToString()); 
            ADperTime = float.Parse(data[9]["statsattackdamageperlevel"].ToString());//3~17�� ����
            AttackSpeed = float.Parse(data[9]["statsattackspeed"].ToString());
            AttackRange = float.Parse(data[9]["statsattackrange"].ToString());

            MaxAP = 40;
            AP = float.Parse(data[9]["statsarmor"].ToString());
            APp = float.Parse(data[9]["statsarmorperlevel"].ToString());//16~30�� ����

            MaxMRP = 40;
            MRP = float.Parse(data[9]["statsspellblock"].ToString());
            MRPp = float.Parse(data[9]["statsspellblockperlevel"].ToString());//16~30�� ����
            Gold = int.Parse(data[9]["gold"].ToString());
            Exp = int.Parse(data[9]["exp"].ToString());

            if (TeamColor)
                Turret_Manager.Instance.Red_TargetBuilding2 = transform;
            else
                Turret_Manager.Instance.Blue_TargetBuilding2 = transform;
        }
        else if ((transform.position.x >= -60 && transform.position.x < -45)
      || (transform.position.x >= 45 && transform.position.x < 60))
        {
            MaxHP = float.Parse(data[10]["statshp"].ToString());
            HP = MaxHP;
            HPregen = float.Parse(data[10]["statshpregen"].ToString()); //5�ʴ� 

            MaxAD = 278;
            AD = float.Parse(data[10]["statsattackdamage"].ToString());
            ADperTime = float.Parse(data[10]["statsattackdamageperlevel"].ToString());//3~17�� ����
            AttackSpeed = float.Parse(data[10]["statsattackspeed"].ToString());
            AttackRange = float.Parse(data[10]["statsattackrange"].ToString());

            MaxAP = 40;
            AP = float.Parse(data[10]["statsarmor"].ToString());

            MaxMRP = 40;
            MRP = float.Parse(data[10]["statsspellblock"].ToString());
            Gold = int.Parse(data[10]["gold"].ToString());
            Exp = int.Parse(data[10]["exp"].ToString());

            if (TeamColor)
            {
                if(transform.position.z>0)
                Turret_Manager.Instance.Red_TargetBuilding4 = transform;
                else
                Turret_Manager.Instance.Red_TargetBuilding5 = transform;
            }
            else
            {
                if (transform.position.z > 0)
                    Turret_Manager.Instance.Blue_TargetBuilding4 = transform;
                else
                    Turret_Manager.Instance.Blue_TargetBuilding5 = transform;
            }
        }
        else if ((transform.position.x >= -75 && transform.position.x < -60)
|| (transform.position.x >= 60 && transform.position.x < 70))
        {
            MaxHP = float.Parse(data[10]["statshp"].ToString());
            HP = MaxHP;
            HPregen = float.Parse(data[10]["statshpregen"].ToString()); //5�ʴ� 

            MaxAD = 278;
            AD = float.Parse(data[10]["statsattackdamage"].ToString());
            ADperTime = float.Parse(data[10]["statsattackdamageperlevel"].ToString());//3~17�� ����
            AttackSpeed = float.Parse(data[10]["statsattackspeed"].ToString());
            AttackRange = float.Parse(data[10]["statsattackrange"].ToString());

            MaxAP = 40;
            AP = float.Parse(data[10]["statsarmor"].ToString());

            MaxMRP = 40;
            MRP = float.Parse(data[10]["statsspellblock"].ToString());
            Gold = int.Parse(data[10]["gold"].ToString());
            Exp = int.Parse(data[10]["exp"].ToString());
        }

        PV.RPC("instantiateExplosition", RpcTarget.AllViaServer);
    }

    public void DropHP(float damage)
    {
        damage *= (1 - AP / (100 + AP));
        HP -= damage;
        Debug.Log("Turret HP" + HP);

        if (HP <= 0)
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, 16.0f);
            int i = 0;
            foreach (Collider col in colliderArray)
            {
                if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                 && (player.TeamColor != TeamColor))
                {
                    i++;
                    col.GetComponent<Player_Level>().GetEXP(Exp); //����ġ ȹ��
                }
            }
            if (i >= 2) //�θ� �̻󿡰� ����ġ �й��� ���
            {
                foreach(Collider col in colliderArray)
                {
                    if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                && (player.TeamColor != TeamColor))
                    {
                        col.GetComponent<Player_Level>().GetEXP(-Exp*0.34f); //����ġ �й�
                    }
                }
            }
            //���� : ���� ����Ʈ ���� �ȹ���
            
            Destroy(gameObject);

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(HP);

        }
        else
        {
            HP = (float)stream.ReceiveNext();

        }
    }

    [PunRPC]
    void instantiateExplosion()
    {
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
    }
}
