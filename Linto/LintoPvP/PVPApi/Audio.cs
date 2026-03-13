using System;
using AEAssist.Helper;
using Linto;

namespace Audio;

public class Voice
{
	public class AudioPlayer
	{
		public DateTime LastPlayTime { get; set; }

		public void PlayEmbeddedAudio(string resourceName)
		{
			LogHelper.Print("音频播放功能已临时禁用。");
		}

		public void StopAudio()
		{
			
		}
	}

	public static void PlayVoiceRandom()
	{
		Random random = new Random();
		AudioPlayer audioPlayer = new AudioPlayer();
		int randomIndex = random.Next(1, 4);
		string file = "";
		string logMessage = "";
		switch (randomIndex)
		{
		case 1:
			file = "启动";
			break;
		case 2:
			file = "大家好啊";
			break;
		case 3: 
			file = "大家好啊";
			break;
		}
		audioPlayer.StopAudio();
		PlayVoice(file);
	}

	public static void PlayVoice(string filename)
	{
		AudioPlayer audioPlayer = new AudioPlayer();
		audioPlayer.StopAudio();
		audioPlayer.PlayEmbeddedAudio("Linto.Resources." + filename + ".mp3");
	}

	public static void PlayVoiceSelect()
	{
		AudioPlayer audioPlayer = new AudioPlayer();
		audioPlayer.StopAudio();
		audioPlayer.PlayEmbeddedAudio("Linto.Resources." + PvPSettings.Instance.语音选择 + ".mp3");
	}

	public static void LoadVoice()
	{
		LogHelper.Print("音频加载流程已临时禁用。");
	}
}
