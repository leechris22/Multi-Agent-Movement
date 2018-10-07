using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager2 : MonoBehaviour {
    // Set prefabs
    public GameObject GreenBoidPrefab;
    public GameObject RedBoidPrefab;

    // Set necessary game objects
    public List<GameObject> Boids;
    public NPCController Player;

    // Set level loading material
    public GameObject GreenBoidSpawner;
    public GameObject RedBoidSpawner;
    public GameObject GreenBoidPath;
    public GameObject RedBoidPath;

    // On initialization
    void Start() {
        Boids = new List<GameObject>();

        // Spawn 2 groups of Boids
        for (int i = 0; i < 6; i++) {
            SpawnBoids(GreenBoidPrefab, GreenBoidSpawner);
        }
        for (int i = 0; i < 6; i++) {
            SpawnBoids(RedBoidPrefab, RedBoidSpawner);
        }
    }

    // Update is called once per frame
    void Update() {
        MovetoScene();
    }

    // Spawn a boid and set necessary targets
    private void SpawnBoids(GameObject boid, GameObject spawner) {
        Vector3 size = spawner.transform.localScale;
        Vector3 position = spawner.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), 0);
        GameObject temp = Instantiate(boid, position, Quaternion.identity);
        temp.GetComponent<NPCController>().target = Player;
        foreach (GameObject Boid in Boids) {
            Boid.GetComponent<Flock>().targets.Add(temp.GetComponent<NPCController>());
            temp.GetComponent<Flock>().targets.Add(Boid.GetComponent<NPCController>());
        }
        Boids.Add(temp);
    }

    // Move to the next scene
    private void MovetoScene() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SceneManager.LoadScene(2);
        }
    }

    // Show the spawn rectangle
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(GreenBoidSpawner.transform.position, GreenBoidSpawner.transform.localScale);
        Gizmos.DrawCube(RedBoidSpawner.transform.position, RedBoidSpawner.transform.localScale);
    }
}
