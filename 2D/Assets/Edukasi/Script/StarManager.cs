using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;

    private int starCount = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddStar()
    {
        starCount++;
        Debug.Log("Bintang didapat: " + starCount);
    }

    public int GetStarCount()
    {
        return starCount;
    }

    public void ResetStars()
    {
        starCount = 0;
    }
}
