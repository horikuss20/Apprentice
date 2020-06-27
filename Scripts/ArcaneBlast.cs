using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBlast : MonoBehaviour
{
    public GameObject HealthUI;
	GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
		gm = GameObject.Find("GameManager");
        HealthUI = GameObject.Find("HealthUI");
    }

    private void OnTriggerEnter(Collider other)
    {
        #region Damage
        if (other.gameObject.tag == "Player")
        {
			if(gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Castle || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Castle)
			{
				HealthUI.GetComponent<Health>().DamageHalf();
			}
			if (gm.GetComponent<PendantSystemNew>().pSide1 != gm.GetComponent<PendantSystemNew>().Castle && gm.GetComponent<PendantSystemNew>().pSide2 != gm.GetComponent<PendantSystemNew>().Castle)
			{
				HealthUI.GetComponent<Health>().Damage(1);
			}
			if(gm.GetComponent<PendantSystemNew>().pSide1 == gm.GetComponent<PendantSystemNew>().Bear || gm.GetComponent<PendantSystemNew>().pSide2 == gm.GetComponent<PendantSystemNew>().Bear)
			{
				gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(2);
				Debug.Log("RAWR");
			}
        }
        #endregion
    }
}
