using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GreaterOrLessController : MonoBehaviour
{
    public enum RelationType
    {
        Less, Equal, Greater
    }
    
    [SerializeField] private TMP_Text leftLabel;
    [SerializeField] private TMP_Text rightLabel;
    [SerializeField] private TMP_Text resultLabel;
    [SerializeField] private Button lessButton;
    [SerializeField] private Button equalButton;
    [SerializeField] private Button greaterButton;

    private int leftNumber;
    private int rightNumber;
    private RelationType? result;

    private void Awake()
    {
        lessButton.onClick.AddListener(() => result = RelationType.Less);
        greaterButton.onClick.AddListener(() => result = RelationType.Greater);
        equalButton.onClick.AddListener(() => result = RelationType.Equal);
    }

    private IEnumerator Start()
    {
        resultLabel.gameObject.SetActive(false);

        while (true)
        {
            GenerateRandomNumbers();
            yield return WaitForUserInput();
            yield return ShowResult();
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void GenerateRandomNumbers()
    {
        leftNumber = Random.Range(1, 20);
        rightNumber = Random.Range(1, 20);

        leftLabel.text = leftNumber.ToString();
        rightLabel.text = rightNumber.ToString();
    }
    
    private IEnumerator WaitForUserInput()
    {
        result = null;
        yield return new WaitUntil(() => result != null);
    }
    
    private IEnumerator ShowResult()
    {
        resultLabel.gameObject.SetActive(true);
        resultLabel.text = IsSuccess ? "Success!" : "Fail!";
        yield return new WaitForSeconds(2);
        resultLabel.gameObject.SetActive(false);
    }

    public bool IsSuccess => 
        result switch
        {
            RelationType.Less => leftNumber < rightNumber,
            RelationType.Equal => leftNumber == rightNumber,
            RelationType.Greater => leftNumber > rightNumber,
            _ => throw new ArgumentOutOfRangeException()
        };
}