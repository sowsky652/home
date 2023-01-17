using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer; // layermask used to isolate raycasts against clickable layers

    public Texture2D pointer; // normal mouse pointer
    public Texture2D target; // target mouse pointer
    public Texture2D doorway; // doorway mouse pointer
    public Texture2D sword;

    public UnityEvent<Vector3> OnClickEnvironment;
    public UnityEvent<GameObject> OnClickAttakable;

    void Update()
    {
        // Raycast into scene
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            bool attakable = hit.collider.GetComponent<IAttakable>() != null;
            if (attakable)
            {
                Cursor.SetCursor(sword, Vector2.zero, CursorMode.Auto);
            }
            else if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (attakable)
                {
                    OnClickAttakable.Invoke(hit.collider.gameObject);
                }
                else if (door)
                {
                    var d= hit.collider.GetComponent<Doorway>();
                    OnClickEnvironment.Invoke(d.OtherPosition);
                }
                else
                {
                    OnClickEnvironment.Invoke(hit.point);
                }
            }

        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

