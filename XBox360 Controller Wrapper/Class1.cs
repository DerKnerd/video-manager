using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace XNAWrapper {
    /// <summary>
    /// Die Buttons
    /// </summary>
    public enum Button {
        /// <summary>
        /// Der A-Button
        /// </summary>
        A,
        /// <summary>
        /// Der B-Button
        /// </summary>
        B,
        /// <summary>
        /// Der X-Button
        /// </summary>
        X,
        /// <summary>
        /// Der Y-Button
        /// </summary>
        Y,
        /// <summary>
        /// Der Rechte Bumper
        /// </summary>
        RB,
        /// <summary>
        /// Der Linke Bumper
        /// </summary>
        LB,
        /// <summary>
        /// Der Rechte Trigger
        /// </summary>
        RT,
        /// <summary>
        /// Der Linke Trigger
        /// </summary>
        LT,
        /// <summary>
        /// Der Start-Button
        /// </summary>
        Start,
        /// <summary>
        /// Der Back-Button
        /// </summary>
        Back,
        /// <summary>
        /// Der Linke Stick
        /// </summary>
        LS,
        /// <summary>
        /// Der Rechte Stick
        /// </summary>
        RS
    }
    /// <summary>
    /// Die Stickrichtung
    /// </summary>
    public enum Stick {
        /// <summary>
        /// Nach links
        /// </summary>
        left,
        /// <summary>
        /// Nach rechts
        /// </summary>
        right,
        /// <summary>
        /// Nach oben
        /// </summary>
        up,
        /// <summary>
        /// Nach unten
        /// </summary>
        down
    }
    /// <summary>
    /// Der Mapper für den XBox360-Controller
    /// </summary>
    public class XBox360Controller {
        /// <summary>
        /// Erstellt eine neue Instanz des XBox360Controllers
        /// </summary>
        public XBox360Controller() {
            timer = new Timer(1);
            timer.Elapsed += new ElapsedEventHandler(execute);
            timer.Start();
        }

        #region Events
        #region Buttonevent
        #region A-Button
        /// <summary>
        /// Wird gefeuert wenn der A-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action AButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der A-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action AButtonReleased;
        #endregion
        #region B-Button
        /// <summary>
        /// Wird gefeuert wenn der B-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action BButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der B-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action BButtonReleased;
        #endregion
        #region X-Button
        /// <summary>
        /// Wird gefeuert wenn der X-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action XButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der X-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action XButtonReleased;
        #endregion
        #region Y-Button
        /// <summary>
        /// Wird gefeuert wenn der Y-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action YButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der Y-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action YButtonReleased;
        #endregion
        #region Back-Button
        /// <summary>
        /// Wird gefeuert wenn der Back-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action BackButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der Back-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action BackButtonReleased;
        #endregion
        #region Start-Button
        /// <summary>
        /// Wird gefeuert wenn der Start-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action StartButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der Start-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action StartButtonReleased;
        #endregion
        #region Left-Button
        /// <summary>
        /// Wird gefeuert wenn der Left-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action LeftButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der Left-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action LeftButtonReleased;
        #endregion
        #region Right-Button
        /// <summary>
        /// Wird gefeuert wenn der Right-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action RightButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der Right-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action RightButtonReleased;
        #endregion
        #region Up-Button
        /// <summary>
        /// Wird gefeuert wenn der Up-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action UpButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der Up-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action UpButtonReleased;
        #endregion
        #region Down-Button
        /// <summary>
        /// Wird gefeuert wenn der Down-Button gedrückt wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action DownButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der Down-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action DownButtonReleased;
        #endregion
        #region LB-Button
        /// <summary>
        /// Wird gefeuert wenn der LB-Button gedrückt wurde
        /// </summary>         
        /// <see cref="Button"/>
        public event Action LbButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der LB-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action LbButtonReleased;
        #endregion
        #region RB-Button
        /// <summary>
        /// Wird gefeuert wenn der RB-Button gedrückt wurde
        /// </summary>         
        /// <see cref="Button"/>
        public event Action RbButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der RB-Button losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action RbButtonReleased;
        #endregion
        #region LS-Button
        /// <summary>
        /// Wird gefeuert wenn der LS gedrückt wurde
        /// </summary>         
        /// <see cref="Button"/>
        public event Action LsButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der LS losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action LsButtonReleased;
        #endregion
        #region RS-Button
        /// <summary>
        /// Wird gefeuert wenn der RS gedrückt wurde
        /// </summary>         
        /// <see cref="Button"/>
        public event Action RsButtonPressed;
        /// <summary>
        /// Wird gefeuert wenn der RS losgelassen wurde
        /// </summary>
        /// <see cref="Button"/>
        public event Action RsButtonReleased;
        #endregion
        #endregion
        #region Stickevents
        /// <summary>
        /// Wird gefeuert wenn der rechte Stick bewegt wurde, das erste Argument ist die Richtung und das dritte die Geschwindigkeit
        /// <list type="number">
        ///    <listheader>
        ///        <description>Argumente</description>
        ///    </listheader>
        ///    <item>
        ///        <term>Winkel vertikal</term>
        ///        <description>Der vertikale Winkel zwischen -1 und 1</description>
        ///    </item>
        ///    <item>
        ///        <term>Winkel horizontal</term>
        ///        <description>Der horizontale Winkel zwischen -1 und 1</description>
        ///    </item>
        ///</list>
        /// </summary>
        /// <see cref="Stick"/>
        public event Action<float, float> RightStickMoved;
        /// <summary>
        /// Wird gefeuert wenn der linke Stick bewegt wurde, das erste Argument ist die Richtung und das dritte die Geschwindigkeit
        /// <list type="number">
        ///    <listheader>
        ///        <description>Argumente</description>
        ///    </listheader>
        ///    <item>
        ///        <term>Winkel vertikal</term>
        ///        <description>Der vertikale Winkel zwischen -1 und 1</description>
        ///    </item>
        ///    <item>
        ///        <term>Winkel horizontal</term>
        ///        <description>Der horizontale Winkel zwischen -1 und 1</description>
        ///    </item>
        ///</list>
        /// </summary>
        /// <see cref="Stick"/>
        public event Action<float, float> LeftStickMoved;
        #endregion
        #endregion

        private Timer timer;

        private void execute(object sender, EventArgs e) {
            GamePadState gp = GamePad.GetState(PlayerIndex.One);
            if (gp.IsConnected == true) {
                Vector2 zero = new Vector2(0);
                GamePadButtons button = gp.Buttons;
                GamePadDPad dpad = gp.DPad;
                GamePadThumbSticks gpts = gp.ThumbSticks;
                GamePadTriggers gpt = gp.Triggers;
                #region Buttons
                #region A
                switch (button.A) {
                    case ButtonState.Pressed:
                        if (AButtonPressed != null)
                            AButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (AButtonReleased != null)
                            AButtonReleased();
                        break;
                }
                #endregion
                #region B
                switch (button.B) {
                    case ButtonState.Pressed:
                        if (BButtonPressed != null)
                            BButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (BButtonReleased != null)
                            BButtonReleased();
                        break;
                }
                #endregion
                #region X
                switch (button.X) {
                    case ButtonState.Pressed:
                        if (XButtonPressed != null)
                            XButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (XButtonReleased != null)
                            XButtonReleased();
                        break;
                }
                #endregion
                #region Y
                switch (button.Y) {
                    case ButtonState.Pressed:
                        if (YButtonPressed != null)
                            YButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (YButtonReleased != null)
                            YButtonReleased();
                        break;
                }
                #endregion
                #region Back
                switch (button.Back) {
                    case ButtonState.Pressed:
                        if (BackButtonPressed != null)
                            BackButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (BackButtonReleased != null)
                            BackButtonReleased();
                        break;
                }
                #endregion
                #region Start
                switch (button.Start) {
                    case ButtonState.Pressed:
                        if (StartButtonPressed != null)
                            StartButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (StartButtonReleased != null)
                            StartButtonReleased();
                        break;
                }
                #endregion
                #region LB
                switch (button.LeftShoulder) {
                    case ButtonState.Pressed:
                        if (LbButtonPressed != null)
                            LbButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (LbButtonReleased != null)
                            LbButtonReleased();
                        break;
                }
                #endregion
                #region RB
                switch (button.RightShoulder) {
                    case ButtonState.Pressed:
                        if (RbButtonPressed != null)
                            RbButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (RbButtonReleased != null)
                            RbButtonReleased();
                        break;
                }
                #endregion
                #endregion
                #region DPad
                #region Left
                switch (dpad.Left) {
                    case ButtonState.Pressed:
                        if (LeftButtonPressed != null)
                            LeftButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (LeftButtonReleased != null)
                            LeftButtonReleased();
                        break;
                }
                #endregion
                #region Right
                switch (dpad.Right) {
                    case ButtonState.Pressed:
                        if (RightButtonPressed != null)
                            RightButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (RightButtonReleased != null)
                            RightButtonReleased();
                        break;
                }
                #endregion
                #region Up
                switch (dpad.Up) {
                    case ButtonState.Pressed:
                        if (UpButtonPressed != null)
                            UpButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (UpButtonReleased != null)
                            UpButtonReleased();
                        break;
                }
                #endregion
                #region Down
                switch (dpad.Down) {
                    case ButtonState.Pressed:
                        if (DownButtonPressed != null)
                            DownButtonPressed();
                        break;
                    case ButtonState.Released:
                        if (DownButtonReleased != null)
                            DownButtonReleased();
                        break;
                }
                #endregion
                #endregion
                #region Sticks
                if (gpts.Left != zero) {
                    if (LeftStickMoved != null) {
                        LeftStickMoved(gpts.Left.Y, gpts.Left.X);
                    }
                }
                if (gpts.Right != zero) {
                    if (RightStickMoved != null) {
                        RightStickMoved(gpts.Right.Y, gpts.Right.X);
                    }
                }
                #endregion
            }
            timer.Start();
        }

        /// <summary>
        /// Lässt den Controller vibieren
        /// </summary>
        public void vibrate() {
            vibrate(1, 1, 10000);
        }
        /// <summary>
        /// Lässt den Controller vibieren
        /// </summary>
        /// <param name="time">Die Länge der Vibration in Millisekunden</param>
        public void vibrate(int time) {
            vibrate(1, 1, time);
        }
        /// <summary>
        /// Lässt den Controller vibieren
        /// </summary>
        /// <param name="low">Der niedrig frequenzvibrations Motor</param>
        /// <param name="high">Der hohe frequenzvibrations Motor</param>
        /// <param name="time">Die Länge der Vibration in Millisekunden</param>
        public void vibrate(float low, float high, int time) {
            GamePad.SetVibration(PlayerIndex.One, low, high);
            Timer stop = new Timer(time);
            stop.Elapsed += new ElapsedEventHandler(stop_Elapsed);
            stop.Start();
        }

        void stop_Elapsed(object sender, ElapsedEventArgs e) {
            GamePad.SetVibration(PlayerIndex.One, 0, 0);
        }
    }
}
