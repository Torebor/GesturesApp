using System;
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
using Microsoft.Gestures;
using Microsoft.Gestures.Endpoint;
using Microsoft.Gestures.Skeleton;
using Microsoft.Gestures.Stock.HandPoses;
using Microsoft.Gestures.Stock.Gestures;
using System.Speech.Synthesis;


namespace GestureApp
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
       
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IniciarPrograma();
            

            
        }

        private async void IniciarPrograma()
        {

            var fist = new HandPose("Fist", new FingerPose(new AllFingersContext(), FingerFlexion.Folded));
            var peace = new HandPose("Peace", new FingerPose(new[] { Finger.Index, Finger.Middle }, FingerFlexion.Open),
                new FingerPose(new[] { Finger.Thumb, Finger.Ring, Finger.Pinky }, FingerFlexion.Folded));

            var makepeace = new Gesture("makepeace", fist, peace);

            var gestureservice = GesturesServiceEndpointFactory.Create();
            await gestureservice.ConnectAsync();
            await gestureservice.RegisterGesture(makepeace);

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 30;
            synthesizer.Rate = -2;

            fist.Triggered += (sender, args) =>

            {

                Dispatcher.Invoke(() => GreetingText.Text = "Empuñaste la mano");
                Dispatcher.Invoke(() => synthesizer.SpeakAsync(GreetingText.Text));

            };

            makepeace.Triggered += (sender, args) =>
             {
                
                 
                 Dispatcher.Invoke(() => GreetingText.Text = "Gesto de Paz ✌");

                 Dispatcher.Invoke(() =>synthesizer.SpeakAsync(GreetingText.Text));
             };

            peace.Triggered += (sender, args) =>

            {

                Dispatcher.Invoke(() => GreetingText.Text = "..");

            };


        }

        

       
    }

}
