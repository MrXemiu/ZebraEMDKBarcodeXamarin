using Android.Views;
using MvvmCross.Binding.Droid.Target;
using MvvmCross.Platform.Droid.WeakSubscription;

namespace BasicScanningTutorial
{
    public class FocusChangedTargetBinding : MvxAndroidTargetBinding<View, bool>
    {
        private MvxAndroidTargetEventSubscription<View, View.FocusChangeEventArgs> _subscription;


        /// <inheritdoc />
        public FocusChangedTargetBinding(View target) : base(target)
        {
        }


        /// <inheritdoc />
        public override void SubscribeToEvents()
        {
            if (Target == null) return;

            _subscription = Target.WeakSubscribe<View, View.FocusChangeEventArgs>(nameof(Target.FocusChange), HandleFocusChanged);
        }


        private void HandleFocusChanged(object sender, View.FocusChangeEventArgs e)
        {
            if (Target == null) return;

            FireValueChanged(e.HasFocus);
        }


        /// <inheritdoc />
        protected override void SetValueImpl(View target, bool value)
        {
            if (Target == null) return;

            if (value) target.RequestFocus();
            else target.ClearFocus();
        }
    }
}