using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion

    public GameObject target;
    public GameObject f;
    public CinemachineVirtualCamera vcam;
    public GameObject money;
    public GameObject moneyTarget;
    public int listCount;
    public int duvarChild;
    public GameObject cube;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            // score islemleri.. animasyon.. efect.. collectiblen destroy edilmesi.. 
            if (!NodeMovement.instance.cargo.Contains(other.gameObject))
            {               
                other.gameObject.tag = "stack";
                NodeMovement.instance.StackCube(other.gameObject, NodeMovement.instance.cargo.Count -1);

            }
          
            GameManager.instance.IncreaseScore();

        }
        else if (other.CompareTag("obstacle"))
        {
            // score islemleri.. animasyon.. efect.. obstaclein destroy edilmesi.. 
            // oyun bitebilir bunun kontrolu de burada yapilabilir..

            if (gameObject.transform.childCount>1)
            {
                StartCoroutine(Shake());
                Destroy(GameObject.FindGameObjectWithTag("last"));
                NodeMovement.instance.cargo.Remove(GameObject.FindGameObjectWithTag("last"));
                GameManager.instance.DecreaseScore();

            }
            else
            {

                UiController.instance.OpenLosePanel();
            }
           
        }
        else if (other.CompareTag("finish"))
        {
            // oyun sonu olaylari... animasyon.. score.. panel acip kapatmak
            // oyunu kazandi mi kaybetti mi kontntrolu gerekirse yapilabilir.
            // player durdurulur. tagi finish olan obje level prefablarinin icinde yolun sonundad�r.
            // ornek olarak asagidaki kodda score 10 dan buyukse kazan degilse kaybet dedik ancak
            // baz� oyunlarda farkli parametlere g�re kontrol etmek veya oyun sonunda karakterin yola devam etmesi gibi
            // durumlarda developer buray� kendisi duzenlemelidir.
           
            listCount = NodeMovement.instance.cargo.Count-1;
            StartCoroutine(Complete(listCount));
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }
        
        else if (other.CompareTag("duvar"))
        {
            PlayerMovement.instance.speed = 0;
            GameObject child = GameObject.Find("Cube");
            duvarChild = child.transform.childCount;

            StartCoroutine(duvarX(duvarChild));

            Debug.Log("�arpt�");
          
        }

    }
    
    public IEnumerator Complete(int adet)
    {
       // yield return new WaitForSeconds(.05f);
        target = GameObject.Find("completeTarget");
        float y = target.transform.position.y;

        StartCoroutine(instantiateMoney(listCount));

        for (int i = 0; i < adet; i++)
        {
            //GameManager.instance.isContinue = false;
            SwerveMovement.instance.swerve = false;
            if (gameObject.transform.childCount > 0)
            {
                PlayerMovement.instance.speed = 1f;
                gameObject.transform.GetChild(gameObject.transform.childCount - 1).DOJump(new Vector3(target.transform.position.x, y, target.transform.position.z), 1, 1, .2f)
                .OnComplete(() => gameObject.transform.GetChild(gameObject.transform.childCount - 1).parent = target.transform
                );
                NodeMovement.instance.cargo.Remove(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
                
                yield return new WaitForSeconds(.8f);
                y += 1;
               
            }
        }
        yield return new WaitForSeconds(.05f);
        // GameManager.instance.isContinue = false;
        SwerveMovement.instance.swerve = false;

        PlayerMovement.instance.speed = 2.8f;


        
    }
    public IEnumerator duvarX(int adet)
    {
        target = GameObject.Find("duvarTarget");
        GameObject child = GameObject.Find("Cube");
        float y = target.transform.position.y+1;
        Debug.Log("�arpt�");
        for (int i = 0; i < adet; i++)
        {
            GameManager.instance.isContinue = false;
            SwerveMovement.instance.swerve = false;
            if (child.transform.childCount > 0)
            {             
               child.transform.GetChild(child.transform.childCount - 1).DOJump(new Vector3(target.transform.position.x, y, target.transform.position.z), 1, 1, .2f)
                    .OnComplete(() => child.transform.GetChild(child.transform.childCount - 1).parent = target.transform
                );
                yield return new WaitForSeconds(.5f);
                y += 1;
            }
        }
        if (child.transform.childCount == 0)
        {
            UiController.instance.OpenWinPanel();

        }
    }
  
    public IEnumerator instantiateMoney(int adet)
    {
        yield return new WaitForSeconds(.5f);
        float y =moneyTarget.transform.position.y+2;
        for (int i = 0; i < adet; i++)
        {
          
          GameObject ss= Instantiate(money,new Vector3( moneyTarget.transform.position.x,y, moneyTarget.transform.position.z), Quaternion.identity);
          ss.transform.parent = moneyTarget.transform;
          y += 1;
          yield return new WaitForSeconds(.8f);

        }
    }



    /// <summary>
    /// next level veya restart level butonuna tiklayinca karakter sifir konumuna tekrar alinir. (baslangic konumu)
    /// varsa animasyonu ayarlan�r. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon i�inde yapilir.
    /// </summary>

    public void PreStartingEvents()
	{
        PlayerMovement.instance.transform.position = Vector3.zero;
        GameManager.instance.isContinue =false;
        SwerveMovement.instance.swerve = false;
       
	}

    /// <summary>
    /// taptostart butonuna t�klan�nca (ya da oyun basi ilk dokunus) karakter kosmaya baslar, belki hizi ayarlan�r, animasyon scale rotate
    /// gibi degerleri degistirilecekse onlar bu fonksiyon icinde yapilir...
    /// </summary>
    public void PostStartingEvents()
	{
        GameManager.instance.levelScore = 0;
        GameManager.instance.isContinue = true;
        SwerveMovement.instance.swerve =true;

    }
    public IEnumerator Shake()
    {
        
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        yield return new WaitForSeconds(0.5f);
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
}
