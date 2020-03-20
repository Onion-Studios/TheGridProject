using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Dictionary<int, List<Image>> Dictionaryofsignsprite = new Dictionary<int, List<Image>>();
    public Image[] signs;

    // Start is called before the first frame update
    void Start()
    {
        CreatePoolofsign();
        foreach(KeyValuePair<int,List<Image>> elementofdictionary in Dictionaryofsignsprite)
        {
            Debug.Log("key:" + elementofdictionary.Key);
            foreach(var segno in elementofdictionary.Value)
            {
                Debug.Log(segno.name);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region CREATE POOL OF SIGN
    void CreatePoolofsign()
    {
        foreach(Image images in signs)
        {
            int _signID = images.GetComponent<signbehaviours>().signID;
            switch (_signID)
            {
                case 0:

                    List<Image> listasegno1 = new List<Image>();
                    Dictionaryofsignsprite.Add(_signID, listasegno1);
                    for(int i=0; i<5; i++)
                    {
                        Image imageinscene = Instantiate(images,Vector3.zero, Quaternion.identity, this.transform);
                        Dictionaryofsignsprite[_signID].Add(imageinscene);
                    }

                    break;

                case 1:

                    List<Image> listasegno2 = new List<Image>();
                    Dictionaryofsignsprite.Add(_signID, listasegno2);
                    for (int i = 0; i < 5; i++)
                    {
                        Image imageinscene = Instantiate(images, Vector3.zero, Quaternion.identity, this.transform);
                        Dictionaryofsignsprite[_signID].Add(imageinscene);
                    }

                    break;

                default:
                    break;
            }
                
        }
    }
    #endregion
}
