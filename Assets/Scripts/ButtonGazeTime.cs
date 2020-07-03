using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonGazeTime : MonoBehaviour {

    public float time = 2.0f;
    public float timer = 0;

    public bool isEnter = false;

    void Update()
    {
        if (isEnter)
        {
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            timer = 0;
        }
        
    }

	public void OnGazeEnter()
    {
        isEnter = true;
    }
    public void OnGazeExit()
    {
        isEnter = false;
    }

    public void OnClicked()
    {
        isEnter = false;
        timer = 0;
    }
}
