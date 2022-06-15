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
    public GameObject duvarTarget;
    public GameObject cameraLookAt;
    public GameObject firstCube;
   
    public Animator anim;
 
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
            GameManager.instance.DecreaseScore();
        }
        else if (other.CompareTag("finish"))
        {        
            SwerveMovement.instance.swerve = false;
        }            
    }

  /// <summary>
  /// paketleri g�nderdik�e o6yuncunun eline para g�nderir.
  /// </summary>
  /// <param name="adet">paket say�s�</param>
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
    /// varsa animasyonu ayarlan�r. varsa scale rotation gibi degerleri sifirlanir.. varsa ekipman collectible v.s. gibi seyler temizlenir
    /// score v.s. sifirlanir. bu gibi durumlar bu fonksiyon i�inde yapilir.
    /// </summary>

    public void PreStartingEvents()
	{
        StartCoroutine(AllahBelan�());
        Debug.Log(NodeMovement.instance.cargo.Count);
        PlayerMovement.instance.transform.position = Vector3.zero;
        GameManager.instance.isContinue = false;
        SwerveMovement.instance.swerve = true;    
        firstCube.transform.localPosition = new Vector3(0,-0.8f,0);
        vcam.LookAt = cameraLookAt.transform;
        vcam.Follow = transform;
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 7.68f, -9f);
        anim.SetBool("idle", true);



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
        PlayerMovement.instance.speed = 10f;
        anim.SetBool("anim", true);


    }
    public IEnumerator AllahBelan�()
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
