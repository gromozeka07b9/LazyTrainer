using System;
using System.Diagnostics;
using Lighter.Components.OnboardingCarousel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace LazyTrainer
{
    public partial class App : Application
    {
        private readonly MainPage _mainPageInstance;

        public App()
        {
            InitializeComponent();

            this._mainPageInstance = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Lighter.ResourceLoader resourceLoader = new Lighter.ResourceLoader(GetType().Module.Assembly);
            var fileOnboardingComponentContent = resourceLoader.GetResourceTextFile("onboarding.json");
            OnboardingCarouselDeserializer onboardingCarouselDeserializer = new OnboardingCarouselDeserializer();
            var deserializedOnboardingComponent = onboardingCarouselDeserializer.Deserialize(fileOnboardingComponentContent);

            Debug.WriteLine($"DebugApp: {DateTime.Now.ToString()} start Onboarding...");
            MainPage = new OnboardingPage(deserializedOnboardingComponent);
            SubscribeMessages();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SubscribeMessages()
        {

            MessagingCenter.Subscribe<Lighter.Events.StartApplicationEvent>(this, string.Empty, (sender) =>
            {
                MainPage = this._mainPageInstance;
            });
        }
    }
}