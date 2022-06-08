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
    public CinemachineVirtualCamera vcam;
    public GameObject money;
   // public GameObject moneyTarget;
    public GameObject duvarTarget;
    public GameObject cameraLookAt;
    public GameObject firstCube;
   
    public Animator anim;
    
   // public int listCount;
   // public int duvarChild;
 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("collectible"))
        {
            GameObject player = GameObject.Find("Player");
            // score islemleri.. animasyon.. efect.. collectiblen destroy edilmesi.. 
            if (!NodeMovement.instance.cargo.Contains(other.gameObject))
            {               
                other.gameObject.tag = "stack";
                NodeMovement.instance.StackCube(other.gameObject, NodeMovement.instance.cargo.Count -1);
                if (player.transform.childCount > 0)
                {
                    GameObject ss = player.transform.GetChild(player.transform.childCount - 1).gameObject;
                    ss.tag = "last";
                    if (player.transform.GetChild(player.transform.childCount - 1).gameObject.tag == "last")
                    {
                        for (int i = 1; i < player.transform.childCount - 1; i++)
                        {
                            player.transform.GetChild(i).gameObject.tag = "stack";
                        }

                    }
                    else
                    {
                        GameObject son = player.transform.GetChild(player.transform.childCount - 1).gameObject;
                        son.tag = "last";
                    }
                }

            }

            GameManager.instance.IncreaseScore();

        }
        else if (other.CompareTag("obstacle"))
        {
            // score islemleri.. animasyon.. efect.. obstaclein destroy edilmesi.. 
            // oyun bitebilir bunun kontrolu de burada yapilabilir..
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
            SwerveMovement.instance.swerve = false;

            // target = GameObject.Find("completeTarget");
            //  listCount = NodeMovement.instance.cargo.Count-1;
            // Debug.Log(listCount);

            //Tamamla();
           // StartCoroutine(complete());
            //vcam.enabled = false;
            //vcam.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
            //vcam.enabled = true;


        }
       
       

    }


    void Tamamla()
	{
        target = GameObject.Find("completeTarget");
        foreach (GameObject obj in NodeMovement.instance.cargo)
		{

			if (!obj.transform.CompareTag("Player"))
			{
                obj.transform.DOKill();
                obj.transform.DOMove(target.transform.position, .3f).OnComplete(() => obj.transform.parent = target.transform);
            }
            else
            {
                GameObject cameraTarget = GameObject.Find("cameraTarget");
                duvarTarget = GameObject.Find("duvarTarget");
                PlayerMovement.instance.speed = 0;
                vcam.LookAt = cameraTarget.transform;
                vcam.Follow = cameraTarget.transform;
                vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 1, -9f);
                StartCoroutine(instantiateMoney(target.transform.childCount * 2));
            }
        }
	}


    /// <summary>
    /// fnish çizgizine geldiðinde elindeki paketleri makineye gönderir.
    /// </summary>
    /// <param name="adet">elindeki paket sayýsý</param>
    /// <returns></returns>

    public IEnumerator complete()
    {
        //yield return new WaitForSeconds(.05f);
        PlayerMovement.instance.speed = 3f;
        target = GameObject.Find("completeTarget");

        if (transform.childCount > 1)
        {
            gameObject.transform.GetChild(gameObject.transform.childCount - 1).DOMove(target.transform.position, .2f).OnComplete(()=> gameObject.transform.GetChild(gameObject.transform.childCount - 1).parent = target.transform);
            NodeMovement.instance.count--;
            NodeMovement.instance.cargo.Remove(gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject);
            yield return new WaitForSeconds(.2f);
            //gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.SetActive(false);
        }
        else
        {
            GameObject cameraTarget = GameObject.Find("cameraTarget");
            PlayerMovement.instance.speed = 0;
            vcam.LookAt = cameraTarget.transform;
            vcam.Follow = cameraTarget.transform;
            vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 1, -9f);
            StartCoroutine(instantiateMoney(target.transform.childCount*2));
        }

           
            //gameObject.transform.DOMove(target.transform.position, .8f);

    }
    
  /// <summary>
  /// paketleri gönderdikçe o6yuncunun eline para gönderir.
  /// </summary>
  /// <param name="adet">paket sayýsý</param>
  /// <returns></returns>
    public IEnumerator instantiateMoney(int adet)
    {

        GameObject complete = GameObject.Find("completeTarget");
        duvarTarget = GameObject.Find("duvarTarget");
        GameObject cameraTarget = GameObject.Find("cameraTarget");

        yield return new WaitForSeconds(.5f);
        float y =duvarTarget.transform.position.y;
        float cameraY = cameraTarget.transform.position.y;
        for (int i = 0; i < adet; i++)
        {         
          GameObject ss= Instantiate(money,new Vector3(duvarTarget.transform.position.x,y, duvarTarget.transform.position.z), Quaternion.Euler(-90,90,0));
          ss.transform.parent = duvarTarget.transform;
          cameraTarget.transform.position = new Vector3(cameraTarget.transform.position.x, cameraY, cameraTarget.transform.position.z);
           y += .3f;
          cameraY += .3f;
          yield return new WaitForSeconds(.05f);
         
        }
        if (complete.transform.childCount*2==duvarTarget.transform.childCount)
        {
            yield return new WaitForSeconds(.2f);
            UiController.instance.OpenWinPanel();
        }
    }
    public void WinEvent()
	{
        GameObject cameraTarget = GameObject.Find("cameraTarget");
        GameObject target = GameObject.Find("completeTarget");
        PlayerMovement.instance.speed = 0;
        vcam.LookAt = cameraTarget.transform;
        vcam.Follow = cameraTarget.transform;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 1, -9f);
        int money = target.transform.childCount;
        StartCoroutine(instantiateMoney(money*2));
    }

    /// <summary>
    /// next level veya restart level butonuna tiklayinca karakter sifir konumuna tekrar alinir. (baslangic konumu)
    /// varsa animasyonu ayarlanýr. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon içinde yapilir.
    /// </summary>

    public void PreStartingEvents()
	{
        StartCoroutine(AllahBelaný());
        Debug.Log(NodeMovement.instance.cargo.Count);
        PlayerMovement.instance.transform.position = Vector3.zero;
        GameManager.instance.isContinue = false;
        SwerveMovement.instance.swerve = true;
        //kamerayý düzeltmiyor
        //vcam.enabled = false;
        //vcam.transform.SetPositionAndRotation(new Vector3(0, 7.68f, -7.06f), Quaternion.Euler(new Vector3(0.8f,0,0)));
        //vcam.enabled = true;
        
        firstCube.transform.localPosition = new Vector3(0,-0.8f,0);
        vcam.LookAt = cameraLookAt.transform;
        vcam.Follow = transform;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 7.68f, -9f);



    }

    /// <summary>
    /// taptostart butonuna týklanýnca (ya da oyun basi ilk dokunus) karakter kosmaya baslar, belki hizi ayarlanýr, animasyon scale rotate
    /// gibi degerleri degistirilecekse onlar bu fonksiyon icinde yapilir...
    /// </summary>

    public void PostStartingEvents()
	{

        GameManager.instance.levelScore = 0;
        GameManager.instance.isContinue = true;
        SwerveMovement.instance.swerve =true;
        PlayerMovement.instance.speed = 10f;
        anim.SetBool("anim", true);


    }
    public IEnumerator AllahBelaný()
    {
        int list = NodeMovement.instance.cargo.Count;
        Debug.Log(list);

        for (int i = 1; i < list; i++)
        {
            NodeMovement.instance.count--;
            NodeMovement.instance.cargo.Remove(transform.GetChild(transform.childCount - 1).gameObject);
            Destroy(transform.GetChild(transform.childCount - 1).gameObject);
            yield return new WaitForSeconds(.001f);

        }
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
