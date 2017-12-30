﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flex.Effects;
using Xamarin.Forms;

namespace Flex.Controls
{
    //[ContentProperty(nameof(ButtonContent))]
    public partial class FlexButton : ContentView
    {

        #region Color Properties

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        // TODO: Border Color does not wokr on Android at the moment due to a Xamarin.Forms bug
        //public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FlexButton), Color.Red);
        //public Color BorderColor
        //{
        //    get { return (Color)GetValue(BorderColorProperty); }
        //    set { SetValue(BorderColorProperty, value); }
        //}

        public static readonly BindableProperty HighlightBackgroundColorProperty = BindableProperty.Create(nameof(HighlightBackgroundColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public Color HighlightBackgroundColor
        {
            get { return (Color)GetValue(HighlightBackgroundColorProperty); }
            set { SetValue(HighlightBackgroundColorProperty, value); }
        }

        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(FlexButton), Color.White, propertyChanged: IconOrIconColorPropertyChanged);
        public Color IconColor
        {
            get { return (Color)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        public static readonly BindableProperty HighlightIconColorProperty = BindableProperty.Create(nameof(HighlightIconColor), typeof(Color), typeof(FlexButton), Color.White);
        public Color HighlightIconColor
        {
            get { return (Color)GetValue(HighlightIconColorProperty); }
            set { SetValue(HighlightIconColorProperty, value); }
        }

        #endregion

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FlexButton), 0);
        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty IconPaddingProperty =
            BindableProperty.Create(
                nameof(IconPadding),
                typeof(Thickness),
                typeof(FlexButton),
                new Thickness(-1));

        public Thickness IconPadding
        {
            get { return (Thickness)GetValue(IconPaddingProperty); }
            set { SetValue(IconPaddingProperty, value); }
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(FlexButton), null, propertyChanged: IconOrIconColorPropertyChanged);
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        static void IconOrIconColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((FlexButton)bindable).ColorIcon(((FlexButton)bindable).IconColor);
        }

        public View ButtonContent
        {
            get { return ContainerContent.Content; }
            set { ContainerContent.Content = value; }
        }

        public event EventHandler<EventArgs> TouchedDown;
        public event EventHandler<EventArgs> TouchedUp;

        public FlexButton()
        {
            InitializeComponent();
            BindingContext = this;

            TouchRecognizer.TouchDown += TouchDown;
            TouchRecognizer.TouchUp += TouchUp;
            SizeChanged += FlexButton_SizeChanged;

            this.ButtonIcon.SizeChanged += ButtonIcon_SizeChanged;
        }

        void ButtonIcon_SizeChanged(object sender, EventArgs e)
        {
            ColorIcon((IconColor));
        }

        void FlexButton_SizeChanged(object sender, EventArgs e)
        {
            // Set IconPadding to 30% of with / height by default if no specific padding is set
            if (IconPadding.Equals(new Thickness(-1)))
            {
                IconPadding = new Thickness(WidthRequest * .3, HeightRequest * .3);
            }
        }

        private void TouchDown()
        {
            TouchedDown?.Invoke(this, null);

            Container.BackgroundColor = HighlightBackgroundColor;

            ColorIcon(HighlightIconColor);

            //ButtonIcon.Effects.Remove(IconColorOverlayEffect);
            //ButtonIcon.Effects.Add(HighlightIconColorOverlayEffect);

        }

        private void TouchUp()
        {
            TouchedUp?.Invoke(this, null);

            Container.BackgroundColor = BackgroundColor;

            ColorIcon(IconColor);

            //ButtonIcon.Effects.Remove(HighlightIconColorOverlayEffect);
            //ButtonIcon.Effects.Add(IconColorOverlayEffect);
        }

        private void ColorIcon(Color color)
        {
            ButtonIcon.Effects.Clear();
            ButtonIcon.Effects.Add(new ColorOverlayEffect() { Color = color });
        }
    }
}