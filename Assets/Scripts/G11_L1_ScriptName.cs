using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class G11_L1_ScriptName : MonoBehaviour
{    
    private Animator[] anim = new Animator[3];
    private int j = 0;
    private bool seal = true;
    private char[] stringToCheck = new char[100];
    public InputField ifield;
    public Button ibutton;
    private Transform spawnPos;
    public GameObject spawnee0;
    public GameObject spawnee1;
    public GameObject spawnee4;
    public Camera gameCamera;
    public AudioClip reject;
    public AudioClip audiospace;
    private AudioSource myAudioSource;
    private char[] tape;
    private int head, p_head;
    private int[] states= new int[11];
    private int[] alphabets= new int[6];
    private string[] statesName = new string[11];
    private char[] alphabetName = new char [6];
    private int[] finalStates = new int [11];
    private int initialState;
    private int currentStare;
    private int[,] transitions = new int[11,6];
    private int[,] movements = new int[11, 6];
    private char[,] writings = new char[11, 6];
    private float x, Cx;
    void Start()
    {
        x = 0;
        Cx = 0;
        //anim = GetComponentInChildren<Animator>();
        anim = GetComponentsInChildren<Animator>();
        myAudioSource = GetComponent<AudioSource>();
        spawnPos = this.transform;
        string[] istateName = { "q0", "q1", "q2", "q3", "q4", "q5", "q6","q7","q8","q9", "Reject" };
        char[] iactionName = { '$', 'a', 'b', 'c', 'd', 'X' };
        int[] ifinalStates = { 0, 0, 0, 0, 0, 0, 0, 0,0,0,0 };
        ifinalStates[1] = 1;
        ifinalStates[10] = 1;
        currentStare = 0;
        initialState = 0;
        int[,] iTransitions = new int[istateName.Length, iactionName.Length];
        for (int i = 0; i < istateName.Length; i++)
            for (int k = 0; k < iactionName.Length; k++)
                iTransitions[i, k] = 10;
        iTransitions[0, 0] = 1;
        iTransitions[0, 1] = 6;
        iTransitions[0, 2] = 7;
        iTransitions[0, 3] = 8;
        iTransitions[0, 4] = 9;
        iTransitions[1, 0] = 1;
        iTransitions[1, 1] = 1;
        iTransitions[1, 2] = 1;
        iTransitions[1, 3] = 1;
        iTransitions[1, 4] = 1;
        iTransitions[1, 5] = 1;
        iTransitions[2, 2] = 2;
        iTransitions[2, 3] = 3;
        iTransitions[2, 5] = 2;
        iTransitions[3, 3] = 3;
        iTransitions[3, 4] = 4;
        iTransitions[3, 5] = 3;
        iTransitions[4, 4] = 4;
        iTransitions[4, 5] = 4;
        iTransitions[4, 0] = 5;
        iTransitions[5, 0] = 6;
        iTransitions[5, 1] = 5;
        iTransitions[5, 2] = 5;
        iTransitions[5, 3] = 5;
        iTransitions[5, 4] = 5;
        iTransitions[5, 5] = 5;
        iTransitions[6, 0] = 1;
        iTransitions[6, 1] = 6;
        iTransitions[6, 2] = 2;
        iTransitions[6, 5] = 6;
        iTransitions[7, 0] = 1;
        iTransitions[7, 2] = 7;
        iTransitions[7, 3] = 8;
        iTransitions[7, 4] = 9;
        iTransitions[8, 0] = 1;
        iTransitions[8, 3] = 8;
        iTransitions[8, 4] = 9;
        iTransitions[9, 0] = 1;
        iTransitions[9, 4] = 9;
        int[,] iMovements = new int[istateName.Length, iactionName.Length];
        for (int i = 0; i < istateName.Length; i++)
            for (int k = 0; k < iactionName.Length; k++)
                iMovements[i, k] = 0;
        iMovements[0, 1] = 1;
        iMovements[0, 2] = 1;
        iMovements[0, 3] = 1;
        iMovements[0, 4] = 1;
        iMovements[2, 2] = 1;
        iMovements[2, 3] = 1;
        iMovements[2, 5] = 1;
        iMovements[3, 3] = 1;
        iMovements[3, 4] = 1;
        iMovements[3, 5] = 1;
        iMovements[4, 4] = 1;
        iMovements[4, 5] = 1;
        iMovements[4, 0] = -1;
        iMovements[5, 0] = 1;
        iMovements[5, 1] = -1;
        iMovements[5, 2] = -1;
        iMovements[5, 3] = -1;
        iMovements[5, 4] = -1;
        iMovements[5, 5] = -1;
        iMovements[6, 1] = 1;
        iMovements[6, 2] = 1;
        iMovements[6, 5] = 1;
        iMovements[7, 0] = 1;
        iMovements[7, 2] = 1;
        iMovements[7, 3] = 1;
        iMovements[7, 4] = 1;
        iMovements[8, 0] = 1;
        iMovements[8, 3] = 1;
        iMovements[8, 4] = 1;
        iMovements[9, 0] = 1;
        iMovements[9, 4] = 1;
        char[,] iWriteTable = new char[istateName.Length, iactionName.Length];
        for (int i = 0; i < istateName.Length; i++)
            for (int k = 0; k < iactionName.Length; k++)
                iWriteTable[i, k] = iactionName[k];
        iWriteTable[0, 1] = '$';
        iWriteTable[6, 1] = '$';
        iWriteTable[6, 2] = 'X';
        iWriteTable[2, 3] = 'X';
        iWriteTable[3, 4] = 'X';
        iWriteTable[5, 5] = 'X';
        statesName = istateName;
        alphabetName = iactionName;
        finalStates = ifinalStates;
        transitions = iTransitions;
        movements = iMovements;
        writings = iWriteTable;

    }

    void Update()
    {
        float Cz = gameCamera.transform.position.z;
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (seal)
            {
                ifield.text = ifield.text + "a";
                stringToCheck[j] = 'a';
                j++;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (seal)
            {
                ifield.text = ifield.text + "b";
                stringToCheck[j] = 'b';
                j++;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (seal)
            {
                ifield.text = ifield.text + "c";
                stringToCheck[j] = 'c';
                j++;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (seal)
            {
                ifield.text = ifield.text + "d";
                stringToCheck[j] = 'd';
                j++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (seal && ifield.text.Length != 0)
            {
                j--;
                ifield.text = ifield.text.Substring(0, ifield.text.Length - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            seal = false;
            char[] SC = new char[j];
            for (int k = 0; k < j; k++)
                SC[k] = stringToCheck[k];
            int a = SC.Length;
            ifield.transform.position = new Vector3(0, 0, -500);
            Destroy(ifield);
            tape = new char[a + 2];
            tape[0] = '$';
            for (int i = 1; i < a + 1; i++)
                tape[i] = SC[i - 1];
            tape[a + 1] = '$';
            for (int i = 0; i < tape.Length; i++)
            {
                Instantiate(spawnee0, spawnPos.position, spawnPos.rotation);
                Ray ray = new Ray(gameCamera.transform.position, gameCamera.transform.forward);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    TextMesh txt = hitInfo.collider.gameObject.GetComponentInChildren<TextMesh>();
                    txt.text = char.ToString(tape[i]);
                }
                x = x + 3;
                Cx = Cx + 3;
                this.transform.position = new Vector3(x, 0, 0);
                gameCamera.transform.position = new Vector3(Cx, 0, Cz);

            }
            head = 1;
            p_head = 1;
            Instantiate(spawnee0, spawnPos.position, spawnPos.rotation);
            spawnPos.position = new Vector3(x + 3, 0, 0);
            Instantiate(spawnee0, spawnPos.position, spawnPos.rotation);
            gameCamera.transform.position = new Vector3(3, 0, Cz);
            this.transform.position = new Vector3(6,0,0);
            x = 6;
            Cx = 3;
            ibutton.GetComponentInChildren<Text>().text = "Your Current State Is: " + statesName[currentStare];
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAudioSource.PlayOneShot(audiospace);
            char alphabet = tape[head];
            int alphabetNumber = 0;

            for (int i = 0; i < alphabets.Length; i++)
                if (alphabet == alphabetName[i])
                {
                    alphabetNumber = i;
                    break;
                }
            tape[head] = writings[currentStare, alphabetNumber];
            p_head = head;
            head = head + movements[currentStare, alphabetNumber];
            if(movements[currentStare, alphabetNumber] == 1)
            {
                spawnee4.GetComponent<Animator>().Play("G11_AhmedGroupToTheRight");
                this.transform.position = new Vector3(x+3, 0, 0);
                x = x + 3;
            }
            else if (movements[currentStare, alphabetNumber] == -1)
            {             
                spawnee4.GetComponent<Animator>().Play("G11_AhmedGroupToTheLeft");
                this.transform.position = new Vector3(x - 3, 0, 0);
                x = x - 3;
            }
            currentStare = transitions[currentStare, alphabetNumber];
            Ray ray = new Ray(gameCamera.transform.position, gameCamera.transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                TextMesh txt = hitInfo.collider.gameObject.GetComponentInChildren<TextMesh>();
                txt.text = char.ToString(tape[p_head]);
            } 
            ibutton.GetComponentInChildren<Text>().text = "Your Current State Is: " + statesName[currentStare];
            int positionOfCamera = head;
            Cx = positionOfCamera * 3;
            x = Cx + 3;
            gameCamera.transform.position = new Vector3(Cx, 0, Cz);
            this.transform.position = new Vector3(x, 0, 0);
            ray = new Ray(gameCamera.transform.position, gameCamera.transform.forward);
            if (Physics.Raycast(ray, out hitInfo))
            {
                hitInfo.collider.GetComponent<Animator>().Play("G11_AhmedCubeBig");
            }
            TextMesh txt1 = this.GetComponentInChildren<TextMesh>();
            if (decide() == "Rejected")
            {
                myAudioSource.PlayOneShot(reject);
                txt1.color = Color.red;
                Instantiate(spawnee1, new Vector3(Cx,0,1), spawnPos.rotation);
                spawnee1.GetComponent<Animator>().Play("G11_Deadly");
                ibutton.GetComponentInChildren<Text>().text = "Rejected";
                Destroy(spawnee4);
            }
            else if (decide() == "Accepted")
            {
                txt1.color = Color.green;
                ibutton.GetComponentInChildren<Text>().text = "Accepted";
                myAudioSource.Play();
                spawnee4.GetComponent<Animator>().Play("G11_AhmedDance");
                Destroy(spawnee1);
            }
            txt1.text = decide();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
    public string decide()
    {
        if (finalStates[currentStare] != 1)
            return " ";
        else if (statesName[currentStare] == "Reject")
            return "Rejected";
        else
            return "Accepted";
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene("G11_Main_Menu", LoadSceneMode.Single);
    }
    public void ResetGame()
     {
        SceneManager.LoadScene("G11_L1_SceneName", LoadSceneMode.Single);
     }
    
 }
