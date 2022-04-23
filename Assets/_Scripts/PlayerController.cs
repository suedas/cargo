using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
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
    public TextMeshProUGUI motorText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("collectible"))
        {
            // score islemleri.. animasyon.. efect.. collectiblen destroy edilmesi.. 
            if (!NodeMovement.instance.cargo.Contains(other.gameObject))
            {
               
                other.gameObject.tag = "Untagged";         
                NodeMovement.instance.StackCube(other.gameObject, NodeMovement.instance.cargo.Count -1);

            }

           // Debug.Log("collectible");
            
            GameManager.instance.IncreaseScore();

        }
        else if (other.CompareTag("obstacle"))
        {
            // score islemleri.. animasyon.. efect.. obstaclein destroy edilmesi.. 
            // oyun bitebilir bunun kontrolu de burada yapilabilir..
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
            GameManager.instance.isContinue = false;
            Debug.Log(GameManager.instance.levelScore);
            if (GameManager.instance.levelScore > 10) UiController.instance.OpenWinPanel();
            else UiController.instance.OpenLosePanel();
        }
        else if (other.CompareTag("motor"))
        {


            //StartCoroutine( destroyet(2));
            StartCoroutine(yolla(2));
           
           
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            
           
            Debug.Log("motor");
            Debug.Log(gameObject.transform.childCount);
        }
        else if (other.CompareTag("car"))
        {
            //StartCoroutine(yolla(5));
            //other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }
        else if (other.CompareTag("plane"))
        {
            //StartCoroutine(yolla(8));
            //other.gameObject.GetComponent<BoxCollider>().enabled = false;

        }

    }
    public IEnumerator yolla(int adet)
    {
        // float y =0;

        target = GameObject.Find("Target");
        float y = target.transform.position.y;
        for (int i = 0; i < adet; i++)
        {
            if (gameObject.transform.childCount>0)
            {
                PlayerMovement.instance.speed =2.3f;
                gameObject.transform.GetChild(gameObject.transform.childCount - 1).DOJump(new Vector3(target.transform.position.x,y, target.transform.position.z), 1, 1, .2f)
                .OnComplete(() => gameObject.transform.GetChild(gameObject.transform.childCount - 1).parent = target.transform);
                NodeMovement.instance.cargo.Remove(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
                //int targetChild = target.transform.childCount;
                //motorText.text = targetChild.ToString();
                yield return new WaitForSeconds(.5f);
                y +=1;

            }
            else
            {
                UiController.instance.OpenLosePanel();
            }
           

        }
        //yield return new WaitForSeconds(.05f);
        //Destroy(other);
        PlayerMovement.instance.speed = 4f;
    }
    //public IEnumerator destroyet(int adet)
    //{
    //    for (int i = 0; i <adet; i++)
    //    {
    //        if (gameObject.transform.childCount>0)
    //        {
    //            Destroy(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
    //            NodeMovement.instance.cargo.Remove(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
    //            PlayerMovement.instance.speed = 0;

    //        }
    //        yield return new WaitForSeconds(0.5f);

    //    }
    //}

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
}
