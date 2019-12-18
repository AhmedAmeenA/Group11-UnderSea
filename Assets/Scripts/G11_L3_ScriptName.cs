using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class G11_L3_ScriptName : MonoBehaviour
{
    private bool lock1 = false;
    public InputField ifield;
    public Button ibutton;
    public GameObject iStarFish;
    private char[] stringToCheck = new char[100];
    private int j = 0;
    public AudioClip audiospace;
    private AudioSource myAudioSource;
    private Transform spawnPos;
    public GameObject spawnee0;
    public GameObject spawnee1;
    public Camera gameCamera;
    private char[] tape;
    private int head, p_head;
    private string[] statesName = new string[11];
    private char[] alphabetName = new char[5];
    private int initialState;
    private int currentState;
    float x, Cx, y, Cy, z, Cz;
    void Start()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;
        z = this.transform.position.z;
        Cx = gameCamera.transform.position.x;
        Cy = gameCamera.transform.position.y;
        Cz = gameCamera.transform.position.z;
        spawnPos = this.transform;
        myAudioSource = GetComponent<AudioSource>();
        string[] istateName = { "q0", "q1", "q2", "q3", "q4", "q5", "q6", "q7", "q8", "q9", "Reject" };
        char[] iactionName = { '$', 'a', 'b', 'c', 'X' };
        currentState = 0;
        initialState = 0;
        statesName = istateName;
        alphabetName = iactionName;
    }

    void Update()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;
        z = this.transform.position.z;
        Cx = gameCamera.transform.position.x;
        Cy = gameCamera.transform.position.y;
        Cz = gameCamera.transform.position.z;
        if (Input.GetKeyDown(KeyCode.A))
        {
            ifield.text = ifield.text + "a";
            stringToCheck[j] = 'a';
            j++;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ifield.text = ifield.text + "b";
            stringToCheck[j] = 'b';
             j++;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
                ifield.text = ifield.text + "c";
                stringToCheck[j] = 'c';
                j++;
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnEnterPress();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (ifield.text.Length != 0)
            {
                j--;
                ifield.text = ifield.text.Substring(0, ifield.text.Length - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAudioSource.PlayOneShot(audiospace);
            if (!lock1)
            {
                readFromTape();
                Ray ray = new Ray(gameCamera.transform.position, gameCamera.transform.forward);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    TextMesh txt = hitInfo.collider.gameObject.GetComponentInChildren<TextMesh>();
                    txt.text = char.ToString(tape[p_head]);
                }
                ibutton.GetComponentInChildren<Text>().text = "Your Current State Is: " + statesName[currentState];
                int positionOfCamera = head;
                float Cx_post = positionOfCamera * 3;
                gameCamera.transform.position = new Vector3(Cx_post, Cy, Cz);
                TextMesh txt1 = this.GetComponentInChildren<TextMesh>();
                txt1.transform.position = new Vector3(Cx_post - 2, -3, 0);
                ray = new Ray(gameCamera.transform.position, gameCamera.transform.forward);             
                if (Physics.Raycast(ray, out hitInfo))
                {
                    hitInfo.collider.GetComponent<Animator>().Play("G11_ShahidCubeRotate");
                }
                iStarFish.GetComponent<Animator>().Play("G11_ShahidStarFishRotate");
                if (currentState == 10)
                {
                    txt1.color = Color.red;
                    iStarFish.GetComponentInChildren<Animator>().Play("G11_ShahidRejectStar");
                    txt1.text = "Rejected";
                    lock1 = true;
                }
                else if (currentState == 9)
                {
                    txt1.color = Color.green;
                    iStarFish.GetComponent<Animator>().Play("G11_ShahidBigFish");
                    txt1.text = "Accepted";
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }

    public string readFromTape()
    {
        p_head = head;
        if (currentState == 0 && tape[head] == 'a')
        {
            currentState = 1;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 0 && tape[head] == 'b')
        {
            currentState = 2;
            tape[head] = 'X';
            head = head + 1;

        }
        else if (currentState == 0 && tape[head] == 'c')
        {
            currentState = 3;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 0 && tape[head] == 'X')
        {
            currentState = 0;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 0 && tape[head] == '$')
        {
            currentState = 9;
            tape[head] = '$';
        }
        else if (currentState == 1 && tape[head] == 'a')
        {
            currentState = 1;
            tape[head] = 'a';
            head = head + 1;
        }
        else if (currentState == 1 && tape[head] == 'b')
        {
            currentState = 6;
            tape[head] = 'X';
            head = head + 1;

        }
        else if (currentState == 1 && tape[head] == 'c')
        {
            currentState = 4;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 2 && tape[head] == 'a')
        {
            currentState = 6;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 2 && tape[head] == 'b')
        {
            currentState = 2;
            tape[head] = 'b';
            head = head + 1;
        }
        else if (currentState == 2 && tape[head] == 'c')
        {
            currentState = 5;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 2 && tape[head] == 'X')
        {
            currentState = 2;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 3 && tape[head] == 'a')
        {
            currentState = 4;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 3 && tape[head] == 'b')
        {
            currentState = 5;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 3 && tape[head] == 'c')
        {
            currentState = 3;
            tape[head] = 'c';
            head = head + 1;
        }
        else if (currentState == 3 && tape[head] == 'X')
        {
            currentState = 3;
            tape[head] = 'X';
            head = head + 1;
        }

        else if (currentState == 4 && tape[head] == 'a')
        {
            currentState = 4;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 4 && tape[head] == 'b')
        {
            currentState = 7;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 4 && tape[head] == 'c')
        {
            currentState = 4;
            tape[head] = 'c';
            head = head + 1;
        }
        else if (currentState == 4 && tape[head] == 'X')
        {
            currentState = 4;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 5 && tape[head] == 'a')
        {
            currentState = 7;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 5 && tape[head] == 'b')
        {
            currentState = 5;
            tape[head] = 'b';
            head = head + 1;
        }
        else if (currentState == 5 && tape[head] == 'c')
        {
            currentState = 5;
            tape[head] = 'c';
            head = head + 1;
        }
        else if (currentState == 5 && tape[head] == 'X')
        {
            currentState = 5;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 6 && tape[head] == 'a')
        {
            currentState = 6;
            tape[head] = 'a';
            head = head + 1;
        }
        else if (currentState == 6 && tape[head] == 'b')
        {
            currentState = 6;
            tape[head] = 'b';
            head = head + 1;
        }
        else if (currentState == 6 && tape[head] == 'c')
        {
            currentState = 7;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 6 && tape[head] == 'X')
        {
            currentState = 6;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 7 && tape[head] == 'a')
        {
            currentState = 7;
            tape[head] = 'a';
            head = head + 1;
        }
        else if (currentState == 7 && tape[head] == 'b')
        {
            currentState = 7;
            tape[head] = 'b';
            head = head + 1;
        }
        else if (currentState == 7 && tape[head] == 'c')
        {
            currentState = 7;
            tape[head] = 'c';
            head = head + 1;
        }
        else if (currentState == 7 && tape[head] == 'X')
        {
            currentState = 7;
            tape[head] = 'X';
            head = head + 1;
        }
        else if (currentState == 7 && tape[head] == '$')
        {
            currentState = 8;
            tape[head] = '$';
            head = head - 1;
        }
        else if (currentState == 8 && tape[head] == 'a')
        {
            currentState = 8;
            tape[head] = 'a';
            head = head - 1;
        }
        else if (currentState == 8 && tape[head] == 'b')
        {
            currentState = 8;
            tape[head] = 'b';
            head = head - 1;
        }
        else if (currentState == 8 && tape[head] == 'c')
        {
            currentState = 8;
            tape[head] = 'c';
            head = head - 1;
        }
        else if (currentState == 8 && tape[head] == 'X')
        {
            currentState = 8;
            tape[head] = 'X';
            head = head - 1;
        }
        else if (currentState == 8 && tape[head] == '$')
        {
            currentState = 0;
            tape[head] = '$';
            head = head + 1;
        }
        else if (currentState == 9)
        {
            currentState = 9;
        }
        else
        {
            currentState = 10;           
        }


        if (statesName[currentState] == "Reject")
            return "Rejected";
        else if (currentState == 9)
            return "Accepted";
        else
            return new string(tape);
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene("G11_Main_Menu", LoadSceneMode.Single);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("G11_L3_SceneName", LoadSceneMode.Single);
    }
    public void OnEnterPress()
    {
        ifield.transform.position = new Vector3(0, 0, -500);
        Destroy(ifield);
        char[] SC = new char[j];
        for (int k = 0; k < j; k++)
            SC[k] = stringToCheck[k];
        int a = SC.Length;
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
            this.transform.position = new Vector3(x, y, z);
            gameCamera.transform.position = new Vector3(Cx, Cy, Cz);

        }
        head = 1;
        p_head = 1;
        Instantiate(spawnee0, spawnPos.position, spawnPos.rotation);
        gameCamera.transform.position = new Vector3(3, Cy, Cz);
        this.transform.position = new Vector3(3, y, z);
        ibutton.GetComponentInChildren<Text>().text = "Your Current State Is: " + statesName[currentState];
    }

}
