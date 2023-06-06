using Microsoft.Maui.Layouts;

namespace PersianUIControlsMaui.Controls
{
    public class SlideButton : AbsoluteLayout
    {
        #region Propertie's
        public static readonly BindableProperty ThumbProperty = BindableProperty.Create("Thumb", typeof(View), typeof(SlideButton), defaultValue: default(View));
        public View Thumb
        {
            get { return (View)GetValue(ThumbProperty); }
            set { SetValue(ThumbProperty, value); }
        }

        public static readonly BindableProperty RightToLeftProperty = BindableProperty.Create("RightToLeft", typeof(bool), typeof(SlideButton), false);
        public bool RightToLeft
        {
            get { return (bool)GetValue(RightToLeftProperty); }
            set { SetValue(RightToLeftProperty, value); }
        }

        public static readonly BindableProperty SlideCompletedCommandProperty = BindableProperty.Create("SlideCompletedCommand", typeof(Command), typeof(SlideButton), default(Command));
        public Command SlideCompletedCommand
        {
            get { return (Command)GetValue(SlideCompletedCommandProperty); }
            set { SetValue(SlideCompletedCommandProperty, value); }
        }

        public static readonly BindableProperty TrackBarProperty = BindableProperty.Create("TrackBar", typeof(View), typeof(SlideButton), defaultValue: default(View));
        public View TrackBar
        {
            get { return (View)GetValue(TrackBarProperty); }
            set { SetValue(TrackBarProperty, value); }
        }

        public static readonly BindableProperty FillBarProperty = BindableProperty.Create("FillBar", typeof(View), typeof(SlideButton), defaultValue: default(View));
        public View FillBar
        {
            get { return (View)GetValue(FillBarProperty); }
            set { SetValue(FillBarProperty, value); }
        }
        #endregion

        #region Field's
        private PanGestureRecognizer _panGesture = new PanGestureRecognizer();
        private View _gestureListener;
        #endregion

        public SlideButton()
        {
            _panGesture.PanUpdated += OnPanGestureUpdated;
            SizeChanged += OnSizeChanged;

            _gestureListener = new ContentView { BackgroundColor = Colors.White, Opacity = 0.05 };
            _gestureListener.GestureRecognizers.Add(_panGesture);
        }

        public event EventHandler SlideCompleted;

        private const double _fadeEffect = 0.5;
        private const uint _animLength = 50;
        async void OnPanGestureUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (Thumb == null || TrackBar == null || FillBar == null)
                return;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    await TrackBar.FadeTo(_fadeEffect, _animLength);
                    break;

                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    var x = Math.Max(0, e.TotalX);
                    if (x > (Width - Thumb.Width))
                        x = (Width - Thumb.Width);

                    //Uncomment this if you want only forward dragging.
                    //if (e.TotalX < Thumb.TranslationX)
                    //    return;
                    Thumb.TranslationX = x;
                    FillBar.IsVisible = true;
                    if (!RightToLeft)
                        this.SetLayoutBounds(FillBar, new Rect(0, 0, x + Thumb.Width / 2, this.Height));
                    else
                        this.SetLayoutBounds(FillBar, new Rect(x - Width, 0, x - Thumb.Width / 2, this.Height));

                    break;

                case GestureStatus.Completed:
                    var posX = Thumb.TranslationX;
                    this.SetLayoutBounds(FillBar, new Rect(RightToLeft ? Width : 0, 0, 0, this.Height));
                    FillBar.IsVisible = false;

                    var thumbX = RightToLeft ? Width : 0;
                    // Reset translation applied during the pan
                    await Task.WhenAll(new Task[]{
                        TrackBar.FadeTo(1, _animLength),
                        Thumb.TranslateTo(thumbX, 0, _animLength * 2, Easing.CubicIn)
                    });
                    if (!RightToLeft)
                    {
                        if (posX >= (Width - Thumb.Width - 10/* keep some margin for error*/))
                        {
                            if (SlideCompletedCommand != null)
                                SlideCompletedCommand.Execute(null);

                            SlideCompleted?.Invoke(this, EventArgs.Empty);
                        }
                    }
                    else
                    {
                        if (posX <= 10/* keep some margin for error*/)
                        {
                            if (SlideCompletedCommand != null)
                                SlideCompletedCommand.Execute(null);
                            SlideCompleted?.Invoke(this, EventArgs.Empty);
                        }
                    }
                    break;
            }
        }

        void OnSizeChanged(object sender, EventArgs e)
        {
            if (Width == 0 || Height == 0)
                return;
            if (Thumb == null || TrackBar == null || FillBar == null)
                return;


            Children.Clear();

            this.SetLayoutFlags(TrackBar, AbsoluteLayoutFlags.SizeProportional);
            this.SetLayoutBounds(TrackBar, new Rect(0, 0, 1, 1));
            Children.Add(TrackBar);

            if (!RightToLeft)
            {
                this.SetLayoutFlags(FillBar, AbsoluteLayoutFlags.None);
                FillBar.IsVisible = false;
                this.SetLayoutBounds(FillBar, new Rect(Thumb.Width / 2, 0, 0, this.Height));
                Children.Add(FillBar);

                this.SetLayoutFlags(Thumb, AbsoluteLayoutFlags.None);
                this.SetLayoutBounds(Thumb, new Rect(0, 0, this.Width / 5, this.Height));
                Children.Add(Thumb);
            }
            if (RightToLeft)
            {
                this.SetLayoutFlags(FillBar, AbsoluteLayoutFlags.PositionProportional);
                this.SetLayoutBounds(FillBar, new Rect(1, 0, 0, this.Height));
                Children.Add(FillBar);

                this.SetLayoutFlags(Thumb, AbsoluteLayoutFlags.PositionProportional);
                this.SetLayoutBounds(Thumb, new Rect(1, 0, this.Width / 5, this.Height));
                Children.Add(Thumb);
            }
            this.SetLayoutFlags(_gestureListener, AbsoluteLayoutFlags.SizeProportional);
            this.SetLayoutBounds(_gestureListener, new Rect(0, 0, 1, 1));
            Children.Add(_gestureListener);
        }
    }
}
