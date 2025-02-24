﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YouiToolkit.Design
{
    public partial class PendingBoxDialog : Window
    {
        private bool _closeHandler = true;

        public PendingBoxDialog(Window owner, string message, string title, bool cancelable, PendingBoxConfig config)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(title))
            {
                TxtTitle.Text = title;
                TxtTitle.Visibility = Visibility.Visible;
                Title = title;
                WindowXCaption.SetHeight(this, 30);
            }

            Owner = owner;
            Cancelable = cancelable;
            Message = message;
            CancelButton = config.CancelButton;

            Foreground = config.Foreground;
            LoadingBackground = config.LoadingBackground;
            LoadingForeground = config.LoadingForeground;
            ButtonBrush = config.ButtonBrush;

            ShowInTaskbar = config.ShowInTaskbar;
            Topmost = config.Topmost;
            FontSize = config.FontSize;
            WindowStartupLocation = config.WindowStartupLocation;
            LoadingStyle = config.LoadingStyle;
            LoadingSize = config.LoadingSize;
            MinHeight = config.MinHeight;
            MinWidth = config.MinWidth;
            MaxHeight = config.MaxHeight;
            MaxWidth = config.MaxWidth;
        }

        #region Event
        public event EventHandler Canceled;
        #endregion

        #region Property


        public bool Cancelable
        {
            get { return (bool)GetValue(CancelableProperty); }
            set { SetValue(CancelableProperty, value); }
        }

        public static readonly DependencyProperty CancelableProperty =
            DependencyProperty.Register("Cancelable", typeof(bool), typeof(PendingBoxDialog));


        /// <summary>
        /// Gets or sets loading style.
        /// </summary>
        public LoadingStyle LoadingStyle
        {
            get { return (LoadingStyle)GetValue(LoadingStyleProperty); }
            set { SetValue(LoadingStyleProperty, value); }
        }

        public static readonly DependencyProperty LoadingStyleProperty =
            DependencyProperty.Register("LoadingStyle", typeof(LoadingStyle), typeof(PendingBoxDialog), new PropertyMetadata(OnLoadingStyleChanged));



        public Brush ButtonBrush
        {
            get { return (Brush)GetValue(ButtonBrushProperty); }
            set { SetValue(ButtonBrushProperty, value); }
        }

        public static readonly DependencyProperty ButtonBrushProperty =
            DependencyProperty.Register("ButtonBrush", typeof(Brush), typeof(PendingBoxDialog));


        /// <summary>
        /// Gets or sets loading stroke.
        /// </summary>
        public Brush LoadingBackground
        {
            get { return (Brush)GetValue(LoadingBackgroundProperty); }
            set { SetValue(LoadingBackgroundProperty, value); }
        }

        public static readonly DependencyProperty LoadingBackgroundProperty =
            DependencyProperty.Register("LoadingBackground", typeof(Brush), typeof(PendingBoxDialog), new PropertyMetadata(OnLoadingStyleChanged));

        /// <summary>
        /// Gets or sets loading stroke cover.
        /// </summary>
        public Brush LoadingForeground
        {
            get { return (Brush)GetValue(LoadingForegroundProperty); }
            set { SetValue(LoadingForegroundProperty, value); }
        }

        public static readonly DependencyProperty LoadingForegroundProperty =
            DependencyProperty.Register("LoadingForeground", typeof(Brush), typeof(PendingBoxDialog), new PropertyMetadata(OnLoadingStyleChanged));

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(PendingBoxDialog));

        /// <summary>
        /// Gets or sets loading size.
        /// </summary>
        public double LoadingSize
        {
            get { return (double)GetValue(LoadingSizeProperty); }
            set { SetValue(LoadingSizeProperty, value); }
        }

        public static readonly DependencyProperty LoadingSizeProperty =
            DependencyProperty.Register("LoadingSize", typeof(double), typeof(PendingBoxDialog));

        /// <summary>
        /// Gets or sets cancel button content.
        /// </summary>
        public string CancelButton
        {
            get { return (string)GetValue(CancelButtonProperty); }
            set { SetValue(CancelButtonProperty, value); }
        }

        public static readonly DependencyProperty CancelButtonProperty =
            DependencyProperty.Register("CancelButton", typeof(string), typeof(PendingBoxDialog));
        #endregion

        #region Event Handler
        private void PendingBox_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_closeHandler)
                e.Cancel = true;
        }

        private void PendingBox_Loaded(object sender, RoutedEventArgs e)
        {
            LdMain.IsRunning = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Canceled?.Invoke(this, e);
            var btnCancel = sender as Button;
            btnCancel.IsEnabled = false;
        }

        private static void OnLoadingStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as PendingBoxDialog;
            if (box.LoadingBackground != null)
                box.LdMain.Background = box.LoadingBackground;
            if (box.LoadingForeground != null)
                box.LdMain.Foreground = box.LoadingForeground;

            box.LdMain.LoadingStyle = box.LoadingStyle;
        }
        #endregion

        #region Calling Methods
        public void UpdateMessage(string message)
        {
            Message = message;
        }

        public void ForceClose()
        {
            _closeHandler = false;
            Close();
        }
        #endregion
    }
}
