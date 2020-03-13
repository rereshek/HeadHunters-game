using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public bool start;
	public bool quit;
	//public bool htp;
	//public bool x;

	public GameObject htplay;


	//public Transitions transitions;

	// Start is called before the first frame update
	void Start()
	{
		//htplay.SetActive(false);
	}

	private void Update()
	{

	}

	void OnMouseEnter()
	{
		gameObject.GetComponent<TextMesh>().color = Color.HSVToRGB(239, 91, 100);
	}

	void OnMouseExit()
	{
		gameObject.GetComponent<TextMesh>().color = Color.white;
	}

	void OnMouseUp()
	{
		if (start)
			{
				//transitions.TransitionToLevel(1);
				Debug.Log("lvl1");
			}
			if (quit)
			{
				Application.Quit();
				Debug.Log("quit");
			}
			//if (htp)
			//{	
				//Debug.Log("htp");
				//htplay.SetActive(true);
			//}
			//if (x)
			//{
				//htplay.SetActive(false);
			//}
		}
}
