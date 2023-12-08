using UnityEngine;
using TMPro;
using System.Collections;

public class NewsTickerManager : MonoBehaviour
{
    [SerializeField] internal TMP_Text newsText;
    [SerializeField] private float scrollSpeed = 100f;
    [SerializeField] private float messageInterval = 15f;

    private string[] unhelpfulSayings = new string[]
    {
    "Just try to be happy!",
    "It's all in your head.",
    "Cheer up!",
    "You just need to get out more.",
    "Everyone feels this way sometimes.",
    "You have so much to be thankful for.",
    "Just think positive thoughts.",
    "This too shall pass.",
    "There are people worse off than you.",
    "You're just having a bad day.",
    "You'll be fine.",
    "Others depend on you, so you can't be sad.",
    "It could be worse.",
    "Happiness is a choice.",
    "Just don't think about it.",
    "You're always so negative.",
    "Try not to be so depressed.",
    "Life isn't meant to be easy.",
    "You should smile more.",
    "You're just tired.",
    "Stop feeling sorry for yourself.",
    "You have to be strong.",
    "Everything happens for a reason.",
    "Just take a deep breath.",
    "You're overthinking it.",
    "You're being too sensitive.",
    "You'll get over it soon.",
    "You're making it a bigger deal than it is.",
    "Just let it go.",
    "Stop being so anxious.",
    "You have to pull yourself together.",
    "Other people have it worse.",
    "Just change your mindset.",
    "You're just stressed out.",
    "You're not even trying.",
    "You'll feel better if you exercise.",
    "You don't look depressed.",
    "It's just a phase.",
    "Just focus on the positive.",
    "You need to stop being so lazy.",
    "You're too young to be depressed.",
    "You should be more grateful.",
    "Life isn't that hard.",
    "You're not trying hard enough.",
    "You're just seeking attention.",
    "Just eat healthier.",
    "You're overreacting.",
    "Just be strong.",
    "You're not praying enough.",
    "You should go out more.",
    "Stop being dramatic.",
    "You're fine.",
    "Just get over it.",
    "You're making a mountain out of a molehill.",
    "Others are coping with more.",
    "Stop being so moody.",
    "You need to be more positive.",
    "You're not really depressed.",
    "Just relax.",
    "You're being irrational.",
    "You need to socialize more.",
    "Stop being a downer.",
    "You don't need medication, just fresh air.",
    "Everyone gets stressed.",
    "Just put on a brave face.",
    "You need to stop overanalyzing.",
    "Just snap out of it.",
    "Stop thinking about it.",
    "You're imagining things.",
    "You're being too dramatic.",
    "It's just a bad day, not a bad life.",
    "You're being selfish.",
    "You need to try harder.",
    "You're not really anxious.",
    "Just focus on the good things.",
    "You're being unreasonable.",
    "You need to stop feeling sorry for yourself.",
    "You're not alone, everyone goes through this.",
    "You need to stop being so pessimistic.",
    "You're not making any effort.",
    "You just need to sleep it off.",
    "You're overcomplicating things.",
    "You need to cheer up.",
    "Just think about something else.",
    "You're making it all up.",
    "You just need a good night's sleep.",
    "You should try not to be so sad.",
    "You're just having a bad week.",
    "It's not that big of a deal.",
    "You need to be more resilient.",
    "You're just having a rough patch.",
    "You need to get over it.",
    "Just keep busy and you'll forget about it.",
    "You're making yourself miserable.",
    "You just need some time alone.",
    "You're being melodramatic.",
    "You just need to find a hobby.",
    "Stop looking for sympathy.",
    "You're not the only one who feels this way.",
    "You'll feel better tomorrow.",
    "It's just a bad day, not a bad life.",
    "Life is tough, but so are you.",
    "You're just going through a rough time.",
    "You should try to be more active.",
    "You'll get through this, you always do.",
    "Just keep your chin up.",
    "You have to fight through some bad days to earn the best days.",
    "Everyone goes through this.",
    "This is just a bump in the road.",
    "You're stronger than you think.",
    "You just need to find the right perspective.",
    "You're letting it control you.",
    "Don't let it get to you.",
    "You're worrying about nothing.",
    "You should try meditation or yoga.",
    "You need to get your mind off of it.",
    "You're only like this because you're tired.",
    "You'll be okay, you always are.",
    "You're making it worse by thinking about it.",
    "You're too emotional.",
    "It's not as bad as you think.",
    "You need to distract yourself.",
    "You just need a good laugh.",
    "You're just having a rough time.",
    "It's not worth getting upset over.",
    "You need to toughen up.",
    "You're just not trying hard enough.",
    "It's a matter of willpower.",
    "You're focusing on the negative too much.",
    "You need to let go of the past.",
    "You're too focused on your problems.",
    "You should be more optimistic.",
    "You're making a big deal out of nothing.",
    "You need to snap out of it.",
    "You're too sensitive.",
    "You need to lighten up.",
    "You're overdoing it.",
    "You should just stop thinking about it.",
    "You're being too hard on yourself.",
    "You just need to pick yourself up.",
    "You're not alone, so don't feel bad.",
    "You just need to try new things.",
    "You should focus on the good in your life.",
    "You're just in a funk.",
    "You should go out and enjoy yourself.",
    "You're just in a bad mood.",
    "You're not seeing things clearly.",
    "You need to get over it.",
    "You're just feeling sorry for yourself.",
    "It's not that serious.",
    "You just need to cheer up.",
    "You're making yourself feel this way.",
    "You need to change your attitude.",
    "You just need to face your fears.",
    "You're just not seeing the positive side.",
    "You're exaggerating the situation.",
    "You need to stop being so negative.",
    "You just need to calm down.",
    "You're not putting in the effort.",
    "You need to stop worrying so much.",
    "You're being irrational.",
    "You just need to relax.",
    "You're thinking too much.",
    "You need to be more realistic.",
    "You're just not facing reality.",
    "You need to get a grip.",
    "You're just tired.",
    "You're not even trying to be happy.",
    "You're just stressed.",
    "You're not really feeling this way.",
    "You just need to get your act together.",
    "You're making things harder for yourself.",
    "You need to stop overreacting.",
    "You're just not doing the right things.",
    "You need to get your life together.",
    "You're not making the best of things.",
    "You just need to change your mindset.",
    "You're not looking at the bright side.",
    "You're just not giving it enough time.",
    "You need to stop being so hard on yourself.",
    "You're just not dealing with it properly.",
    "You need to stop feeling this way.",
    "You're just not being rational.",
    "You need to control your emotions.",
    "You're not really trying to get better.",
    "You're just not taking care of yourself.",
    "You need to focus on the present.",
    "You're just not being positive.",
    "You need to stop thinking negatively.",
    "You're just not working hard enough.",
    "You need to take things less seriously.",
    "You're just not focusing on the right things.",
    "You need to stop letting it bother you.",
    "Just divide and conquer your problems."
    };

