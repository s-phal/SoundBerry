using NAudio.Wave;
using SoundBerry.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoundBerry.Player
{
    public class Player
    {
        private WaveOutEvent? _outputDevice;
        private AudioFileReader? _audioFile;
        public PlaybackState PlaybackState => _outputDevice.PlaybackState;

        public Player()
        {
            _outputDevice = new WaveOutEvent();
        }


        public void Play(Track track)
        {
            if (IsValid(track) == false)
            {
                return;
            }


            this.Stop();

            _audioFile = new AudioFileReader($"C:\\Users\\sam\\source\\repos\\s-phal\\SoundBerry\\SoundBerry.UI\\{track.FilePath}");
            _outputDevice = new WaveOutEvent();

            _outputDevice.Init(_audioFile);
            _outputDevice.Play();
        }

        public void Stop()
        {
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _audioFile?.Dispose();

            _audioFile = null;
            _outputDevice = null;
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
