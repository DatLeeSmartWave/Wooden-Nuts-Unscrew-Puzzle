using System.Collections;
using TMPro;
using UnityEngine;

public class DailyReward : MonoBehaviour {
    public int LastDate;
    [SerializeField] GameObject ticketPrefab;
    [SerializeField] Transform destinyPos;

    public int Day_1;
    public int Day_2;
    public int Day_3;
    public int Day_4;
    public int Day_5;
    public int Day_6;
    public int Day_7;

    public GameObject OFF_1;
    public GameObject ACTIVE_1;
    public GameObject CHECK_1;

    public GameObject OFF_2;
    public GameObject ACTIVE_2;
    public GameObject CHECK_2;

    public GameObject OFF_3;
    public GameObject ACTIVE_3;
    public GameObject CHECK_3;

    public GameObject OFF_4;
    public GameObject ACTIVE_4;
    public GameObject CHECK_4;

    public GameObject OFF_5;
    public GameObject ACTIVE_5;
    public GameObject CHECK_5;

    public GameObject OFF_6;
    public GameObject ACTIVE_6;
    public GameObject CHECK_6;

    public GameObject OFF_7;
    public GameObject ACTIVE_7;
    public GameObject CHECK_7;

    void Start() {
        Day_1 = PlayerPrefs.GetInt("Day_1", 0);
        Day_2 = PlayerPrefs.GetInt("Day_2", 0);
        Day_3 = PlayerPrefs.GetInt("Day_3", 0);
        Day_4 = PlayerPrefs.GetInt("Day_4", 0);
        Day_5 = PlayerPrefs.GetInt("Day_5", 0);
        Day_6 = PlayerPrefs.GetInt("Day_6", 0);
        Day_7 = PlayerPrefs.GetInt("Day_7", 0);
        LastDate = PlayerPrefs.GetInt("LastDate", -1);

        Reward();

        if (LastDate != System.DateTime.Now.Day) {
            if (Day_1 == 0) {
                Day_1 = 1;
            } else if (Day_2 == 0) {
                Day_2 = 1;
            } else if (Day_3 == 0) {
                Day_3 = 1;
            } else if (Day_4 == 0) {
                Day_4 = 1;
            } else if (Day_5 == 0) {
                Day_5 = 1;
            } else if (Day_6 == 0) {
                Day_6 = 1;
            } else if (Day_7 == 0) {
                Day_7 = 1;
            } else {
                // Reset lại sau 7 ngày
                ResetRewards();
                Day_1 = 1;
            }

            Reward();
        }
    }

    public void Reward() {
        SetDayReward(Day_1, OFF_1, ACTIVE_1, CHECK_1);
        SetDayReward(Day_2, OFF_2, ACTIVE_2, CHECK_2);
        SetDayReward(Day_3, OFF_3, ACTIVE_3, CHECK_3);
        SetDayReward(Day_4, OFF_4, ACTIVE_4, CHECK_4);
        SetDayReward(Day_5, OFF_5, ACTIVE_5, CHECK_5);
        SetDayReward(Day_6, OFF_6, ACTIVE_6, CHECK_6);
        SetDayReward(Day_7, OFF_7, ACTIVE_7, CHECK_7);
    }

    private void SetDayReward(int dayStatus, GameObject offObj, GameObject activeObj, GameObject checkObj) {
        if (dayStatus == 0) {
            offObj.SetActive(true);
            activeObj.SetActive(false);
            checkObj.SetActive(false);
        } else if (dayStatus == 1) {
            offObj.SetActive(false);
            activeObj.SetActive(true);
            checkObj.SetActive(false);
        } else if (dayStatus == 2) {
            offObj.SetActive(false);
            activeObj.SetActive(false);
            checkObj.SetActive(true);
        }
    }

    public void GetReward_1() {
        GetReward(ref Day_1, "Day_1", OFF_1.transform);
    }

    public void GetReward_2() {
        GetReward(ref Day_2, "Day_2", OFF_2.transform);
    }

    public void GetReward_3() {
        GetReward(ref Day_3, "Day_3", OFF_3.transform);
    }

    public void GetReward_4() {
        GetReward(ref Day_4, "Day_4", OFF_4.transform);
    }

    public void GetReward_5() {
        GetReward(ref Day_5, "Day_5", OFF_5.transform);
    }

    public void GetReward_6() {
        GetReward(ref Day_6, "Day_6", OFF_6.transform);
    }

    public void GetReward_7() {
        GetReward(ref Day_7, "Day_7", OFF_7.transform);
    }

    private void GetReward(ref int day, string dayKey, Transform spawnPos) {
        LastDate = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", LastDate);
        day = 2;
        PlayerPrefs.SetInt(dayKey, 2);
        Reward();
        SpawnTicket(spawnPos);
    }

    private void ResetRewards() {
        Day_1 = 0;
        Day_2 = 0;
        Day_3 = 0;
        Day_4 = 0;
        Day_5 = 0;
        Day_6 = 0;
        Day_7 = 0;

        PlayerPrefs.SetInt("Day_1", Day_1);
        PlayerPrefs.SetInt("Day_2", Day_2);
        PlayerPrefs.SetInt("Day_3", Day_3);
        PlayerPrefs.SetInt("Day_4", Day_4);
        PlayerPrefs.SetInt("Day_5", Day_5);
        PlayerPrefs.SetInt("Day_6", Day_6);
        PlayerPrefs.SetInt("Day_7", Day_7);
    }

    public void SpawnTicket(Transform spawnPos) {
        GameObject ticket = Instantiate(ticketPrefab, spawnPos.position, spawnPos.rotation);
        StartCoroutine(MoveTicketToDestiny(ticket));
    }

    private IEnumerator MoveTicketToDestiny(GameObject ticket) {
        float duration = 1.0f; // Duration of the move
        float elapsedTime = 0.0f;
        Vector3 startPos = ticket.transform.position;
        Vector3 endPos = destinyPos.position;

        while (elapsedTime < duration) {
            ticket.transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ticket.transform.position = endPos;
        Destroy(ticket);
    }
}