    private string currentMessage;
    private float messageWidth;
    private bool enableScroll = true;

    void Start()
    {
        StartCoroutine(ScrollTextCoroutine());
    }

    private IEnumerator ScrollTextCoroutine()
    {
        while (true)
        {
            SetRandomMessage();
            ResetMessagePosition();
            yield return new WaitUntil(() => HasMessageScrolledOut());

            enableScroll = false;
            yield return new WaitForSeconds(messageInterval);
            enableScroll = true;
        }
    }

    void Update()
    {
        if(enableScroll)
            ScrollText();
    }

    void ScrollText()
    {
        newsText.rectTransform.position += new Vector3(-scrollSpeed * Time.deltaTime, 0, 0);
    }

    bool HasMessageScrolledOut()
    {
        RectTransform panelRectTransform = newsText.transform.parent.GetComponent<RectTransform>();
        float panelLeftEdge = panelRectTransform.rect.xMin;

        Vector3 textPosition = newsText.rectTransform.localPosition;
        float textRightEdge = textPosition.x + messageWidth;

        return textRightEdge < panelLeftEdge;
    }

    void SetRandomMessage()
    {
        currentMessage = unhelpfulSayings[Random.Range(0, unhelpfulSayings.Length)];
        newsText.text = currentMessage;

        // Measure the width of the message
        messageWidth = newsText.preferredWidth;
        ResetMessagePosition();
    }

    void ResetMessagePosition()
    {
        Vector3 startPosition = newsText.rectTransform.localPosition;
        startPosition.x = Screen.width;
        newsText.rectTransform.localPosition = startPosition;
    }
}
