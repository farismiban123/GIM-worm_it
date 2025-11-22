using UnityEngine;

public class IsopodAnimator : MonoBehaviour
{
    public GameObject IsopodParent;
    public void IsopodKeluarAnim()
    {
        IsopodParent.GetComponent<Isopod>().IsopodKeluar();
    }

    public void IsopodMasukAnim()
    {
        IsopodParent.GetComponent<Isopod>().IsopodMasuk();
    }
}
