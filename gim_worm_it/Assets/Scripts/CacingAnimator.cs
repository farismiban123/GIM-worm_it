using UnityEngine;

public class CacingAnimator : MonoBehaviour
{
    public GameObject CacingParent;

    public void CacingKeluarAnim()
    {
        CacingParent.GetComponent<WormMovement>().CacingKeluar();
    }

    public void CacingMasukAnim()
    {
        CacingParent.GetComponent<WormMovement>().CacingMasuk();
    }
}
