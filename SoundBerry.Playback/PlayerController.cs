using NAudio.Wave;
using SoundBerry.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SoundBerry.Playback
{
    public class PlayerController
    {
        private readonly AudioPlayer? _audioPlayer;
        private readonly List<Track>? _trackList;
        private static int _selectedIndex = 0;

        public int SelectedIndex => _selectedIndex;
        public Track CurrentTrack => _trackList[_selectedIndex];


        public PlayerController(AudioPlayer audioPlayer, List<Track> trackList)
        {
            _audioPlayer = audioPlayer;
            _trackList = trackList;
        }

        public void HandleKeyInput(ConsoleKey key)
        {
            if (IsControllerStateValid() == false)
            {
                return;
            }

            switch (key)
            {
                case ConsoleKey.Escape:
                    return;

                case ConsoleKey.UpArrow:
                    if (_selectedIndex > 0)
                    {
                        _selectedIndex--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (_selectedIndex < _trackList.Count - 1)
                    {
                        _selectedIndex++;
                    }
                    break;

                case ConsoleKey.Enter:
                    _audioPlayer.Play(CurrentTrack);
                    break;

                case ConsoleKey.Spacebar:
                    if (_audioPlayer.PlaybackState == PlaybackState.Playing)
                    {
                        _audioPlayer.Pause();
                    }
                    else if (_audioPlayer.PlaybackState == PlaybackState.Paused)
                    {
                        _audioPlayer.Resume();
                    }

                    break;
            }
        }

        private bool IsControllerStateValid()
        {
            return _audioPlayer != null && _trackList != null;

        }


    }
}
