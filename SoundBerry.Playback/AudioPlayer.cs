using NAudio.Wave;
using SoundBerry.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace SoundBerry.Playback
{
    public class AudioPlayer
    {
        private WaveOutEvent? _outputDevice;
        private AudioFileReader? _audioFile;
        public PlaybackState PlaybackState => _outputDevice.PlaybackState;

        public AudioPlayer()
        {
            _outputDevice = new WaveOutEvent();
        }


        public async Task Play(Track track)
        {
            this.Stop();

            using var youtube = new YoutubeClient();

            var videoUrl = track.Url;

            var video = await youtube.Videos.GetAsync(videoUrl);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("    Fetching stream...");
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);

            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            if (video != null)
            {
                var title = video.Title;
                var author = video.Author.ChannelTitle;
                var duration = video.Duration;
            }

            if (track.IsDownloaded == false)
            {
                var url = streamInfo.Url;
                using (var mf = new MediaFoundationReader(url))

                _outputDevice.Init(mf);
                _outputDevice.Play();

            }
            else if (track.IsDownloaded && !string.IsNullOrWhiteSpace(track.FilePath))
            {
                _audioFile = new AudioFileReader($"C:\\Users\\sam\\source\\repos\\s-phal\\SoundBerry\\SoundBerry.UI\\{track.FilePath}");
                _outputDevice = new WaveOutEvent();

                _outputDevice.Init(_audioFile);
                _outputDevice.Play();

            }

            Console.WriteLine("Playing");

            Console.ResetColor();

        }

        public void Stop()
        {
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _audioFile?.Dispose();
        }

        public void Pause()
        {
            _outputDevice?.Pause();
        }

        public void Resume()
        {
            if (_outputDevice?.PlaybackState == PlaybackState.Paused)
            {
                _outputDevice.Play();
            }
        }

        private bool IsValid(Track track)
        {
            if (track == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(track.FilePath))
            {
                return false;
            }

            return true;
        }


    }
}
