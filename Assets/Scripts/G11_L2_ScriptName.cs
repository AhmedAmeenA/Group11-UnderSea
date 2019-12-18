using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class G11_L2_ScriptName : MonoBehaviour
{
    private char[] stringToCheck = new char[100];
    private int j = 0;
    public InputField ifield;
    public Button ibutton;
    private string text = "Input so far: "; 
    private Transform spawnPos;
    public GameObject spawnee0;
    public GameObject spawnee1;
    public AudioClip audiospace;
    private AudioSource myAudioSource;
    public Camera gameCamera;
    private char[] tape;
    private int head, p_head;
    private int[] alphabets = new int[6];
    private string[] statesName = new string[12];
    private char[] alphabetName = new char[6];
    private int[] finalStates = new int[12];
    private int initialState;
    private int currentstate;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        spawnPos = this.transform;
        string[] istateName = { "q0", "q1", "q2", "q3", "q4", "q5", "q6", "q7", "q8","q9","q10", "Reject" };
        char[] iactionName = { '$', 'a', 'b', '0', '1', 'X' };
        int[] ifinalStates = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        ifinalStates[7] = 1;
        ifinalStates[11] = 1;
        currentstate = 0;
        initialState = 0;
        statesName = istateName;
        alphabetName = iactionName;
        finalStates = ifinalStates;
    }

    // Update is called once per frame
    void Update()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;
        float Cx = gameCamera.transform.position.x;
        float Cy = gameCamera.transform.position.y;
        float Cz = gameCamera.transform.position.z;
        if (Input.GetKeyDown(KeyCode.A))
        {
                text = text + "a";
                ifield.text = ifield.text + "a";
                stringToCheck[j] = 'a';
                j++;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
                text = text + "b";
                ifield.text = ifield.text + "b";
                stringToCheck[j] = 'b';
                j++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
                text = text + "0";
                ifield.text = ifield.text + "0";
                stringToCheck[j] = '0';
                j++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                text = text + "1";
                ifield.text = ifield.text + "1";
                stringToCheck[j] = '1';
                j++;
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (ifield.text.Length != 0)
            {
                j--;
                text = text.Substring(0, text.Length - 1);
                ifield.text = ifield.text.Substring(0, ifield.text.Length - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
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
                this.transform.position = new Vector3(x, 0, z);
                gameCamera.transform.position = new Vector3(Cx, Cy, Cz);
                
            }
            head = 1;
            p_head = 1;
            Instantiate(spawnee0, spawnPos.position, spawnPos.rotation);
            gameCamera.transform.position = new Vector3(3, Cy, Cz);
            this.transform.position = new Vector3(3, 0, z);
            ibutton.GetComponentInChildren<Text>().text = "Your Current State Is: " + statesName[currentstate];
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            p_head = head;
            myAudioSource.PlayOneShot(audiospace);
            if (currentstate == 0 && tape[head] == '0')
                {
	                currentstate = 1;
	                tape[head] = 'X';
	                head= head+1;
                }
            else if (currentstate == 0 && tape[head] == 'X')
            {
    	        currentstate = 0;
	            tape[head] = 'X';
	            head= head+1;
            }
            else if (currentstate == 0 && tape[head] == '$')
            {
	            currentstate = 7;
	            tape[head] = '$';
            }
            else if (currentstate == 1 && tape[head] == 'a')
            {
	            currentstate = 2;
	            tape[head] = 'X';
	            head= head+1;
            }
            else if (currentstate == 2 && tape[head] == '0')
            {
	            currentstate = 8;
	            tape[head] = '0';
	            head= head+1;
            }
            else if (currentstate == 2 && tape[head] == 'X')
            {
	            currentstate = 2;
	            tape[head] = 'X';
	            head= head+1;
            }
            else if (currentstate == 3 && tape[head] == '0')
            {
	            currentstate = 3;
	            tape[head] = 'X';
	            head= head+1;
            }
            else if (currentstate == 3 && tape[head] == 'b')
            {
	            currentstate = 4;
	            tape[head] = 'X';
	            head= head+1;
            }

            else if (currentstate == 4 && tape[head] == 'X')
            {
	            currentstate = 4;
	            tape[head] = 'X';
	            head= head+1;
            }
            else if (currentstate == 4 && tape[head] == 'b')
            {
	            currentstate = 4;
	            tape[head] = 'b';
	            head= head+1;
            }
            else if (currentstate == 4 && tape[head] == '0')
            {
	            currentstate = 4;
	            tape[head] = '0';
	            head= head+1;
            }
            else if (currentstate == 4 && tape[head] == '1')
            {
	            currentstate = 5;
	            tape[head] = 'X';
	            head= head+1;
            }

            else if (currentstate == 5 && tape[head] == 'b')
            {
	            currentstate = 9;
	            tape[head] = 'X';
	            head= head + 1;
            }

            else if (currentstate == 6 && tape[head] == 'a')
            {
	            currentstate = 6;
	            tape[head] = 'a';
	            head= head - 1;
            }
            else if (currentstate == 6 && tape[head] == 'b')
            {
	            currentstate = 6;
	            tape[head] = 'b';
	            head= head - 1;
            }
            else if (currentstate == 6 && tape[head] == '0')
            {
	            currentstate = 6;
	            tape[head] = '0';
	            head= head - 1;
            }
            else if (currentstate == 6 && tape[head] == '1')
            {
	            currentstate = 6;
	            tape[head] = '1';
	            head= head - 1;
            }
                        else if (currentstate == 6 && tape[head] == 'X')
                        {
                            currentstate = 6;
                            tape[head] = 'X';
                            head = head - 1;
                        }
             else if (currentstate == 6 && tape[head] == '$')
            {
	            currentstate = 0;
	            tape[head] = '$';
	            head= head+1;
            }
            else if (currentstate == 7 && tape[head] == 'a')
            {
	            currentstate = 7;
	            tape[head] = 'a';
	            head= head+1;
            }
            else if (currentstate == 8 && tape[head] == 'b')
            {
	            currentstate = 3;
	            tape[head] = 'b';
	            head= head - 1;
            }
            else if (currentstate == 8 && tape[head] == 'a')
            {
	            currentstate = 2;
	            tape[head] = 'a';
	            head= head+1;
            }
            else if (currentstate == 9 && tape[head] == '1')
            {
	            currentstate = 10;
	            tape[head] = 'X';
	            head= head+1;
            }
            else if (currentstate == 10 && tape[head] == 'b')
            {
	            currentstate = 6;
	            tape[head] = 'X';
	            head= head - 1;
            }
            else if (currentstate == 7)
            {
                currentstate = 7;
            }
            else
            {
                currentstate = 11;
            }
            Ray ray = new Ray(gameCamera.transform.position, gameCamera.transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                TextMesh txt = hitInfo.collider.gameObject.GetComponentInChildren<TextMesh>();
                txt.text = char.ToString(tape[p_head]);
            }
            ibutton.GetComponentInChildren<Text>().text = "Your Current State Is: " + statesName[currentstate];
            int positionOfCamera = head;
            float Cx_post = positionOfCamera * 3;
            gameCamera.transform.position = new Vector3(Cx_post, Cy, Cz);
            this.transform.position = new Vector3(Cx_post, y, z);
            TextMesh txt1 = this.GetComponentInChildren<TextMesh>();
            if (finalStates[currentstate] != 1)
                txt1.text = " ";
            else if (statesName[currentstate] == "Reject")
            {
                txt1.text = "Rejected";
                txt1.color = Color.red;
                Instantiate(spawnee1, new Vector3(Cx-5,y,z), spawnPos.rotation);
                GetComponentInChildren<Animator>().Play("G11_InaamFishCaught1");
            }
            else
            {
                GetComponentInChildren<Animator>().Play("G11_InaamFishFinalAccepted");
                txt1.text = "Accepted";
                txt1.color = Color.green;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene("G11_Main_Menu", LoadSceneMode.Single);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene("G11_L2_SceneName", LoadSceneMode.Single);
    }
}
