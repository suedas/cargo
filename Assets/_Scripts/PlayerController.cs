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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            // score islemleri.. animasyon.. efect.. collectiblen destroy edilmesi.. 
            if (!NodeMovement.instance.cargo.Contains(other.gameObject))
            {               
                other.gameObject.tag = "Untagged";
                //other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                NodeMovement.instance.StackCube(other.gameObject, NodeMovement.instance.cargo.Count -1);

            }

            
            GameManager.instance.IncreaseScore();

        }
        else if (other.CompareTag("obstacle"))
        {
            // score islemleri.. animasyon.. efect.. obstaclein destroy edilmesi.. 
            // oyun bitebilir bunun kontrolu de burada yapilabilir..
            StartCoroutine(Shake());
            Debug.Log("obstacle");
            GameManager.instance.DecreaseScore();
        }
        else if (other.CompareTag("finish"))
        {
            // oyun sonu olaylari... animasyon.. score.. panel acip kapatmak
            // oyunu kazandi mi kaybetti mi kontntrolu gerekirse yapilabilir.
            // player durdurulur. tagi finish olan obje level prefablarinin icinde yolun sonundadýr.
            // ornek olarak asagidaki kodda score 10 dan buyukse kazan degilse kaybet dedik ancak
            // bazý oyunlarda farkli parametlere göre kontrol etmek veya oyun sonunda karakterin yola devam etmesi gibi
            // durumlarda developer burayý kendisi duzenlemelidir.
           
            listCount = NodeMovement.instance.cargo.Count-1;
            StartCoroutine(goComplete(listCount));
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }
        else if (other.CompareTag("motor"))
        {
            //StartCoroutine( destroyet(2));
            StartCoroutine(goMotor(2));
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
                   
            Debug.Log("motor");
         
        }
        else if (other.CompareTag("car"))
        {
           
            StartCoroutine(goCar(3));
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }
        else if (other.CompareTag("plane"))
        {
            StartCoroutine(goPlane(4));
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }
        else if (other.CompareTag("duvar"))
        {
            PlayerMovement.instance.speed = 0;
            GameObject child = GameObject.Find("Cube");
            int duvarChild = child.transform.childCount;

            StartCoroutine(duvarX(duvarChild));
            Debug.Log("çarptý");
        }
        
    }
    public IEnumerator goMotor(int adet)
    {
        target = GameObject.Find("motorTarget");   
        float y = target.transform.position.y;
        for (int i = 0; i < adet; i++)
        {
            GameManager.instance.isContinue = false;
            if (gameObject.transform.childCount>0)
            {
                PlayerMovement.instance.speed =2.3f;
                gameObject.transform.GetChild(gameObject.transform.childCount - 1).DOJump(new Vector3(target.transform.position.x, y, target.transform.position.z), 1, 1, .2f)
                .OnComplete(() => gameObject.transform.GetChild(gameObject.transform.childCount - 1).parent = target.transform
                );
                NodeMovement.instance.cargo.Remove(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
                yield return new WaitForSeconds(.5f);
                y +=1;
            }
            else
            {
                //UiController.instance.OpenLosePanel();
            }
           
        }
        yield return new WaitForSeconds(.5f);
        GameManager.instance.isContinue = true;

        if (target.transform.childCount == 2)
        {
            taskComplete();
        }
        PlayerMovement.instance.speed = 4f;
    }

    public IEnumerator goCar(int adet)
    {
        target = GameObject.Find("carTarget");
        float y = target.transform.position.y;
        for (int i = 0; i < adet; i++)
        {
            GameManager.instance.isContinue = false;
            if (gameObject.transform.childCount > 0)
            {
                PlayerMovement.instance.speed = 2.3f;
                gameObject.transform.GetChild(gameObject.transform.childCount - 1).DOJump(new Vector3(target.transform.position.x, y, target.transform.position.z), 1, 1, .2f)
                .OnComplete(() => gameObject.transform.GetChild(gameObject.transform.childCount - 1).parent = target.transform
                );
                NodeMovement.instance.cargo.Remove(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
                yield return new WaitForSeconds(.5f);
                y += 1;
            }
            else
            {
               // UiController.instance.OpenLosePanel();
            }

        }
        yield return new WaitForSeconds(.5f);
        GameManager.instance.isContinue = true;
    
        if (target.transform.childCount == 3)
        {
            taskComplete();
        }

        PlayerMovement.instance.speed = 4f;
    }
    public IEnumerator goPlane(int adet)
    {
        target = GameObject.Find("planeTarget");
        float y = target.transform.position.y;
        for (int i = 0; i < adet; i++)
        {
            GameManager.instance.isContinue = false;
            if (gameObject.transform.childCount > 0)
            {
                PlayerMovement.instance.speed = 2.3f;
                gameObject.transform.GetChild(gameObject.transform.childCount - 1).DOJump(new Vector3(target.transform.position.x, y, target.transform.position.z), 1, 1, .2f)
                .OnComplete(() => gameObject.transform.GetChild(gameObject.transform.childCount - 1).parent = target.transform
                );
                NodeMovement.instance.cargo.Remove(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
                yield return new WaitForSeconds(.5f);
                y += 1;
            }
            else
            {
                //UiController.instance.OpenLosePanel();
            }

        }
        yield return new WaitForSeconds(.5f);
        GameManager.instance.isContinue = true;

        if (target.transform.childCount == 4)
        {
            taskComplete();
        }

        PlayerMovement.instance.speed = 4f;
    }
    public IEnumerator goComplete(int adet)
    {
       // yield return new WaitForSeconds(.05f);
        target = GameObject.Find("completeTarget");
        float y = target.transform.position.y;

        StartCoroutine(instantiateMoney(listCount));

        for (int i = 0; i < adet; i++)
        {
            GameManager.instance.isContinue = false;
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
        GameManager.instance.isContinue = false;
        PlayerMovement.instance.speed = 1f;


        //Debug.Log(GameManager.instance.levelScore);
        //if (GameManager.instance.levelScore > 10) UiController.instance.OpenWinPanel();
        //else UiController.instance.OpenLosePanel();
    }
    public IEnumerator duvarX(int adet)
    {
        target = GameObject.Find("duvarTarget");
        GameObject child = GameObject.Find("Cube");
        float y = target.transform.position.y+1;
        Debug.Log("çarptý");
  
        for (int i = 0; i < adet; i++)
        {
            GameManager.instance.isContinue = false;
            if (child.transform.childCount > 0)
            {
               
                child.transform.GetChild(child.transform.childCount - 1).DOJump(new Vector3(target.transform.position.x, y, target.transform.position.z), 1, 1, .2f)
                    .OnComplete(() => child.transform.GetChild(child.transform.childCount - 1).parent = target.transform
                );
                yield return new WaitForSeconds(1f);
                y += 1;

            }
        }
       
    }
    public void taskComplete()
    {
        if (NodeMovement.instance.cargo.Count > 0)
        {
            target.transform.DOMove(new Vector3(0.1f, 0, 111f), 2f).OnComplete(() => Destroy(target));

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
    /// varsa animasyonu ayarlanýr. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon içinde yapilir.
    /// </summary>

    public void PreStartingEvents()
	{
        PlayerMovement.instance.transform.position = Vector3.zero;
        GameManager.instance.isContinue = false;
	}

    /// <summary>
    /// taptostart butonuna týklanýnca (ya da oyun basi ilk dokunus) karakter kosmaya baslar, belki hizi ayarlanýr, animasyon scale rotate
    /// gibi degerleri degistirilecekse onlar bu fonksiyon icinde yapilir...
    /// </summary>
    public void PostStartingEvents()
	{
        GameManager.instance.levelScore = 0;
        GameManager.instance.isContinue = true;
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
