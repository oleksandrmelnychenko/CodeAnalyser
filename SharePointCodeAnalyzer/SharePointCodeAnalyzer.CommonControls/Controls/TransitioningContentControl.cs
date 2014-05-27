using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SharePointCodeAnalyzer.CommonControls.Controls
{
    [TemplateVisualState(GroupName = "PresentationStates", Name = "Fadein"), TemplateVisualState(GroupName = "PresentationStates", Name = "Fadeout")]
    public class TransitioningContentControl : ContentControl
    {
        private bool contentIsloaded;
        public static readonly DependencyProperty DeferredContentProperty = DependencyProperty.Register("DeferredContent", typeof(object), typeof(TransitioningContentControl), new PropertyMetadata(null));
        private const string FadeinState = "Fadein";
        private Storyboard FadeInStoryBoard;
        private const string FadeoutState = "Fadeout";
        private Storyboard FadeOutStoryBoard;
        private bool isAnimating;
        private bool isFadingIn;
        private bool isFadingOut;
        private const string PresentationGroup = "PresentationStates";
        private bool stopAllAnimations;
        private List<VisualState> storyboards;
        private object tempSavedContent;

        public TransitioningContentControl()
        {
            base.DefaultStyleKey = typeof(TransitioningContentControl);
        }

        private void DebugMessage(string message)
        {
        }

        private Storyboard FindStoryboard(string newTransition)
        {
            Storyboard storyboard = this.GetStoryboard(newTransition);
            if ((storyboard == null) && (TryGetVisualStateGroup(this, "PresentationStates") == null))
            {
                return null;
            }
            return storyboard;
        }

        private Storyboard GetStoryboard(string newTransition)
        {
            if (this.storyboards == null)
            {
                VisualStateGroup group = TryGetVisualStateGroup(this, "PresentationStates");
                if (group != null)
                {
                    this.storyboards = group.States.OfType<VisualState>().ToList<VisualState>();
                }
            }
            return (from state in this.storyboards
                    where state.Name == newTransition
                    select state.Storyboard).FirstOrDefault<Storyboard>();
        }

        private void InternalOnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.FadeOutStoryBoard = this.FindStoryboard("Fadeout");
            this.FadeOutStoryBoard.Completed += new EventHandler(this.OnFadeoutCompleted);
            this.FadeInStoryBoard = this.FindStoryboard("Fadein");
            this.FadeInStoryBoard.Completed += new EventHandler(this.OnFadeinCompleted);
            this.SetContent(base.Content);
        }

        private void InternalOnFadeinCompleted(object sender)
        {
            if (!this.isFadingOut)
            {
                this.DebugMessage("InternalOnFadeinCompleted");
                this.isFadingIn = false;
            }
        }

        private void InternalOnFadeoutCompleted(object sender)
        {
            this.isFadingOut = false;
            this.DebugMessage("InternalOnFadeoutCompleted");
            this.TriggerFadeIn();
        }

        public override void OnApplyTemplate()
        {
            this.InternalOnApplyTemplate();
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            this.stopAllAnimations = false;
            if (this.isFadingIn || this.isFadingOut)
            {
                this.isFadingIn = false;
                this.isFadingOut = false;
                this.stopAllAnimations = true;
                this.DeferredContent = newContent;
                this.tempSavedContent = null;
                VisualStateManager.GoToState(this, "Fadein", false);
                base.IsHitTestVisible = false;
            }
            else
            {
                this.stopAllAnimations = false;
                this.StartTransition(oldContent, newContent);
            }
        }

        private void OnFadeinCompleted(object sender, EventArgs e)
        {
            base.IsHitTestVisible = true;
            if (!this.stopAllAnimations)
            {
                this.InternalOnFadeinCompleted(sender);
            }
        }

        private void OnFadeoutCompleted(object sender, EventArgs e)
        {
            if (!this.stopAllAnimations)
            {
                this.InternalOnFadeoutCompleted(sender);
            }
        }

        private void SetContent(object content)
        {
            this.DebugMessage("SetContent");
            if (!this.stopAllAnimations)
            {
                try
                {
                    this.contentIsloaded = false;
                    ContentPresenter presenter = new ContentPresenter
                    {
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    };
                    presenter.Loaded += new RoutedEventHandler(this.TempControlOnLoaded);
                    presenter.Content = content;
                    this.DeferredContent = presenter;
                }
                catch (Exception)
                {
                }
            }
        }

        private void StartFadeOut()
        {
            if (!this.stopAllAnimations && !this.isFadingOut)
            {
                this.DebugMessage("StartFadeOut");
                this.isFadingOut = true;
                base.IsHitTestVisible = false;
                VisualStateManager.GoToState(this, "Fadeout", false);
            }
        }

        private void StartTransition(object oldContent, object newContent)
        {
            if (!this.stopAllAnimations)
            {
                bool isFadingOut = this.isFadingOut;
                this.DebugMessage("StartTransition");
                if (this.isFadingIn)
                {
                    this.isFadingIn = false;
                }
                if (oldContent == null)
                {
                    this.SetContent(newContent);
                }
                else if (this.tempSavedContent == null)
                {
                    this.tempSavedContent = newContent;
                    this.StartFadeOut();
                }
            }
        }

        private void TempControlOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.DebugMessage("TempControlOnLoaded");
            this.contentIsloaded = true;
            VisualStateManager.GoToState(this, "Fadein", false);
        }

        private void TriggerFadeIn()
        {
            if ((!this.isFadingIn && this.contentIsloaded) && (this.tempSavedContent != null))
            {
                this.DebugMessage("TriggerFadeIn");
                object tempSavedContent = this.tempSavedContent;
                this.tempSavedContent = null;
                this.SetContent(tempSavedContent);
            }
        }

        private static VisualStateGroup TryGetVisualStateGroup(TransitioningContentControl source, string PresentationGroup)
        {
            Func<VisualStateGroup, bool> predicate = null;
            VisualStateGroup group = (from sg in VisualStateManager.GetVisualStateGroups(source).Cast<VisualStateGroup>()
                                      where sg.Name == PresentationGroup
                                      select sg).SingleOrDefault<VisualStateGroup>();
            if (group != null)
            {
                return group;
            }
            FrameworkElement child = VisualTreeHelper.GetChild(source, 0) as FrameworkElement;
            if (predicate == null)
            {
                predicate = sg => sg.Name == PresentationGroup;
            }
            return VisualStateManager.GetVisualStateGroups(child).Cast<VisualStateGroup>().Where<VisualStateGroup>(predicate).SingleOrDefault<VisualStateGroup>();
        }

        public object DeferredContent
        {
            get
            {
                return base.GetValue(DeferredContentProperty);
            }
            set
            {
                base.SetValue(DeferredContentProperty, value);
            }
        }
    }
}
