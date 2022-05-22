using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    [SerializeField] private float swerveSpeed = .5f;
    [SerializeField] private float maxSwerveAmount = 2;
    [SerializeField] private float maxHorizontalDistance = 2;

    // Tiklama ile hizli yer degisiminin onune gecer.
    // TODO: katsayilari kontrol et.
    [SerializeField] private bool checkDistanceChange = true;
    [SerializeField] private float maxHorizontalChange = .5f;
    
    private float deltaPos;
    private float lastMousePosX;
    private float lastPositonChange;
    public bool swerve;

    #region Singleton
    public static SwerveMovement instance;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    #endregion

    private void Update()
    {
        if (swerve== true)
        {
            //UiController.instance.tapToStartPanel.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                // oyunu baþlatýyoruz.. karakter ileri hareket etmeye baþlýyor..
                if (!GameManager.instance.isContinue && UiController.instance.tapToStartPanel.activeInHierarchy)
                {
                    PlayerController.instance.PostStartingEvents();
                    UiController.instance.tapToStartPanel.SetActive(false);
                    GameObject first = NodeMovement.instance.cargo[0];
                    
                    //NodeMovement.instance.Lerp();
                }

                lastMousePosX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                deltaPos = Input.mousePosition.x - lastMousePosX;
                lastMousePosX = Input.mousePosition.x;
                NodeMovement.instance.Lerp();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                deltaPos = 0;
                NodeMovement.instance.Origin();
            }

            var swerve = Time.deltaTime * swerveSpeed * deltaPos;
            swerve = Mathf.Clamp(swerve, -maxSwerveAmount, maxSwerveAmount);
            if (transform.childCount > 0)
            {
                var x = transform.GetChild(0).transform.position.x + swerve;
                if (x < maxHorizontalDistance && x > -maxHorizontalDistance)
                    if (checkDistanceChange)
                    {
                        if (Mathf.Abs(x - lastPositonChange) < maxHorizontalChange) transform.GetChild(0).transform.Translate(swerve, 0, 0);
                    }
                    else
                        transform.GetChild(0).transform.Translate(swerve, 0, 0);


                lastPositonChange = x;
            }
        }
    }
}
