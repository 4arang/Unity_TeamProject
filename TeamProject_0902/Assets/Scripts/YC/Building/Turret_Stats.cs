using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Turret_Stats : MonoBehaviour
{
    public float xx;
    public int testnum=0;
    PhotonView PV;
    public float HP;
    public float MaxHP;
    public float HPregen; 

    public float AD;
    public float MaxAD;
    public float AttackSpeed;
    public float ADperTime;//매분증가
    public float AttackRange; //775고정

    public float AP;
    public float MaxAP;//14분 이후 ap
    public float APp;
    public float MRP;
    public float MRPp;
    public float MaxMRP; //14분 이후 mrp

    public bool TeamColor;
    public int Gold;
    public int Exp;

    public bool isAttack_Minion;
    [SerializeField] private GameObject ExplosionEffect;

    Rigidbody rigidBody;

    private void Awake()
    {
        List<Dictionary<string, object>> data = StatCSVreader.Read("Character_Stats");

        if (transform.position.y > 0) TeamColor = true;
        else TeamColor = false; 

        if (transform.position.x>=-25 && transform.position.x<25)
        {
            testnum = 1;
            MaxHP = float.Parse(data[8]["statshp"].ToString());
            HP = MaxHP;

            MaxAD = 278;
            AD = float.Parse(data[8]["statsattackdamage"].ToString()); 
            ADperTime = float.Parse(data[8]["statsattackdamageperlevel"].ToString()); //1분부터
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
       else if((transform.position.x>=-43 && transform.position.x<-25)
            ||(transform.position.x>=25 && transform.position.x<40))
         {
            MaxHP = float.Parse(data[9]["statshp"].ToString());
            HP = MaxHP;

            MaxAD = 278;
            AD = float.Parse(data[9]["statsattackdamage"].ToString()); 
            ADperTime = float.Parse(data[9]["statsattackdamageperlevel"].ToString());//3~17분 증가
            AttackSpeed = float.Parse(data[9]["statsattackspeed"].ToString());
            AttackRange = float.Parse(data[9]["statsattackrange"].ToString());

            MaxAP = 40;
            AP = float.Parse(data[9]["statsarmor"].ToString());
            APp = float.Parse(data[9]["statsarmorperlevel"].ToString());//16~30분 증가

            MaxMRP = 40;
            MRP = float.Parse(data[9]["statsspellblock"].ToString());
            MRPp = float.Parse(data[9]["statsspellblockperlevel"].ToString());//16~30분 증가
            Gold = int.Parse(data[9]["gold"].ToString());
            Exp = int.Parse(data[9]["exp"].ToString());
            testnum = 2;
            if (TeamColor)
                Turret_Manager.Instance.Red_TargetBuilding2 = transform;
            else
                Turret_Manager.Instance.Blue_TargetBuilding2 = transform;
        }
        else if ((transform.position.x >= -73 && transform.position.x < -60)
      || (transform.position.x >= 60 && transform.position.x < 70))
        {
            MaxHP = float.Parse(data[10]["statshp"].ToString());
            HP = MaxHP;
            HPregen = float.Parse(data[10]["statshpregen"].ToString()); //5초당 
            testnum = 4;
            MaxAD = 278;
            AD = float.Parse(data[10]["statsattackdamage"].ToString());
            ADperTime = float.Parse(data[10]["statsattackdamageperlevel"].ToString());//3~17분 증가
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
        else if ((transform.position.x >= -58 && transform.position.x < -44)
|| (transform.position.x >= 45 && transform.position.x < 58))
        {
            MaxHP = float.Parse(data[11]["statshp"].ToString());
            HP = MaxHP;
            HPregen = float.Parse(data[11]["statshpregen"].ToString()); //5초당 

            MaxAD = 278;
            AD = float.Parse(data[11]["statsattackdamage"].ToString());
            ADperTime = float.Parse(data[11]["statsattackdamageperlevel"].ToString());//3~17분 증가
            AttackSpeed = float.Parse(data[11]["statsattackspeed"].ToString());
            AttackRange = float.Parse(data[11]["statsattackrange"].ToString());

            MaxAP = 40;
            AP = float.Parse(data[11]["statsarmor"].ToString());
            testnum = 3;
            MaxMRP = 40;
            MRP = float.Parse(data[11]["statsspellblock"].ToString());
            Gold = int.Parse(data[11]["gold"].ToString());
            Exp = int.Parse(data[11]["exp"].ToString());

            if (TeamColor)
                Turret_Manager.Instance.Red_TargetBuilding3 = transform;
            else
                Turret_Manager.Instance.Blue_TargetBuilding3 = transform;
        }
        else if ((transform.position.x <= -73)||(transform.position.x >= 73))
        {
            MaxHP = float.Parse(data[12]["statshp"].ToString());
            HP = MaxHP;
            HPregen = float.Parse(data[12]["statshpregen"].ToString()); //5초당 
            testnum = 6;
            MaxAP = 40;
            AP = float.Parse(data[12]["statsarmor"].ToString());
            MaxMRP = 40;
            MRP = float.Parse(data[12]["statsspellblock"].ToString());

            if (TeamColor)
                Turret_Manager.Instance.Red_TargetBuilding6 = transform;
            else
                Turret_Manager.Instance.Blue_TargetBuilding6 = transform;
        }
        xx = transform.position.x;
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        GetComponentInChildren<HP_Bar>().SetMaxHP(MaxHP, 0);
        GetComponentInChildren<HP_Bar>().SetHP(HP);
        ExplosionEffect.SetActive(false);
        rigidBody = GetComponent<Rigidbody>();
    }

    public void DropHP(float damage)
    {
        damage *= (1 - AP / (100 + AP));
        HP -= damage;
  

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
                    col.GetComponent<Player_Level>().GetEXP(Exp); //경험치 획득
                }
            }
            if (i >= 2) //두명 이상에게 경험치 분배한 경우
            {
                foreach(Collider col in colliderArray)
                {
                    if (col.TryGetComponent<Player_Stats>(out Player_Stats player)
                && (player.TeamColor != TeamColor))
                    {
                        col.GetComponent<Player_Level>().GetEXP(-Exp*0.34f); //경험치 분배
                    }
                }
            }
            StartCoroutine("Explosion");
            rigidBody.useGravity = true;
            if ((transform.position.x >= -58 && transform.position.x < -44)
|| (transform.position.x >= 45 && transform.position.x < 58)) //억제기인 경우
                Turret_Manager.Instance.spawnMinion(TeamColor);

        }
        GetComponentInChildren<HP_Bar>().SetHP(HP);
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

    IEnumerator Explosion()
    {
        while (true)
        {
            PV.RPC("activeExplosion", RpcTarget.AllViaServer, true);
            yield return new WaitForSeconds(1.5f);
            break;
        }
        PV.RPC("DestroyTurret", RpcTarget.AllViaServer);
    }

    [PunRPC]
    void activeExplosion(bool b)
    {
        ExplosionEffect.SetActive(b);
    }
    [PunRPC]
    void DestroyTurret()
    {
        Destroy(gameObject);
    }
}
