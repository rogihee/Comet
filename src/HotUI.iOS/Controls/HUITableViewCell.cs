using System;
using UIKit;

namespace HotUI.iOS.Controls
{
    public class HUITableViewCell : UITableViewCell
    {
        public static int _instanceCount;

        private UIView _currentContent;
        private View _virtualView;

        public HUITableViewCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
        {
            ContentView.Tag = _instanceCount++;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (_currentContent == null)
                return;

            _virtualView.Frame = ContentView.Bounds.ToRectangleF();
        }

        public void SetView(View view, bool shouldDispose)
        {
            if (_virtualView != null && !_virtualView.IsDisposed)
                view = view.Diff(_virtualView);

            if (shouldDispose)
                _virtualView?.Dispose();

            _virtualView = view;
            var newView = view.ToView();

            if (_currentContent != null && _currentContent != newView)
            {
                if (_currentContent is UIButton button)
                    Logger.Debug($"[{ContentView.Tag}] Removing button: {button.Title(UIControlState.Normal)}");
                _currentContent.RemoveFromSuperview();
                if (newView is UIButton newButton)
                    Logger.Debug($"[{ContentView.Tag}] Replaced with button: {newButton.Title(UIControlState.Normal)}");
            }
            else
            {
                if (newView is UIButton newButton)
                    Logger.Debug($"[{ContentView.Tag}] Rendering button: {newButton.Title(UIControlState.Normal)}");
            }

            _currentContent = newView;
            if (_currentContent != null && _currentContent.Superview != ContentView)
                ContentView.Add(_currentContent);
        }
    }
}