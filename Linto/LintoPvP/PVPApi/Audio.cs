using System;
using System.IO;
using System.Reflection;
using AEAssist.Helper;
using CSCore;
using CSCore.Codecs.MP3;
using CSCore.SoundOut;
using Linto;

namespace Audio;

public class Voice
{
	public class AudioPlayer
	{
		private ISoundOut outputDevice;

		private IWaveSource audioSource;

		public DateTime LastPlayTime { get; set; }

		public void PlayEmbeddedAudio(string resourceName)
		{
			StopAudio();
			try
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				using Stream audioStream = assembly.GetManifestResourceStream(resourceName);
				if (audioStream == null || audioStream.Length == 0)
				{
					throw new ArgumentException("未找到资源 '" + resourceName + "' 或该资源为空。");
				}
				audioSource = (IWaveSource)new Mp3MediafoundationDecoder(audioStream);
				outputDevice = (ISoundOut)new WasapiOut();
				outputDevice.Initialize(audioSource);
				outputDevice.Play();
				outputDevice.Volume = (float)(0.015 * (double)PvPSettings.Instance.Volume);
			}
			catch (Exception ex)
			{
				LogHelper.Print("播放嵌入音频时发生错误: " + ex.Message);
			}
		}

		public void StopAudio()
		{
			if (outputDevice != null)
			{
				outputDevice.Stop();
				((IDisposable)outputDevice).Dispose();
				outputDevice = null;
			}
			if (audioSource != null)
			{
				((IDisposable)audioSource).Dispose();
				audioSource = null;
			}
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
		Assembly assembly = Assembly.GetExecutingAssembly();
		string[] manifestResourceNames = assembly.GetManifestResourceNames();
		foreach (string resource in manifestResourceNames)
		{
			
		}
		AppDomain.CurrentDomain.AssemblyResolve += delegate(object? sender, ResolveEventArgs args)
		{
			if (args.Name.Contains("CSCore"))
			{
				using (Stream stream = assembly.GetManifestResourceStream("Linto.Resources.CSCore.dll"))
				{
					if (stream == null)
					{
						LogHelper.Print("无法加载CSCore");
						return null;
					}
					byte[] array = new byte[stream.Length];
					stream.Read(array, 0, array.Length);
					return Assembly.Load(array);
				}
			}
			return null;
		};
	}
}
