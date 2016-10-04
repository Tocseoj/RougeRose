using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Initialize the grid
	int gridRows = 19;
	int gridCols = 27;
	Transform[,] grid;
	public Transform getLocation(int x, int y) { return grid[x, y]; }
	public void addLocation(int x, int y, Transform obj) { grid[x, y] = obj; }

	public void updateLocation(ref int x, ref int y, int deltaX, int deltaY, Transform obj) {
		grid[x, y] = null;
		x = Mathf.Clamp(x + deltaX, 0, gridCols-1);
		y = Mathf.Clamp(y + deltaY, 0, gridRows-1);
		//Debug.Log("x: " + x + " y:" + y);
		grid[x, y] = obj;
	}

	// Use this for initialization
	void Awake () {
		grid = new Transform[gridCols, gridRows];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
