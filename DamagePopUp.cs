using UnityEngine;
using TMPro;
public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private float disapperTimer = 1;
    private Color textColor;
    private float disappearSpeed = 1;
    private Vector3 moveVector;
    private static int sortingOrder;
    public static DamagePopUp Create(Vector3 position , int damageAmount , bool isCritical)
    {
        GameObject damageTransform = Pools.Instance.SpawnFromPool("Damage", position + Vector3.up, Quaternion.Euler(45, 0, 0));
        Transform dTR = damageTransform.transform;
        DamagePopUp damagePopUp = dTR.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount, isCritical);
        return damagePopUp;
    }
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount , bool isCriticalHit)
    {
        transform.localScale = Vector3.one;
        textMesh.SetText(damageAmount.ToString());
        if (isCriticalHit)
        {
            textMesh.fontSize = 7;
            textColor = Color.red;
        }
        else
        {
            textMesh.fontSize = 5;
            textColor = Color.white;
        }
        textMesh.color = textColor;

        moveVector = Vector3.up * 5;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= Vector3.up * 15f * Time.deltaTime;
        disapperTimer -= Time.deltaTime;
        if (disapperTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseAmount = 1f;
            transform.localScale += Vector3.one * increaseAmount * Time.deltaTime;
        }
        else
        {
            float increaseAmount = 1f;
            transform.localScale -= Vector3.one * increaseAmount * Time.deltaTime;
        }
        if (disapperTimer > 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0.01)
            {
                textColor.a = 1;
                textMesh.color = textColor;
                disapperTimer = 1;
                gameObject.SetActive(false);
            }
        }
        else
        {
            textColor.a = 1;
            textMesh.color = textColor;
            disapperTimer = 1;
            gameObject.SetActive(false);
        }
    }
}
