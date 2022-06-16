//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;
//using System.Threading;
//using System.Globalization;
//using System.Reflection;
//using System.Text;

//namespace GameCoreSystem
//{
//    [System.Serializable]
//    public class Result
//    {
//        public float totalScore = 0;

//        [Header("REF UI")]
//        public Text textTime;
//        public Text textTotalScore;

//        [Header("REF RESET SCREEN")]
//        public Text textResultScore;
//        public Text textInfo;
//        public GameObject resultCanvas;

//        public void ShowResult()
//        {
//            //show damage dealt
//            textResultScore.text = totalScore.ToString();
//            StringBuilder textInfoResult = new StringBuilder("Your damage is ");
//            textInfoResult.Append(WordScramble.main.words.Length);
//            textInfoResult.Append(" damages");
//            textInfo.text = textInfoResult.ToString();

//            int allTimeLimit = WordScramble.main.GetAllTimeLimit();

//            resultCanvas.SetActive(true);
//        }
//    }

//    [System.Serializable]
//    public class Word
//    {
//        public string word;
//        [Header("leave empty if you want to randomized")]
//        public string desiredRandom;

//        [Space(10)]
//        public int timeLimit;



//        public string GetString()
//        {
//            if (!string.IsNullOrEmpty(desiredRandom))
//            {
//                return desiredRandom;
//            }

//            // Why would do while check if here you already declaried word is result?
//            string result = word;

//            while (result == word)
//            {
//                result = "";
//                List<char> characters = new List<char>(word.ToCharArray());
//                while (characters.Count > 0)
//                {
//                    int indexChar = Random.Range(0, characters.Count - 1);
//                    result += characters[indexChar];

//                    characters.RemoveAt(indexChar);
//                }
//            }

//            return result;
//        }
//    }



//    public class WordScramble : MonoBehaviour
//    {
//        public Word[] words;

//        [Space(10)]
//        public Result result;

//        [Header("UI REFERENCE")]
//        public GameObject wordCanvas;
//        //public CharObject prefab;
//        public Transform container;
//        public float space;
//        public float lerpSpeed = 5;
//        public float resultChangeRate = 0.5f;   // The time in seconds.
//        private float timer = 0;                // A private timer variable.


//        List<CharObject> charObjects = new List<CharObject>();
//        CharObject firstSelected;

//        public int currentWord;

//        public static WordScramble main;

//        public float totalScore;

//        void Awake()
//        {
//            if (main == null)
//            {
//                main = this;
//            }
//            else
//            {
//                Destroy(this);
//            }
//        }

//        // Start is called before the first frame update
//        void Start()
//        {
//            ShowScramble(currentWord);
//            result.textTotalScore.text = result.totalScore.ToString();
//        }

//        void Update()
//        {
//            RepositionObject();

//            if (totalScore != result.totalScore)
//            {
//                timer += Time.deltaTime;
//                if (timer >= resultChangeRate)
//                {
//                    timer -= resultChangeRate;
//                    // First I calculated the difference between two scores
//                    var resultDifference = result.totalScore - totalScore;

//                    // If the new total score higher than the old one, which means the score should be going upward.
//                    totalScore += resultDifference > 0 ? 1 : -1;

//                    result.textTotalScore.text = Mathf.RoundToInt(totalScore).ToString();
//                }
//            }
//        }

//        public int GetAllTimeLimit()
//        {
//            float result = 0;
//            foreach (Word w in words)
//            {
//                result += w.timeLimit / 2;
//            }

//            return Mathf.RoundToInt(result);
//        }

//        void RepositionObject()
//        {
//            if (charObjects.Count == 0)
//            {
//                return;
//            }

//            float center = (charObjects.Count - 1) / 2;
//            for (int i = 0; i < charObjects.Count; i++)
//            {
//                charObjects[i].rectTransform.anchoredPosition
//                    = Vector2.Lerp(charObjects[i].rectTransform.anchoredPosition,
//                    new Vector2((i - center) * space, 0), lerpSpeed * Time.deltaTime);
//                charObjects[i].index = i;
//            }
//        }

//        public void ShowScramble()
//        {
//            ShowScramble(Random.Range(0, words.Length - 1));
//        }

//        public void ShowScramble(int index)
//        {
//            charObjects.Clear();
//            foreach (Transform child in container)
//            {
//                Destroy(child.gameObject);
//            }

//            //FINISHED DEBUG
//            if (index > words.Length - 1)
//            {
//                result.ShowResult();
//                wordCanvas.SetActive(false);
//                //Debug.LogError("index out of range, please enter number between 0-" + (words.Length - 1).ToString());
//                return;
//            }

//            char[] chars = words[index].GetString().ToCharArray();
//            foreach (char c in chars)
//            {
//                CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
//                clone.transform.SetParent(container);
//                clone.Init(c);

//                charObjects.Add(clone);

//            }

//            currentWord = index;
//            StartCoroutine(TimeLimit());
//        }

//        public void Swap(int indexA, int indexB)
//        {
//            CharObject tmpA = charObjects[indexA];

//            charObjects[indexA] = charObjects[indexB];
//            charObjects[indexB] = tmpA;

//            charObjects[indexA].transform.SetAsLastSibling();
//            charObjects[indexB].transform.SetAsLastSibling();

//            CheckWord();
//        }
//        /*
//        public void Select(CharObject charObject)
//        {
//            if (firstSelected)
//            {
//                Swap(firstSelected.index, charObject.index);

//                //Unselect
//                firstSelected.Select();
//                charObject.Select();

//            }
//            else
//            {
//                firstSelected = charObject;
//            }
//        }
//        */
//        public void UnSelect()
//        {
//            firstSelected = null;
//        }

//        public void CheckWord()
//        {
//            StartCoroutine(CoCheckWord());
//        }

//        //Check if right or wrong
//        IEnumerator CoCheckWord()
//        {
//            yield return new WaitForSeconds(0.5f);

//            string word = "";
//            foreach (CharObject charObject in charObjects)
//            {
//                word += charObject.character;
//            }

//            if (timeLimit <= 0)
//            {
//                currentWord++;
//                ShowScramble(currentWord);
//                yield break;
//            }

//            if (word == words[currentWord].word)
//            {
//                currentWord++;
//                result.totalScore += Mathf.RoundToInt(timeLimit);

//                //StopCorontine(TimeLimit());

//                ShowScramble(currentWord);
//            }
//        }

//        //Change time
//        public float timeLimit;
//        IEnumerator TimeLimit()
//        {
//            timeLimit = words[currentWord].timeLimit;
//            result.textTime.text = Mathf.RoundToInt(timeLimit).ToString();

//            int myWord = currentWord;

//            yield return new WaitForSeconds(1);

//            while (timeLimit > 0)
//            {
//                if (myWord != currentWord) { yield break; }

//                timeLimit -= Time.deltaTime;
//                result.textTime.text = Mathf.RoundToInt(timeLimit).ToString();
//                yield return null;
//            }
//            //score text
//            CheckWord();
//        }
//    }
//}
