using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class ScrambleMiniGame : MiniGame
    {
        public List<PortableWordList> allWordList;

        // Two instances
        [ReadOnlyInspector] public Word[] player1Words;
        [ReadOnlyInspector] public Word[] player2Words;

        List<CharObject> charObjects = new List<CharObject>();
        [SerializeField] List<CharObject> player1CharObjects = new List<CharObject>();
        [SerializeField] List<CharObject> player2CharObjects = new List<CharObject>();

        CharObject player1FirstSelectedChar;
        CharObject player2FirstSelectedChar;

        [ReadOnlyInspector] public int player1CurrentWordPos;
        [ReadOnlyInspector] public int player2CurrentWordPos;

        public float player1TimeLimit;
        public float player2TimeLimit;

        float player1CorrectWords;
        float player2CorrectWords;

        public float player1CorrectPercent { get; set; }
        public float player2CorrectPercent { get; set; }

        public Transform player1Container;
        public Transform player2Container;

        // Single instances
        public CharObject prefab;
        public float space;
        public float lerpSpeed = 5;
        public float resultChangeRate = 30;
        public float maxDamage;
        public bool finishedGame;
        public bool isPlayersBattle;

        UIManager ui;

        #region ScrambleWord maneuver system
        [Header("Options Maneuver")]
        CharObject player1CurrentCharObj;
        CharObject player2CurrentCharObj;

        [Header("Current Pos")]
        [ReadOnlyInspector]
        public int p1_CharObjPos = 0;
        [ReadOnlyInspector]
        public int p2_CharObjPos = 0;

        [Header("Input Wait")]
        public float player1InputWaitRate;
        public float player2InputWaitRate;

        [ReadOnlyInspector]
        public float player1InputWaitTimer;
        [ReadOnlyInspector]
        public float player2InputWaitTimer;

        [Header("Color")]
        public Color initalColor;
        public Color pressedColor;

        #endregion

        public static ScrambleMiniGame singleton;

        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            ui = UIManager.singleton;
        }

        public IEnumerator Init(bool isPlayer_1)
        {
            finishedGame = false;
            if (isPlayer_1)
            {
                player1CurrentWordPos = 0;
                ShowScramble(player1CharObjects, player1Words, player1Container, player1CurrentWordPos, true);
                
            }
            else
            {
                player2CurrentWordPos = 0;
                ShowScramble(player2CharObjects, player2Words, player2Container, player2CurrentWordPos, false);
            }

            yield return new WaitForSeconds(.75f);
        }

        IEnumerator ReverseBackToCommand(StateManager states)
        {
            Debug.Log("Tick4");
            yield return new WaitForSeconds(.75f);
            ui.FadeInCombatCommandsUI(states.player_1);
        }

        public override void Tick(StateManager states)
        {
            if (finishedGame)
            {
                if (!states.isBattleFinished && !states.isDead)
                {
                    StartCoroutine(ReverseBackToCommand(states));
                }
            }

            RepositionObject(states.player_1);

            UpdateTotalScoreText(states);

            if (states.player_1)
            {
                CharObjManeuverPlayer1(states);
                states.horizontal = 0;
            }
            else
            {
                CharObjManeuverPlayer2(states);
                states.horizontal = 0;
            }
        }

        void RepositionObject(bool isPlayer_1)
        {
            if (isPlayer_1)
            {
                if (player1CharObjects.Count == 0)
                {
                    return;
                }

                float center = (player1CharObjects.Count - 1) / 2;
                for (int i = 0; i < player1CharObjects.Count; i++)
                {
                    player1CharObjects[i].rectTransform.anchoredPosition
                        = Vector2.Lerp(player1CharObjects[i].rectTransform.anchoredPosition,
                        new Vector2((i - center) * space, 0), lerpSpeed * Time.deltaTime);
                    player1CharObjects[i].index = i;
                }
            }
            else
            {
                if (player2CharObjects.Count == 0)
                {
                    return;
                }

                float center = (player2CharObjects.Count - 1) / 2;
                for (int i = 0; i < player2CharObjects.Count; i++)
                {
                    player2CharObjects[i].rectTransform.anchoredPosition
                        = Vector2.Lerp(player2CharObjects[i].rectTransform.anchoredPosition,
                        new Vector2((i - center) * space, 0), lerpSpeed * Time.deltaTime);
                    player2CharObjects[i].index = i;
                }
            }
        }

        void UpdateTotalScoreText(StateManager states)
        {
            if (states.player_1)
            {
                //Debug.Log("UpdateTotalScoreText1");
                if (states.currentScore != result.player1TotalScore)
                {
                    states.currentScore = result.player1TotalScore;
                    ui.UpdateTotalScoreText(true, result.player1TotalScore);
                }
            }
            else
            {
                if (states.currentScore != result.player2TotalScore)
                {
                    states.currentScore = result.player2TotalScore;
                    LeanTween.value(states.currentScore, result.player2TotalScore, 2f);
                    ui.UpdateTotalScoreText(false, result.player2TotalScore);
                }
            }


            //Update Score and calculate NEED TO FIX
            
            //while (totalScore != result.totalScore)
            //{
                //bool isPlus = result.totalScore > totalScore ? true : false;

                // First I calculated the difference between two scores
                //float resultDifference = result.totalScore - totalScore;

               // totalScoreUpdateTimer += Time.deltaTime;
                //if (totalScoreUpdateTimer >= totalScoreUpdatRate)
                //{
                    //if (isPlus)
                    //{
                    //    totalScore += 0.25f;
                    //}
                    //else
                    //{
                    //    totalScore -= 0.25f;
                    //}

                    //float newDifference = result.totalScore - totalScore;
                    //if (newDifference <= 0.5f)
                    //{
                    //    totalScore = result.totalScore;
                    //}

                    //UIManager.singleton.totalScoreText.text = totalScore.ToString();
                //}
            //}
            
        //} 

        void Swap(int indexA, int indexB, bool isPlayer_1)
        {
            if (isPlayer_1)
            {
                CharObject tmpA = player1CharObjects[indexA];

                player1CharObjects[indexA] = player1CharObjects[indexB];
                player1CharObjects[indexB] = tmpA;

                player1CharObjects[indexA].transform.SetAsLastSibling();
                player1CharObjects[indexB].transform.SetAsLastSibling();

                Word currentWord = player1Words[player1CurrentWordPos];
                currentWord.tryTimes--;
                ui.UpdateTriesText(true, currentWord.tryTimes);
                if (currentWord.tryTimes == 0)
                    StartCoroutine(CheckWord(player1CharObjects, player1Words, true));
            }
            else
            {
                CharObject tmpA = player2CharObjects[indexA];

                player2CharObjects[indexA] = player2CharObjects[indexB];
                player2CharObjects[indexB] = tmpA;

                player2CharObjects[indexA].transform.SetAsLastSibling();
                player2CharObjects[indexB].transform.SetAsLastSibling();

                Word currentWord = player2Words[player2CurrentWordPos];
                currentWord.tryTimes--;
                ui.UpdateTriesText(false, currentWord.tryTimes);
                if (currentWord.tryTimes == 0)
                    StartCoroutine(CheckWord(player2CharObjects, player2Words, false));
            }
        }

        void CharObjManeuverPlayer1(StateManager states)
        {
            GetInputPlayer1(states);

            if (player1CharObjects.Count != 0)
            {
                CharObject newCharObj1 = player1CharObjects[p1_CharObjPos];

                if (player1CurrentCharObj == null)
                {
                    player1CurrentCharObj = newCharObj1;
                    player1CurrentCharObj.text.color = pressedColor;
                }
                else if (newCharObj1 != player1CurrentCharObj)
                {
                    player1CurrentCharObj.text.color = initalColor;
                    player1CurrentCharObj = newCharObj1;
                    player1CurrentCharObj.text.color = pressedColor;
                }

                if (states.enter)
                {
                    newCharObj1.Select(this, true);
                }
            }
        }

        void CharObjManeuverPlayer2(StateManager states)
        {
            GetInputPlayer2(states);

            if (player2CharObjects.Count != 0)
            {
                CharObject newCharObj2 = player2CharObjects[p2_CharObjPos];

                if (player2CurrentCharObj == null)
                {
                    player2CurrentCharObj = newCharObj2;
                    player2CurrentCharObj.text.color = pressedColor;
                }
                else if (newCharObj2 != player2CurrentCharObj)
                {
                    player2CurrentCharObj.text.color = initalColor;
                    player2CurrentCharObj = newCharObj2;
                    player2CurrentCharObj.text.color = pressedColor;
                }

                if (states.enter)
                {
                    newCharObj2.Select(this, false);
                }
            }
        }

        void GetInputPlayer1(StateManager states)
        {
            player1InputWaitTimer += states.delta;
            if (player1InputWaitTimer >= player1InputWaitRate && states.horizontal != 0)
            {
                player1InputWaitTimer = 0;
                if (states.horizontal > 0)
                {
                    p1_CharObjPos++;
                    if (p1_CharObjPos > player1CharObjects.Count - 1)
                    {
                        p1_CharObjPos = 0;
                    }
                }
                else
                {
                    p1_CharObjPos--;
                    if (p1_CharObjPos < 0)
                    {
                        p1_CharObjPos = player1CharObjects.Count - 1;
                    }
                }
            }
        }

        void GetInputPlayer2(StateManager states)
        {
            player2InputWaitTimer += states.delta;
            if (player2InputWaitTimer >= player2InputWaitRate && states.horizontal != 0)
            {
                player2InputWaitTimer = 0;
                if (states.horizontal > 0)
                {
                    p2_CharObjPos++;
                    if (p2_CharObjPos > player2CharObjects.Count - 1)
                    {
                        p2_CharObjPos = 0;
                    }
                }
                else
                {
                    p2_CharObjPos--;
                    if (p2_CharObjPos < 0)
                    {
                        p2_CharObjPos = player2CharObjects.Count - 1;
                    }
                }
            }
        }

        //Check if right or wrong
        IEnumerator CheckWord(List<CharObject> charObjects, Word[] words, bool isPlayer_1)
        {
            yield return new WaitForSeconds(0.5f);

            string word = "";
            foreach (CharObject charObject in charObjects)
            {
                word += charObject.character;
            }

            if (timeLimit <= 0)
             {
                currentWord++;
                ShowScramble(currentWord);        //buggy code
                yield break;
             }

            if (isPlayer_1)
            {
                if (!finishedGame)
                {
                    if (player1Words[player1CurrentWordPos].word == word)
                    {
                        result.player1TotalScore += Mathf.RoundToInt(player1TimeLimit + 5);
                        //ui.updateTotalScoreText(true, result.player1TotalScore);
                        player1CorrectWords++;
                    }

                    p1_CharObjPos = 0;
                    player1CorrectPercent = player1CorrectWords / (float)player1Words.Length;
                    player1CurrentWordPos++;
                    ShowScramble(charObjects, player1Words, player1Container, player1CurrentWordPos, true);
                }
            }
            else
            {
                if (!finishedGame)
                {
                    if (player2Words[player2CurrentWordPos].word == word)
                    {
                        result.player2TotalScore += Mathf.RoundToInt(player2TimeLimit + 5);
                        //ui.updateTotalScoreText(false, result.player2TotalScore);
                        player2CorrectWords++;
                    }

                    p2_CharObjPos = 0;
                    player2CorrectPercent = player2CorrectWords / (float)player2Words.Length;
                    player2CurrentWordPos++;
                    ShowScramble(charObjects, player2Words, player2Container, player2CurrentWordPos, false);
                }
            }
        }

        IEnumerator TimeLimit(int currentWordPos, Word[] words, bool isPlayer_1)
        {
            if (!finishedGame)
            {
                int myWord = 0;

                if (isPlayer_1)
                {
                    player1TimeLimit = player1Words[currentWordPos].timeLimit;
                    myWord = currentWordPos;

                    yield return new WaitForSeconds(1);

                    while (player1TimeLimit > 0)
                    {
                        if (myWord != currentWordPos)
                        {
                            yield break;
                        }

                        player1TimeLimit -= Time.deltaTime;
                        ui.UpdateTimeLimitText(true, player1TimeLimit);
                        yield return null;
                    }

                    StartCoroutine(CheckWord(player1CharObjects, player1Words, true));
                }
                else
                {
                    player2TimeLimit = player2Words[currentWordPos].timeLimit;
                    myWord = currentWordPos;

                    yield return new WaitForSeconds(1);

                    while (player2TimeLimit > 0)
                    {
                        if (myWord != currentWordPos)
                        {
                            yield break;
                        }

                        player2TimeLimit -= Time.deltaTime;
                        ui.UpdateTimeLimitText(false, player2TimeLimit);
                        yield return null;
                    }

                    StartCoroutine(CheckWord(player2CharObjects, player2Words, false));
                }
            }
        }

        public void ShowScramble(List<CharObject> charObjects, Word[] words, Transform container, int currentWordPos, bool isPlayer_1)         // Executed in Init() and CheckWord.
        {
            charObjects.Clear();
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }

            // If the game is over...
            if (currentWordPos > words.Length - 1)
            {
                if (isPlayer_1)
                {
                    ui.UpdateDamageDealtText(result.player1TotalScore, true, isPlayersBattle, this);
                    ui.FadeInDamageDealtUI(true, isPlayersBattle);
                    ui.FadeOutScrambleGameStatsUI(true);
                    ui.FadeOutScrambleGameBackgroundUI(true);
                }
                else
                {
                    ui.UpdateDamageDealtText(result.player2TotalScore, false, isPlayersBattle, this);
                    ui.FadeInDamageDealtUI(false, isPlayersBattle);
                    ui.FadeOutScrambleGameStatsUI(false);
                    ui.FadeOutScrambleGameBackgroundUI(false);
                }
                
                for (int i = 0; i < words.Length; i++)
                {
                    words[i].characters.Clear();
                }

                finishedGame = true;

                return;
            }

            // Get characters in random order out of current word.
            List<char> chars = new List<char>(words[currentWordPos].GetScrambledString().ToCharArray());
            foreach (char c in chars)
            {
                CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
                clone.transform.SetParent(container);
                clone.Init(c);

                charObjects.Add(clone);
            }

            if (isPlayer_1)
            {
                ui.UpdateTriesText(true, player1Words[player1CurrentWordPos].tryTimes);
                StartCoroutine(TimeLimit(player1CurrentWordPos, player1Words, true));
            }
            else
            {
                ui.UpdateTriesText(false, player2Words[player2CurrentWordPos].tryTimes);
                StartCoroutine(TimeLimit(player2CurrentWordPos, player2Words, false));
            }
        }

        public void Select(CharObject charObject, bool isPlayer_1)
        {
            if (isPlayer_1)
            {
                if (player1FirstSelectedChar)
                {
                    Swap(player1FirstSelectedChar.index, charObject.index, true);
                    player1FirstSelectedChar.Select(this, true);
                    charObject.Select(this, true);
                }
                else
                {
                    player1FirstSelectedChar = charObject;
                }
            }
            else
            {
                if (player2FirstSelectedChar)
                {
                    Swap(player2FirstSelectedChar.index, charObject.index, false);
                    player2FirstSelectedChar.Select(this, false);
                    charObject.Select(this, false);
                }
                else
                {
                    player2FirstSelectedChar = charObject;
                }
            }
        }

        public void UnSelect(bool isPlayer_1)
        {
            if (isPlayer_1)
                player1FirstSelectedChar = null;
            else
                player2FirstSelectedChar = null;
        }
    }

    [System.Serializable]
    public class Word
    {
        [Header("Custom Word")]
        [Tooltip("The word that will be randomized automatically.")]
        public string word;

        [Header("Custom Word with target order")]
        [Tooltip("The word that will returns without randomzied.")]
        public string desiredRandom;

        [Header("Solve time")]
        public int timeLimit;

        [Header("TryTimes")]
        public int tryTimes;

        [ReadOnlyInspector]
        public List<char> characters = new List<char>();
        public List<char> tempChars;

        public string GetScrambledString()
        {
            // If you have any desired word want to put in then wrote in there.
            if (!string.IsNullOrEmpty(desiredRandom))
            {
                tempChars = new List<char>(desiredRandom.ToCharArray());
                return desiredRandom;
            }
            
            string result = "";
            tempChars = new List<char>(word.ToCharArray());

            // Scramble the characters of a word in random order.
            checkAgain:
            while (tempChars.Count > 0)
            {
                int indexChar = Random.Range(0, tempChars.Count - 1);
                result += tempChars[indexChar];
                characters.Add(tempChars[indexChar]);
                tempChars.RemoveAt(indexChar);
            }

            if (string.Equals(result, word))
            {
                for (int i = 0; i < characters.Count; i++)
                {
                    tempChars.Add(characters[i]);
                }

                characters.Clear();
                result = "";

                goto checkAgain;
            }

            // Return the scrambled word as result.
            return result;
        }
    }
}