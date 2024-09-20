using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tile : MonoBehaviour
{
    private const float LOWER_BOUND = -6.5f;
    private const string CLICKED = "Clicked";

    [SerializeField] GameObject sparkPS;

    private bool isClicked = false;
    private Button[] buttons;
    private RectTransform tileTransform;
    private Animator tileAnimator;


    public void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        tileTransform = GetComponent<RectTransform>();
        tileAnimator = GetComponent<Animator>();
        GameManager.Instance.OnRestartGame += Instance_OnRestartGame;
    }

    private void Instance_OnRestartGame(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        // Move the tile downward
        transform.Translate(Vector3.down * GameManager.Instance.GetSpeed() * Time.deltaTime);
        // Destroy the tile when it goes off-screen
        if (tileTransform.position.y < LOWER_BOUND)
        {
            if (!isClicked)
            {
                GameManager.Instance.GameOver();
            }
            Destroy(gameObject);
        }
    }

    public void OnClickedTile(bool isPerfect)
    {
        Debug.Log("clicked???");
        if (!isClicked)
        {
            if (isPerfect) Debug.Log("Perfect");
            else Debug.Log("Great");
            isClicked = true;
            // Add point
            GameManager.Instance.IncreasePoint(isPerfect);
            // Turn on particle effect
            sparkPS.SetActive(true);
            // Turn on tile disappear animation
            tileAnimator.SetTrigger(CLICKED);
            foreach (Button button in buttons)
            {
                button.interactable = false;
            }
        }
    }

    // Unsubscribe from event when the object is destroyed
    public void OnDestroy()
    {
        GameManager.Instance.OnRestartGame -= Instance_OnRestartGame;
    }
}
