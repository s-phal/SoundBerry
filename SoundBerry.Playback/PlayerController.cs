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
        private int _selectedIndex = 0;
        private ConsoleKey _selectedConsoleKey = ConsoleKey.None;

        public int SelectedIndex => _selectedIndex;
        public Track CurrentTrack => _trackList[_selectedIndex];
        public ConsoleKey SelectedConsoleKey => _selectedConsoleKey;


        public PlayerController(AudioPlayer audioPlayer, List<Track> trackList)
        {
            _audioPlayer = audioPlayer;
            _trackList = trackList;
        }

        public void HandleKeyInput()
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;

                case ConsoleKey.A:
                    _selectedConsoleKey = ConsoleKey.A;
                    return;

                case ConsoleKey.Escape:
                    _selectedConsoleKey = ConsoleKey.Escape;
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
                    if (_trackList == null || _trackList.Count == 0)
                    {
                        return;
                    }
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

        private void RunAddTrackPrompt()
        {
            Console.WriteLine("Enter YouTube URL:");
        }

        private bool IsControllerStateValid()
        {
            return _audioPlayer != null && _trackList != null;

        }


    }
}
