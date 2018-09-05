﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Syncfusion.Android.ProgressBar;
using Timer = System.Timers.Timer;

namespace e_SpaMobileApp.Fragments
{
    public class TimerFragment : Fragment
    {
        private Timer _timer;
        private int _min = 2;
        private int _sec = 59;
        private SfCircularProgressBar _circularProgressBar;
        private TextView _timerTxtView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            _timerTxtView = new TextView(Context.ApplicationContext);
            _timerTxtView.Text = $"{_min:00} : {_sec:00}";
            _timerTxtView.SetTextColor(Color.White);
            _timerTxtView.TextSize = 28;
            _timerTxtView.TextAlignment = TextAlignment.Center;

            _circularProgressBar = new SfCircularProgressBar(Context.ApplicationContext);
            _circularProgressBar.Maximum = 100;
            _circularProgressBar.Minimum = 0;
            //_circularProgressBar.StartAngle = 360;
            //_circularProgressBar.EndAngle = 0;
            _circularProgressBar.Progress = 100;
            _circularProgressBar.TrackColor = Color.White;
            _circularProgressBar.Content = _timerTxtView;

            ThreadPool.QueueUserWorkItem(o => BeginTimer());

            return _circularProgressBar;
        }
        private void BeginTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _sec--;
            if (_sec == 0)
            {
                _min--;
                _sec = 59;
            }

            var x = ((_min * 60 + _sec + 1) / 180) * 100;
            Activity.RunOnUiThread(() =>
            {
                _timerTxtView.Text = $"{_min} : {_sec}";
                _circularProgressBar.Progress = x;
            });

            if (_min == 0 && _sec == 0)
                _timer.Stop();
        }

    }
}