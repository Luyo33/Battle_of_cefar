using UnityEngine;

public class Selector : MonoBehaviour
{
    public GameObject selector;
    public GameObject mark2;
    public bool IsSelected = false;
    private GameObject selected = null;
    private GameObject marked = null;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (selected == null)
        {
            selected = Instantiate(selector, transform, true);
            selected.transform.position = transform.position;
            IsSelected = true;
        }
    }

    private void OnMouseUpAsButton()
    {
        
    }

    private void OnMouseExit()
    {
        Destroy(selected);
        selected = null;
        IsSelected = false;
    }

    public void Highlight()
    {
        if (marked == null)
        {
            marked = Instantiate(mark2, transform, true);
            marked.transform.position = transform.position;
        }
    }

    public void ExitHighlight()
    {
        Destroy(marked);
        marked = null;
    }
}