using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Healt : MonoBehaviour {

    public float healt = 5;
	private float starthealt;
    public GameObject DeathEffect;
	public GameObject findimage;
	public GameObject canvas;
	public Image HealtBar;
	public float defenceShield;
	public float defenceArmor;
	public GameObject fading = null;

	public void GetKilled()
	{
		Destroy(gameObject);
	}
	private void Start()
	{
		findimage = transform.Find("Canvas/Healtbg/HealtBar").gameObject;
		canvas = transform.Find("Canvas").gameObject;
		canvas.SetActive(false);
		starthealt = healt;
		HealtBar = findimage.GetComponent<Image>();
		
	}
	void Update () {
		if (healt < starthealt)
		{
			canvas.SetActive(true);
		}
		HealtBar.fillAmount = healt / starthealt;

	}
	public void GetShot(float damage)
	{
		healt -= damage - defenceShield/2;
		if (healt <= 0)
		{
			Destroy(gameObject);
			GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, transform.rotation);
			Destroy(effect, 2f);
			if (fading != null)
			{
				fading.GetComponent<Fading>().StartFade();
			}
			return;
		}
	}
    public void GetHit(float damage)
    {
        healt -= damage;
		if (healt <= 0)
		{
			Destroy(gameObject);
			GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, transform.rotation);
			Destroy(effect, 2f);
			if (fading != null)
			{
				fading.GetComponent<Fading>().StartFade();
			}
			return;
		}
	}
}
