using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneButtonController : MonoBehaviour {
    public static PlaySceneButtonController instance;
    [SerializeField] GameObject buyBoard, handObject;
    [SerializeField] Transform destinyPosOfHand;
    public GameObject hammerObject, hammerObjectIcon;
    public GameObject unscrewObjectIcon, undoObjectIcon;
    public GameObject itemNoticeBoard, screwNotificationText, woodNotificationText, unscrewLeadText, undoLeadText, hammerLeadText;
    public GameObject unscrewNumberTextIcon, undoNumberTextIcon, hammerNumberTextIcon;
    public TextMeshProUGUI unscrewNumberText, undoNumberText, hammerNumberText, goldenTicketNumberText,buyGoldenTicketNumberText;
    public int unscrewNumber, undoNumber, hammerNumber, goldenTicketNumber;
    public GameObject[] backgrounds;

    public int secondsElapsed = 0;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        unscrewNumber = PlayerPrefs.GetInt(StringsTextManager.UnscrewNumber, 3);
        undoNumber = PlayerPrefs.GetInt(StringsTextManager.UndoNumber, 3);
        hammerNumber = PlayerPrefs.GetInt(StringsTextManager.HammerNumber, 3);
        UpdateBackground();
        StartCoroutine(CountSeconds());
        Invoke(nameof(MoveHandToDestiny),1f);
    }

    private void Update() {
        if (unscrewNumberTextIcon.activeSelf) {
            if (unscrewNumber < 0) unscrewNumber = 0;
            unscrewNumber = PlayerPrefs.GetInt(StringsTextManager.UnscrewNumber);
            unscrewNumberText.text = unscrewNumber.ToString();
            hammerLeadText.SetActive(false);
            unscrewLeadText.SetActive(true);
            undoLeadText.SetActive(false);
        } else if (undoNumberTextIcon.activeSelf) {
            if (undoNumber < 0) undoNumber = 0;
            undoNumber = PlayerPrefs.GetInt(StringsTextManager.UndoNumber);
            undoNumberText.text = undoNumber.ToString();
            hammerLeadText.SetActive(false);
            unscrewLeadText.SetActive(false);
            undoLeadText.SetActive(true);
        } else if (hammerNumberTextIcon.activeSelf) {
            if (hammerNumber < 0) hammerNumber = 0;
            hammerNumber = PlayerPrefs.GetInt(StringsTextManager.HammerNumber);
            hammerNumberText.text = hammerNumber.ToString();
            hammerLeadText.SetActive(true);
            unscrewLeadText.SetActive(false);
            undoLeadText.SetActive(false);
        }
        goldenTicketNumber = PlayerPrefs.GetInt(StringsTextManager.GoldenTicketNumber);
        PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber);
        goldenTicketNumberText.text = goldenTicketNumber.ToString();
        if (goldenTicketNumber <= 0) goldenTicketNumber = 0;
        if(buyBoard.activeSelf)
        buyGoldenTicketNumberText.text = goldenTicketNumberText.text;
    }

    public void ShowHammer() {
        if (hammerObjectIcon.activeSelf && hammerNumber > 0) {
            PlayerPrefs.SetInt(StringsTextManager.HammerNumber, hammerNumber -= 1);
            ItemManager.instance.SetUpCanDestroyWood(true);
            hammerObject.SetActive(true);
            itemNoticeBoard.SetActive(true);
            //woodNotificationText.SetActive(true);
            ItemManager.instance.SetUpCanDestroyScrew(false);
        } else if (unscrewObjectIcon.activeSelf && unscrewNumber > 0) {
            PlayerPrefs.SetInt(StringsTextManager.UnscrewNumber, unscrewNumber -= 1);
            ItemManager.instance.SetUpCanDestroyWood(false);
            ItemManager.instance.SetUpCanDestroyScrew(true);
            itemNoticeBoard.SetActive(true);
            //screwNotificationText.SetActive(true);
        } else if (undoObjectIcon.activeSelf && undoNumber > 0 && BarrierManager.Instance.HasHiddenWood()) {
            BarrierManager.Instance.RestoreHiddenWood();
            PlayerPrefs.SetInt(StringsTextManager.UndoNumber, undoNumber -= 1);
            ItemManager.instance.SetUpCanDestroyWood(false);
            ItemManager.instance.SetUpCanDestroyScrew(false);
            ScrewControlManager.instance.MoveToPreviousHole();
            //BarrierManager.Instance.ResetLastMovedWoodPosition();
            //ScrewManager.instance.UndoScrewMove();
        }
    }

    public IEnumerator HideItemNoticePanel() {
        yield return new WaitForSeconds(0.5f);
        itemNoticeBoard.SetActive(false);
        //screwNotificationText.SetActive(false);
    }

    public IEnumerator HideWoodNoticePanel() {
        yield return new WaitForSeconds(0.5f);
        itemNoticeBoard.SetActive(false);
        //woodNotificationText.SetActive(false);
    }

    public void MoveToHomeScene() {
        //LevelDisplay.Instance.UpdateLevel();  
        SceneManager.LoadScene("HomeScene");
    }

    public void MoveToNextLevel() {
        //LevelDisplay.Instance.UpdateLevel();
        SceneManager.LoadScene("PlayScene");
    }

    public IEnumerator MoveToCurrentScene() {
        SceneManager.LoadScene("PlayScene");
        yield return new WaitForSeconds(.35f);
        SceneManager.LoadScene("PlayScene");
    }
    public void MoveToCurrentSceneAndMinusTicket() {
        PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber -= 1);
        StartCoroutine(MoveToCurrentScene());
    }

    public void MoveToHomeSceneAfterLosing() {
        SceneManager.LoadScene("HomeScene");
    }

    //public void PauseCountDown() {
    //    TimeManager.instance.PauseTimer();
    //}

    public void KeepOnCountDown() {
        TimeManager.instance.ContinueTimer();
    }

    public void PopSoundEffect() {
        PlaySoundEffectManager.instance.audioEffectSource.PlayOneShot(PlaySoundEffectManager.instance.popEffectSound);
    }

    public void BuyItemFunction() {
        if (unscrewObjectIcon.activeSelf && goldenTicketNumber > 0) {
            PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber -= 1);
            unscrewNumber++;
            PlayerPrefs.SetInt(StringsTextManager.UnscrewNumber, unscrewNumber);
        } else if (undoObjectIcon.activeSelf && goldenTicketNumber > 0 && BarrierManager.Instance.HasHiddenWood()) {
            PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber -= 1);
            undoNumber++;
            PlayerPrefs.SetInt(StringsTextManager.UndoNumber, undoNumber);
        } else if (hammerObjectIcon.activeSelf && goldenTicketNumber > 0) {
            PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber -= 1);
            hammerNumber++;
            PlayerPrefs.SetInt(StringsTextManager.HammerNumber, hammerNumber);
        }
    }

    private void UpdateBackground() {
        int backgroundIdx = PlayerPrefs.GetInt(StringsTextManager.BackgroundIdx);
        for (int i = 0; i < backgrounds.Length; i++) {
            backgrounds[i].SetActive(i == backgroundIdx);
        }
    }

    public void BuyGoldenTicket(int amount) {
        goldenTicketNumber += amount;
        PlayerPrefs.SetInt(StringsTextManager.GoldenTicketNumber, goldenTicketNumber);
        goldenTicketNumberText.text = goldenTicketNumber.ToString();
        //purchasePanel.SetActive(true);
        //StartCoroutine(HideObject(purchasePanel));
    }

    /// <summary>
    /// time to win
    /// </summary>
    /// <returns></returns>
    public IEnumerator CountSeconds() {
        while (true) {
            yield return new WaitForSeconds(1f);
            secondsElapsed++;
        }
    }
    /// <summary>
    /// move hand function
    /// </summary>
    public void MoveHandToDestiny() {
        StartCoroutine(MoveHandCoroutine());
    }

    private IEnumerator MoveHandCoroutine() {
        float duration = 1.0f; 
        Vector3 startPos = handObject.transform.position;
        Vector3 endPos = destinyPosOfHand.position;
        float elapsedTime = 0;

        while (elapsedTime < duration) {
            handObject.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        handObject.transform.position = endPos;
        yield return new WaitForSeconds(.75f);
        handObject.SetActive(false); 
    }
}
