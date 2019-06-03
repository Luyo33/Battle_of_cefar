using UnityEngine;

public class Selector : MonoBehaviour
{
    public GameObject selector;
    public GameObject mark2;
    public GameObject enemyMoveRange;
    public GameObject MoveRangeWhenCantMove;
    public bool IsSelected = false;
    private GameObject selected = null;
    private GameObject marked = null;
    private GameObject enemyMoveRangeMarked = null;
    private GameObject MoveWhenCantMarked = null;
    
    
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

    public void HighlightMoveWhenCan()
    {
        if (marked == null)
        {
            marked = Instantiate(mark2, transform, true);
            marked.transform.position = transform.position;
        }
    }

    public void HighlightMoveWhenCant()
    {
        if (MoveWhenCantMarked == null)
        {
            MoveWhenCantMarked = Instantiate(MoveRangeWhenCantMove, transform, true);
            MoveWhenCantMarked.transform.position = transform.position;
        }
    }
    
    public void HighlightEnemyMove()
    {
        if (enemyMoveRangeMarked == null)
        {
            enemyMoveRangeMarked = Instantiate(enemyMoveRange, transform, true);
            enemyMoveRangeMarked.transform.position = transform.position;
        }
    }

    public void ExitHighlight()
    {
        Destroy(marked);
        marked = null;
        Destroy(MoveWhenCantMarked);
        MoveWhenCantMarked = null;
        Destroy(enemyMoveRangeMarked);
        enemyMoveRangeMarked = null;
    }

}