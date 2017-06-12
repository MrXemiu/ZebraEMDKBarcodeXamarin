using Android.Views;
using MvvmCross.Binding.Droid.Target;
using MvvmCross.Platform.Droid.WeakSubscription;

namespace BasicScanningTutorial
{
    public class FocusChangedTargetBinding : MvxAndroidTargetBinding<View, bool>
    {
        #region Private Fields

        private MvxAndroidTargetEventSubscription<View, View.FocusChangeEventArgs> _subscription;

        #endregion Private Fields

        #region Public Constructors

        /// <inheritdoc />
        public FocusChangedTargetBinding(View target) : base(target)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        /// <inheritdoc />
        public override void SubscribeToEvents()
        {
            if (Target == null) return;

            _subscription = Target.WeakSubscribe<View, View.FocusChangeEventArgs>(nameof(Target.FocusChange), HandleFocusChanged);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <inheritdoc />
        protected override void SetValueImpl(View target, bool value)
        {
            if (Target == null) return;

            if (value) target.RequestFocus();
            else target.ClearFocus();
        }

        #endregion Protected Methods

        #region Private Methods

        private void HandleFocusChanged(object sender, View.FocusChangeEventArgs e)
        {
            if (Target == null) return;

            FireValueChanged(e.HasFocus);
        }

        #endregion Private Methods
    }
}