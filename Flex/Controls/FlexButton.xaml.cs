﻿using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Flex.Effects;
using Flex.Extensions;
using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Forms.Xaml;
using System.Linq;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Flex.Controls
{
    public partial class FlexButton : ContentView
    {
        ButtonMode mode;
		ToggleMode toggleMode;

        bool userChangedPadding;

        #region Bindable Properties

        // Foreground and Background Properties

        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly BindableProperty HighlightBackgroundColorProperty = BindableProperty.Create(nameof(HighlightBackgroundColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public Color HighlightBackgroundColor
        {
            get => (Color)GetValue(HighlightBackgroundColorProperty);
            set => SetValue(HighlightBackgroundColorProperty, value);
        }

        public static readonly BindableProperty ForegroundColorProperty = BindableProperty.Create(nameof(ForegroundColor), typeof(Color), typeof(FlexButton), Color.White);
        public Color ForegroundColor
        {
            get => (Color)GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        public static readonly BindableProperty HighlightForegroundColorProperty = BindableProperty.Create(nameof(HighlightForegroundColor), typeof(Color), typeof(FlexButton), Color.White);
        public Color HighlightForegroundColor
        {
            get => (Color)GetValue(HighlightForegroundColorProperty);
            set => SetValue(HighlightForegroundColorProperty, value);
        }

        // Border Properties

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty HighlightBorderColorProperty = BindableProperty.Create(nameof(HighlightBorderColor), typeof(Color), typeof(FlexButton), Color.Transparent);
        public Color HighlightBorderColor
        {
            get => (Color)GetValue(HighlightBorderColorProperty);
            set => SetValue(HighlightBorderColorProperty, value);
        }

        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(Thickness), typeof(FlexButton), new Thickness(0));
        public Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(FlexButton), 0);
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        public int InnerCornerRadius { get; private set; }

        // Icon Properties

        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(FlexButton), null);
        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly BindableProperty IconOrientationProperty = BindableProperty.Create(nameof(IconOrientation), typeof(IconOrientation), typeof(FlexButton), IconOrientation.Left);
        public IconOrientation IconOrientation
        {
            get => (IconOrientation)GetValue(IconOrientationProperty);
            set => SetValue(IconOrientationProperty, value);
        }

        // Text Properties

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FlexButton), string.Empty);
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(FlexButton), Device.GetNamedSize(NamedSize.Default, typeof(Label)));
        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(FlexButton), (FontAttributes)Label.FontAttributesProperty.DefaultValue);
        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(FlexButton), (string)Label.FontFamilyProperty.DefaultValue);
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        // Toggle Properties

		public static readonly BindableProperty ToggleModeProperty = BindableProperty.Create(nameof(ToggleMode), typeof(ToggleMode), typeof(FlexButton), ToggleMode.None);
        public ToggleMode ToggleMode
        {
			get => (ToggleMode)GetValue(ToggleModeProperty);
            set => SetValue(ToggleModeProperty, value);
        }

        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(FlexButton), false, BindingMode.TwoWay);
        public bool IsToggled
        {
            get => (bool)GetValue(IsToggledProperty);
            set => SetValue(IsToggledProperty, value);
        }

        // Other Properties

        public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(FlexButton), new Thickness(-1));
        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

		public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(nameof(LineBreakMode), typeof(LineBreakMode), typeof(FlexButton), LineBreakMode.WordWrap);
        public LineBreakMode LineBreakMode
        {
            get => (LineBreakMode)GetValue(LineBreakModeProperty);
            set => SetValue(LineBreakModeProperty, value);
        }

        public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create(nameof(MaxLines), typeof(int), typeof(FlexButton), 99999);
        public int MaxLines
        {
            get => (int)GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }

        #endregion

        #region Commands

        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(FlexButton), null);
        public ICommand ClickedCommand
        {
            get => (ICommand)GetValue(ClickedCommandProperty);
            set => SetValue(ClickedCommandProperty, value);
        }

        public static readonly BindableProperty ClickedCommandParameterProperty = BindableProperty.Create(nameof(ClickedCommandParameter), typeof(object), typeof(FlexButton), null);
        public object ClickedCommandParameter
        {
            get => GetValue(ClickedCommandParameterProperty);
            set => SetValue(ClickedCommandParameterProperty, value);
        }

        public static BindableProperty TouchedDownCommandProperty = BindableProperty.Create(nameof(TouchedDownCommand), typeof(ICommand), typeof(FlexButton), null);
        public ICommand TouchedDownCommand
        {
            get { return (ICommand)GetValue(TouchedDownCommandProperty); }
            set { SetValue(TouchedDownCommandProperty, value); }
        }

        public static readonly BindableProperty TouchedDownCommandParameterProperty = BindableProperty.Create(nameof(TouchedDownCommandParameter), typeof(object), typeof(FlexButton), null);
        public object TouchedDownCommandParameter
        {
            get => GetValue(TouchedDownCommandParameterProperty);
            set => SetValue(TouchedDownCommandParameterProperty, value);
        }

        public static BindableProperty TouchedUpCommandProperty = BindableProperty.Create(nameof(TouchedUpCommand), typeof(ICommand), typeof(FlexButton), null);
        public ICommand TouchedUpCommand
        {
            get => (ICommand)GetValue(TouchedUpCommandProperty);
            set => SetValue(TouchedUpCommandProperty, value);
        }

        public static readonly BindableProperty TouchedUpCommandParameterProperty = BindableProperty.Create(nameof(TouchedUpCommandParameter), typeof(object), typeof(FlexButton), null);
        public object TouchedUpCommandParameter
        {
            get => GetValue(TouchedUpCommandParameterProperty);
            set => SetValue(TouchedUpCommandParameterProperty, value);
        }

        #endregion

        #region Events

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == PaddingProperty.PropertyName)
            {
                userChangedPadding = true;
            }
            else if (propertyName == IsEnabledProperty.PropertyName)
            {
                // Set Opacity based on IsEnabled
                Opacity = IsEnabled ? 1 : 0.5;
            }
            else if (propertyName == IconProperty.PropertyName || propertyName == ForegroundColorProperty.PropertyName)
            {
                SetButtonMode();

                // Make sure, that Icon Source is set manually, as Binding is too slow sometimes
                ButtonIcon.Source = Icon;
                ColorIcon(ForegroundColor);
            }
			else if (propertyName == MaxLinesProperty.PropertyName)
            {
                MaxLinesEffect effect = ButtonText.Effects.OfType<MaxLinesEffect>().FirstOrDefault();

                if (effect != null)
                {
                    effect.MaxLines = MaxLines;
                }
            }
            else if (propertyName == LineBreakModeProperty.PropertyName)
            {
                LineBreakModeEffect effect = ButtonText.Effects.OfType<LineBreakModeEffect>().FirstOrDefault();

                if (effect != null)
                {
                    effect.LineBreakMode = LineBreakMode;
                }
            } 
			else if (propertyName == IsToggledProperty.PropertyName)
            {
                if (ToggleMode > 0)
                {
                    Highlight(IsToggled);
                }
            }
            else if (
                propertyName == TextProperty.PropertyName ||
                propertyName == IconOrientationProperty.PropertyName ||
                propertyName == BorderThicknessProperty.PropertyName ||
                propertyName == ToggleModeProperty.PropertyName)
            {
                SetButtonMode();
            }



            base.OnPropertyChanged(propertyName);
        }


        #endregion

        void SetButtonMode()
        {
            if (Icon != null && Text.Length > 0)
            {
                mode = ButtonMode.IconWithText;
            }
            else if (Icon != null && Text.Length == 0)
            {
                mode = ButtonMode.IconOnly;
            }
            else if (Icon == null && Text.Length > 0)
            {
                mode = ButtonMode.TextOnly;
            }

            switch (mode)
            {
                case ButtonMode.IconOnly:

                    // Configure Container
                    ContainerContent.HorizontalOptions = LayoutOptions.Fill;
                    Grid.SetColumnSpan(ButtonIcon, 2);
                    Grid.SetColumn(ButtonText, 1);

                    // Set Visibilities
                    ButtonText.IsVisible = false;

                    // Adjust Default Padding
                    if (!userChangedPadding)
                    {
                        Padding = new Thickness(HeightRequest * .3, HeightRequest * .3);
                        userChangedPadding = false; // Set this back to false, as the above line triggers OnPropertyChanged 
                    }

                    break;

                case ButtonMode.IconWithText:

                    // Configure Container
                    ContainerContent.HorizontalOptions = LayoutOptions.Center;
                    switch (IconOrientation)
                    {
                        case IconOrientation.Left:
                            FirstColumn.Width = new GridLength(1, GridUnitType.Star);
                            SecondColumn.Width = GridLength.Auto;
                            Grid.SetColumn(ButtonIcon, 0);
                            Grid.SetColumnSpan(ButtonIcon, 1);
                            Grid.SetColumn(ButtonText, 1);
                            break;

                        case IconOrientation.Right:
                            FirstColumn.Width = GridLength.Auto;
                            SecondColumn.Width = new GridLength(1, GridUnitType.Star);
                            Grid.SetColumn(ButtonIcon, 1);
                            Grid.SetColumnSpan(ButtonIcon, 1);
                            Grid.SetColumn(ButtonText, 0);
                            break;
                    }

                    // Set Visibilities
                    ButtonText.IsVisible = true;

                    // Adjust Default Padding
                    if (!userChangedPadding)
                    {
                        Padding = new Thickness(HeightRequest * .1, HeightRequest * .3);
                        userChangedPadding = false; // Set this back to false, as the above line triggers OnPropertyChanged 
                    }

                    break;

                case ButtonMode.TextOnly:

                    // Configure Container
                    ContainerContent.HorizontalOptions = LayoutOptions.FillAndExpand;
                    Grid.SetColumnSpan(ButtonIcon, 1);
                    Grid.SetColumnSpan(ButtonText, 2);
                    Grid.SetColumn(ButtonText, 0);

                    // Set Visibilities
                    ButtonText.IsVisible = true;

                    // Adjust Default Padding
                    if (!userChangedPadding)
                    {
                        Padding = new Thickness(0);
                        userChangedPadding = false; // Set this back to false, as the above line triggers OnPropertyChanged 
                    }

                    break;
            }

            // HACK: Horrible Hack, that makes the Xamarin.Forms Previewer work, who seems to give up some Binding support
            // since Xamarin.Forms 2.5.1
            try
            {

                Border.BackgroundColor = Color.Red;
                Border.BackgroundColor = BorderColor;
                Border.CornerRadius = CornerRadius;
                Border.Padding = BorderThickness;
                Container.BackgroundColor = BackgroundColor;
                Container.CornerRadius = InnerCornerRadius;
                ContainerContent.Margin = Padding;
                ButtonText.Text = Text;
                ButtonText.FontSize = FontSize;
                ButtonText.FontAttributes = FontAttributes;
                ButtonText.FontFamily = FontFamily;
                ButtonText.TextColor = ForegroundColor;

            }
            catch (NullReferenceException)
            {

            }

            if (ToggleMode > 0)
            {
                Highlight(IsToggled);
            }

            // Calculate inner corner radius
            // Use the outer radius minus the max thickness of a single direction
            InnerCornerRadius = Math.Max(0, CornerRadius - (int)Math.Max(Math.Max(BorderThickness.Left, BorderThickness.Top), Math.Max(BorderThickness.Right, BorderThickness.Bottom)));
            Container.CornerRadius = InnerCornerRadius;

            //ColorIcon(ForegroundColor);
        }

        public event EventHandler<EventArgs> TouchedDown;
        public event EventHandler<EventArgs> TouchedUp;
        public event EventHandler<EventArgs> Clicked;
        public event EventHandler<ToggledEventArgs> Toggled;

        public FlexButton()
        {
            InitializeComponent();
            
            TouchRecognizer.TouchDown += TouchDown;
            TouchRecognizer.TouchUp += TouchUp;
            SizeChanged += FlexButton_SizeChanged;
        }

        void FlexButton_SizeChanged(object sender, EventArgs e)
        {
            // HACK: Needs to be called to not make the Designer do stupid things
            SetButtonMode();
            ColorIcon(ForegroundColor);
        }

        void TouchDown()
        {
            if (IsEnabled)
            {
                TouchedDown?.Invoke(this, null);
                TouchedDownCommand?.Execute(TouchedDownCommandParameter);

                Highlight(true);
            }
        }

		void TouchUp()
		{
			if (IsEnabled)
			{
				TouchedUp?.Invoke(this, null);
				TouchedUpCommand?.Execute(TouchedUpCommandParameter);
				Clicked?.Invoke(this, null);
				ClickedCommand?.Execute(ClickedCommandParameter);

				if (ToggleMode == ToggleMode.None)
				{
					Highlight(false);
				}
				else
				{
					if (ToggleMode == ToggleMode.Check)
					{
						IsToggled = !IsToggled;
					}
					else
					{
						IsToggled = true;
					}

					Toggled?.Invoke(this, new ToggledEventArgs(IsToggled));

                    Highlight(IsToggled);
				}
			}
		}

        void ColorIcon(Color color)
        {
            // Attach Color Overlay Effect
            ButtonIcon.Effects.Clear();
            ButtonIcon.Effects.Add(new ColorOverlayEffect { Color = color });
        }

        void Highlight(bool isHighlighted)
        {
            if (isHighlighted)
            {
                Border.BackgroundColor = HighlightBorderColor;
                Container.BackgroundColor = HighlightBackgroundColor;
                ButtonText.TextColor = HighlightForegroundColor;
                ColorIcon(HighlightForegroundColor);
            }
            else
            {
                Border.BackgroundColor = BorderColor;
                Container.BackgroundColor = BackgroundColor;
                ButtonText.TextColor = ForegroundColor;
                ColorIcon(ForegroundColor);
            }
        }
    }
}
