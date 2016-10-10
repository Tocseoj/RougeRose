using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// Initialize the grid
	int gridRows = 19;
	int gridCols = 27;
	Transform[,] grid;
	public Transform getLocation(int x, int y) { if((x >= 0 && x < gridCols) && (y >= 0 && y < gridRows)) return grid[x, y]; return null; }
	public void addLocation(int x, int y, Transform obj) { grid[x, y] = obj; }

	public bool updateLocation(ref int x, ref int y, int deltaX, int deltaY, Transform obj) {
		int tempX = Mathf.Clamp(x + deltaX, 0, gridCols-1);
		int tempY = Mathf.Clamp(y + deltaY, 0, gridRows-1);
		if (grid[tempX, tempY] == null) {
			grid[x, y] = null;
			x = tempX; y = tempY;
			grid[x, y] = obj;
			return true;
		}
		return false;
	}

	// Step method
	int turn = 0;
	public int getTurn() {return turn;}
	public void step() {turn++;}

	// Use this for initialization
	void Awake () {
		GameObject.DontDestroyOnLoad(gameObject);
		grid = new Transform[gridCols, gridRows];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
			Reset();
	}

	public void AttackPosition(ref int x, ref int y, float ang, int attackRange) {
		if (attackRange <= 8)
			attackRange %= 2;
		if (attackRange == 1) {
			if ((ang >= 0 && ang < 22.5f) || (ang >= 337.5f && ang < 360f)) {
				x = 1;
				y = 0;
			} else if (ang >= 22.5f && ang < 67.5f) {
				x = 1;
				y = 1;
			} else if (ang >= 67.5f && ang < 112.5f) {
				x = 0;
				y = 1;
			} else if (ang >= 112.5f && ang < 157.5f) {
				x = -1;
				y = 1;
			} else if (ang >= 157.5f && ang < 202.5f) {
				x = -1;
				y = 0;
			} else if (ang >= 202.5f && ang < 247.5f) {
				x = -1;
				y = -1;
			} else if (ang >= 247.5f && ang < 292.5f) {
				x = 0;
				y = -1;
			} else if (ang >= 292.5f && ang < 337.5f) {
				x = 1;
				y = -1;
			}
		} else if (attackRange == 0) {
			if (ang >= 0 && ang < 45) {
				x = 1;
				y = 0;
			} else if (ang >= 45 && ang < 90) {
				x = 1;
				y = 1;
			} else if (ang >= 90 && ang < 135) {
				x = 0;
				y = 1;
			} else if (ang >= 135 && ang < 180) {
				x = -1;
				y = 1;
			} else if (ang >= 180 && ang < 225) {
				x = -1;
				y = 0;
			} else if (ang >= 225 && ang < 270) {
				x = -1;
				y = -1;
			} else if (ang >= 270 && ang < 315) {
				x = 0;
				y = -1;
			} else if (ang >= 315 && ang < 360) {
				x = 1;
				y = -1;
			}
		}
	}

	void Reset() {
		SceneManager.LoadScene("Scene1");
	}
}
