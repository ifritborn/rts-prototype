using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildingPlacementManager : MonoBehaviour
{

    [SerializeField] private GameObject testPrefab;
    [SerializeField] private Tilemap BuildingGrid;
    [SerializeField] private Tilemap ValidBuildGrid;

    [SerializeField] private Tilemap PlayerBuildZone;

    [SerializeField] private TileBase validTile;

    [SerializeField] private TileBase invalidTile;
    private TilemapRenderer renderer;
    private bool inBuildMode = false;
    private bool inPlaceMode = false;
    private bool isValidToPlace;

    private GameObject ghostBuilding;

    Vector3Int currentGridCell;

    Vector3Int lastGridCell;





    private Grid grid;

    // ----------------------------------------------------------------------------------------------------------------

    void Start()
    {
        grid = this.GetComponent<Grid>();
        renderer = BuildingGrid.GetComponent<TilemapRenderer>();
        renderer.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        enterBuildMode();
    }


    // ----------------------------------------------------------------------------------------------------------------



    private void enterBuildMode()
    {

        if (Input.GetKeyUp(KeyCode.B))
        {
            toggleBuildGrid();
            return;
        }

        if (inBuildMode && !inPlaceMode && Input.GetMouseButtonDown(0))
        {
            togglePlaceMode();
            ghostBuilding = spawnGhostBuilding();
            return;
        }

        if (inPlaceMode)
        {

            clipGhostToMouse(ghostBuilding);
            isValidToPlace = isValidBuildArea(currentGridCell);
            setGridCells();
            highlightTile(isValidToPlace);

            if (Input.GetMouseButtonDown(0) && isValidToPlace)
            {
                togglePlaceMode();
                clearTileHilights();
                Destroy(ghostBuilding);
                placeBuilding(testPrefab);
                return;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                togglePlaceMode();
                clearTileHilights();
                Destroy(ghostBuilding);
                return;
            }
        }
    }

    private void toggleBuildGrid()
    {
        renderer.enabled = !renderer.enabled;
        inBuildMode = !inBuildMode;
    }

    private void togglePlaceMode()
    {
        inPlaceMode = !inPlaceMode;
    }


    private bool isValidBuildArea(Vector3Int cell)
    {
        return PlayerBuildZone.HasTile(cell) ? true : false;
    }

    private void highlightTile(bool isValidToPlace)
    {
        TileBase tile; 

        if (isValidToPlace)
        {
            tile = validTile;
        }
        else
        {
            tile = invalidTile;
        }

        ValidBuildGrid.SetTile(currentGridCell, tile);
        ValidBuildGrid.SetTile(lastGridCell, null);
    }

    private void clearTileHilights()
    {
        ValidBuildGrid.SetTile(currentGridCell, null);
        ValidBuildGrid.SetTile(lastGridCell, null);
    }

    private GameObject spawnGhostBuilding()
    {
        Vector3 cellCenter = mouseToGridCenter(currentGridCell);
        Quaternion rotation = grid.transform.rotation;
        GameObject newBuilding = Instantiate(testPrefab, cellCenter, rotation);
        adjustTransparency(newBuilding, true);
        return newBuilding;
    }

    private void clipGhostToMouse(GameObject ghost)
    {
        Vector3 cellCenter = mouseToGridCenter(currentGridCell);
        ghost.transform.position = cellCenter;
    }


    private Vector3Int getGridCell()
    {
        // mouse pos on screen 
        Vector3 mpos = Input.mousePosition;
        // mouse pos converted to world position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mpos);
        //mouse world pos converted to grid cell
        Vector3Int gridCell = grid.WorldToCell(mouseWorldPos);
        return gridCell;
    }

    private void setGridCells()
    {
        Vector3Int cellNow = getGridCell();
        
        if ( cellNow !=  currentGridCell)
        {
            lastGridCell = currentGridCell;
            currentGridCell = cellNow;
        }
    }
    private Vector3 mouseToGridCenter(Vector3Int currentGridCell)
    {
        Vector3 cellCenter = grid.GetCellCenterWorld(currentGridCell);
        return cellCenter;
    }

    private void placeBuilding(GameObject prefab)
    {
        Debug.Log("2 placing building");
        Vector3 cellCenter = mouseToGridCenter(currentGridCell);
        Quaternion rotation = grid.transform.rotation;
        GameObject newBuilding = Instantiate(prefab, cellCenter, rotation);
        adjustTransparency(newBuilding, false);

    }

    private void adjustTransparency(GameObject building, bool ghost)
    {
        SpriteRenderer renderer = building.GetComponent<SpriteRenderer>();
        Color transparency = renderer.color;
        if (!ghost)
        {
            transparency.a = 1f;
        }
        else
        {
            transparency.a = 0.5f;
        }
        renderer.color = transparency;
    }

}
