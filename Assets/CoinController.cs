using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float speed; // Base speed of rotation



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.up, speed * Time.deltaTime);
        //transform.Rotate(Vector3.right, 2 * speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider triggeredObject)
    {
        Debug.Log("Collision!!");
        if(triggeredObject.CompareTag("Player")) {
            GameManager.Instance.IncrementScore();
            Destroy(this.gameObject);
        }
    }
}
