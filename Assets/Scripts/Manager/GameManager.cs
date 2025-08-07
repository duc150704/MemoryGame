using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameDifficulty
    {
        Easy,
        Medium,
        Hard
    }

    [SerializeField] GameObject _cardPref;
    [SerializeField] List<LightCardData> _listCardData = new List<LightCardData>();

    List<GameObject> _listCard = new List<GameObject>();
    List<GameObject> _selectedListCard = new List<GameObject>();

    bool _canClick = false;

    float freezeTime = 0.2f;
    float freezeTimeCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    // Update is called once per frame
    void Update()
    {
        freezeTimeCounter += Time.deltaTime;
        if (_canClick && freezeTimeCounter >= freezeTime)
        {
            Click();
        }
    }

    void Click()
    {
        if (InputManager.Instance.RightMouseClick())
        {
            freezeTimeCounter = 0;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            RaycastHit2D hitInfo = Physics2D.Raycast(mousePos, Vector3.zero, 2f);
            if (hitInfo.collider == null || _selectedListCard.Contains(hitInfo.collider.gameObject))
                return;
            if(hitInfo.collider.gameObject.tag == "Card")
            {
                if (hitInfo.collider.GetComponent<Card>().IsFacingUp == true)
                    return;
                hitInfo.transform.GetComponent<Card>().Flip();
                _selectedListCard.Add(hitInfo.collider.gameObject);
            }
            
            if(_selectedListCard.Count == 2)
            {
                Debug.Log($"So luong chon :{_selectedListCard.Count}");
                StartCoroutine(Check());
            }
        }
    }

    IEnumerator Init()
    {
        yield return StartCoroutine(SetUp());
        RandomizeList();
        MoveCard();

        yield return new WaitForSeconds(0.5f);
        _canClick = true;
    }

    IEnumerator SetUp()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        for(int i = 0;i < 15; i++)
        {
            int ran = Random.Range(0, _listCardData.Count);
            for (int j = 0; j < 2; j++)
            {
                GameObject go = Instantiate(_cardPref, Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
                go.GetComponent<Card>().SetupData(_listCardData[ran]._frontCard, _listCardData[ran]._backCard, _listCardData[ran]._value);
                _listCard.Add(go);
            }
            yield return wait;
        }
    }

    void RandomizeList()
    {
        for(int i = 0; i < _listCard.Count; i++)
        {
            int ranIndex = Random.Range(i, _listCard.Count);
            GameObject tmp = _listCard[i];
            _listCard[i] = _listCard[ranIndex];
            _listCard[ranIndex] = tmp; 
        }
    }

    public void MoveCard()
    {
        int cardIndex = 0;
        for(float i = -4; i <= 6; i+= 1.5f)
        {
            for (float j = -4; j <= 6; j += 1.5f)
            {
                if (cardIndex >= _listCard.Count)
                    return;
                _listCard[cardIndex].GetComponent<Card>().MoveTo(new Vector3(i, j, 0f), 0.2f);
                cardIndex++;
            }
        }
    }

    IEnumerator Check()
    {
        if (_selectedListCard[0] == null || _selectedListCard[1] == null)
            yield break;
        Card card1 = _selectedListCard[0].GetComponent<Card>();
        Card card2 = _selectedListCard[1].GetComponent<Card>();

        _selectedListCard.Remove(card1.gameObject);
        _selectedListCard.Remove(card2.gameObject);

        yield return new WaitForSeconds(0.5f);// neu qua nho thi card 2 dang flip => khong flip tiep duoc vi _isFlipping == true;
        if (card1.Value == card2.Value)
        {
            float time = 0.2f;
            card1.Disappear(time);
            card2.Disappear(time);
        }
        else
        {
            card1.Flip();
            card2.Flip();
        }
    }
}
