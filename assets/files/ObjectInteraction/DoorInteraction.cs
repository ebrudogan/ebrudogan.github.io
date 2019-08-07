using UnityEngine;
using System.Collections;

//Your class name MATCH with your script file name
public class DoorInteraction : MonoBehaviour {

    //Enum to keep track of door's state
    public enum DoorState { open, close, inProcess };

    public DoorState state;     //Variable to change and keep track of door's object
    public AudioClip doorSound; //VAriable to store audio file
    private Color defaultColor; //Variable to store the default color of the object

	// Use this for initialization
	void Start () {
        //Initially, door is closed
        state = DoorState.close;

        //Initialize object's default color
        defaultColor = renderer.material.GetColor("_Color");
	}

    //When the mouse enters the object
    public void OnMouseEnter() {
        Debug.Log("Mouse Enter");
        //If mouse is over object change its color
        changeApperance(true);
    }

    //When the mouse exits th object
    public void OnMouseExit() {
        Debug.Log("Mouse Exit");
        //If mouse is NOT over object don't change its color
        changeApperance(false);
    }

    //When user clickes the left mouse button 
    public void OnMouseDown() {
        Debug.Log("Mouse Down");

        switch(state){
            case DoorState.open:
                state = DoorState.inProcess;    //Opening action is started and it's in process, NOT finished
                StartCoroutine("CloseDoor");    
                break;
            case DoorState.close:
                state = DoorState.inProcess;    //Closing action is started and it's in process, NOT finished
                StartCoroutine("OpenDoor");
                break;
        }
    }

    //Play door opening animation
    private IEnumerator OpenDoor(){
        //Play "DoorOpen" animation
        animation.Play("DoorOpen");

        //Play audio file attached to door object
        audio.PlayOneShot(doorSound);

        //Wait for duration of DoorOpen animation
        yield return new WaitForSeconds(animation["DoorOpen"].length);

        //After opening animation and audio play is done, change door object's state to open
        state = DoorState.open;
    }

    //Play door closing animation
    private IEnumerator CloseDoor()
    {
        //Play "DoorClose" animation
        animation.Play("DoorClose");

        //Play audio file attached to door object
        audio.PlayOneShot(doorSound);

        //Wait for duration of DoorClose animation
        yield return new WaitForSeconds(animation["DoorClose"].length);

        //After closing animation and audio play is done, change door object's state to close
        state = DoorState.close;
    }

    //Change apperance when object is highlighted (mouse over)
    private void changeApperance(bool highlight) {
        //If object is highlighted, change object color to green
        if (highlight)
        {
            renderer.material.SetColor("_Color", Color.green);
        }
        //If object is NOT highlighted, change object color to its default
        else {
            renderer.material.SetColor("_Color", defaultColor);
        }
    }
}
