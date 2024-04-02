using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    private const long CLIENT_ID = 1224498142091939880; // replace this with your client ID from the discord applicatiom
    // ^^ application ID on the discord dev application page ^^
    private Discord.Discord discord;

    [Space] // I don't think this matters but it 
    private string details = "a game of all time";
    [Space]
    private string largeImage = "warewareLogo.png"; //"this should match the file name in your discord application on the discord site";
    private string largeText = "wareware"; //"this will be the first line, should be the title";

    private long time;

    private bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            discord = new Discord.Discord(CLIENT_ID, (UInt64)Discord.CreateFlags.NoRequireDiscord);
        }
        catch 
        {
        }
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    void OnApplicationQuit()
    {
        StopAllCoroutines();
        if (discord != null)
        {
            discord.Dispose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (discord == null)
        {
            if (!waiting)
            {
                waiting = true;
                StartCoroutine(ReTryDiscordConnect());
            }
        }
        else
        {
            try
            {
                MainUpdate();
                discord.RunCallbacks();
            }
            catch
            {
                discord = null;
            }
        }
    }

    private IEnumerator ReTryDiscordConnect()
    {
        yield return new WaitForSecondsRealtime(10f);
        try
        {
            discord = new Discord.Discord(CLIENT_ID, (UInt64)Discord.CreateFlags.NoRequireDiscord);
        }
        catch
        {
        }
        waiting = false;
    }

    // If you want the game status to change dynamically, try setting the details here 
    // to something else based on the game state
    private void MainUpdate()
    {
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            Details = details,
            Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
            Timestamps =
                {
                    Start = time
                }
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res != Discord.Result.Ok) Debug.LogError("Failed connecting to Discord!");
        });
    }
}